using YediDoga_Server.Domain.Abstractions;

namespace YediDoga_Server.Domain.Honeys;
public sealed class Honey : Entity
{
    public string Name { get; set; } = default!;
    public string Category { get; set; } = default!;
    public decimal Price { get; set; }
    public string Type { get; set; } = default!;
}
