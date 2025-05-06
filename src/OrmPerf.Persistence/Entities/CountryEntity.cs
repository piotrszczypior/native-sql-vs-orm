using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrmPerf.Persistence.Entities;

public class CountryEntity : Entity, IEntityTypeConfiguration<CountryEntity>
{
    public string Country { get; set; }
    public DateTime LastUpdate { get; set; }

    public ICollection<CityEntity> Cities { get; set; }
    
    public void Configure(EntityTypeBuilder<CountryEntity> builder)
    {
        builder.Property(e => e.Country)
            .HasMaxLength(50);

        builder.HasMany(e => e.Cities)
            .WithOne(e => e.Country)
            .HasForeignKey(e => e.CountryId);
    }
}