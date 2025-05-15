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
        var polandCountry = new CountryEntity
        {
            Country = "Poland",
            LastUpdate = lastUpdateDate
        };
        var unitedStatesCountry = new CountryEntity
        {
            Country = "United States",
            LastUpdate = lastUpdateDate
        };
        var germanyCountry = new CountryEntity
        {
            Country = "Germany",
            LastUpdate = lastUpdateDate
        };

        var countries = new List<CountryEntity>
        {
            polandCountry,
            unitedStatesCountry,
            germanyCountry
        };
        
        context.AddRange(countries);
        await context.SaveChangesAsync(cancellationToken);

        // Cities
        var wroclawCity = new CityEntity
        {
            City = "Wrocław",
            Country = polandCountry,
            LastUpdate = lastUpdateDate
        };
        var warsawCity = new CityEntity
        {
            City = "Warsaw",
            Country = polandCountry,
            LastUpdate = lastUpdateDate
        };
        var katowiceCity = new CityEntity
        {
            City = "Katowice",
            Country = polandCountry,
            LastUpdate = lastUpdateDate
        };
        var washingtonCity = new CityEntity
        {
            City = "Washington",
            Country = unitedStatesCountry,
            LastUpdate = lastUpdateDate
        };
        var sanFranciscoCity = new CityEntity
        {
            City = "San Francisco",
            Country = unitedStatesCountry,
            LastUpdate = lastUpdateDate
        };
        var newYorkCity = new CityEntity
        {
            City = "New York",
            Country = unitedStatesCountry,
            LastUpdate = lastUpdateDate
        };
        var berlinCity = new CityEntity
        {
            City = "Berlin",
            Country = germanyCountry,
            LastUpdate = lastUpdateDate
        };

        var cities = new List<CityEntity>
        {
            wroclawCity,
            warsawCity,
            katowiceCity,
            washingtonCity,
            sanFranciscoCity,
            newYorkCity,
            berlinCity
        };
        
        context.AddRange(cities);
        await context.SaveChangesAsync(cancellationToken);

        // Addresses
        var wladyslawaSikorskiegoAddress = new AddressEntity
        {
            Address = "Gen. Władysława Sikorskiego",
            Address2 = "5b/12",
            District = "Wrocław",
            City = wroclawCity,
            PostCode = "53-659",
            Phone = "531742125",
            LastUpdate = lastUpdateDate
        };
        var kazimierzaWielkiegoAddress = new AddressEntity
        {
            Address = "Kazimierza Wielkiego",
            Address2 = "81/8",
            District = "Wrocław",
            City = wroclawCity,
            PostCode = "50-056",
            Phone = "732653231",
            LastUpdate = lastUpdateDate
        };
        var julianaUrsynaNiemcewiczaAddress = new AddressEntity
        {
            Address = "Juliana Ursyna Niemcewicza",
            Address2 = "25",
            District = "Wrocław",
            City = wroclawCity,
            PostCode = "50-238",
            Phone = "213742069",
            LastUpdate = lastUpdateDate
        };

        var addresses = new List<AddressEntity>
        {
            wladyslawaSikorskiegoAddress,
            kazimierzaWielkiegoAddress,
            julianaUrsynaNiemcewiczaAddress
        };

        context.AddRange(addresses);
        await context.SaveChangesAsync(cancellationToken);
        
        // Actors
        var johnTravoltaActor = new ActorEntity
        {
            FirstName = "John",
            LastName = "Travolta",
            LastUpdate = lastUpdateDate
        };
        var umaThurmanActor = new ActorEntity
        {
            FirstName = "Uma",
            LastName = "Thurman",
            LastUpdate = lastUpdateDate
        };
        var samuelJacksonActor = new ActorEntity
        {
            FirstName = "Samuel",
            LastName = "Jackson",
            LastUpdate = lastUpdateDate
        };
        var bruceWillisActor = new ActorEntity
        {
            FirstName = "Bruce",
            LastName = "Willis",
            LastUpdate = lastUpdateDate
        };
        var timRothActor = new ActorEntity
        {
            FirstName = "Tim",
            LastName = "Roth",
            LastUpdate = lastUpdateDate
        };
        var amandaPlummerActor = new ActorEntity
        {
            FirstName = "Amanda",
            LastName = "Plummer",
            LastUpdate = lastUpdateDate
        };
        
        var actors = new List<ActorEntity>
        {
            johnTravoltaActor,
            umaThurmanActor,
            samuelJacksonActor,
            bruceWillisActor,
            timRothActor,
            amandaPlummerActor
        };
        
        context.AddRange(actors);
        await context.SaveChangesAsync(cancellationToken);

        // Languages
        var polishLanguage = new LanguageEntity
        {
            Name = "Polish",
            LastUpdate = lastUpdateDate
        };
        var englishLanguage = new LanguageEntity
        {
            Name = "English",
            LastUpdate = lastUpdateDate
        };
        var germanLanguage = new LanguageEntity
        {
            Name = "German",
            LastUpdate = lastUpdateDate
        };

        var languages = new List<LanguageEntity>
        {
            polishLanguage,
            englishLanguage,
            germanLanguage
        };
        
        context.AddRange(languages);
        await context.SaveChangesAsync(cancellationToken);

        // Films
        var pulpFictionFilm = new FilmEntity
        {
            Title = "Pulp Fiction",
            Description = "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
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
            FullText = "No, forget it, it's too risky. I'm through doin' that shit. [...]",
            RevenueProjection = 199.99M
        };

        var films = new List<FilmEntity>
        {
            pulpFictionFilm
        };
        
        context.AddRange(films);
        await context.SaveChangesAsync(cancellationToken);

        // Films actors
        var johnTravoltaInPulpFictionFilmActor = new FilmActorEntity
        {
            ActorId = johnTravoltaActor.Id,
            Actor = johnTravoltaActor,
            FilmId = pulpFictionFilm.Id,
            Film = pulpFictionFilm,
            LastUpdate = lastUpdateDate
        };

        var filmActors = new List<FilmActorEntity>
        {
            johnTravoltaInPulpFictionFilmActor
        };
        
        context.AddRange(filmActors);
        await context.SaveChangesAsync(cancellationToken);

        // Categories
        var darkComedyCategory = new CategoryEntity
        {
            Name = "Dark Comedy",
            LastUpdated = lastUpdateDate
        };
        var drugCrimeCategory = new CategoryEntity
        {
            Name = "Drug Crime",
            LastUpdated = lastUpdateDate
        };
        var gangsterCategory = new CategoryEntity
        {
            Name = "Gangster",
            LastUpdated = lastUpdateDate
        };
        var crimeCategory = new CategoryEntity
        {
            Name = "Crime",
            LastUpdated = lastUpdateDate
        };
        var dramaCategory = new CategoryEntity
        {
            Name = "Drama",
            LastUpdated = lastUpdateDate
        };
        var actionCategory = new CategoryEntity
        {
            Name = "Action",
            LastUpdated = lastUpdateDate
        };
        var horrorCategory = new CategoryEntity
        {
            Name = "Horror",
            LastUpdated = lastUpdateDate
        };

        var categories = new List<CategoryEntity>
        {
            darkComedyCategory,
            drugCrimeCategory,
            gangsterCategory,
            crimeCategory,
            dramaCategory,
            actionCategory,
            horrorCategory
        };
        
        context.AddRange(categories);
        await context.SaveChangesAsync(cancellationToken);
        
        // Films categories
        var pulpFictionDarkComedyCategory = new FilmCategoryEntity
        {
            Film = pulpFictionFilm,
            Category = darkComedyCategory
        };
        var pulpFictionDrugCrimeCategory = new FilmCategoryEntity
        {
            Film = pulpFictionFilm,
            Category = drugCrimeCategory
        };
        var pulpFictionGangsterCategory = new FilmCategoryEntity
        {
            Film = pulpFictionFilm,
            Category = gangsterCategory
        };
        var pulpFictionCrimeCategory = new FilmCategoryEntity
        {
            Film = pulpFictionFilm,
            Category = crimeCategory
        };
        var pulpFictionDramaCategory = new FilmCategoryEntity
        {
            Film = pulpFictionFilm,
            Category = dramaCategory
        };

        var filmsCategories = new List<FilmCategoryEntity>
        {
            pulpFictionDarkComedyCategory,
            pulpFictionDrugCrimeCategory,
            pulpFictionGangsterCategory,
            pulpFictionCrimeCategory,
            pulpFictionDramaCategory
        };
        
        context.AddRange(filmsCategories);
        await context.SaveChangesAsync(cancellationToken);

        // Stores
        var netflixStore = new StoreEntity
        {
            Address = kazimierzaWielkiegoAddress,
            LastUpdate = lastUpdateDate
        };

        var stores = new List<StoreEntity>
        {
            netflixStore
        };
        
        context.AddRange(stores);
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

        var staff = new List<StaffEntity>
        {
            pawelKleszczStaff
        };

        context.AddRange(staff);
        await context.SaveChangesAsync(cancellationToken);

        // Inventories
        var pulpFictionNetflixInventory = new InventoryEntity
        {
            Film = pulpFictionFilm,
            Store = netflixStore,
            LastUpdate = lastUpdateDate
        };

        var inventories = new List<InventoryEntity>
        {
            pulpFictionNetflixInventory
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

        var customers = new List<CustomerEntity>
        {
            janeDoeCustomer
        };
        
        context.AddRange(customers);
        await context.SaveChangesAsync(cancellationToken);
        
        // Rentals
        var janeDoePulpFictionRental = new RentalEntity
        {
            Inventory = pulpFictionNetflixInventory,
            Customer = janeDoeCustomer,
            Staff = pawelKleszczStaff,
            LastUpdate = lastUpdateDate,
            RentalStart = lastUpdateDate,
            RentalEnd = null
        };

        var rentals = new List<RentalEntity>
        {
            janeDoePulpFictionRental
        };
        
        context.AddRange(rentals);
        await context.SaveChangesAsync(cancellationToken);
    }
}