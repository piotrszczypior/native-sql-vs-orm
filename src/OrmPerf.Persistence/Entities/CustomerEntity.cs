using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrmPerf.Persistence.Entities;

public class CustomerEntity : Entity, IEntityTypeConfiguration<CustomerEntity>
{
    public int StoreId { get; set; }
    public StoreEntity Store { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public int AddressId { get; set; }
    public AddressEntity Address { get; set; }

    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime LastUpdate { get; set; }
    
    public ICollection<RentalEntity> Rentals { get; set; }

    public void Configure(EntityTypeBuilder<CustomerEntity> builder)
    {
        builder.HasIndex(e => e.AddressId);
        builder.HasIndex(e => e.StoreId);
        builder.HasIndex(e => e.LastName);
    }
}