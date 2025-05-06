using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrmPerf.Persistence.Entities;

public class RentalEntity : Entity, IEntityTypeConfiguration<RentalEntity>
{
    public int InventoryId { get; set; }
    public InventoryEntity Inventory { get; set; }

    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; }
    
    public int StaffId { get; set; }
    public StaffEntity Staff { get; set; }
    
    public DateTime LastUpdate { get; set; }
    
    public DateTime RentalStart { get; set; }
    public DateTime? RentalEnd { get; set; }
    
    public void Configure(EntityTypeBuilder<RentalEntity> builder)
    {
        builder.HasIndex(e => e.InventoryId);

        builder.HasOne(e => e.Customer)
            .WithMany(e => e.Rentals)
            .HasForeignKey(e => e.CustomerId);
    }
}