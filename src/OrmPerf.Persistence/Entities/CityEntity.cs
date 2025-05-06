using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrmPerf.Persistence.Entities;

public class CityEntity : Entity, IEntityTypeConfiguration<CityEntity>
{
    public string City { get; set; }
    
    public int CountryId { get; set; }
    public CountryEntity Country { get; set; }
    
    public DateTime LastUpdate { get; set; }

    public ICollection<AddressEntity> Addresses { get; set; }
    
    public void Configure(EntityTypeBuilder<CityEntity> builder)
    {
        builder.Property(e => e.City)
            .HasMaxLength(50);

        builder.HasIndex(e => e.CountryId);

        builder.HasMany(e => e.Addresses)
            .WithOne(e => e.City)
            .HasForeignKey(e => e.CityId);
    }
}