using GenericRepository;
using Microsoft.EntityFrameworkCore;
using YediDoga_Server.Domain.Abstractions;
using YediDoga_Server.Domain.Honeys;

namespace YediDoga_Server.Infrastructure.Context;
public class PostgresDbContext : DbContext , IUnitOfWork
{
    public PostgresDbContext(DbContextOptions options) : base(options)
    {
    }

    protected PostgresDbContext()
    {
    }

    public DbSet<Honey> Honeys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgresDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<Entity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(p => p.CreateAt).CurrentValue = DateTimeOffset.UtcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                if (entry.Property(p => p.IsDeleted).CurrentValue == true)
                {
                    entry.Property(p => p.DeleteAt).CurrentValue = DateTimeOffset.UtcNow;
                }
                else
                {
                    entry.Property(p => p.UpdateAt).CurrentValue = DateTimeOffset.UtcNow;
                }
            }
            if (entry.State == EntityState.Deleted)
            {
                throw new ArgumentException("Dbden direkt silme işlemi yapılamaz");
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
