﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OrmPerf.Persistence;

#nullable disable

namespace OrmPerf.Persistence.Migrations
{
    [DbContext(typeof(DbContext))]
    partial class DbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OrmPerf.Persistence.Entities.ActorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("LastName");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.AddressEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Address2")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("CityId")
                        .HasColumnType("integer");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("Phone");

                    b.HasIndex("Address", "Address2");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.CategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.CityEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.CountryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.CustomerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("StoreId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("LastName");

                    b.HasIndex("StoreId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.FilmActorEntity", b =>
                {
                    b.Property<int>("ActorId")
                        .HasColumnType("integer");

                    b.Property<int>("FilmId")
                        .HasColumnType("integer");

                    b.Property<int>("ActorId1")
                        .HasColumnType("integer");

                    b.Property<int>("FilmId1")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ActorId", "FilmId");

                    b.HasIndex("ActorId1");

                    b.HasIndex("FilmId");

                    b.HasIndex("FilmId1");

                    b.ToTable("FilmsActors");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.FilmCategoryEntity", b =>
                {
                    b.Property<int>("FilmId")
                        .HasColumnType("integer");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.HasKey("FilmId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("FilmsCategories");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.FilmEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("FullText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("Length")
                        .HasColumnType("integer");

                    b.Property<int>("OriginalLanguageId")
                        .HasColumnType("integer");

                    b.Property<int?>("Rating")
                        .HasColumnType("integer");

                    b.Property<int?>("ReleaseYear")
                        .HasColumnType("integer");

                    b.Property<int>("RentalDuration")
                        .HasColumnType("integer");

                    b.Property<decimal>("RentalRate")
                        .HasPrecision(4, 2)
                        .HasColumnType("numeric(4,2)");

                    b.Property<decimal>("ReplacementCost")
                        .HasPrecision(5, 2)
                        .HasColumnType("numeric(5,2)");

                    b.Property<decimal?>("RevenueProjection")
                        .HasPrecision(5, 2)
                        .HasColumnType("numeric(5,2)");

                    b.PrimitiveCollection<string[]>("SpecialFeatures")
                        .HasColumnType("text[]");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("FullText");

                    b.HasIndex("LanguageId");

                    b.HasIndex("OriginalLanguageId");

                    b.HasIndex("Title");

                    b.ToTable("Films");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.InventoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FilmId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("StoreId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FilmId");

                    b.HasIndex("StoreId", "FilmId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.LanguageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.PaymentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<int>("PaymentId")
                        .HasColumnType("integer");

                    b.Property<int>("RentalId")
                        .HasColumnType("integer");

                    b.Property<int>("StaffId")
                        .HasColumnType("integer");

                    b.Property<decimal>("amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("paymentDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("RentalId")
                        .IsUnique();

                    b.HasIndex("StaffId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.RentalEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<int>("InventoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("RentalEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("RentalStart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("StaffId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("InventoryId");

                    b.HasIndex("StaffId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.StaffEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Password")
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<byte[]>("Picture")
                        .HasColumnType("bytea");

                    b.Property<int>("StoreId")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("StoreId");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.StoreEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("StaffEntityId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("StaffEntityId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.AddressEntity", b =>
                {
                    b.HasOne("OrmPerf.Persistence.Entities.CityEntity", "City")
                        .WithMany("Addresses")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.CityEntity", b =>
                {
                    b.HasOne("OrmPerf.Persistence.Entities.CountryEntity", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.CustomerEntity", b =>
                {
                    b.HasOne("OrmPerf.Persistence.Entities.AddressEntity", "Address")
                        .WithMany("Customers")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrmPerf.Persistence.Entities.StoreEntity", "Store")
                        .WithMany("Customers")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.FilmActorEntity", b =>
                {
                    b.HasOne("OrmPerf.Persistence.Entities.ActorEntity", null)
                        .WithMany("Films")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrmPerf.Persistence.Entities.ActorEntity", "Actor")
                        .WithMany()
                        .HasForeignKey("ActorId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrmPerf.Persistence.Entities.FilmEntity", null)
                        .WithMany("Actors")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrmPerf.Persistence.Entities.FilmEntity", "Film")
                        .WithMany()
                        .HasForeignKey("FilmId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Film");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.FilmCategoryEntity", b =>
                {
                    b.HasOne("OrmPerf.Persistence.Entities.CategoryEntity", "Category")
                        .WithMany("Films")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrmPerf.Persistence.Entities.FilmEntity", "Film")
                        .WithMany("Categories")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Film");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.FilmEntity", b =>
                {
                    b.HasOne("OrmPerf.Persistence.Entities.LanguageEntity", "Language")
                        .WithMany("TranslatedFilms")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrmPerf.Persistence.Entities.LanguageEntity", "OriginalLanguage")
                        .WithMany("OriginalFilms")
                        .HasForeignKey("OriginalLanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("OriginalLanguage");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.InventoryEntity", b =>
                {
                    b.HasOne("OrmPerf.Persistence.Entities.FilmEntity", "Film")
                        .WithMany("Inventories")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrmPerf.Persistence.Entities.StoreEntity", "Store")
                        .WithMany("Inventories")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.PaymentEntity", b =>
                {
                    b.HasOne("OrmPerf.Persistence.Entities.CustomerEntity", "Customer")
                        .WithMany("Payments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrmPerf.Persistence.Entities.RentalEntity", "rental")
                        .WithOne("Payment")
                        .HasForeignKey("OrmPerf.Persistence.Entities.PaymentEntity", "RentalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrmPerf.Persistence.Entities.StaffEntity", "Staff")
                        .WithMany("Payments")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Staff");

                    b.Navigation("rental");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.RentalEntity", b =>
                {
                    b.HasOne("OrmPerf.Persistence.Entities.CustomerEntity", "Customer")
                        .WithMany("Rentals")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrmPerf.Persistence.Entities.InventoryEntity", "Inventory")
                        .WithMany("Rentals")
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrmPerf.Persistence.Entities.StaffEntity", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Inventory");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.StaffEntity", b =>
                {
                    b.HasOne("OrmPerf.Persistence.Entities.AddressEntity", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrmPerf.Persistence.Entities.StoreEntity", "Store")
                        .WithMany("Staff")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.StoreEntity", b =>
                {
                    b.HasOne("OrmPerf.Persistence.Entities.AddressEntity", "Address")
                        .WithMany("Stores")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrmPerf.Persistence.Entities.StaffEntity", null)
                        .WithMany("Stores")
                        .HasForeignKey("StaffEntityId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.ActorEntity", b =>
                {
                    b.Navigation("Films");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.AddressEntity", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("Stores");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.CategoryEntity", b =>
                {
                    b.Navigation("Films");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.CityEntity", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.CountryEntity", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.CustomerEntity", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.FilmEntity", b =>
                {
                    b.Navigation("Actors");

                    b.Navigation("Categories");

                    b.Navigation("Inventories");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.InventoryEntity", b =>
                {
                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.LanguageEntity", b =>
                {
                    b.Navigation("OriginalFilms");

                    b.Navigation("TranslatedFilms");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.RentalEntity", b =>
                {
                    b.Navigation("Payment")
                        .IsRequired();
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.StaffEntity", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("Stores");
                });

            modelBuilder.Entity("OrmPerf.Persistence.Entities.StoreEntity", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("Inventories");

                    b.Navigation("Staff");
                });
#pragma warning restore 612, 618
        }
    }
}
