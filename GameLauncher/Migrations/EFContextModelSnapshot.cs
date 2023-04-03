﻿// <auto-generated />
using System;
using GameLauncher;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameLauncher.Migrations
{
    [DbContext(typeof(EFContext))]
    partial class EFContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GameLauncher.Entities.Content", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CostCoins")
                        .HasColumnType("int");

                    b.Property<double>("CostReal")
                        .HasColumnType("float");

                    b.Property<DateTime?>("DeleteDt")
                        .HasColumnType("datetime2");

                    b.Property<string>("InstallationFolder")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstallationLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TransactionId");

                    b.ToTable("Contents", t =>
                        {
                            t.HasCheckConstraint("CK_Contents_Type_Enum", "[Type] IN (0, 1)");
                        });
                });

            modelBuilder.Entity("GameLauncher.Entities.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AvatarLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CoinsCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeleteDt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Players");
                });

            modelBuilder.Entity("GameLauncher.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PayerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PayerId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("GameLauncher.Entities.Content", b =>
                {
                    b.HasOne("GameLauncher.Entities.Player", null)
                        .WithMany("UnlockedContent")
                        .HasForeignKey("PlayerId");

                    b.HasOne("GameLauncher.Entities.Transaction", null)
                        .WithMany("Contents")
                        .HasForeignKey("TransactionId");
                });

            modelBuilder.Entity("GameLauncher.Entities.Transaction", b =>
                {
                    b.HasOne("GameLauncher.Entities.Player", "Payer")
                        .WithMany("Transactions")
                        .HasForeignKey("PayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payer");
                });

            modelBuilder.Entity("GameLauncher.Entities.Player", b =>
                {
                    b.Navigation("Transactions");

                    b.Navigation("UnlockedContent");
                });

            modelBuilder.Entity("GameLauncher.Entities.Transaction", b =>
                {
                    b.Navigation("Contents");
                });
#pragma warning restore 612, 618
        }
    }
}