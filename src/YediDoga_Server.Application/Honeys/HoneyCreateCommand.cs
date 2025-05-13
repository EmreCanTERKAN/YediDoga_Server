using FluentValidation;
using GenericRepository;
using Mapster;
using MediatR;
using TS.Result;
using YediDoga_Server.Domain.Honeys;

namespace YediDoga_Server.Application.Honeys;
public sealed record HoneyCreateCommand(
    string Name,
    string Category,
    decimal Price,
    string Type) : IRequest<Result<string>>;

public sealed class HoneyCreateCommandValidator : AbstractValidator<HoneyCreateCommand>
{
    public HoneyCreateCommandValidator()
    {
        RuleFor(x => x.Name).MinimumLength(3).WithMessage("Bal adı en az 3 karakter olmalıdır");
    }
}

internal sealed class HoneyCreateCommandHandler(
    IHoneyRepository honeyRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<HoneyCreateCommand, Result<string>>
{
    public async Task<Result<string>> Handle(HoneyCreateCommand request, CancellationToken cancellationToken)
    {
        var isHoneyExist = await honeyRepository.AnyAsync(p => p.Name == request.Name, cancellationToken);

        if (isHoneyExist)
        {
            return Result<string>.Failure("Bu bal daha önce kayıt edildi.");
        }

        Honey honey = request.Adapt<Honey>();

        honeyRepository.Add(honey);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Bal kaydı başarıyla tamamlandı";
    }
}
