﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShootingWebAgent.SQLite;

namespace ShootingWebAgent.Migrations
{
    [DbContext(typeof(DataDbContext))]
    [Migration("20201025123034_IntialCreate")]
    partial class IntialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9");

            modelBuilder.Entity("ShootingWebAgent.DataModels.APIModel.Club", b =>
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

            modelBuilder.Entity("ShootingWebAgent.DataModels.APIModel.Competition", b =>
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

            modelBuilder.Entity("ShootingWebAgent.DataModels.APIModel.DisagJson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
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

                    b.ToTable("DisagJsons");
                });

            modelBuilder.Entity("ShootingWebAgent.DataModels.APIModel.MenuItem", b =>
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

            modelBuilder.Entity("ShootingWebAgent.DataModels.APIModel.Object", b =>
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

            modelBuilder.Entity("ShootingWebAgent.DataModels.APIModel.Shooter", b =>
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

            modelBuilder.Entity("ShootingWebAgent.DataModels.APIModel.Object", b =>
                {
                    b.HasOne("ShootingWebAgent.DataModels.APIModel.Competition", "Competition")
                        .WithMany()
                        .HasForeignKey("CompetitionId");

                    b.HasOne("ShootingWebAgent.DataModels.APIModel.DisagJson", null)
                        .WithMany("Objects")
                        .HasForeignKey("DisagJsonId");

                    b.HasOne("ShootingWebAgent.DataModels.APIModel.MenuItem", "MenuItem")
                        .WithMany()
                        .HasForeignKey("MenuItemId");

                    b.HasOne("ShootingWebAgent.DataModels.APIModel.Shooter", "Shooter")
                        .WithMany()
                        .HasForeignKey("ShooterId");
                });

            modelBuilder.Entity("ShootingWebAgent.DataModels.APIModel.Shooter", b =>
                {
                    b.HasOne("ShootingWebAgent.DataModels.APIModel.Club", "Club")
                        .WithMany()
                        .HasForeignKey("ClubId");
                });
#pragma warning restore 612, 618
        }
    }
}
