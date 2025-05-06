using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrmPerf.Persistence.Entities;

public class FilmActorEntity : KeylessEntity, IEntityTypeConfiguration<FilmActorEntity>
{
    public int ActorId { get; set; }
    public ActorEntity Actor { get; set; }
    
    public int FilmId { get; set; }
    public FilmEntity Film { get; set; }
    
    public DateTime LastUpdate { get; set; }
    
    public void Configure(EntityTypeBuilder<FilmActorEntity> builder)
    {
        builder.HasKey(e => new { e.ActorId, e.FilmId });
        
        builder.HasOne<ActorEntity>()
            .WithMany(e => e.Films)
            .HasForeignKey(e => e.ActorId);

        builder.HasOne<FilmEntity>()
            .WithMany(e => e.Actors)
            .HasForeignKey(e => e.FilmId);

        builder.HasIndex(e => e.FilmId);
    }
}