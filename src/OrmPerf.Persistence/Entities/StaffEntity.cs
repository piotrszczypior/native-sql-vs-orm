using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrmPerf.Persistence.Entities;

public class StaffEntity : Entity, IEntityTypeConfiguration<StaffEntity>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public int AddressId { get; set; }
    public AddressEntity Address { get; set; }
    
    public string? Email { get; set; }

    public int StoreId { get; set; }
    public StoreEntity Store { get; set; }
    
    public bool IsActive { get; set; }
    public string Username { get; set; }
    public string? Password { get; set; }
    public DateTime LastUpdate { get; set; }
    public byte[]? Picture { get; set; }

    public ICollection<StoreEntity> Stores { get; set; }
    
    public void Configure(EntityTypeBuilder<StaffEntity> builder)
    {
        builder.Property(e => e.FirstName)
            .HasMaxLength(45);
        builder.Property(e => e.LastName)
            .HasMaxLength(45);
        builder.Property(e => e.Email)
            .HasMaxLength(50);
        builder.Property(e => e.Username)
            .HasMaxLength(16);
        builder.Property(e => e.Password)
            .HasMaxLength(40);

        builder.HasOne(e => e.Store)
            .WithMany(e => e.Staff)
            .HasForeignKey(e => e.StoreId);
        
        builder.HasMany(e => e.Stores)
            .WithOne(e => e.ManagerStaff)
            .HasForeignKey(e => e.ManagerStaffId);

    }
}