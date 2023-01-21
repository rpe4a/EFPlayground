﻿// <auto-generated />
using System;
using EFDatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFDatabaseMigration.Migrations
{
    [DbContext(typeof(PlaygroundContext))]
    [Migration("20230121142320_InitializeDatabase")]
    partial class InitializeDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("EFDatabaseContext.Models.Company.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("create_at")
                        .HasDefaultValueSql("DATETIME('now')");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdateAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("TEXT")
                        .HasColumnName("update_at")
                        .HasDefaultValueSql("DATETIME('now')");

                    b.Property<long>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0L)
                        .HasColumnName("version");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Company", (string)null);
                });

            modelBuilder.Entity("EFDatabaseContext.Models.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("TEXT")
                        .HasColumnName("company_id");

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("create_at")
                        .HasDefaultValueSql("DATETIME('now')");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("login");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("password");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("TEXT")
                        .HasColumnName("profile_id");

                    b.Property<DateTime>("UpdateAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("TEXT")
                        .HasColumnName("update_at")
                        .HasDefaultValueSql("DATETIME('now')");

                    b.Property<long>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0L)
                        .HasColumnName("version");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("ProfileId")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("EFDatabaseContext.Models.UserProfile.UserProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<int>("Age")
                        .HasMaxLength(3)
                        .HasColumnType("INTEGER")
                        .HasColumnName("age");

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("create_at")
                        .HasDefaultValueSql("DATETIME('now')");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("update_at")
                        .HasDefaultValueSql("DATETIME('now')");

                    b.Property<long>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0L)
                        .HasColumnName("version");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Profile", null, t =>
                        {
                            t.HasCheckConstraint("Age", "Age > 0 AND Age < 150");
                        });
                });

            modelBuilder.Entity("EFDatabaseContext.Models.User.User", b =>
                {
                    b.HasOne("EFDatabaseContext.Models.Company.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("EFDatabaseContext.Models.UserProfile.UserProfile", "Profile")
                        .WithOne("User")
                        .HasForeignKey("EFDatabaseContext.Models.User.User", "ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("EFDatabaseContext.Models.UserProfile.UserProfile", b =>
                {
                    b.OwnsOne("EFDatabaseContext.Models.UserProfile.OfficialData", "OfficialData", b1 =>
                        {
                            b1.Property<Guid>("UserProfileId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Passport")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("TEXT")
                                .HasColumnName("passport");

                            b1.Property<string>("Phone")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("TEXT")
                                .HasColumnName("phone");

                            b1.HasKey("UserProfileId");

                            b1.HasIndex("Passport")
                                .IsUnique();

                            b1.HasIndex("Phone")
                                .IsUnique();

                            b1.ToTable("Profile");

                            b1.WithOwner()
                                .HasForeignKey("UserProfileId");
                        });

                    b.Navigation("OfficialData")
                        .IsRequired();
                });

            modelBuilder.Entity("EFDatabaseContext.Models.Company.Company", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("EFDatabaseContext.Models.UserProfile.UserProfile", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}