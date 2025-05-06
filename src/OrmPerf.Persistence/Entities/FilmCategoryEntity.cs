using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrmPerf.Persistence.Entities;

public class FilmCategoryEntity : KeylessEntity, IEntityTypeConfiguration<FilmCategoryEntity>
{
    public int FilmId { get; set; }
    public FilmEntity Film { get; set; }

    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; }
    
    public void Configure(EntityTypeBuilder<FilmCategoryEntity> builder)
    {
        builder.HasKey(e => new { e.FilmId, e.CategoryId });
        
        builder.HasOne(e => e.Film)
            .WithMany(e => e.Categories)
            .HasForeignKey(e => e.FilmId);
        
        builder.HasOne(e => e.Category)
            .WithMany(e => e.Films)
            .HasForeignKey(e => e.CategoryId);
    }
}