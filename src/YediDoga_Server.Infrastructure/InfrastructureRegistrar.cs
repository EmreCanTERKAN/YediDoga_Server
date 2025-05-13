using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using YediDoga_Server.Domain.Honeys;
using YediDoga_Server.Infrastructure.Context;
using YediDoga_Server.Infrastructure.Repositories;

namespace YediDoga_Server.Infrastructure;
public static class InfrastructureRegistrar
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            string connectionString = configuration.GetConnectionString("SqlServer")!;
            opt.UseSqlServer(connectionString);
        });

        services.AddDbContext<PostgresDbContext>(opt =>
        {
            string connectionString = configuration.GetConnectionString("PostgreSql")!;
            opt.UseNpgsql(connectionString);
        });
        

       // services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<PostgresDbContext>());

        services.Scan(opt => opt
            .FromAssemblies(typeof(InfrastructureRegistrar).Assembly)
            .AddClasses(publicOnly:false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );


        return services;
    }
}
