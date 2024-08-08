using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labo.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MinPlayers = table.Column<int>(type: "int", nullable: false),
                    MaxPlayers = table.Column<int>(type: "int", nullable: false),
                    EloMin = table.Column<int>(type: "int", nullable: true),
                    EloMax = table.Column<int>(type: "int", nullable: true),
                    Categories = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WomenOnly = table.Column<bool>(type: "bit", nullable: false),
                    CurrentRound = table.Column<int>(type: "int", nullable: false),
                    EndOfRegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EncodedPassword = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Salt = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Elo = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WhiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Round = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Match_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Match_Users_BlackId",
                        column: x => x.BlackId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Users_WhiteId",
                        column: x => x.WhiteId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentUser",
                columns: table => new
                {
                    PlayersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TournamentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentUser", x => new { x.PlayersId, x.TournamentsId });
                    table.ForeignKey(
                        name: "FK_TournamentUser_Tournaments_TournamentsId",
                        column: x => x.TournamentsId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentUser_Users_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Elo", "Email", "EncodedPassword", "Gender", "IsDeleted", "Role", "Salt", "Username" },
                values: new object[,]
                {
                    { new Guid("2649548b-ba8d-4f9b-b1ab-c58bc64a063b"), new DateTime(2000, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1500, "j@yopmail.com", new byte[] { 125, 87, 111, 213, 101, 94, 74, 103, 51, 111, 215, 126, 213, 212, 244, 226, 29, 10, 168, 162, 159, 114, 93, 88, 148, 68, 163, 199, 69, 121, 83, 139, 131, 52, 227, 162, 240, 163, 163, 165, 37, 144, 61, 152, 133, 46, 133, 7, 41, 250, 5, 144, 106, 116, 23, 81, 28, 98, 107, 250, 153, 94, 40, 19 }, "Male", false, "Player", new Guid("3778dbeb-089f-4e4a-8429-f2c764508204"), "John" },
                    { new Guid("34303638-fe61-4d76-83d3-d4ca8576542a"), new DateTime(2000, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800, "s@yopmail.com", new byte[] { 251, 150, 55, 77, 13, 233, 107, 179, 32, 234, 76, 32, 51, 147, 131, 220, 35, 138, 211, 234, 86, 142, 172, 238, 148, 188, 182, 230, 254, 148, 90, 142, 145, 238, 205, 122, 201, 216, 217, 35, 71, 108, 221, 171, 200, 202, 0, 56, 214, 103, 244, 26, 5, 42, 182, 13, 189, 208, 91, 69, 186, 170, 50, 124 }, "Female", false, "Player", new Guid("9661f007-fa05-4157-9d45-d1e1fdb22eda"), "Sarah" },
                    { new Guid("a6a71a1f-699e-4a01-a3b3-89354a01ef4f"), new DateTime(1982, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800, "lykhun@gmail.com", new byte[] { 40, 231, 100, 182, 70, 180, 236, 212, 130, 67, 117, 128, 122, 190, 151, 184, 27, 89, 8, 128, 136, 20, 52, 174, 234, 236, 143, 52, 40, 161, 20, 12, 116, 36, 254, 110, 76, 213, 11, 11, 109, 101, 110, 194, 68, 117, 161, 126, 30, 190, 152, 158, 21, 179, 68, 29, 246, 231, 223, 225, 41, 60, 186, 57 }, "Male", false, "Admin", new Guid("a87100d6-1cb2-4c3d-b0b0-468d6d6ca662"), "Checkmate" },
                    { new Guid("aa9dad4c-f575-4036-bb5f-c66ba0c565be"), new DateTime(2000, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800, "p@yopmail.com", new byte[] { 42, 189, 61, 202, 141, 168, 248, 201, 152, 241, 47, 66, 56, 115, 180, 28, 55, 6, 233, 38, 207, 121, 116, 24, 86, 72, 102, 44, 66, 62, 154, 72, 252, 245, 8, 54, 91, 212, 176, 223, 11, 79, 192, 92, 70, 78, 177, 49, 32, 116, 159, 248, 41, 43, 143, 119, 135, 26, 240, 133, 62, 25, 49, 209 }, "Male", false, "Player", new Guid("de88ab94-a07a-4c95-a93f-461f1b8a2ca1"), "Paul" },
                    { new Guid("b4ed6087-6683-421d-9dc4-d286cd077058"), new DateTime(2000, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800, "g@yopmail.com", new byte[] { 175, 137, 165, 92, 157, 160, 33, 48, 64, 125, 46, 32, 194, 167, 215, 65, 128, 86, 74, 86, 166, 33, 220, 215, 234, 170, 13, 163, 185, 43, 238, 108, 52, 201, 11, 123, 132, 96, 151, 52, 128, 80, 101, 122, 244, 129, 103, 146, 21, 198, 24, 27, 60, 143, 14, 30, 250, 223, 15, 108, 241, 175, 133, 177 }, "Male", false, "Player", new Guid("32311147-6328-4722-a824-480a0c083b14"), "Georges" },
                    { new Guid("e578d77c-cc21-414a-b98d-1795ab13fe6e"), new DateTime(2000, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800, "r@yopmail.com", new byte[] { 142, 37, 160, 155, 223, 80, 221, 129, 26, 126, 140, 163, 232, 104, 250, 190, 213, 195, 22, 175, 139, 22, 40, 85, 80, 213, 186, 116, 63, 171, 165, 183, 51, 207, 143, 39, 158, 109, 164, 107, 94, 202, 66, 63, 210, 6, 230, 174, 176, 31, 252, 220, 6, 109, 193, 246, 4, 151, 106, 141, 229, 99, 53, 130 }, "Male", false, "Player", new Guid("32205f5c-42e4-4095-8fdd-d8b2560ce2c4"), "Ringo" },
                    { new Guid("f6ea3526-9298-4312-b666-a41df3af88c2"), new DateTime(2005, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1200, "b@yopmail.com", new byte[] { 155, 72, 145, 221, 117, 63, 188, 202, 211, 101, 199, 93, 137, 255, 150, 219, 110, 115, 26, 226, 36, 24, 230, 201, 19, 108, 104, 152, 59, 21, 176, 157, 29, 44, 38, 223, 138, 155, 70, 90, 227, 255, 110, 239, 66, 163, 118, 199, 42, 1, 229, 65, 32, 211, 203, 32, 183, 54, 146, 117, 241, 163, 173, 73 }, "Female", false, "Player", new Guid("fa7900ef-b38f-4ef9-b6f6-896f187e831c"), "Brithney" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Match_BlackId",
                table: "Match",
                column: "BlackId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_TournamentId",
                table: "Match",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_WhiteId",
                table: "Match",
                column: "WhiteId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentUser_TournamentsId",
                table: "TournamentUser",
                column: "TournamentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Salt",
                table: "Users",
                column: "Salt",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "TournamentUser");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
