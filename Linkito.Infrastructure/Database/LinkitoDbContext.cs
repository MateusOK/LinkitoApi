using Linkito.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Linkito.Infrastructure.Database;

public class LinkitoDbContext : DbContext
{
    public LinkitoDbContext(DbContextOptions<LinkitoDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<ShortUrl> ShortUrls => Set<ShortUrl>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ShortUrl>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OriginalUrl).IsRequired();
            entity.Property(e => e.ShortCode).IsRequired();
            entity.Property(e => e.CreatedAt);
            entity.Property(e => e.ExpirationDate);
        });
    }
}