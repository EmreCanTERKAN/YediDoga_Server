using MediatR;
using YediDoga_Server.Domain.Employees;

namespace YediDoga_Server.Application.Employees;
public sealed record EmployeeGetAllQuery() : IRequest<IQueryable<EmployeeGetAllQueryResponse>>;

public sealed class EmployeeGetAllQueryResponse : EntityDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateOnly BirthOfDate { get; set; }
    public decimal Salary { get; set; }
    public string TcNo { get; set; } = default!;
}

internal sealed class EmployeeGetAllQueryHandler(
    IEmployeeRepository employeeRepository) : IRequestHandler<EmployeeGetAllQuery, IQueryable<EmployeeGetAllQueryResponse>>
{
    public Task<IQueryable<EmployeeGetAllQueryResponse>> Handle(EmployeeGetAllQuery request, CancellationToken cancellationToken)
    {
        var response = employeeRepository.GetAll()
            .Select(s => new EmployeeGetAllQueryResponse
        {
            FirstName = s.FirstName,
            LastName = s.LastName,
            BirthOfDate = s.BirthOfDate,
            Salary = s.Salary,
            CreateAt = s.CreateAt,
            DeleteAt = s.DeleteAt,
            IsDeleted = s.IsDeleted,
            Id = s.Id,
            TcNo = s.PersonelInformation.TcNo,
            UpdateAt = s.UpdateAt
        })
            .AsQueryable();

        return Task.FromResult(response);     
    }
}
