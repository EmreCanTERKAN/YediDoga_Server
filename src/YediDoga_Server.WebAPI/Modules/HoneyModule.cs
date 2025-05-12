using MediatR;
using TS.Result;
using YediDoga_Server.Application.Honeys;

namespace YediDoga_Server.WebAPI.Modules;

public static class HoneyModule
{
    public static void RegisterHoneyRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/honeys").WithTags("Honeys");

        group.MapPost(string.Empty,
            async (ISender sender, HoneyCreateCommand request, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();
    }
}
