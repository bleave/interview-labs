using mugMaker.Contracts;

namespace mugMaker.Services
{
    public sealed class MugMakerService : IMugMakerService
    {        
        public MugDTO Create(CreateMugRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Saying))
            {
                throw new ArgumentException("Saying is required to create a mug.");
            }
            var newMug = new MugDTO(
                Id: Guid.NewGuid(),
                Saying: request.Saying,
                CreatedUtc: DateTime.UtcNow
            );
            // Here you would typically save the new mug to a database or in-memory store
            return newMug;
        }
    }
}
