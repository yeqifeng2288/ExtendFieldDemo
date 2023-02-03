﻿// <auto-generated />
using ExtendFieldDemo.DatabaseAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExtendFieldDemo.Migrations.ExtendFieldDb
{
    [DbContext(typeof(ExtendFieldDbContext))]
    partial class ExtendFieldDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("ExtendFieldDemo.Models.ExtendFieldModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FieldName")
                        .HasColumnType("TEXT");

                    b.Property<int>("FieldType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ModelType")
                        .HasColumnType("TEXT");

                    b.Property<string>("TableName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ExtendFieldModels");
                });
#pragma warning restore 612, 618
        }
    }
}
