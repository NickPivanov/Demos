﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(HRAgencyDBContext))]
    [Migration("20210624122612_AgencyDb")]
    partial class AgencyDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("DataAccess.Models.EmployeeBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EmployedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Group")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("LeadId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<double>("SalaryBase")
                        .HasColumnType("REAL");

                    b.Property<int?>("SalesmanId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.HasIndex("SalesmanId");

                    b.ToTable("EmployeeBase");

                    b.HasDiscriminator<string>("Discriminator").HasValue("EmployeeBase");
                });

            modelBuilder.Entity("DataAccess.Models.PersonelExpense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("CurrentSalary")
                        .HasColumnType("REAL");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("PersonelExpenses");
                });

            modelBuilder.Entity("DataAccess.Models.Employee", b =>
                {
                    b.HasBaseType("DataAccess.Models.EmployeeBase");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("DataAccess.Models.Manager", b =>
                {
                    b.HasBaseType("DataAccess.Models.EmployeeBase");

                    b.HasDiscriminator().HasValue("Manager");
                });

            modelBuilder.Entity("DataAccess.Models.Salesman", b =>
                {
                    b.HasBaseType("DataAccess.Models.EmployeeBase");

                    b.HasDiscriminator().HasValue("Salesman");
                });

            modelBuilder.Entity("DataAccess.Models.EmployeeBase", b =>
                {
                    b.HasOne("DataAccess.Models.Manager", null)
                        .WithMany("Subordinates")
                        .HasForeignKey("ManagerId");

                    b.HasOne("DataAccess.Models.Salesman", null)
                        .WithMany("Subordinates")
                        .HasForeignKey("SalesmanId");
                });

            modelBuilder.Entity("DataAccess.Models.PersonelExpense", b =>
                {
                    b.HasOne("DataAccess.Models.EmployeeBase", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("DataAccess.Models.Manager", b =>
                {
                    b.Navigation("Subordinates");
                });

            modelBuilder.Entity("DataAccess.Models.Salesman", b =>
                {
                    b.Navigation("Subordinates");
                });
#pragma warning restore 612, 618
        }
    }
}
