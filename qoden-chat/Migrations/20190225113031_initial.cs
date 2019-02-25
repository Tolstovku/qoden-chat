using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace qoden_chat.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[,]
                {
                    { 100, "AQAAAAEAACcQAAAAENvrSDK5PYg6qzw1sQXOWs3qAyQ0/D2kZwUZr8CP1HFDRCL5JZ2zQgk8RgKV0tV1Dw==", "Tolstovku" },
                    { 200, "AQAAAAEAACcQAAAAENvrSDK5PYg6qzw1sQXOWs3qAyQ0/D2kZwUZr8CP1HFDRCL5JZ2zQgk8RgKV0tV1Dw==", "Slimakanzer" },
                    { 300, "AQAAAAEAACcQAAAAENvrSDK5PYg6qzw1sQXOWs3qAyQ0/D2kZwUZr8CP1HFDRCL5JZ2zQgk8RgKV0tV1Dw==", "Nimatora" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
