using GenericRepository;
using YediDoga_Server.Domain.Honeys;
using YediDoga_Server.Infrastructure.Context;

namespace YediDoga_Server.Infrastructure.Repositories;

internal sealed class HoneyRepository : Repository<Honey, ApplicationDbContext>, IHoneyRepository
{
    public HoneyRepository(ApplicationDbContext context) : base(context)
    {
    }
}
