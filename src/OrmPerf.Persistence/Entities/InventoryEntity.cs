using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrmPerf.Persistence.Entities;

public class InventoryEntity : Entity, IEntityTypeConfiguration<InventoryEntity>
{
    public int FilmId { get; set; }
    public FilmEntity Film { get; set; }
    
    public int StoreId { get; set; }
    public StoreEntity Store { get; set; }

    public DateTime LastUpdate { get; set; }

    public ICollection<RentalEntity> Rentals { get; set; }
    
    public void Configure(EntityTypeBuilder<InventoryEntity> builder)
    {
        builder.HasIndex(e => new { e.StoreId, e.FilmId });

        builder.HasMany(e => e.Rentals)
            .WithOne(e => e.Inventory)
            .HasForeignKey(e => e.InventoryId);
    }
}