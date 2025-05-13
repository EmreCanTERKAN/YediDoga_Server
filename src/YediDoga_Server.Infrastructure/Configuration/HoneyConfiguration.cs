using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YediDoga_Server.Domain.Honeys;

namespace YediDoga_Server.Infrastructure.Configuration;

internal sealed class HoneyConfiguration : IEntityTypeConfiguration<Honey>
{
    public void Configure(EntityTypeBuilder<Honey> builder)
    {
        builder.Property(i => i.Price).HasColumnType("numeric(12,2)");
    }
}

