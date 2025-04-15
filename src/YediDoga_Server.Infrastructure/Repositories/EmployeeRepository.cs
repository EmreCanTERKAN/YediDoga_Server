using GenericRepository;
using YediDoga_Server.Domain.Employees;
using YediDoga_Server.Infrastructure.Context;

namespace YediDoga_Server.Infrastructure.Repositories;
internal sealed class EmployeeRepository : Repository<Employee, ApplicationDbContext>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
