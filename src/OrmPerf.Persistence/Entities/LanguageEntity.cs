using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrmPerf.Persistence.Entities;

public class LanguageEntity : Entity, IEntityTypeConfiguration<LanguageEntity>
{
    public string Name { get; set; }
    public DateTime LastUpdate { get; set; }

    public ICollection<FilmEntity> TranslatedFilms { get; set; }
    public ICollection<FilmEntity> OriginalFilms { get; set; }
    
    public void Configure(EntityTypeBuilder<LanguageEntity> builder)
    {
        builder.Property(e => e.Name)
            .HasMaxLength(20);

        builder.HasMany(e => e.TranslatedFilms)
            .WithOne(e => e.Language)
            .HasForeignKey(e => e.LanguageId);

        builder.HasMany(e => e.OriginalFilms)
            .WithOne(e => e.OriginalLanguage)
            .HasForeignKey(e => e.OriginalLanguageId);
    }
}