﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SafeDriver.Domain.Data;

namespace SafeDriver.Domain.Migrations
{
    [DbContext(typeof(SafeDriverDbContext))]
    [Migration("20210911161013_AddDriverScore")]
    partial class AddDriverScore
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("SafeDriver.Domain.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("DriverId")
                        .HasColumnType("integer")
                        .HasColumnName("driver_id");

                    b.Property<DateTime>("EventDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("event_date_time");

                    b.Property<int>("EventType")
                        .HasColumnType("integer")
                        .HasColumnName("event_type");

                    b.Property<int>("GeneratedScore")
                        .HasColumnType("integer")
                        .HasColumnName("generated_score");

                    b.Property<decimal>("PrecisionLevel")
                        .HasColumnType("numeric")
                        .HasColumnName("precision_level");

                    b.HasKey("Id")
                        .HasName("pk_events");

                    b.HasIndex("DriverId")
                        .HasDatabaseName("ix_events_driver_id");

                    b.ToTable("events");
                });

            modelBuilder.Entity("SafeDriver.Domain.Entities.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("DriverId")
                        .HasColumnType("integer")
                        .HasColumnName("driver_id");

                    b.Property<TimeSpan>("TripDuration")
                        .HasColumnType("interval")
                        .HasColumnName("trip_duration");

                    b.HasKey("Id")
                        .HasName("pk_trips");

                    b.HasIndex("DriverId")
                        .HasDatabaseName("ix_trips_driver_id");

                    b.ToTable("trips");
                });

            modelBuilder.Entity("SafeDriver.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("text")
                        .HasColumnName("email_address");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("SafeDriver.Domain.Entities.Driver", b =>
                {
                    b.HasBaseType("SafeDriver.Domain.Entities.User");

                    b.Property<string>("AutomotiveInsuranceProvider")
                        .HasColumnType("text")
                        .HasColumnName("automotive_insurance_provider");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("birth_date");

                    b.Property<string>("DocumentNumber")
                        .HasColumnType("text")
                        .HasColumnName("document_number");

                    b.Property<DateTime>("DriverLicenseExpireDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("driver_license_expire_date");

                    b.Property<string>("DriverUUID")
                        .HasColumnType("text")
                        .HasColumnName("driver_uuid");

                    b.Property<string>("DriversLicenseNumber")
                        .HasColumnType("text")
                        .HasColumnName("drivers_license_number");

                    b.Property<bool>("IsProfessionalDriver")
                        .HasColumnType("boolean")
                        .HasColumnName("is_professional_driver");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Score")
                        .HasColumnType("integer")
                        .HasColumnName("score");

                    b.ToTable("drivers");
                });

            modelBuilder.Entity("SafeDriver.Domain.Entities.Event", b =>
                {
                    b.HasOne("SafeDriver.Domain.Entities.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .HasConstraintName("fk_events_users_driver_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SafeDriver.Domain.ValueObjects.Coordinate", "Coordinates", b1 =>
                        {
                            b1.Property<int>("EventId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasColumnName("id")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                            b1.Property<double>("Latitude")
                                .HasColumnType("double precision")
                                .HasColumnName("coordinates_latitude");

                            b1.Property<double>("Longitude")
                                .HasColumnType("double precision")
                                .HasColumnName("coordinates_longitude");

                            b1.HasKey("EventId")
                                .HasName("pk_events");

                            b1.ToTable("events");

                            b1.WithOwner()
                                .HasForeignKey("EventId")
                                .HasConstraintName("fk_events_events_id");
                        });

                    b.Navigation("Coordinates");

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("SafeDriver.Domain.Entities.Trip", b =>
                {
                    b.HasOne("SafeDriver.Domain.Entities.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .HasConstraintName("fk_trips_users_driver_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SafeDriver.Domain.ValueObjects.Coordinate", "FinalCoordinates", b1 =>
                        {
                            b1.Property<int>("TripId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasColumnName("id")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                            b1.Property<double>("Latitude")
                                .HasColumnType("double precision")
                                .HasColumnName("final_coordinates_latitude");

                            b1.Property<double>("Longitude")
                                .HasColumnType("double precision")
                                .HasColumnName("final_coordinates_longitude");

                            b1.HasKey("TripId")
                                .HasName("pk_trips");

                            b1.ToTable("trips");

                            b1.WithOwner()
                                .HasForeignKey("TripId")
                                .HasConstraintName("fk_trips_trips_id");
                        });

                    b.OwnsOne("SafeDriver.Domain.ValueObjects.Coordinate", "StartingCoordinates", b1 =>
                        {
                            b1.Property<int>("TripId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasColumnName("id")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                            b1.Property<double>("Latitude")
                                .HasColumnType("double precision")
                                .HasColumnName("starting_coordinates_latitude");

                            b1.Property<double>("Longitude")
                                .HasColumnType("double precision")
                                .HasColumnName("starting_coordinates_longitude");

                            b1.HasKey("TripId")
                                .HasName("pk_trips");

                            b1.ToTable("trips");

                            b1.WithOwner()
                                .HasForeignKey("TripId")
                                .HasConstraintName("fk_trips_trips_id");
                        });

                    b.Navigation("Driver");

                    b.Navigation("FinalCoordinates");

                    b.Navigation("StartingCoordinates");
                });

            modelBuilder.Entity("SafeDriver.Domain.Entities.Driver", b =>
                {
                    b.HasOne("SafeDriver.Domain.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("SafeDriver.Domain.Entities.Driver", "Id")
                        .HasConstraintName("fk_drivers_users_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
