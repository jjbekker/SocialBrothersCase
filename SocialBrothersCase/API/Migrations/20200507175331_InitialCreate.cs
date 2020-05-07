using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Street = table.Column<string>(nullable: false),
                    HouseNumber = table.Column<int>(nullable: false),
                    PostalCode = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Country", "HouseNumber", "PostalCode", "Street" },
                values: new object[] { 1L, "Woerden", "Netherlands", 11, "3446XP", "Forintdreef" });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Country", "HouseNumber", "PostalCode", "Street" },
                values: new object[] { 2L, "Woerden", "Netherlands", 5, "3441AP", "Eendrachtstraat" });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Country", "HouseNumber", "PostalCode", "Street" },
                values: new object[] { 3L, "Woerden", "Netherlands", 86, "3446JG", "Kallameer" });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Country", "HouseNumber", "PostalCode", "Street" },
                values: new object[] { 4L, "Woerden", "Netherlands", 2, "3446JL", "Tornemeer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
