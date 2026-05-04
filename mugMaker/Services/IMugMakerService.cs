using mugMaker.Contracts;

namespace mugMaker.Services
{
    public interface IMugMakerService
    {
        MugDTO Create(CreateMugRequest request);
    }
}
