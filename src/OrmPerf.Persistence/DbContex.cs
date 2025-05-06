using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OrmPerf.Persistence.Entities;
using EfDbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace OrmPerf.Persistence;

// Db based on: https://www.dbdiagrams.com/online-diagrams/Pagila-2-1-0/index.html?page=Diagrams&item=itm-5656fe50-533b-4cd7-9a6c-535f3111b8a1
// TODO: Add Payments table

public class DbContext : EfDbContext
{
    private readonly Assembly _entitiesAssembly = typeof(FilmEntity).Assembly;
    
    public DbSet<ActorEntity> Actors { get; set; }
    public DbSet<FilmActorEntity> FilmsActors { get; set; }
    public DbSet<FilmEntity> Films { get; set; }
    public DbSet<FilmCategoryEntity> FilmsCategories { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<LanguageEntity> Languages { get; set; }
    public DbSet<InventoryEntity> Inventories { get; set; }
    public DbSet<RentalEntity> Rentals { get; set; }
    public DbSet<StoreEntity> Stores { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<CityEntity> Cities { get; set; }
    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<StaffEntity> Staff { get; set; }

    public DbContext(DbContextOptions<DbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(_entitiesAssembly);
    }
}
