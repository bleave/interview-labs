namespace mugMaker.Contracts
{
    public sealed record MugDTO
    (
        Guid Id,
        string Saying,
        DateTime CreatedUtc
    );
}
