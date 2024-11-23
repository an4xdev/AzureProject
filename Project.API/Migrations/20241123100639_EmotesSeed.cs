using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project.API.Migrations
{
    /// <inheritdoc />
    public partial class EmotesSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Emotes",
                columns: new[] { "Id", "Emoji" },
                values: new object[,]
                {
                    { new Guid("25434973-ac5b-4df8-a53d-f9517caf4d14"), "🤣" },
                    { new Guid("2c09d024-7bff-427d-bd8e-dd39dc8d41e5"), "😡" },
                    { new Guid("7904c836-6c63-4bc6-8db8-eb421bcfb6e6"), "😭" },
                    { new Guid("d9881368-5d26-4fb8-945f-48caa7f9fc41"), "❤️" },
                    { new Guid("fb96496c-6330-4cbe-bc94-b02d787780b9"), "😮" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Emotes",
                keyColumn: "Id",
                keyValue: new Guid("25434973-ac5b-4df8-a53d-f9517caf4d14"));

            migrationBuilder.DeleteData(
                table: "Emotes",
                keyColumn: "Id",
                keyValue: new Guid("2c09d024-7bff-427d-bd8e-dd39dc8d41e5"));

            migrationBuilder.DeleteData(
                table: "Emotes",
                keyColumn: "Id",
                keyValue: new Guid("7904c836-6c63-4bc6-8db8-eb421bcfb6e6"));

            migrationBuilder.DeleteData(
                table: "Emotes",
                keyColumn: "Id",
                keyValue: new Guid("d9881368-5d26-4fb8-945f-48caa7f9fc41"));

            migrationBuilder.DeleteData(
                table: "Emotes",
                keyColumn: "Id",
                keyValue: new Guid("fb96496c-6330-4cbe-bc94-b02d787780b9"));
        }
    }
}
