using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedCommentDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Emotes",
                columns: new[] { "Id", "Emoji" },
                values: new object[,]
                {
                    { new Guid("1aed586c-2122-4c58-abaa-ef1b3a65616a"), "❤️" },
                    { new Guid("4084e682-9a5f-484c-ae18-59d741de3349"), "🤣" },
                    { new Guid("6703b229-384b-48ed-81f5-37fc86e0750f"), "😭" },
                    { new Guid("67de7108-b508-41d6-b1a4-05a2b748b3e0"), "😡" },
                    { new Guid("735398e0-5c2e-4441-92c1-6986b1d2b147"), "😮" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Emotes",
                keyColumn: "Id",
                keyValue: new Guid("1aed586c-2122-4c58-abaa-ef1b3a65616a"));

            migrationBuilder.DeleteData(
                table: "Emotes",
                keyColumn: "Id",
                keyValue: new Guid("4084e682-9a5f-484c-ae18-59d741de3349"));

            migrationBuilder.DeleteData(
                table: "Emotes",
                keyColumn: "Id",
                keyValue: new Guid("6703b229-384b-48ed-81f5-37fc86e0750f"));

            migrationBuilder.DeleteData(
                table: "Emotes",
                keyColumn: "Id",
                keyValue: new Guid("67de7108-b508-41d6-b1a4-05a2b748b3e0"));

            migrationBuilder.DeleteData(
                table: "Emotes",
                keyColumn: "Id",
                keyValue: new Guid("735398e0-5c2e-4441-92c1-6986b1d2b147"));

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Comments");

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
    }
}
