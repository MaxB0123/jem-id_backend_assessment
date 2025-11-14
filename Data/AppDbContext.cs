using jem_id_backend_assessment.Models;
using Microsoft.EntityFrameworkCore;
using jem_id_backend_assessment.Models;

namespace jem_id_backend_assessment.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<ArticleModel.Article> Articles => Set<ArticleModel.Article>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Optional: explicit config for Article
        modelBuilder.Entity<ArticleModel.Article>(entity =>
        {
            entity.HasKey(a => a.Id);
            
            entity.HasIndex(a => a.Code)
                .IsUnique();

            entity.Property(a => a.Code)
                .IsRequired()
                .HasMaxLength(13);

            entity.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(a => a.PotSize)
                .IsRequired();

            entity.Property(a => a.PlantHeight)
                .IsRequired();

            entity.Property(a => a.ProductGroup)
                .IsRequired();
        });
    }
}
