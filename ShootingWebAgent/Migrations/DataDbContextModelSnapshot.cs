﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShootingWebAgent.SQLite;

namespace ShootingWebAgent.Migrations
{
    [DbContext(typeof(DataDbContext))]
    partial class DataDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10");

            modelBuilder.Entity("ShootingWebAgent.DataModels.Club", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("ShortName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UUID")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("ShootingWebAgent.DataModels.Competition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Evaluation")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("UUID")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Competition");
                });

            modelBuilder.Entity("ShootingWebAgent.DataModels.DisagJson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MatchId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MessageType")
                        .HasColumnType("TEXT");

                    b.Property<string>("MessageVerb")
                        .HasColumnType("TEXT");

                    b.Property<int>("Ranges")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Sequential")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UUID")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.ToTable("DisagJsons");
                });

            modelBuilder.Entity("ShootingWebAgent.DataModels.Match", b =>
                {
                    b.Property<int>("MatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("MatchName")
                        .HasColumnType("TEXT");

                    b.Property<int>("MatchStatus")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SessionCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShotsPerSession")
                        .HasColumnType("INTEGER");

                    b.HasKey("MatchId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("ShootingWebAgent.DataModels.MenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("MenuID")
                        .HasColumnType("TEXT");

                    b.Property<string>("MenuItemName")
                        .HasColumnType("TEXT");

                    b.Property<string>("MenuPointName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UUID")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("ShootingWebAgent.DataModels.Object", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CompetitionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<double>("DecValue")
                        .HasColumnType("REAL");

                    b.Property<double>("DecimalValue")
                        .HasColumnType("REAL");

                    b.Property<int?>("DisagJsonId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DiscType")
                        .HasColumnType("TEXT");

                    b.Property<double>("Distance")
                        .HasColumnType("REAL");

                    b.Property<int>("FullValue")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDummy")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsHot")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsInnerten")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsShootoff")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsValid")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsWarmup")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LastTLChange")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MenuItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Range")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Remark")
                        .HasColumnType("TEXT");

                    b.Property<int>("Run")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ShooterId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ShotDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Source")
                        .HasColumnType("TEXT");

                    b.Property<string>("TLStatus")
                        .HasColumnType("TEXT");

                    b.Property<string>("UUID")
                        .HasColumnType("TEXT");

                    b.Property<int>("X")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Y")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("DisagJsonId");

                    b.HasIndex("MenuItemId");

                    b.HasIndex("ShooterId");

                    b.ToTable("Objects");
                });

            modelBuilder.Entity("ShootingWebAgent.DataModels.Point", b =>
                {
                    b.Property<int>("PointId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("StatisticModelId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("x")
                        .HasColumnType("INTEGER");

                    b.Property<int>("y")
                        .HasColumnType("INTEGER");

                    b.HasKey("PointId");

                    b.HasIndex("StatisticModelId");

                    b.ToTable("Points");
                });

            modelBuilder.Entity("ShootingWebAgent.DataModels.Session", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("StatisticModelId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("value")
                        .HasColumnType("REAL");

                    b.HasKey("SessionId");

                    b.HasIndex("StatisticModelId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("ShootingWebAgent.DataModels.Shooter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Birthyear")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ClubId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Firstname")
                        .HasColumnType("TEXT");

                    b.Property<string>("Identification")
                        .HasColumnType("TEXT");

                    b.Property<string>("InternalID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Lastname")
                        .HasColumnType("TEXT");

                    b.Property<string>("Team")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.ToTable("Shooters");
                });

            modelBuilder.Entity("ShootingWebAgent.DataModels.StatisticModel", b =>
                {
                    b.Property<int>("StatisticModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<double>("DecValue")
                        .HasColumnType("REAL");

                    b.Property<double>("DecValueSum")
                        .HasColumnType("REAL");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<double>("HR")
                        .HasColumnType("REAL");

                    b.Property<int>("InternalCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InternalId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<int?>("MatchId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Range")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SessionCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShotsCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Team")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TeamName")
                        .HasColumnType("TEXT");

                    b.HasKey("StatisticModelId");

                    b.HasIndex("MatchId");

                    b.ToTable("StatisticModel");
                });

            modelBuilder.Entity("ShootingWebAgent.DataModels.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MatchId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TeamHashId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TeamName")
                        .HasColumnType("TEXT");

                    b.HasKey("TeamId");

                    b.HasIndex("MatchId");

                    b.HasIndex("TeamHashId")
                        .IsUnique();

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("ShootingWebAgent.DataModels.DisagJson", b =>
                {
                    b.HasOne("ShootingWebAgent.DataModels.Match", null)
                        .WithMany("DisagData")
                        .HasForeignKey("MatchId");
                });

            modelBuilder.Entity("ShootingWebAgent.DataModels.Object", b =>
                {
                    b.HasOne("ShootingWebAgent.DataModels.Competition", "Competition")
                        .WithMany()
                        .HasForeignKey("CompetitionId");

                    b.HasOne("ShootingWebAgent.DataModels.DisagJson", null)
                        .WithMany("Objects")
                        .HasForeignKey("DisagJsonId");

                    b.HasOne("ShootingWebAgent.DataModels.MenuItem", "MenuItem")
                        .WithMany()
                        .HasForeignKey("MenuItemId");

                    b.HasOne("ShootingWebAgent.DataModels.Shooter", "Shooter")
                        .WithMany()
                        .HasForeignKey("ShooterId");
                });

            modelBuilder.Entity("ShootingWebAgent.DataModels.Point", b =>
                {
                    b.HasOne("ShootingWebAgent.DataModels.StatisticModel", null)
                        .WithMany("Points")
                        .HasForeignKey("StatisticModelId");
                });

            modelBuilder.Entity("ShootingWebAgent.DataModels.Session", b =>
                {
                    b.HasOne("ShootingWebAgent.DataModels.StatisticModel", null)
                        .WithMany("Sessions")
                        .HasForeignKey("StatisticModelId");
                });

            modelBuilder.Entity("ShootingWebAgent.DataModels.Shooter", b =>
                {
                    b.HasOne("ShootingWebAgent.DataModels.Club", "Club")
                        .WithMany()
                        .HasForeignKey("ClubId");
                });

            modelBuilder.Entity("ShootingWebAgent.DataModels.StatisticModel", b =>
                {
                    b.HasOne("ShootingWebAgent.DataModels.Match", null)
                        .WithMany("StatisticModels")
                        .HasForeignKey("MatchId");
                });

            modelBuilder.Entity("ShootingWebAgent.DataModels.Team", b =>
                {
                    b.HasOne("ShootingWebAgent.DataModels.Match", null)
                        .WithMany("Teams")
                        .HasForeignKey("MatchId");
                });
#pragma warning restore 612, 618
        }
    }
}
