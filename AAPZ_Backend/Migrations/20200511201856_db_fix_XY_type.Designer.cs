﻿// <auto-generated />
using System;
using AAPZ_Backend;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AAPZ_Backend.Migrations
{
    [DbContext(typeof(SheringDBContext))]
    [Migration("20200511201856_db_fix_XY_type")]
    partial class db_fix_XY_type
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-preview3-35497")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AAPZ_Backend.Models.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Flat");

                    b.Property<string>("House")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int?>("LandlordId");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<double?>("X");

                    b.Property<double?>("Y");

                    b.HasKey("Id");

                    b.HasIndex("LandlordId");

                    b.ToTable("Building");
                });

            modelBuilder.Entity("AAPZ_Backend.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("date");

                    b.Property<int?>("ChairHight");

                    b.Property<string>("Drink");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int?>("Hight");

                    b.Property<string>("IdentityId");

                    b.Property<int>("IsInBlackList");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<int?>("Light");

                    b.Property<string>("Music");

                    b.Property<long>("PassportNumber");

                    b.Property<string>("Phone");

                    b.Property<int?>("Sale");

                    b.Property<int?>("TableHight");

                    b.Property<int?>("Temperature");

                    b.Property<int?>("Vision");

                    b.HasKey("Id");

                    b.HasIndex("IdentityId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("AAPZ_Backend.Models.Distance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId");

                    b.Property<DateTime>("Date");

                    b.Property<double>("DistanceValue");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Diastance");
                });

            modelBuilder.Entity("AAPZ_Backend.Models.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Equipment");
                });

            modelBuilder.Entity("AAPZ_Backend.Models.Landlord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("IdentityId");

                    b.Property<int>("IsInBlackList");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<long>("PassportNumber");

                    b.Property<long>("Phone");

                    b.HasKey("Id");

                    b.HasIndex("IdentityId");

                    b.ToTable("Landlord");
                });

            modelBuilder.Entity("AAPZ_Backend.Models.SearchSetting", b =>
                {
                    b.Property<int>("SearchSettingId");

                    b.Property<double>("Radius");

                    b.Property<double>("WantedCost");

                    b.HasKey("SearchSettingId");

                    b.ToTable("SearchSetting");
                });

            modelBuilder.Entity("AAPZ_Backend.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("AAPZ_Backend.Models.Workplace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BuildingId");

                    b.Property<int>("Cost");

                    b.Property<int?>("Mark");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("Workplace");
                });

            modelBuilder.Entity("AAPZ_Backend.Models.WorkplaceEquipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<int>("EquipmentId");

                    b.Property<int>("WorkplaceId");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentId");

                    b.HasIndex("WorkplaceId");

                    b.ToTable("WorkplaceEquipment");
                });

            modelBuilder.Entity("AAPZ_Backend.Models.WorkplaceOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId");

                    b.Property<DateTime>("FinishTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime");

                    b.Property<int>("SumToPay");

                    b.Property<int>("WorkplaceId");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("WorkplaceId");

                    b.ToTable("WorkplaceOrder");
                });

            modelBuilder.Entity("AAPZ_Backend.Models.WorkplaceParameter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId");

                    b.Property<int>("Count");

                    b.Property<int>("EquipmentId");

                    b.Property<int>("Priority");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("EquipmentId");

                    b.ToTable("WorkplaceParameter");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("AAPZ_Backend.Models.Building", b =>
                {
                    b.HasOne("AAPZ_Backend.Models.Landlord", "Landlord")
                        .WithMany("Building")
                        .HasForeignKey("LandlordId")
                        .HasConstraintName("FK_LandlordId");
                });

            modelBuilder.Entity("AAPZ_Backend.Models.Client", b =>
                {
                    b.HasOne("AAPZ_Backend.Models.User", "Identity")
                        .WithMany()
                        .HasForeignKey("IdentityId");
                });

            modelBuilder.Entity("AAPZ_Backend.Models.Distance", b =>
                {
                    b.HasOne("AAPZ_Backend.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AAPZ_Backend.Models.Landlord", b =>
                {
                    b.HasOne("AAPZ_Backend.Models.User", "Identity")
                        .WithMany()
                        .HasForeignKey("IdentityId");
                });

            modelBuilder.Entity("AAPZ_Backend.Models.SearchSetting", b =>
                {
                    b.HasOne("AAPZ_Backend.Models.Client", "Client")
                        .WithOne("SearchSetting")
                        .HasForeignKey("AAPZ_Backend.Models.SearchSetting", "SearchSettingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AAPZ_Backend.Models.Workplace", b =>
                {
                    b.HasOne("AAPZ_Backend.Models.Building", "Building")
                        .WithMany("Workplace")
                        .HasForeignKey("BuildingId")
                        .HasConstraintName("FK_BuildingId");
                });

            modelBuilder.Entity("AAPZ_Backend.Models.WorkplaceEquipment", b =>
                {
                    b.HasOne("AAPZ_Backend.Models.Equipment", "Equipment")
                        .WithMany("WorkplaceEquipment")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AAPZ_Backend.Models.Workplace", "Workplace")
                        .WithMany("WorkplaceEquipment")
                        .HasForeignKey("WorkplaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AAPZ_Backend.Models.WorkplaceOrder", b =>
                {
                    b.HasOne("AAPZ_Backend.Models.Client", "Client")
                        .WithMany("WorkplaceOrder")
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK_ClientId");

                    b.HasOne("AAPZ_Backend.Models.Workplace", "Workplace")
                        .WithMany("WorkplaceOrder")
                        .HasForeignKey("WorkplaceId")
                        .HasConstraintName("FK_WorkplaceId");
                });

            modelBuilder.Entity("AAPZ_Backend.Models.WorkplaceParameter", b =>
                {
                    b.HasOne("AAPZ_Backend.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AAPZ_Backend.Models.Equipment", "Equipment")
                        .WithMany()
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("AAPZ_Backend.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AAPZ_Backend.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AAPZ_Backend.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AAPZ_Backend.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
