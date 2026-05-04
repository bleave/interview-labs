
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


// ---------------------------------------------------------
// In-memory store (simple, interview-friendly)
// ---------------------------------------------------------

var todos = new Dictionary<Guid, Todo>();

// ---------------------------------------------------------
// GET /todos
// ---------------------------------------------------------

app.MapGet("/todos", () =>
{
    return Results.Ok(todos.Values);
});

// ---------------------------------------------------------
// GET /todos/{id}
// ---------------------------------------------------------

app.MapGet("/todos/{id:guid}", (Guid id) =>
{
    return todos.TryGetValue(id, out var todo)
        ? Results.Ok(todo)
        : Results.NotFound();
});

// ---------------------------------------------------------
// POST /todos
// ---------------------------------------------------------

app.MapPost("/todos", (CreateTodo request) =>
{
    if (string.IsNullOrWhiteSpace(request.Title))
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            ["title"] = ["Title is required."]
        });
    }

    var now = DateTimeOffset.UtcNow;

    var todo = new Todo(
        Id: Guid.NewGuid(),
        Title: request.Title.Trim(),
        IsDone: false,
        CreatedAtUtc: now,
        UpdatedAtUtc: now
    );

    todos[todo.Id] = todo;

    return Results.Created($"/todos/{todo.Id}", todo);
});

// ---------------------------------------------------------
// PUT /todos/{id}
// ---------------------------------------------------------

app.MapPut("/todos/{id:guid}", (Guid id, UpdateTodo request) =>
{
    if (!todos.TryGetValue(id, out var existing))
        return Results.NotFound();

    if (request.Title is not null && request.Title.Trim().Length == 0)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            ["title"] = ["Title cannot be empty."]
        });
    }

    var updated = existing with
    {
        Title = request.Title is null ? existing.Title : request.Title.Trim(),
        IsDone = request.IsDone ?? existing.IsDone,
        UpdatedAtUtc = DateTimeOffset.UtcNow
    };

    todos[id] = updated;

    return Results.Ok(updated);
});

// ---------------------------------------------------------
// DELETE /todos/{id}
// ---------------------------------------------------------

app.MapDelete("/todos/{id:guid}", (Guid id) =>
{
    return todos.Remove(id)
        ? Results.NoContent()
        : Results.NotFound();
});

app.Run();

// ---------------------------------------------------------
// Models (keep at bottom for interview speed)
// ---------------------------------------------------------

record Todo(
    Guid Id,
    string Title,
    bool IsDone,
    DateTimeOffset CreatedAtUtc,
    DateTimeOffset UpdatedAtUtc
);

record CreateTodo(string? Title);
record UpdateTodo(string? Title, bool? IsDone);
