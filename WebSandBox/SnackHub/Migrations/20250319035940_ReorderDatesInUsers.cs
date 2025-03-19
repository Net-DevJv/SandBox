using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnackHub.Migrations
{
    /// <inheritdoc />
    public partial class ReorderDatesInUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Products");

            migrationBuilder.AddColumn<DateTime?>(
                name: "CreationDate",
                table: "Products",
                type: "datetime2(0)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime?>(
                name: "UpdateDate",
                table: "Products",
                type: "datetime2(0)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "UpdateDate", table: "Products");
            migrationBuilder.DropColumn(name: "CreationDate", table: "Products");

            migrationBuilder.AddColumn<DateTime?>(
                name: "UpdateDate",
                table: "Products",
                type: "datetime2(0)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime?>(
                name: "CreationDate",
                table: "Products",
                type: "datetime2(0)",
                nullable: true);
        }
    }
}
