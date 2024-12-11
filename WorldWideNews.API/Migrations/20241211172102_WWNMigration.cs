using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorldWideNews.API.Migrations
{
    /// <inheritdoc />
    public partial class WWNMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NewsAgencies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsAgencies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CountryCategories",
                columns: table => new
                {
                    CountryID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCategories", x => new { x.CategoryID, x.CountryID });
                    table.ForeignKey(
                        name: "FK_CountryCategories_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryCategories_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reporters",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewsAgencyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewsAgencyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reporters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reporters_NewsAgencies_NewsAgencyID",
                        column: x => x.NewsAgencyID,
                        principalTable: "NewsAgencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NewsAgencyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReporterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReporterID = table.Column<int>(type: "int", nullable: false),
                    NewsAgencyID = table.Column<int>(type: "int", nullable: false),
                    CountryCategoriesCategoryID = table.Column<int>(type: "int", nullable: false),
                    CountryCategoriesCountryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.ID);
                    table.ForeignKey(
                        name: "FK_News_CountryCategories_CountryCategoriesCategoryID_CountryCategoriesCountryID",
                        columns: x => new { x.CountryCategoriesCategoryID, x.CountryCategoriesCountryID },
                        principalTable: "CountryCategories",
                        principalColumns: new[] { "CategoryID", "CountryID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_News_NewsAgencies_NewsAgencyID",
                        column: x => x.NewsAgencyID,
                        principalTable: "NewsAgencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_News_Reporters_ReporterID",
                        column: x => x.ReporterID,
                        principalTable: "Reporters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryCategories_CountryID",
                table: "CountryCategories",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_News_CountryCategoriesCategoryID_CountryCategoriesCountryID",
                table: "News",
                columns: new[] { "CountryCategoriesCategoryID", "CountryCategoriesCountryID" });

            migrationBuilder.CreateIndex(
                name: "IX_News_NewsAgencyID",
                table: "News",
                column: "NewsAgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_News_ReporterID",
                table: "News",
                column: "ReporterID");

            migrationBuilder.CreateIndex(
                name: "IX_Reporters_NewsAgencyID",
                table: "Reporters",
                column: "NewsAgencyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "CountryCategories");

            migrationBuilder.DropTable(
                name: "Reporters");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "NewsAgencies");
        }
    }
}
