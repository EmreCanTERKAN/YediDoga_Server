﻿using GenericRepository;
using Microsoft.EntityFrameworkCore;
using YediDoga_Server.Domain.Abstractions;
using YediDoga_Server.Domain.Employees;
using YediDoga_Server.Domain.Honeys;

namespace YediDoga_Server.Infrastructure.Context;
internal class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Honey> Honeys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<Entity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(p => p.CreateAt).CurrentValue = DateTimeOffset.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                if (entry.Property(p => p.IsDeleted).CurrentValue == true)
                {
                    entry.Property(p => p.DeleteAt).CurrentValue = DateTimeOffset.Now;
                }
                else
                {
                    entry.Property(p => p.UpdateAt).CurrentValue = DateTimeOffset.Now;
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
