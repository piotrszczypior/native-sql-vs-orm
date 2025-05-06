using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrmPerf.Persistence.Enums;

namespace OrmPerf.Persistence.Entities;

public class FilmEntity : Entity, IEntityTypeConfiguration<FilmEntity>
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public int? ReleaseYear { get; set; }

    public int LanguageId { get; set; }
    public LanguageEntity Language { get; set; }
    
    public int OriginalLanguageId { get; set; }
    public LanguageEntity OriginalLanguage { get; set; }
    
    public int RentalDuration { get; set; }
    public decimal RentalRate { get; set; }
    public int? Length { get; set; }
    public decimal ReplacementCost { get; set; }
    public MpaaRating? Rating { get; set; }
    public DateTime LastUpdate { get; set; }
    public string[]? SpecialFeatures { get; set; }
    public string FullText { get; set; }
    public decimal? RevenueProjection { get; set; }
    
    public ICollection<FilmActorEntity> Actors { get; set; }
    public ICollection<FilmCategoryEntity> Categories { get; set; }
    public ICollection<InventoryEntity> Inventories { get; set; }
    
    public void Configure(EntityTypeBuilder<FilmEntity> builder)
    {
        builder.Property(e => e.Title)
            .HasMaxLength(255);

        builder.Property(e => e.RentalRate)
            .HasPrecision(4, 2);

        builder.Property(e => e.ReplacementCost)
            .HasPrecision(5, 2);

        builder.Property(e => e.RevenueProjection)
            .HasPrecision(5, 2);

        builder.HasIndex(e => e.FullText);
        builder.HasIndex(e => e.LanguageId);
        builder.HasIndex(e => e.OriginalLanguageId);
        builder.HasIndex(e => e.Title);

        builder.HasMany(e => e.Inventories)
            .WithOne(e => e.Film)
            .HasForeignKey(e => e.FilmId);

        builder.HasOne(e => e.Language)
            .WithMany(e => e.TranslatedFilms)
            .HasForeignKey(e => e.LanguageId);

        builder.HasOne(e => e.OriginalLanguage)
            .WithMany(e => e.OriginalFilms)
            .HasForeignKey(e => e.OriginalLanguageId);
    }
}