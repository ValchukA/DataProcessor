﻿// <auto-generated />
using DataProcessor.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataProcessor.Database.Migrations
{
    [DbContext(typeof(DataProcessorDbContext))]
    [Migration("20240228001835_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("DataProcessor.Database.Entities.DeviceStatusEntity", b =>
                {
                    b.Property<string>("ModuleCategoryId")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("ModuleState")
                        .HasColumnType("INTEGER");

                    b.HasKey("ModuleCategoryId");

                    b.ToTable("DeviceStatuses");
                });
#pragma warning restore 612, 618
        }
    }
}
