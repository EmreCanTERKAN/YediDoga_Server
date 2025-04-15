using FluentValidation;
using GenericRepository;
using Mapster;
using MediatR;
using TS.Result;
using YediDoga_Server.Domain.Employees;

namespace YediDoga_Server.Application.Employees;
public sealed record EmployeeCreateCommand(
    string FirstName,
    string LastName,
    DateOnly BirthOfDate,
    decimal Salary,
    PersonelInformation PersonelInformation,
    Address? Address) : IRequest<Result<string>>;

public sealed class EmployeeCreateCommandValidator : AbstractValidator<EmployeeCreateCommand>
{
    public EmployeeCreateCommandValidator()
    {
        RuleFor(x => x.FirstName).MinimumLength(3).WithMessage("Ad alanı en az 3 karakter olmalıdır");
        RuleFor(x => x.LastName).MinimumLength(3).WithMessage("Soyadı alanı en az 3 karakter olmalıdır");
        RuleFor(x => x.PersonelInformation.TcNo)
            .MinimumLength(11).WithMessage("Geçerli bir tc numarası olmalı")
            .MaximumLength(11).WithMessage("Geçerli bir tc numarası olmalı");

    }
}

internal sealed class EmployeeCreateCommandHandler(
    IEmployeeRepository employeeRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<EmployeeCreateCommand, Result<string>>
{
    public async Task<Result<string>> Handle(EmployeeCreateCommand request, CancellationToken cancellationToken)
    {
        var isEmployeeExists = await employeeRepository.AnyAsync(p => p.PersonelInformation.TcNo == request.PersonelInformation.TcNo, cancellationToken);

        if (isEmployeeExists)
        {
            return Result<string>.Failure("Bu tc no daha önce kaydedildi");
        }

        Employee employee = request.Adapt<Employee>();

        employeeRepository.Add(employee);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Personel kaydı başarıyla tamamlandı";

    }
}
