using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using jem_id_backend_assessment.Models;

namespace jem_id_backend_assessment.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Article> Articles => Set<Article>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var article = modelBuilder.Entity<Article>();

        article.HasKey(a => a.Id);

        article.HasIndex(a => a.Code)
            .IsUnique();

        article.Property(a => a.Code)
            .IsRequired()
            .HasMaxLength(13);

        article.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(50);

        article.Property(a => a.PotSize)
            .IsRequired();

        article.Property(a => a.PlantHeight)
            .IsRequired();

        article.Property(a => a.ProductGroup)
            .IsRequired();
    }
}
