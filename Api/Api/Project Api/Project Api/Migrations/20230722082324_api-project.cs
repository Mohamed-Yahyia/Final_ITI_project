using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Api.Migrations
{
    /// <inheritdoc />
    public partial class apiproject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Addreess = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppointTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Idpatient = table.Column<int>(type: "int", nullable: true),
                    IdEmp = table.Column<int>(type: "int", nullable: true),
                    patientsId = table.Column<int>(type: "int", nullable: true),
                    employeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_appointments_employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_appointments_patients_patientsId",
                        column: x => x.patientsId,
                        principalTable: "patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    inviocDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Idpatient = table.Column<int>(type: "int", nullable: true),
                    IdEmp = table.Column<int>(type: "int", nullable: true),
                    patientsId = table.Column<int>(type: "int", nullable: true),
                    employeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_invoices_employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_invoices_patients_patientsId",
                        column: x => x.patientsId,
                        principalTable: "patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "operations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    operation_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Procedures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    attachments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    operation_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    operation_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Idpatient = table.Column<int>(type: "int", nullable: true),
                    IdEmp = table.Column<int>(type: "int", nullable: true),
                    patientsId = table.Column<int>(type: "int", nullable: true),
                    employeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_operations_employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_operations_patients_patientsId",
                        column: x => x.patientsId,
                        principalTable: "patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "records",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Treatment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Idpatient = table.Column<int>(type: "int", nullable: true),
                    IdEmp = table.Column<int>(type: "int", nullable: true),
                    patientsId = table.Column<int>(type: "int", nullable: true),
                    employeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_records", x => x.Id);
                    table.ForeignKey(
                        name: "FK_records_employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_records_patients_patientsId",
                        column: x => x.patientsId,
                        principalTable: "patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "reportscs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RepType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Idpatient = table.Column<int>(type: "int", nullable: true),
                    IdEmp = table.Column<int>(type: "int", nullable: true),
                    patientsId = table.Column<int>(type: "int", nullable: true),
                    employeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportscs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reportscs_employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_reportscs_patients_patientsId",
                        column: x => x.patientsId,
                        principalTable: "patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_employeeId",
                table: "appointments",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_patientsId",
                table: "appointments",
                column: "patientsId");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_employeeId",
                table: "invoices",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_patientsId",
                table: "invoices",
                column: "patientsId");

            migrationBuilder.CreateIndex(
                name: "IX_operations_employeeId",
                table: "operations",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_operations_patientsId",
                table: "operations",
                column: "patientsId");

            migrationBuilder.CreateIndex(
                name: "IX_records_employeeId",
                table: "records",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_records_patientsId",
                table: "records",
                column: "patientsId");

            migrationBuilder.CreateIndex(
                name: "IX_reportscs_employeeId",
                table: "reportscs",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_reportscs_patientsId",
                table: "reportscs",
                column: "patientsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "operations");

            migrationBuilder.DropTable(
                name: "records");

            migrationBuilder.DropTable(
                name: "reportscs");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "patients");
        }
    }
}
