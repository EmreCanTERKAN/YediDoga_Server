using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.RateLimiting;
using Scalar.AspNetCore;
using YediDoga_Server.Application;
using YediDoga_Server.Infrastructure;
using YediDoga_Server.WebAPI;
using YediDoga_Server.WebAPI.Controllers;
using YediDoga_Server.WebAPI.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCors();
builder.Services.AddOpenApi();
builder.Services.AddControllers()
    .AddOData(opt =>
            opt.Select()
                .Filter()
                .Count()
                .Expand()
                .OrderBy()
                .SetMaxTop(null)
                .AddRouteComponents("odata", AppODataController.GetEdmModel())
        );

builder.Services.AddRateLimiter(x =>
x.AddFixedWindowLimiter("fixed", cfg =>
{
    cfg.QueueLimit = 100;
    cfg.Window = TimeSpan.FromSeconds(1);
    cfg.PermitLimit = 100;
    cfg.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
}));

builder.Services
    .AddExceptionHandler<ExceptionHandler>()
    .AddProblemDetails();
var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseCors(x => x
.AllowAnyHeader()
.AllowCredentials()
.AllowAnyMethod()
.SetIsOriginAllowed(x => true)
.SetPreflightMaxAge(TimeSpan.FromMinutes(10)));

app.UseExceptionHandler();
app.RegisterRoutes();

app.MapControllers().RequireRateLimiting("fixed");
app.Run();
