using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project.API.Migrations
{
    /// <inheritdoc />
    public partial class ToManyChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Emoji = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostEmotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostEmotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostEmotes_Emotes_EmoteId",
                        column: x => x.EmoteId,
                        principalTable: "Emotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostEmotes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Emotes",
                columns: new[] { "Id", "Emoji" },
                values: new object[,]
                {
                    { new Guid("2f48ff5f-2b47-48d5-a939-dc57a5bcabfb"), "😡" },
                    { new Guid("6fa459ea-ee8a-3ca4-894e-db77e160355e"), "❤️" },
                    { new Guid("b85fc1ea-a338-11d8-8f73-0002440126c0"), "😭" },
                    { new Guid("d9428888-122b-11e1-b85c-61cd3cbb3210"), "🤣" },
                    { new Guid("e6f8e7d7-3323-4c93-8b85-083fb172a2a0"), "😮" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("49dc72ab-50ce-49b4-91f6-9c69d5c6e8bb"), "aa@example.com", "aa", "lhtt0+3jy47LqsvWjeBAzXjrLtWIkTDM60xJJo6k1QY=" },
                    { new Guid("a70f1ae1-285d-41e3-a93a-1b0dcf0bc44d"), "cc@example.com", "cc", "NVsbv8lnJc3Oj0onCP2jEKgObRMxWuxOXu0qdf6AMs4=" },
                    { new Guid("c9da05f8-892b-41e5-99cc-7a89b6edc789"), "dd@example.com", "dd", "m37MbuuDq/mt4Q/jiGXfRJm+NWjcxQeuLsO0SYnLAJM=" },
                    { new Guid("f18b62db-69b3-45b6-9d93-23489db44c4f"), "ee@example.com", "ee", "J6hHEuSyLEFfxUTVXN7oIyeoKfltAzKUV/duv5r03Ko=" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), "bb@example.com", "bb", "O2TblctVx2M5HHBxCEia4YtBEteDMA3jjgM7TJjD3q8=" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Description", "Likes", "PhotoPath", "UserId" },
                values: new object[,]
                {
                    { new Guid("0f37c9f4-b3c3-4da4-a7f3-08d6bcd27dcf"), null, 40, "https://anax.blob.core.windows.net/test/24d738d3-3da9-4e7d-a992-0d5dbb916415.png", new Guid("a70f1ae1-285d-41e3-a93a-1b0dcf0bc44d") },
                    { new Guid("3e052e9d-1c18-40c6-8913-94a067b6b5e3"), "Skibidi", 30, "https://anax.blob.core.windows.net/test/24d738d3-3da9-4e7d-a992-0d5dbb916415.png", new Guid("f18b62db-69b3-45b6-9d93-23489db44c4f") },
                    { new Guid("9f8fdbcc-3765-4623-91e5-fb4ccf2d78a8"), "Some other description", 25, "https://anax.blob.core.windows.net/test/24d738d3-3da9-4e7d-a992-0d5dbb916415.png", new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479") },
                    { new Guid("d2a7e8f6-e26f-489a-832c-4c93b7be62f5"), null, 50, "https://anax.blob.core.windows.net/test/24d738d3-3da9-4e7d-a992-0d5dbb916415.png", new Guid("49dc72ab-50ce-49b4-91f6-9c69d5c6e8bb") },
                    { new Guid("d511a30e-e1e5-402f-8c5e-d1e0b5d4f34a"), "Some description", 20, "https://anax.blob.core.windows.net/test/24d738d3-3da9-4e7d-a992-0d5dbb916415.png", new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479") }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "PostId", "Time", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("a2f1a7c8-3f1b-4db9-b251-ffbb924fcd47"), new Guid("d2a7e8f6-e26f-489a-832c-4c93b7be62f5"), new DateTime(2024, 10, 1, 12, 0, 12, 0, DateTimeKind.Unspecified), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), "Great!" },
                    { new Guid("b1e9873d-5036-4e21-9ea2-0f74b2bcb3db"), new Guid("9f8fdbcc-3765-4623-91e5-fb4ccf2d78a8"), new DateTime(2024, 12, 1, 14, 34, 44, 0, DateTimeKind.Unspecified), new Guid("f18b62db-69b3-45b6-9d93-23489db44c4f"), "Skibidi!" },
                    { new Guid("c3d5e77e-d60a-4219-a5b7-869f6d2e03b2"), new Guid("d2a7e8f6-e26f-489a-832c-4c93b7be62f5"), new DateTime(2024, 11, 15, 8, 30, 45, 0, DateTimeKind.Unspecified), new Guid("f18b62db-69b3-45b6-9d93-23489db44c4f"), "LOL!" },
                    { new Guid("e5a2a814-98a0-4d9d-9c2e-04f32a58f2bd"), new Guid("d511a30e-e1e5-402f-8c5e-d1e0b5d4f34a"), new DateTime(2024, 9, 10, 17, 15, 23, 0, DateTimeKind.Unspecified), new Guid("49dc72ab-50ce-49b4-91f6-9c69d5c6e8bb"), "KEKW!" },
                    { new Guid("f7bc2d1c-0f5c-4b25-a5e8-08c9df3f6f3b"), new Guid("9f8fdbcc-3765-4623-91e5-fb4ccf2d78a8"), new DateTime(2024, 11, 27, 23, 59, 59, 0, DateTimeKind.Unspecified), new Guid("c9da05f8-892b-41e5-99cc-7a89b6edc789"), "Slay!" }
                });

            migrationBuilder.InsertData(
                table: "PostEmotes",
                columns: new[] { "Id", "EmoteId", "PostId", "UserId" },
                values: new object[,]
                {
                    { new Guid("11e2bffa-7d44-40be-a0a6-bcc32b618289"), new Guid("6fa459ea-ee8a-3ca4-894e-db77e160355e"), new Guid("3e052e9d-1c18-40c6-8913-94a067b6b5e3"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479") },
                    { new Guid("3b99b4cf-dfb7-4e6c-bb68-88dc96c2dca2"), new Guid("b85fc1ea-a338-11d8-8f73-0002440126c0"), new Guid("0f37c9f4-b3c3-4da4-a7f3-08d6bcd27dcf"), new Guid("a70f1ae1-285d-41e3-a93a-1b0dcf0bc44d") },
                    { new Guid("843d9b36-c24d-43d9-b98f-9cbfcbdc1ae6"), new Guid("d9428888-122b-11e1-b85c-61cd3cbb3210"), new Guid("d2a7e8f6-e26f-489a-832c-4c93b7be62f5"), new Guid("49dc72ab-50ce-49b4-91f6-9c69d5c6e8bb") },
                    { new Guid("94c4b210-5f9f-468c-95a6-c11d37f82a93"), new Guid("b85fc1ea-a338-11d8-8f73-0002440126c0"), new Guid("d511a30e-e1e5-402f-8c5e-d1e0b5d4f34a"), new Guid("f18b62db-69b3-45b6-9d93-23489db44c4f") },
                    { new Guid("aa2de3f9-d5f0-41eb-95df-f3b08b446fae"), new Guid("2f48ff5f-2b47-48d5-a939-dc57a5bcabfb"), new Guid("d511a30e-e1e5-402f-8c5e-d1e0b5d4f34a"), new Guid("c9da05f8-892b-41e5-99cc-7a89b6edc789") },
                    { new Guid("bfb5e5ac-1a6d-4c69-9094-1cd627e38a0a"), new Guid("6fa459ea-ee8a-3ca4-894e-db77e160355e"), new Guid("d2a7e8f6-e26f-489a-832c-4c93b7be62f5"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479") },
                    { new Guid("c7e8fd21-318c-4e94-bd1d-f6e3743c9a6b"), new Guid("d9428888-122b-11e1-b85c-61cd3cbb3210"), new Guid("9f8fdbcc-3765-4623-91e5-fb4ccf2d78a8"), new Guid("a70f1ae1-285d-41e3-a93a-1b0dcf0bc44d") },
                    { new Guid("d02a51f5-4ee1-4a9b-a3b5-d76b3e3a7716"), new Guid("2f48ff5f-2b47-48d5-a939-dc57a5bcabfb"), new Guid("d2a7e8f6-e26f-489a-832c-4c93b7be62f5"), new Guid("49dc72ab-50ce-49b4-91f6-9c69d5c6e8bb") },
                    { new Guid("e31d39a7-f3d1-4b75-8dc1-abc62c3bb9d5"), new Guid("e6f8e7d7-3323-4c93-8b85-083fb172a2a0"), new Guid("0f37c9f4-b3c3-4da4-a7f3-08d6bcd27dcf"), new Guid("c9da05f8-892b-41e5-99cc-7a89b6edc789") },
                    { new Guid("f0537b5e-420f-4577-b348-c44dc543fc09"), new Guid("e6f8e7d7-3323-4c93-8b85-083fb172a2a0"), new Guid("9f8fdbcc-3765-4623-91e5-fb4ccf2d78a8"), new Guid("f18b62db-69b3-45b6-9d93-23489db44c4f") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostEmotes_EmoteId",
                table: "PostEmotes",
                column: "EmoteId");

            migrationBuilder.CreateIndex(
                name: "IX_PostEmotes_PostId",
                table: "PostEmotes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "PostEmotes");

            migrationBuilder.DropTable(
                name: "Emotes");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
