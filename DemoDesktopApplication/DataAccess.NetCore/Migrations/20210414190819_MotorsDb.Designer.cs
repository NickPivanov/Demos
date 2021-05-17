﻿// <auto-generated />
using System;
using DataAccess.NetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.NetCore.Migrations
{
    [DbContext(typeof(MotorDbContext))]
    [Migration("20210414190819_MotorsDb")]
    partial class MotorsDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Models.Characteristic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MotorId")
                        .HasColumnType("int");

                    b.Property<int>("MotorPropertyId")
                        .HasColumnType("int");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MotorId");

                    b.HasIndex("MotorPropertyId");

                    b.ToTable("Characteristics");
                });

            modelBuilder.Entity("Domain.Models.Measurement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MotorId")
                        .HasColumnType("int");

                    b.Property<int>("MotorPropertyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MotorId");

                    b.HasIndex("MotorPropertyId");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("Domain.Models.Motor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Motors");
                });

            modelBuilder.Entity("Domain.Models.MotorProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MotorType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MotorProperties");
                });

            modelBuilder.Entity("Domain.Models.Characteristic", b =>
                {
                    b.HasOne("Domain.Models.Motor", "Motor")
                        .WithMany("Characteristics")
                        .HasForeignKey("MotorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.MotorProperty", "MotorProperty")
                        .WithMany()
                        .HasForeignKey("MotorPropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Motor");

                    b.Navigation("MotorProperty");
                });

            modelBuilder.Entity("Domain.Models.Measurement", b =>
                {
                    b.HasOne("Domain.Models.Motor", "Motor")
                        .WithMany("MeasurementsLog")
                        .HasForeignKey("MotorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.MotorProperty", "MotorProperty")
                        .WithMany()
                        .HasForeignKey("MotorPropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Motor");

                    b.Navigation("MotorProperty");
                });

            modelBuilder.Entity("Domain.Models.Motor", b =>
                {
                    b.Navigation("Characteristics");

                    b.Navigation("MeasurementsLog");
                });
#pragma warning restore 612, 618
        }
    }
}
