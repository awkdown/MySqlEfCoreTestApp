﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MySqlEfCore.Data;

namespace MySqlEfCore.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20221002164401_second")]
    partial class second
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MySqlEfCore.Data.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<ulong>("IsPlayer")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("person");
                });
#pragma warning restore 612, 618
        }
    }
}
