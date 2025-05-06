using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrmPerf.Persistence.Entities;

public class StoreEntity : Entity, IEntityTypeConfiguration<StoreEntity>
{
    public int ManagerStaffId { get; set; }
    public StaffEntity ManagerStaff { get; set; }
    
    public int AddressId { get; set; }
    public AddressEntity Address { get; set; }

    public DateTime LastUpdate { get; set; }

    public ICollection<InventoryEntity> Inventories { get; set; }
    public ICollection<CustomerEntity> Customers { get; set; }
    public ICollection<StaffEntity> Staff { get; set; }
    
    public void Configure(EntityTypeBuilder<StoreEntity> builder)
    {
        builder.HasIndex(e => e.ManagerStaffId);

        builder.HasMany(e => e.Inventories)
            .WithOne(e => e.Store)
            .HasForeignKey(e => e.StoreId);

        builder.HasMany(e => e.Customers)
            .WithOne(e => e.Store)
            .HasForeignKey(e => e.StoreId);
    }
}