using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrmPerf.Persistence.Entities;

public class PaymentEntity : Entity, IEntityTypeConfiguration<PaymentEntity>
{
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; }
    
    public int StaffId { get; set; }
    public StaffEntity Staff { get; set; }
    
    public int RentalId { get; set; }
    public RentalEntity Rental { get; set; }
    
    public decimal Amount { get; set; }
    
    public DateTime PaymentDate { get; set; }
    
    
    public void Configure(EntityTypeBuilder<PaymentEntity> builder)
    {
        builder.HasOne(e => e.Customer)
            .WithMany(e => e.Payments)
            .HasForeignKey(e => e.CustomerId);
        
        builder.HasOne(e => e.Staff)
            .WithMany(e => e.Payments)
            .HasForeignKey(e => e.StaffId);
        
        builder.HasOne(e => e.Rental)
            .WithOne(e => e.Payment)
            .HasForeignKey<PaymentEntity>(e => e.RentalId);
    }
}