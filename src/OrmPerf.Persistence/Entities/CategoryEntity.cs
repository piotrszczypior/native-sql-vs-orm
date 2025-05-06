namespace OrmPerf.Persistence.Entities;

public class CategoryEntity : Entity
{
    public string Name { get; set; }
    public DateTime LastUpdated { get; set; }
    
    public ICollection<FilmCategoryEntity> Films { get; set; }
}