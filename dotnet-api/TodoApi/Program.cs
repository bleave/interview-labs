using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------
// JWT Auth (demo-friendly constants)
// ---------------------------------------------------------
const string JwtIssuer = "todos-api";
const string JwtAudience = "todos-api";
const string JwtSecret = "SUPER_LONG_DEV_SECRET_CHANGE_ME_32+_CHARS"; // >= 32 chars

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = JwtIssuer,
            ValidAudience = JwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret)),
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });

builder.Services.AddAuthorization();

// ---------------------------------------------------------
// Swagger
// ---------------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "Todos API", Version = "v1" });

    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header. Example: \"Bearer {token}\""
    });

    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// ---------------------------------------------------------
// App services
// ---------------------------------------------------------
builder.Services.AddProblemDetails();

builder.Services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();
builder.Services.AddSingleton<ITodoService, TodoService>();

builder.Services.AddHttpClient<IUsersClient, UsersClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5092"); // users-api
    client.Timeout = TimeSpan.FromSeconds(2);
});

var app = builder.Build();

// ---------------------------------------------------------
// Middleware pipeline
// ---------------------------------------------------------
app.UseExceptionHandler(exceptionApp =>
{
    exceptionApp.Run(async context =>
    {
        var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        // In an interview: log ex, map known exceptions to 4xx, others to 500
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await Results.Problem(
            title: "Unexpected error",
            detail: ex?.Message,
            statusCode: 500).ExecuteAsync(context);
    });
});

app.UseStatusCodePages();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

// ---------------------------------------------------------
// Auth endpoint (JsonElement to avoid body-binding surprises)
// ---------------------------------------------------------
app.MapPost("/auth/login", (JsonElement body) =>
{
    static string? ReadString(JsonElement b, string camel, string pascal)
        => b.TryGetProperty(camel, out var c) ? c.GetString()
         : b.TryGetProperty(pascal, out var p) ? p.GetString()
         : null;

    var username = ReadString(body, "username", "Username");
    var password = ReadString(body, "password", "Password");

    if (username != "eric" || password != "password")
        return Results.Unauthorized();

    var claims = new List<Claim>
    {
        new(JwtRegisteredClaimNames.Sub, username!),
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new(ClaimTypes.Name, username!),
        new(ClaimTypes.Role, "Trader")
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: JwtIssuer,
        audience: JwtAudience,
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(60),
        signingCredentials: creds);

    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

    return Results.Ok(new { access_token = jwt, token_type = "Bearer" });
})
.AllowAnonymous()
.WithTags("Auth");

// ---------------------------------------------------------
// Todos routes
// ---------------------------------------------------------
var todoRoutes = app.MapGroup("/todos")
    .WithTags("Todos")
    .RequireAuthorization();

// GET /todos
todoRoutes.MapGet("/", async (ITodoService svc) =>
{
    var items = await svc.GetAllAsync();
    return Results.Ok(items);
});

// GET /todos/{id}
todoRoutes.MapGet("/{id:guid}", async (Guid id, ITodoService svc) =>
{
    var todo = await svc.GetByIdAsync(id);
    return todo is null ? Results.NotFound() : Results.Ok(todo);
});

// POST /todos
todoRoutes.MapPost("/", async (CreateTodoRequest request, ITodoService svc, CancellationToken ct) =>
{
    var validation = Validators.ValidateCreate(request);
    if (validation is not null) return validation;

    var result = await svc.CreateAsync(request, ct);

    return result.Match<IResult>(
        ok => Results.Created($"/todos/{ok.Id}", ok),
        badRequest => Results.BadRequest(new { message = badRequest })
    );
});

// PUT /todos/{id}
todoRoutes.MapPut("/{id:guid}", async (Guid id, UpdateTodoRequest request, ITodoService svc) =>
{
    var validation = Validators.ValidateUpdate(request);
    if (validation is not null) return validation;

    var updated = await svc.UpdateAsync(id, request);
    return updated is null ? Results.NotFound() : Results.Ok(updated);
});

// DELETE /todos/{id}
todoRoutes.MapDelete("/{id:guid}", async (Guid id, ITodoService svc) =>
{
    var removed = await svc.DeleteAsync(id);
    return removed ? Results.NoContent() : Results.NotFound();
});

app.Run();

// ---------------------------------------------------------
// Contracts (API DTOs)
// ---------------------------------------------------------
record CreateTodoRequest(string? Title, Guid? UserId);
record UpdateTodoRequest(string? Title, bool? IsDone);

// API output model
record TodoResponse(Guid Id, string Title, bool IsDone, DateTimeOffset CreatedAtUtc, DateTimeOffset UpdatedAtUtc);

// ---------------------------------------------------------
// Domain
// ---------------------------------------------------------
record Todo(Guid Id, string Title, bool IsDone, DateTimeOffset CreatedAtUtc, DateTimeOffset UpdatedAtUtc);

// ---------------------------------------------------------
// Validation
// ---------------------------------------------------------
static class Validators
{
    public static IResult? ValidateCreate(CreateTodoRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Title))
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["title"] = new[] { "Title is required." }
            });
        }

        if (req.UserId is null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["userId"] = new[] { "UserId is required." }
            });
        }

        return null;
    }

    public static IResult? ValidateUpdate(UpdateTodoRequest req)
    {
        if (req.Title is not null && req.Title.Trim().Length == 0)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["title"] = new[] { "Title cannot be empty." }
            });
        }

        return null;
    }
}

// ---------------------------------------------------------
// Small Result union helper (so service can return OK or error string)
// ---------------------------------------------------------
readonly record struct Result<T>(T? Value, string? Error)
{
    public static Result<T> Ok(T value) => new(value, null);
    public static Result<T> Fail(string error) => new(default, error);

    public TResult Match<TResult>(Func<T, TResult> ok, Func<string, TResult> fail)
        => Error is null ? ok(Value!) : fail(Error);
}

// ---------------------------------------------------------
// Service
// ---------------------------------------------------------
interface ITodoService
{
    Task<IReadOnlyList<TodoResponse>> GetAllAsync();
    Task<TodoResponse?> GetByIdAsync(Guid id);
    Task<Result<TodoResponse>> CreateAsync(CreateTodoRequest req, CancellationToken ct);
    Task<TodoResponse?> UpdateAsync(Guid id, UpdateTodoRequest req);
    Task<bool> DeleteAsync(Guid id);
}

sealed class TodoService(ITodoRepository repo, IUsersClient users) : ITodoService
{
    public async Task<IReadOnlyList<TodoResponse>> GetAllAsync()
        => (await repo.GetAllAsync()).Select(Map).ToList();

    public async Task<TodoResponse?> GetByIdAsync(Guid id)
        => (await repo.GetByIdAsync(id)) is { } t ? Map(t) : null;

    public async Task<Result<TodoResponse>> CreateAsync(CreateTodoRequest req, CancellationToken ct)
    {
        // Validate user exists (service-to-service call)
        var user = await users.GetUserAsync(req.UserId!.Value, ct);

        if (user is null)
            return Result<TodoResponse>.Fail("Invalid userId.");

        var now = DateTimeOffset.UtcNow;

        var todo = new Todo(
            Id: Guid.NewGuid(),
            Title: req.Title!.Trim(),
            IsDone: false,
            CreatedAtUtc: now,
            UpdatedAtUtc: now
        );

        await repo.UpsertAsync(todo);
        return Result<TodoResponse>.Ok(Map(todo));
    }

    public async Task<TodoResponse?> UpdateAsync(Guid id, UpdateTodoRequest req)
    {
        var existing = await repo.GetByIdAsync(id);
        if (existing is null) return null;

        var updated = existing with
        {
            Title = req.Title is null ? existing.Title : req.Title.Trim(),
            IsDone = req.IsDone ?? existing.IsDone,
            UpdatedAtUtc = DateTimeOffset.UtcNow
        };

        await repo.UpsertAsync(updated);
        return Map(updated);
    }

    public Task<bool> DeleteAsync(Guid id) => repo.DeleteAsync(id);

    private static TodoResponse Map(Todo t)
        => new(t.Id, t.Title, t.IsDone, t.CreatedAtUtc, t.UpdatedAtUtc);
}

// ---------------------------------------------------------
// Repository
// ---------------------------------------------------------
interface ITodoRepository
{
    Task<IReadOnlyList<Todo>> GetAllAsync();
    Task<Todo?> GetByIdAsync(Guid id);
    Task UpsertAsync(Todo todo);
    Task<bool> DeleteAsync(Guid id);
}

sealed class InMemoryTodoRepository : ITodoRepository
{
    private readonly Dictionary<Guid, Todo> _store = new();

    public Task<IReadOnlyList<Todo>> GetAllAsync()
        => Task.FromResult((IReadOnlyList<Todo>)_store.Values.ToList());

    public Task<Todo?> GetByIdAsync(Guid id)
        => Task.FromResult(_store.TryGetValue(id, out var t) ? t : null);

    public Task UpsertAsync(Todo todo)
    {
        _store[todo.Id] = todo;
        return Task.CompletedTask;
    }

    public Task<bool> DeleteAsync(Guid id)
        => Task.FromResult(_store.Remove(id));
}

// ---------------------------------------------------------
// Users API client (service-to-service call)
// ---------------------------------------------------------
interface IUsersClient
{
    Task<UserDto?> GetUserAsync(Guid userId, CancellationToken ct);
}

sealed class UsersClient(HttpClient http) : IUsersClient
{
    public async Task<UserDto?> GetUserAsync(Guid userId, CancellationToken ct)
    {
        var resp = await http.GetAsync($"/users/{userId}", ct);

        if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;

        if (!resp.IsSuccessStatusCode)
            return null; // interview-simple: treat downstream failure as "unknown user"

        return await resp.Content.ReadFromJsonAsync<UserDto>(cancellationToken: ct);
    }
}

record UserDto(Guid Id, string Name);