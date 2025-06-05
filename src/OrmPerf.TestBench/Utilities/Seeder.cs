using OrmPerf.Persistence;
using OrmPerf.Persistence.Entities;
using OrmPerf.Persistence.Enums;

namespace OrmPerf.TestBench.Utilities;

internal static class Seeder
{
    public static async Task SeedAsync(
        DbContext context,
        DateTime? lastUpdate = null,
        CancellationToken cancellationToken = default)
    {
        var lastUpdateDate = (lastUpdate ?? new DateTime(2025, 01, 01)).ToUniversalTime();

        // Countries
        var polandCountry = new CountryEntity { Country = "Poland", LastUpdate = lastUpdateDate };
        var unitedStatesCountry = new CountryEntity { Country = "United States", LastUpdate = lastUpdateDate };
        var germanyCountry = new CountryEntity { Country = "Germany", LastUpdate = lastUpdateDate };

        context.AddRange(polandCountry, unitedStatesCountry, germanyCountry);
        await context.SaveChangesAsync(cancellationToken);

        // Cities
        var wroclawCity = new CityEntity { City = "Wrocław", Country = polandCountry, LastUpdate = lastUpdateDate };
        var warsawCity = new CityEntity { City = "Warsaw", Country = polandCountry, LastUpdate = lastUpdateDate };
        var katowiceCity = new CityEntity { City = "Katowice", Country = polandCountry, LastUpdate = lastUpdateDate };
        var washingtonCity = new CityEntity { City = "Washington", Country = unitedStatesCountry, LastUpdate = lastUpdateDate };
        var sanFranciscoCity = new CityEntity { City = "San Francisco", Country = unitedStatesCountry, LastUpdate = lastUpdateDate };
        var newYorkCity = new CityEntity { City = "New York", Country = unitedStatesCountry, LastUpdate = lastUpdateDate };
        var berlinCity = new CityEntity { City = "Berlin", Country = germanyCountry, LastUpdate = lastUpdateDate };

        context.AddRange(wroclawCity, warsawCity, katowiceCity, washingtonCity, sanFranciscoCity, newYorkCity, berlinCity);
        await context.SaveChangesAsync(cancellationToken);

        // Addresses
        var wladyslawaSikorskiegoAddress = new AddressEntity { Address = "Gen. Władysława Sikorskiego", Address2 = "5b/12", District = "Wrocław", City = wroclawCity, PostCode = "53-659", Phone = "531742125", LastUpdate = lastUpdateDate };
        var kazimierzaWielkiegoAddress = new AddressEntity { Address = "Kazimierza Wielkiego", Address2 = "81/8", District = "Wrocław", City = wroclawCity, PostCode = "50-056", Phone = "732653231", LastUpdate = lastUpdateDate };
        var julianaUrsynaNiemcewiczaAddress = new AddressEntity { Address = "Juliana Ursyna Niemcewicza", Address2 = "25", District = "Wrocław", City = wroclawCity, PostCode = "50-238", Phone = "213742069", LastUpdate = lastUpdateDate };

        context.AddRange(wladyslawaSikorskiegoAddress, kazimierzaWielkiegoAddress, julianaUrsynaNiemcewiczaAddress);
        await context.SaveChangesAsync(cancellationToken);

        // Actors
        var johnTravoltaActor = new ActorEntity { FirstName = "John", LastName = "Travolta", LastUpdate = lastUpdateDate };
        var umaThurmanActor = new ActorEntity { FirstName = "Uma", LastName = "Thurman", LastUpdate = lastUpdateDate };
        var samuelJacksonActor = new ActorEntity { FirstName = "Samuel", LastName = "Jackson", LastUpdate = lastUpdateDate };
        var bruceWillisActor = new ActorEntity { FirstName = "Bruce", LastName = "Willis", LastUpdate = lastUpdateDate };
        var timRothActor = new ActorEntity { FirstName = "Tim", LastName = "Roth", LastUpdate = lastUpdateDate };
        var amandaPlummerActor = new ActorEntity { FirstName = "Amanda", LastName = "Plummer", LastUpdate = lastUpdateDate };

        context.AddRange(johnTravoltaActor, umaThurmanActor, samuelJacksonActor, bruceWillisActor, timRothActor, amandaPlummerActor);
        await context.SaveChangesAsync(cancellationToken);

        // Languages
        var polishLanguage = new LanguageEntity { Name = "Polish", LastUpdate = lastUpdateDate };
        var englishLanguage = new LanguageEntity { Name = "English", LastUpdate = lastUpdateDate };
        var germanLanguage = new LanguageEntity { Name = "German", LastUpdate = lastUpdateDate };

        context.AddRange(polishLanguage, englishLanguage, germanLanguage);
        await context.SaveChangesAsync(cancellationToken);

        // Films
        var pulpFictionFilm = new FilmEntity
        {
            Title = "Pulp Fiction",
            Description = "The lives of two mob hitmen, a boxer, a gangster and his wife...",
            ReleaseYear = 1994,
            Language = englishLanguage,
            OriginalLanguage = englishLanguage,
            RentalDuration = 14,
            RentalRate = 9.99M,
            Length = 194,
            ReplacementCost = 19.99M,
            Rating = MpaaRating.R,
            LastUpdate = lastUpdateDate,
            SpecialFeatures = Array.Empty<string>(),
            FullText = "No, forget it, it's too risky...",
            RevenueProjection = 199.99M
        };

        var dieHardFilm = new FilmEntity
        {
            Title = "Die Hard",
            Description = "An NYPD officer tries to save his wife and others taken hostage.",
            ReleaseYear = 1988,
            Language = englishLanguage,
            RentalDuration = 5,
            RentalRate = 4.99M,
            Length = 132,
            ReplacementCost = 14.99M,
            Rating = MpaaRating.R,
            LastUpdate = lastUpdateDate,
            SpecialFeatures = new[] { "Deleted Scenes", "Commentary" },
            FullText = "Yippee-ki-yay...",
            RevenueProjection = 250.00M
        };

        var dasBootFilm = new FilmEntity
        {
            Title = "Das Boot",
            Description = "A German submarine crew experiences the terror and tedium of war.",
            ReleaseYear = 1981,
            Language = germanLanguage,
            RentalDuration = 7,
            RentalRate = 6.99M,
            Length = 149,
            ReplacementCost = 17.99M,
            Rating = MpaaRating.PG13,
            LastUpdate = lastUpdateDate,
            SpecialFeatures = Array.Empty<string>(),
            FullText = "Submarine war drama.",
            RevenueProjection = 180.00M
        };

        context.AddRange(pulpFictionFilm, dieHardFilm, dasBootFilm);
        await context.SaveChangesAsync(cancellationToken);

        // Film Actors
        var filmActors = new List<FilmActorEntity>
        {
            new() { Actor = johnTravoltaActor, Film = pulpFictionFilm, LastUpdate = lastUpdateDate },
            new() { Actor = bruceWillisActor, Film = dieHardFilm, LastUpdate = lastUpdateDate }
        };

        var dasBootCast = new[]
        {
            new ActorEntity { FirstName = "Jürgen", LastName = "Prochnow", LastUpdate = lastUpdateDate },
            new ActorEntity { FirstName = "Herbert", LastName = "Grönemeyer", LastUpdate = lastUpdateDate }
        };

        context.AddRange(dasBootCast);
        await context.SaveChangesAsync(cancellationToken);

        filmActors.AddRange(dasBootCast.Select(actor => new FilmActorEntity
        {
            Actor = actor,
            Film = dasBootFilm,
            LastUpdate = lastUpdateDate
        }));

        context.AddRange(filmActors);
        await context.SaveChangesAsync(cancellationToken);

        // Categories
        var categories = new[]
        {
            new CategoryEntity { Name = "Dark Comedy", LastUpdated = lastUpdateDate },
            new CategoryEntity { Name = "Drug Crime", LastUpdated = lastUpdateDate },
            new CategoryEntity { Name = "Gangster", LastUpdated = lastUpdateDate },
            new CategoryEntity { Name = "Crime", LastUpdated = lastUpdateDate },
            new CategoryEntity { Name = "Drama", LastUpdated = lastUpdateDate },
            new CategoryEntity { Name = "Action", LastUpdated = lastUpdateDate },
            new CategoryEntity { Name = "Horror", LastUpdated = lastUpdateDate }
        };

        context.AddRange(categories);
        await context.SaveChangesAsync(cancellationToken);

        var filmsCategories = new List<FilmCategoryEntity>
        {
            new() { Film = pulpFictionFilm, Category = categories[0] },
            new() { Film = pulpFictionFilm, Category = categories[1] },
            new() { Film = pulpFictionFilm, Category = categories[2] },
            new() { Film = pulpFictionFilm, Category = categories[3] },
            new() { Film = pulpFictionFilm, Category = categories[4] },
            new() { Film = dieHardFilm, Category = categories[5] },
            new() { Film = dasBootFilm, Category = categories[4] }
        };

        context.AddRange(filmsCategories);
        await context.SaveChangesAsync(cancellationToken);

        // Stores
        var netflixStore = new StoreEntity { Address = kazimierzaWielkiegoAddress, LastUpdate = lastUpdateDate };
        context.Add(netflixStore);
        await context.SaveChangesAsync(cancellationToken);

        // Staff
        var pawelKleszczStaff = new StaffEntity
        {
            FirstName = "Paweł",
            LastName = "Kleszcz",
            Address = wladyslawaSikorskiegoAddress,
            Email = "p.kleszcz@example.com",
            Store = netflixStore,
            IsActive = false,
            Username = "pkleszcz",
            Password = "6aad43a307868eb673b212ed9be344ce",
            LastUpdate = lastUpdateDate,
            Picture = null
        };

        context.Add(pawelKleszczStaff);
        await context.SaveChangesAsync(cancellationToken);

        // Inventories
        var inventories = new[]
        {
            new InventoryEntity { Film = pulpFictionFilm, Store = netflixStore, LastUpdate = lastUpdateDate },
            new InventoryEntity { Film = dieHardFilm, Store = netflixStore, LastUpdate = lastUpdateDate },
            new InventoryEntity { Film = dasBootFilm, Store = netflixStore, LastUpdate = lastUpdateDate }
        };

        context.AddRange(inventories);
        await context.SaveChangesAsync(cancellationToken);

        // Customers
        var janeDoeCustomer = new CustomerEntity
        {
            Store = netflixStore,
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane.doe@example.com",
            Address = julianaUrsynaNiemcewiczaAddress,
            IsActive = true,
            CreateDate = lastUpdateDate,
            LastUpdate = lastUpdateDate
        };

        context.Add(janeDoeCustomer);
        await context.SaveChangesAsync(cancellationToken);

        // Rentals
        var janeDoePulpFictionRental = new RentalEntity
        {
            Inventory = inventories[0],
            Customer = janeDoeCustomer,
            Staff = pawelKleszczStaff,
            LastUpdate = lastUpdateDate,
            RentalStart = lastUpdateDate,
            RentalEnd = null
        };

        context.Add(janeDoePulpFictionRental);
        await context.SaveChangesAsync(cancellationToken);
    }
}