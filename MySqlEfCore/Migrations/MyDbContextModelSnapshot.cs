﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MySqlEfCore.Data;

namespace MySqlEfCore.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("MySqlEfCore.Models.Category", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MySqlEfCore.Models.LeaderboardEntry", b =>
                {
                    b.Property<int>("LeaderboardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("League")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("TeamCode")
                        .HasColumnType("text");

                    b.Property<string>("TeamName")
                        .HasColumnType("text");

                    b.HasKey("LeaderboardId");

                    b.ToTable("LeaderboardEntries");
                });

            modelBuilder.Entity("MySqlEfCore.Models.Player", b =>
                {
                    b.Property<byte[]>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<string>("PlayerName")
                        .HasColumnType("text");

                    b.HasKey("PlayerId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("MySqlEfCore.Models.Question", b =>
                {
                    b.Property<byte[]>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<string>("AnswerA")
                        .HasColumnType("text");

                    b.Property<string>("AnswerB")
                        .HasColumnType("text");

                    b.Property<string>("AnswerC")
                        .HasColumnType("text");

                    b.Property<string>("AnswerD")
                        .HasColumnType("text");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CorrectAnswer")
                        .HasColumnType("text");

                    b.Property<string>("QuestionText")
                        .HasColumnType("text");

                    b.Property<string>("QuestionType")
                        .HasColumnType("text");

                    b.HasKey("QuestionId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("MySqlEfCore.Models.QuizCategoryLength", b =>
                {
                    b.Property<byte[]>("QuizCategoryLengthId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfQuestions")
                        .HasColumnType("int");

                    b.HasKey("QuizCategoryLengthId");

                    b.ToTable("QuizCategoryLengths");
                });

            modelBuilder.Entity("MySqlEfCore.Models.QuizGame", b =>
                {
                    b.Property<byte[]>("QuizGameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<string>("AppId")
                        .HasColumnType("text");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CurrentQuestionPosition")
                        .HasColumnType("int");

                    b.Property<byte[]>("PlayerId")
                        .IsRequired()
                        .HasColumnType("varbinary(16)");

                    b.Property<int>("QuizGameLength")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("QuizGameId");

                    b.ToTable("QuizGames");
                });

            modelBuilder.Entity("MySqlEfCore.Models.QuizGameQuestion", b =>
                {
                    b.Property<byte[]>("QuizGameQuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<byte[]>("QuestionId")
                        .IsRequired()
                        .HasColumnType("varbinary(16)");

                    b.Property<int>("QuestionPosition")
                        .HasColumnType("int");

                    b.Property<byte[]>("QuizGameId")
                        .IsRequired()
                        .HasColumnType("varbinary(16)");

                    b.HasKey("QuizGameQuestionId");

                    b.ToTable("QuizGameQuestions");
                });
#pragma warning restore 612, 618
        }
    }
}
