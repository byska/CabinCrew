using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CabinCrew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CabinAttendants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KnownLanguages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttendantType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recipes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleRestrictions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinAttendants", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CabinAttendants");
        }
    }
}
