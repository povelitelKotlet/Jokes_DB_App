using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JokesApp.Data.Migrations
{
    public partial class MySecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    Login = table.Column<string>(maxLength: 256, nullable: false),
                    Password = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });
            
            migrationBuilder.CreateTable(
                name: "Joke",
                columns: table => new
                {
                    JokeId = table.Column<int>(nullable: false),
                    JokeText = table.Column<string>(maxLength: 256, nullable: false),
                    Author = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Joke", x => x.JokeId);
                    table.ForeignKey(
                        name: "FK_Joke_User_UserId",
                        column: x => x.Author,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    RatingId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    JokeId = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_Rating_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rating_Joke_JokeId",
                        column: x => x.JokeId,
                        principalTable: "Joke",
                        principalColumn: "JokeId",
                        onDelete: ReferentialAction.Cascade);
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rating");
            migrationBuilder.DropTable(
                name: "Joke");
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
