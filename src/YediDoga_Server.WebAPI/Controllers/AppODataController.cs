using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using YediDoga_Server.Application.Employees;
using YediDoga_Server.Application.Honeys;

namespace YediDoga_Server.WebAPI.Controllers;

[Microsoft.AspNetCore.Mvc.Route("odata/")]
[EnableQuery]
[ApiController]
public class AppODataController(
    ISender sender) : ODataController
{
    public static IEdmModel GetEdmModel()
    {
        ODataConventionModelBuilder builder = new();
        builder.EnableLowerCamelCase();
        builder.EntitySet<EmployeeGetAllQueryResponse>("employees");
        builder.EntitySet<HoneyGetAllQueryResponse>("honeys");
        return builder.GetEdmModel();
    }

    [HttpGet("employees")]
    public async Task<IQueryable<EmployeeGetAllQueryResponse>> GetAllEmployees(CancellationToken cancellationToken)
    {
        var response = await sender.Send(new EmployeeGetAllQuery(), cancellationToken);
        return response;
    }

    [HttpGet("honeys")]
    public async Task<IQueryable<HoneyGetAllQueryResponse>> GetAllHoneys(CancellationToken cancellationToken)
    {
        var response = await sender.Send(new HoneyGetAllQuery(), cancellationToken);
        return response;
    }
}
