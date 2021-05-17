﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MotorAPI;

namespace MotorAPI.Migrations
{
    [DbContext(typeof(MotorDbContext))]
    partial class MotorDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MotorAPI.Models.Characteristic", b =>
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

            modelBuilder.Entity("MotorAPI.Models.Measurement", b =>
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

            modelBuilder.Entity("MotorAPI.Models.Motor", b =>
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

            modelBuilder.Entity("MotorAPI.Models.MotorProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MotorType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MotorProperties");
                });

            modelBuilder.Entity("MotorAPI.Models.Characteristic", b =>
                {
                    b.HasOne("MotorAPI.Models.Motor", "Motor")
                        .WithMany("Characteristics")
                        .HasForeignKey("MotorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotorAPI.Models.MotorProperty", "MotorProperty")
                        .WithMany()
                        .HasForeignKey("MotorPropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Motor");

                    b.Navigation("MotorProperty");
                });

            modelBuilder.Entity("MotorAPI.Models.Measurement", b =>
                {
                    b.HasOne("MotorAPI.Models.Motor", "Motor")
                        .WithMany("MeasurementsLog")
                        .HasForeignKey("MotorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MotorAPI.Models.MotorProperty", "MotorProperty")
                        .WithMany()
                        .HasForeignKey("MotorPropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Motor");

                    b.Navigation("MotorProperty");
                });

            modelBuilder.Entity("MotorAPI.Models.Motor", b =>
                {
                    b.Navigation("Characteristics");

                    b.Navigation("MeasurementsLog");
                });
#pragma warning restore 612, 618
        }
    }
}
