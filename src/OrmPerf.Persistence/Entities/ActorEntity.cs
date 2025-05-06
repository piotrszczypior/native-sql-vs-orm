using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrmPerf.Persistence.Entities;

public class ActorEntity : Entity, IEntityTypeConfiguration<ActorEntity>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public DateTime LastUpdate { get; set; }
    
    public ICollection<FilmActorEntity> Films { get; set; }

    public void Configure(EntityTypeBuilder<ActorEntity> builder)
    {
        builder.HasIndex(e => e.LastName);
    }
}