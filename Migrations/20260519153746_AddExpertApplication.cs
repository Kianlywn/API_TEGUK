using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teguk_API.Migrations
{
    /// <inheritdoc />
    public partial class AddExpertApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verified",
                table: "HealthExperts");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "HealthExperts",
                newName: "Status");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "HealthExperts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ExperienceYears",
                table: "HealthExperts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LicenseNumber",
                table: "HealthExperts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "HealthExperts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "HealthExperts");

            migrationBuilder.DropColumn(
                name: "ExperienceYears",
                table: "HealthExperts");

            migrationBuilder.DropColumn(
                name: "LicenseNumber",
                table: "HealthExperts");

            migrationBuilder.DropColumn(
                name: "Profession",
                table: "HealthExperts");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "HealthExperts",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "HealthExperts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
