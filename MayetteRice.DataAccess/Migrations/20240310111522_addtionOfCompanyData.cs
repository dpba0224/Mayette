using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MayetteRice.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addtionOfCompanyData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Barangay", "City", "Name", "PhoneNumber", "PostalCode", "Province", "StreetAddress" },
                values: new object[,]
                {
                    { 1, "Pulong Santa Cruz", "Santa Rosa", "Coca-Cola Beverages Philippines Inc.", "+639061234567", "4026", "Laguna", "Santa Rosa Exit" },
                    { 2, "Mamplasan", "Binan", "Amherst Laboratories Inc.", "+639067654321", "4026", "Laguna", "Unilab PharmaCampus" },
                    { 3, "Malusak", "Santa Rosa", "Santa Rosa City Hall", "+639977654321", "4026", "Laguna", "J.P. Rizal Street" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
