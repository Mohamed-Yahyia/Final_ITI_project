﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project_Api.DataContext;

#nullable disable

namespace Project_Api.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230704144336_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Project_Api.Models.Appointments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AppointDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AppointTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdEmp")
                        .HasColumnType("int");

                    b.Property<int?>("Idpatient")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("employeeId")
                        .HasColumnType("int");

                    b.Property<int?>("patientsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("employeeId");

                    b.HasIndex("patientsId");

                    b.ToTable("appointments");
                });

            modelBuilder.Entity("Project_Api.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Addreess")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("Project_Api.Models.Invoices", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int?>("IdEmp")
                        .HasColumnType("int");

                    b.Property<int?>("Idpatient")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("employeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("inviocDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("patientsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("employeeId");

                    b.HasIndex("patientsId");

                    b.ToTable("invoices");
                });

            modelBuilder.Entity("Project_Api.Models.MedicalRecords", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Diagnosis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdEmp")
                        .HasColumnType("int");

                    b.Property<int?>("Idpatient")
                        .HasColumnType("int");

                    b.Property<string>("Treatment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("VisitDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("employeeId")
                        .HasColumnType("int");

                    b.Property<int?>("patientsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("employeeId");

                    b.HasIndex("patientsId");

                    b.ToTable("records");
                });

            modelBuilder.Entity("Project_Api.Models.Patients", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("patients");
                });

            modelBuilder.Entity("Project_Api.Models.Reportscs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdEmp")
                        .HasColumnType("int");

                    b.Property<int?>("Idpatient")
                        .HasColumnType("int");

                    b.Property<DateTime>("RepDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RepType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("employeeId")
                        .HasColumnType("int");

                    b.Property<int?>("patientsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("employeeId");

                    b.HasIndex("patientsId");

                    b.ToTable("reportscs");
                });

            modelBuilder.Entity("Project_Api.Models.Appointments", b =>
                {
                    b.HasOne("Project_Api.Models.Employee", "employee")
                        .WithMany("appointments")
                        .HasForeignKey("employeeId");

                    b.HasOne("Project_Api.Models.Patients", "patients")
                        .WithMany("appointments")
                        .HasForeignKey("patientsId");

                    b.Navigation("employee");

                    b.Navigation("patients");
                });

            modelBuilder.Entity("Project_Api.Models.Invoices", b =>
                {
                    b.HasOne("Project_Api.Models.Employee", "employee")
                        .WithMany("invoices")
                        .HasForeignKey("employeeId");

                    b.HasOne("Project_Api.Models.Patients", "patients")
                        .WithMany("invoices")
                        .HasForeignKey("patientsId");

                    b.Navigation("employee");

                    b.Navigation("patients");
                });

            modelBuilder.Entity("Project_Api.Models.MedicalRecords", b =>
                {
                    b.HasOne("Project_Api.Models.Employee", "employee")
                        .WithMany("records")
                        .HasForeignKey("employeeId");

                    b.HasOne("Project_Api.Models.Patients", "patients")
                        .WithMany("records")
                        .HasForeignKey("patientsId");

                    b.Navigation("employee");

                    b.Navigation("patients");
                });

            modelBuilder.Entity("Project_Api.Models.Reportscs", b =>
                {
                    b.HasOne("Project_Api.Models.Employee", "employee")
                        .WithMany("reportscs")
                        .HasForeignKey("employeeId");

                    b.HasOne("Project_Api.Models.Patients", "patients")
                        .WithMany("reportscs")
                        .HasForeignKey("patientsId");

                    b.Navigation("employee");

                    b.Navigation("patients");
                });

            modelBuilder.Entity("Project_Api.Models.Employee", b =>
                {
                    b.Navigation("appointments");

                    b.Navigation("invoices");

                    b.Navigation("records");

                    b.Navigation("reportscs");
                });

            modelBuilder.Entity("Project_Api.Models.Patients", b =>
                {
                    b.Navigation("appointments");

                    b.Navigation("invoices");

                    b.Navigation("records");

                    b.Navigation("reportscs");
                });
#pragma warning restore 612, 618
        }
    }
}
