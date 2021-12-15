namespace Lore.Api
{
    public record Todo
    {
        public int Id { get; set; }
        public string? Name { get; init; }
    }
}
