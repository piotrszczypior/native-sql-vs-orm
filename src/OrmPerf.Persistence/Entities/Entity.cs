namespace OrmPerf.Persistence.Entities;

public abstract class Entity : KeylessEntity
{
    public int Id { get; set; }
}