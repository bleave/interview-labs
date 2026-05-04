using mugMaker.Endpoints;
using mugMaker.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

builder.Services.AddProblemDetails();

builder.Services.AddSingleton<IMugMakerService, MugMakerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.MapMugMakerEndpoint();

app.UseExceptionHandler(exceptionApp =>
{
    exceptionApp.Run(async context =>
    {
        var exception = context.Features
            .Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?
            .Error;

        var problem = exception switch
        {
            ArgumentException => Results.Problem(
                title: exception.Message,
                statusCode: StatusCodes.Status400BadRequest),

            KeyNotFoundException => Results.Problem(
                title: exception.Message,
                statusCode: StatusCodes.Status404NotFound),

            _ => Results.Problem(
                title: "An unexpected error occurred.",
                statusCode: StatusCodes.Status500InternalServerError)
        };

        await problem.ExecuteAsync(context);
    });
});

//health check
app.MapHealthChecks("/health");

//app.MapGet("/health", () => Results.Ok(new 
//{ 
//    status = "Healthy",
//    TimestampAttribute = DateTime.UtcNow
//}))
//.WithName("Health") //route name
//.WithOpenApi(); //swagger

app.Run();
