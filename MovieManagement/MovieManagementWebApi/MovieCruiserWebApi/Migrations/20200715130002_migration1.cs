using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieCruiserWebApi.Migrations
{
    public partial class migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    FavId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    BoxOffice = table.Column<bool>(nullable: false),
                    HasTeaser = table.Column<bool>(nullable: false),
                    IsFavorite = table.Column<bool>(nullable: false),
                    DateOfLaunch = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    GenreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieItems_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteMovies",
                columns: table => new
                {
                    MovieItemId = table.Column<int>(nullable: false),
                    FavoriteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteMovies", x => new { x.FavoriteId, x.MovieItemId });
                    table.ForeignKey(
                        name: "FK_FavoriteMovies_MovieItems_MovieItemId",
                        column: x => x.MovieItemId,
                        principalTable: "MovieItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMovies_MovieItemId",
                table: "FavoriteMovies",
                column: "MovieItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieItems_GenreId",
                table: "MovieItems",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteMovies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "MovieItems");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
