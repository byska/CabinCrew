using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CabinCrew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "CabinAttendants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "CabinAttendants",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "CabinAttendants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CabinAttendants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "CabinAttendants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "CabinAttendants",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "CabinAttendants");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CabinAttendants");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "CabinAttendants");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CabinAttendants");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "CabinAttendants");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "CabinAttendants");
        }
    }
}
