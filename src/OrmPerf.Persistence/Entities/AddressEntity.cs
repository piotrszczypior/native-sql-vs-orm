using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrmPerf.Persistence.Entities;

public class AddressEntity : Entity, IEntityTypeConfiguration<AddressEntity>
{
    public string Address { get; set; }
    public string Address2 { get; set; }
    public string District { get; set; }
    
    public int CityId { get; set; }
    public CityEntity City { get; set; }
    
    public string PostCode { get; set; }
    public string Phone { get; set; }
    public DateTime LastUpdate { get; set; }
    
    public ICollection<StoreEntity> Stores { get; set; }
    public ICollection<CustomerEntity> Customers { get; set; }

    public void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.Property(e => e.Address)
            .HasMaxLength(50);
        builder.Property(e => e.Address2)
            .HasMaxLength(50);
        builder.Property(e => e.District)
            .HasMaxLength(20);
        builder.Property(e => e.PostCode)
            .HasMaxLength(10);
        builder.Property(e => e.Phone)
            .HasMaxLength(20);

        builder.HasIndex(e => e.CityId);
        builder.HasIndex(e => e.Phone);
        builder.HasIndex(e => new { e.Address, e.Address2 });

        builder.HasMany(e => e.Stores)
            .WithOne(e => e.Address)
            .HasForeignKey(e => e.AddressId);
        
        builder.HasMany(e => e.Customers)
            .WithOne(e => e.Address)
            .HasForeignKey(e => e.AddressId);
    }
}