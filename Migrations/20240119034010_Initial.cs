using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Mailid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employeeinformation",
                columns: table => new
                {
                    Employeeid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    JoiningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeinformation", x => x.Employeeid);
                });

            migrationBuilder.CreateTable(
                name: "RequestLeavetbl",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberofDates = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLeavetbl", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RoleSalaries",
                columns: table => new
                {
                    Roleid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    BasicPay = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleSalaries", x => x.Roleid);
                });

            migrationBuilder.CreateTable(
                name: "TraceActivity",
                columns: table => new
                {
                    traceid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Controllername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Actionname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraceActivity", x => x.traceid);
                });

            migrationBuilder.CreateTable(
                name: "Salarydata",
                columns: table => new
                {
                    Salaryid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employeeid = table.Column<int>(type: "int", nullable: false),
                    Rolename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bonus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravellAllowance = table.Column<float>(type: "real", nullable: false),
                    MedicalAllowance = table.Column<float>(type: "real", nullable: false),
                    GrossSalary = table.Column<float>(type: "real", nullable: false),
                    BasicPay = table.Column<float>(type: "real", nullable: false),
                    PF = table.Column<float>(type: "real", nullable: false),
                    ESI = table.Column<float>(type: "real", nullable: false),
                    Tax = table.Column<float>(type: "real", nullable: false),
                    NetPay = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salarydata", x => x.Salaryid);
                    table.ForeignKey(
                        name: "FK_Salarydata_employeeinformation_Employeeid",
                        column: x => x.Employeeid,
                        principalTable: "employeeinformation",
                        principalColumn: "Employeeid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Salarydata_Employeeid",
                table: "Salarydata",
                column: "Employeeid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Complaints");

            migrationBuilder.DropTable(
                name: "RequestLeavetbl");

            migrationBuilder.DropTable(
                name: "RoleSalaries");

            migrationBuilder.DropTable(
                name: "Salarydata");

            migrationBuilder.DropTable(
                name: "TraceActivity");

            migrationBuilder.DropTable(
                name: "employeeinformation");
        }
    }
}
