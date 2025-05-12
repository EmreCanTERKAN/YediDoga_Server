using MediatR;
using YediDoga_Server.Domain.Honeys;

namespace YediDoga_Server.Application.Honeys;
public sealed record HoneyGetAllQuery() : IRequest<IQueryable<HoneyGetAllQueryResponse>>;

public sealed class HoneyGetAllQueryResponse : EntityDto
{
    public string Name { get; set; } = default!;
    public string Category { get; set; } = default!;
    public string Price { get; set; } = default!;
    public string Type { get; set; } = default!;
}

internal sealed class HoneyGetAllQueryHandler(
    IHoneyRepository honeyRepository) : IRequestHandler<HoneyGetAllQuery, IQueryable<HoneyGetAllQueryResponse>>
{
    public Task<IQueryable<HoneyGetAllQueryResponse>> Handle(HoneyGetAllQuery request, CancellationToken cancellationToken)
    {
        var response = honeyRepository
            .GetAll()
            .Select(s => new HoneyGetAllQueryResponse
            {
                Name = s.Name,
                Category = s.Category,
                Price = s.Price,
                Type = s.Type
            })
            .AsQueryable();

        return Task.FromResult(response);
    }
}
