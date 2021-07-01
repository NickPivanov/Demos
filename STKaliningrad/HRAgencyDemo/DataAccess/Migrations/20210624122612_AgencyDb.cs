using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AgencyDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LeadId = table.Column<int>(type: "INTEGER", nullable: true),
                    EmployedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Group = table.Column<int>(type: "INTEGER", nullable: false),
                    SalaryBase = table.Column<double>(type: "REAL", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    ManagerId = table.Column<int>(type: "INTEGER", nullable: true),
                    SalesmanId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBase_EmployeeBase_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "EmployeeBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeBase_EmployeeBase_SalesmanId",
                        column: x => x.SalesmanId,
                        principalTable: "EmployeeBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonelExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: true),
                    CurrentSalary = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonelExpenses_EmployeeBase_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBase_ManagerId",
                table: "EmployeeBase",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBase_SalesmanId",
                table: "EmployeeBase",
                column: "SalesmanId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelExpenses_EmployeeId",
                table: "PersonelExpenses",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonelExpenses");

            migrationBuilder.DropTable(
                name: "EmployeeBase");
        }
    }
}
