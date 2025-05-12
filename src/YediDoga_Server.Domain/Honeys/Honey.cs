using YediDoga_Server.Domain.Abstractions;

namespace YediDoga_Server.Domain.Honeys;
public class Honey : Entity
{
    public string Name { get; set; } = default!;
    public string Category { get; set; } = default!;
    public string Price { get; set; } = default!;
    public string Type { get; set; } = default!;
}
