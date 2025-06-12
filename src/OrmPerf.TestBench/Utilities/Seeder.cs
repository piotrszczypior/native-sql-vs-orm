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
        var lastUpdateDate = (lastUpdate ?? new DateTime(2025, 01, 01))
            .ToUniversalTime();

        var countries = GenerateCountries(lastUpdateDate);
        context.AddRange(countries);
        await context.SaveChangesAsync(cancellationToken);

        var cities = GenerateCities(countries, lastUpdateDate);
        context.AddRange(cities);
        await context.SaveChangesAsync(cancellationToken);

        var addresses = GenerateAddresses(cities, lastUpdateDate);
        context.AddRange(addresses);
        await context.SaveChangesAsync(cancellationToken);

        var languages = GenerateLanguages(lastUpdateDate);
        context.AddRange(languages);
        await context.SaveChangesAsync(cancellationToken);

        var actors = GenerateActors(lastUpdateDate);
        context.AddRange(actors);
        await context.SaveChangesAsync(cancellationToken);

        var categories = GenerateCategories(lastUpdateDate);
        context.AddRange(categories);
        await context.SaveChangesAsync(cancellationToken);

        var films = GenerateFilms(languages, lastUpdateDate);
        context.AddRange(films);
        await context.SaveChangesAsync(cancellationToken);

        var filmActors = GenerateFilmActors(films, actors, lastUpdateDate);
        context.AddRange(filmActors);
        await context.SaveChangesAsync(cancellationToken);

        var filmCategories = GenerateFilmCategories(films, categories);
        context.AddRange(filmCategories);
        await context.SaveChangesAsync(cancellationToken);

        var stores = GenerateStores(addresses, lastUpdateDate);
        context.AddRange(stores);
        await context.SaveChangesAsync(cancellationToken);

        var staff = GenerateStaff(stores, addresses, lastUpdateDate);
        context.AddRange(staff);
        await context.SaveChangesAsync(cancellationToken);

        var inventories = GenerateInventories(stores, films, lastUpdateDate);
        context.AddRange(inventories);
        await context.SaveChangesAsync(cancellationToken);

        var customers = GenerateCustomers(stores, addresses, lastUpdateDate);
        context.AddRange(customers);
        await context.SaveChangesAsync(cancellationToken);

        var rentals = GenerateRentals(customers, inventories, staff, lastUpdateDate);
        context.AddRange(rentals);
        await context.SaveChangesAsync(cancellationToken);
    }

    private static List<CountryEntity> GenerateCountries(DateTime lastUpdateDate)
    {
        var countryNames = new[]
        {
            "Poland", "USA", "Germany", "France", "UK", "Italy", "Spain", "Japan", "Canada", "Australia",
            "Brazil", "Mexico", "Argentina", "India", "China", "S Korea", "Russia", "Netherlands", "Belgium", "Switzerland",
            "Austria", "Sweden", "Norway", "Denmark", "Finland", "Portugal", "Greece", "Turkey", "Egypt", "S Africa",
            "Nigeria", "Kenya", "Morocco", "Israel", "Saudi", "UAE", "Iran", "Iraq", "Afghanistan", "Pakistan",
            "Bangladesh", "Thailand", "Vietnam", "Philippines", "Indonesia", "Malaysia", "Singapore", "N Zealand", "Chile", "Peru"
        };

        return countryNames.Select(name => new CountryEntity
        {
            Country = name,
            LastUpdate = lastUpdateDate
        }).ToList();
    }

    private static List<CityEntity> GenerateCities(List<CountryEntity> countries, DateTime lastUpdateDate)
    {
        var citiesData = new Dictionary<string, string[]>
        {
            ["Poland"] = new[] { "Warsaw", "Krakow", "Wroclaw", "Poznan", "Gdansk", "Szczecin", "Bydgoszcz", "Lublin", "Katowice", "Bialystok" },
            ["USA"] = new[] { "NYC", "LA", "Chicago", "Houston", "Phoenix", "Philly", "San Antonio", "San Diego", "Dallas", "San Jose" },
            ["Germany"] = new[] { "Berlin", "Hamburg", "Munich", "Cologne", "Frankfurt", "Stuttgart", "Dusseldorf", "Leipzig", "Dortmund", "Essen" },
            ["France"] = new[] { "Paris", "Marseille", "Lyon", "Toulouse", "Nice", "Nantes", "Montpellier", "Strasbourg", "Bordeaux", "Lille" },
            ["UK"] = new[] { "London", "Birmingham", "Manchester", "Glasgow", "Liverpool", "Leeds", "Sheffield", "Edinburgh", "Bristol", "Cardiff" },
            ["Italy"] = new[] { "Rome", "Milan", "Naples", "Turin", "Palermo", "Genoa", "Bologna", "Florence", "Bari", "Catania" },
            ["Spain"] = new[] { "Madrid", "Barcelona", "Valencia", "Seville", "Zaragoza", "Malaga", "Murcia", "Palma", "Las Palmas", "Bilbao" },
            ["Japan"] = new[] { "Tokyo", "Yokohama", "Osaka", "Nagoya", "Sapporo", "Fukuoka", "Kobe", "Kawasaki", "Kyoto", "Saitama" },
            ["Canada"] = new[] { "Toronto", "Montreal", "Vancouver", "Calgary", "Edmonton", "Ottawa", "Winnipeg", "Quebec", "Hamilton", "Kitchener" },
            ["Australia"] = new[] { "Sydney", "Melbourne", "Brisbane", "Perth", "Adelaide", "Gold Coast", "Newcastle", "Canberra", "Sunshine", "Wollongong" }
        };

        var cities = new List<CityEntity>();
        
        foreach (var country in countries)
        {
            if (citiesData.ContainsKey(country.Country))
            {
                foreach (var cityName in citiesData[country.Country])
                {
                    cities.Add(new CityEntity
                    {
                        City = cityName,
                        Country = country,
                        LastUpdate = lastUpdateDate
                    });
                }
            }
            else
            {
                for (int i = 1; i <= 5; i++)
                {
                    cities.Add(new CityEntity
                    {
                        City = $"City{i}",
                        Country = country,
                        LastUpdate = lastUpdateDate
                    });
                }
            }
        }

        return cities;
    }

    private static List<AddressEntity> GenerateAddresses(List<CityEntity> cities, DateTime lastUpdateDate)
    {
        var streetNames = new[]
        {
            "Main St", "Oak Ave", "Park Rd", "Church Ln", "High St", "King St", "Queen St", "Victoria Rd", "Station Rd", "Mill Ln",
            "School St", "Market Sq", "Castle St", "Bridge Rd", "Hill St", "Garden Ave", "Forest Rd", "River St", "Lake Dr", "Mountain Vw"
        };

        var addresses = new List<AddressEntity>();
        int addressIndex = 0;

        foreach (var city in cities)
        {
            int addressesPerCity = city.Country.Country switch
            {
                "USA" => 50,
                "Poland" => 30,
                "Germany" => 25,
                "France" => 20,
                "UK" => 20,
                _ => 10
            };

            for (int i = 0; i < addressesPerCity; i++)
            {
                var streetName = streetNames[addressIndex % streetNames.Length];
                var buildingNumber = (addressIndex % 999) + 1;
                var apartmentNumber = addressIndex % 3 == 0 ? $"/{(addressIndex % 100) + 1}" : "";

                addresses.Add(new AddressEntity
                {
                    Address = streetName,
                    Address2 = $"{buildingNumber}{apartmentNumber}",
                    District = city.City,
                    City = city,
                    PostCode = GeneratePostCode(city.Country.Country, addressIndex),
                    Phone = GeneratePhoneNumber(city.Country.Country, addressIndex),
                    LastUpdate = lastUpdateDate
                });

                addressIndex++;
            }
        }

        return addresses;
    }

    private static List<LanguageEntity> GenerateLanguages(DateTime lastUpdateDate)
    {
        var languageNames = new[]
        {
            "English", "Spanish", "French", "German", "Italian", "Portuguese", "Russian", "Japanese", "Korean", "Chinese",
            "Arabic", "Hindi", "Bengali", "Punjabi", "Urdu", "Indonesian", "Malay", "Thai", "Vietnamese", "Turkish",
            "Polish", "Dutch", "Swedish", "Norwegian", "Danish", "Finnish", "Greek", "Hebrew", "Czech", "Hungarian"
        };

        return languageNames.Select(name => new LanguageEntity
        {
            Name = name,
            LastUpdate = lastUpdateDate
        }).ToList();
    }

    private static List<ActorEntity> GenerateActors(DateTime lastUpdateDate)
    {
        var firstNames = new[]
        {
            "James", "Robert", "John", "Michael", "William", "David", "Richard", "Joseph", "Thomas", "Chris",
            "Charles", "Daniel", "Matthew", "Anthony", "Mark", "Donald", "Steven", "Paul", "Andrew", "Joshua",
            "Mary", "Patricia", "Jennifer", "Linda", "Elizabeth", "Barbara", "Susan", "Jessica", "Sarah", "Karen"
        };

        var lastNames = new[]
        {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
            "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin"
        };

        var actors = new List<ActorEntity>();
        int actorIndex = 0;

        for (int i = 0; i < 2000; i++)
        {
            actors.Add(new ActorEntity
            {
                FirstName = firstNames[actorIndex % firstNames.Length],
                LastName = lastNames[(actorIndex / firstNames.Length) % lastNames.Length],
                LastUpdate = lastUpdateDate
            });
            actorIndex++;
        }

        return actors;
    }

    private static List<CategoryEntity> GenerateCategories(DateTime lastUpdateDate)
    {
        var categoryNames = new[]
        {
            "Action", "Adventure", "Animation", "Biography", "Comedy", "Crime", "Documentary", "Drama", "Family", "Fantasy",
            "Film-Noir", "History", "Horror", "Music", "Musical", "Mystery", "Romance", "Sci-Fi", "Sport", "Thriller",
            "War", "Western", "Superhero", "Psychological", "Neo-Noir", "Heist", "Martial Arts", "Disaster", "Spy", "Zombie"
        };

        return categoryNames.Select(name => new CategoryEntity
        {
            Name = name,
            LastUpdated = lastUpdateDate
        }).ToList();
    }

    private static List<FilmEntity> GenerateFilms(List<LanguageEntity> languages, DateTime lastUpdateDate)
    {
        var titles = new[]
        {
            "Shattered Mirror", "Echoes Tomorrow", "Midnight Paradise", "Last Guardian", "Whispers Dark",
            "City Dreams", "Forgotten Path", "Shadows Past", "Beyond Horizon", "Silent Storm",
            "Dancing Destiny", "Crimson Tide", "Secrets Heart", "Golden Hour", "Beneath Surface",
            "Wandering Soul", "Flames Passion", "Hidden Truth", "Voices Beyond", "Eternal Journey"
        };

        var descriptions = new[]
        {
            "A gripping tale of love, loss, and redemption.",
            "An epic adventure that spans generations.",
            "A psychological thriller that keeps you guessing.",
            "A heartwarming story about family and friendship.",
            "A dark journey into the mind of a complex character.",
            "An action-packed adventure with stunning visuals.",
            "A romantic drama exploring modern relationships.",
            "A science fiction epic pushing imagination.",
            "A historical drama bringing the past to life.",
            "A comedy that will have you laughing."
        };

        var films = new List<FilmEntity>();
        int filmIndex = 0;

        for (int i = 0; i < 500; i++)
        {
            var languageIndex = filmIndex % languages.Count;
            var originalLanguageIndex = (filmIndex + 1) % languages.Count;
            var titleIndex = filmIndex % titles.Length;
            var descriptionIndex = filmIndex % descriptions.Length;

            films.Add(new FilmEntity
            {
                Title = titles[titleIndex] + (i >= titles.Length ? $" {i / titles.Length + 1}" : ""),
                Description = descriptions[descriptionIndex],
                ReleaseYear = 1950 + (filmIndex % 75),
                Language = languages[languageIndex],
                OriginalLanguage = languages[originalLanguageIndex],
                RentalDuration = 3 + (filmIndex % 28),
                RentalRate = Math.Round(0.99M + (filmIndex % 1500) / 100M, 2),
                Length = 80 + (filmIndex % 160),
                ReplacementCost = Math.Round(9.99M + (filmIndex % 4000) / 100M, 2),
                Rating = (MpaaRating)(filmIndex % 5),
                LastUpdate = lastUpdateDate,
                SpecialFeatures = GenerateSpecialFeatures(filmIndex),
                FullText = GenerateFullText(filmIndex),
                RevenueProjection = Math.Round((filmIndex % 1000M) + 100M, 2)
            });

            filmIndex++;
        }

        return films;
    }

    private static string[] GenerateSpecialFeatures(int index)
    {
        var features = new[] { "Trailers", "Commentaries", "Deleted Scenes", "Behind Scenes" };
        var count = (index % 3) + 1;
        return features.Take(count).ToArray();
    }

    private static string GenerateFullText(int index)
    {
        var quotes = new[]
        {
            "Life is what happens.",
            "Do great work.",
            "Remember friends.",
            "Believe in dreams.",
            "Focus on light.",
            "Continue with courage.",
            "Begin the journey.",
            "Find inner summer.",
            "Be yourself.",
            "Universe is infinite."
        };

        return quotes[index % quotes.Length];
    }

    private static List<FilmActorEntity> GenerateFilmActors(List<FilmEntity> films, List<ActorEntity> actors, DateTime lastUpdateDate)
    {
        var filmActors = new List<FilmActorEntity>();
        int actorIndex = 0;

        foreach (var film in films)
        {
            var actorCount = 2 + (film.Id % 6);
            
            for (int i = 0; i < actorCount; i++)
            {
                var actor = actors[actorIndex % actors.Count];
                filmActors.Add(new FilmActorEntity
                {
                    ActorId = actor.Id,
                    Actor = actor,
                    FilmId = film.Id,
                    Film = film,
                    LastUpdate = lastUpdateDate
                });
                actorIndex++;
            }
        }

        return filmActors;
    }

    private static List<FilmCategoryEntity> GenerateFilmCategories(List<FilmEntity> films, List<CategoryEntity> categories)
    {
        var filmCategories = new List<FilmCategoryEntity>();
        int categoryIndex = 0;

        foreach (var film in films)
        {
            var categoryCount = 1 + (film.Id % 3);
            
            for (int i = 0; i < categoryCount; i++)
            {
                var category = categories[categoryIndex % categories.Count];
                filmCategories.Add(new FilmCategoryEntity
                {
                    Film = film,
                    Category = category
                });
                categoryIndex++;
            }
        }

        return filmCategories;
    }

    private static List<StoreEntity> GenerateStores(List<AddressEntity> addresses, DateTime lastUpdateDate)
    {
        var stores = new List<StoreEntity>();
        
        for (int i = 0; i < 100; i++)
        {
            stores.Add(new StoreEntity
            {
                Address = addresses[i % addresses.Count],
                LastUpdate = lastUpdateDate
            });
        }

        return stores;
    }

    private static List<StaffEntity> GenerateStaff(List<StoreEntity> stores, List<AddressEntity> addresses, DateTime lastUpdateDate)
    {
        var staff = new List<StaffEntity>();
        var firstNames = new[] { "John", "Jane", "Mike", "Sarah", "David", "Emma", "Bob", "Lisa", "Jim", "Maria" };
        var lastNames = new[] { "Smith", "Johnson", "Brown", "Davis", "Wilson", "Miller", "Moore", "Taylor", "Anderson", "Thomas" };
        
        int staffIndex = 0;

        foreach (var store in stores)
        {
            var staffCount = 2 + (store.Id % 4);
            
            for (int i = 0; i < staffCount; i++)
            {
                var firstName = firstNames[staffIndex % firstNames.Length];
                var lastName = lastNames[(staffIndex / firstNames.Length) % lastNames.Length];
                
                staff.Add(new StaffEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Address = addresses[staffIndex % addresses.Count],
                    Email = $"{firstName.ToLower()}.{lastName.ToLower()}{staffIndex}@test.com",
                    Store = store,
                    IsActive = staffIndex % 10 != 0,
                    Username = $"{firstName.ToLower()}{staffIndex}",
                    Password = GenerateHash(staffIndex),
                    LastUpdate = lastUpdateDate,
                    Picture = null
                });
                
                staffIndex++;
            }
        }

        return staff;
    }

    private static List<InventoryEntity> GenerateInventories(List<StoreEntity> stores, List<FilmEntity> films, DateTime lastUpdateDate)
    {
        var inventories = new List<InventoryEntity>();
        int inventoryIndex = 0;

        foreach (var store in stores)
        {
            var filmsInStore = films.Skip((store.Id % 10) * 20).Take(100 + (store.Id % 50));
            
            foreach (var film in filmsInStore)
            {
                var copies = 1 + (inventoryIndex % 7);
                
                for (int i = 0; i < copies; i++)
                {
                    inventories.Add(new InventoryEntity
                    {
                        Film = film,
                        Store = store,
                        LastUpdate = lastUpdateDate
                    });
                }
                
                inventoryIndex++;
            }
        }

        return inventories;
    }

    private static List<CustomerEntity> GenerateCustomers(List<StoreEntity> stores, List<AddressEntity> addresses, DateTime lastUpdateDate)
    {
        var customers = new List<CustomerEntity>();
        var firstNames = new[] { "Alice", "Bob", "Charlie", "Diana", "Ed", "Fiona", "George", "Hannah", "Ian", "Julia" };
        var lastNames = new[] { "Adams", "Baker", "Clark", "Davis", "Evans", "Fisher", "Green", "Harris", "Jackson", "King" };
        
        int customerIndex = 0;

        for (int i = 0; i < 10000; i++)
        {
            var firstName = firstNames[customerIndex % firstNames.Length];
            var lastName = lastNames[(customerIndex / firstNames.Length) % lastNames.Length];
            var store = stores[customerIndex % stores.Count];
            var createDate = lastUpdateDate.AddDays(-(customerIndex % 1095));
            
            customers.Add(new CustomerEntity
            {
                Store = store,
                FirstName = firstName,
                LastName = lastName,
                Email = $"{firstName.ToLower()}.{lastName.ToLower()}{customerIndex}@test.com",
                Address = addresses[customerIndex % addresses.Count],
                IsActive = customerIndex % 20 != 0,
                CreateDate = createDate,
                LastUpdate = createDate.AddDays(customerIndex % 100)
            });
            
            customerIndex++;
        }

        return customers;
    }

    private static List<RentalEntity> GenerateRentals(List<CustomerEntity> customers, List<InventoryEntity> inventories, List<StaffEntity> staff, DateTime lastUpdateDate)
    {
        var rentals = new List<RentalEntity>();
        int rentalIndex = 0;

        for (int i = 0; i < 50000; i++)
        {
            var customer = customers[rentalIndex % customers.Count];
            var availableInventories = inventories.Where(inv => inv.Store == customer.Store).ToList();
            
            if (availableInventories.Any())
            {
                var inventory = availableInventories[rentalIndex % availableInventories.Count];
                var availableStaff = staff.Where(s => s.Store == customer.Store && s.IsActive).ToList();
                
                if (availableStaff.Any())
                {
                    var staffMember = availableStaff[rentalIndex % availableStaff.Count];
                    var rentalStart = lastUpdateDate.AddDays(-(rentalIndex % 730));
                    var isReturned = rentalIndex % 5 != 0;

                    rentals.Add(new RentalEntity
                    {
                        Inventory = inventory,
                        Customer = customer,
                        Staff = staffMember,
                        LastUpdate = lastUpdateDate,
                        RentalStart = rentalStart,
                        RentalEnd = isReturned ? rentalStart.AddDays((rentalIndex % 45) + 1) : null
                    });
                }
            }
            
            rentalIndex++;
        }

        return rentals;
    }

    private static string GeneratePostCode(string country, int index)
    {
        return country switch
        {
            "Poland" => $"{10 + (index % 89)}-{100 + (index % 899)}",
            "USA" => $"{10000 + (index % 89999)}",
            "Germany" => $"{10000 + (index % 89999)}",
            "UK" => $"SW{1 + (index % 99)}",
            _ => $"{10000 + (index % 89999)}"
        };
    }

    private static string GeneratePhoneNumber(string country, int index)
    {
        return country switch
        {
            "Poland" => $"{100000000 + (index % 899999999)}",
            "USA" => $"{2125550000 + (index % 9999)}",
            "Germany" => $"{491234567 + (index % 999999)}",
            _ => $"{100000000 + (index % 899999999)}"
        };
    }

    private static string GenerateHash(int index)
    {
        const string chars = "abcdef0123456789";
        var hash = "";
        var temp = index;
        
        for (int i = 0; i < 32; i++)
        {
            hash += chars[temp % chars.Length];
            temp = (temp / chars.Length) + i;
        }
        
        return hash;
    }
}