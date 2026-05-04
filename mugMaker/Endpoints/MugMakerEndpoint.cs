using mugMaker.Contracts;
using mugMaker.Services;

namespace mugMaker.Endpoints
{
    public static class MugMakerEndpoint
    {

        //public record MugRequest(string Name);

        public static IEndpointRouteBuilder MapMugMakerEndpoint(this IEndpointRouteBuilder app) 
        {
            var group = app.MapGroup("/api/mugmaker").WithTags("Mug Maker");

            group.MapGet("/mugs", () => Results.Ok(new
            {
                mugs = new[] { "Classic Mug", "Travel Mug", "Magic Mug" },
                TimestampAttribute = DateTime.UtcNow
            }))
            .WithOpenApi();

            group.MapPost("/mugs", (CreateMugRequest request, IMugMakerService service) => 
            {
                if (string.IsNullOrWhiteSpace(request.Saying))
                {
                    return Results.BadRequest(new { error = "Mug name is required." });
                }
                
                var newMug = service.Create(request);
                return Results.Created($"/mugmaker/mugs/{newMug.Id}", newMug);
            })
            .WithOpenApi();

            return app;
        }
    }
}
