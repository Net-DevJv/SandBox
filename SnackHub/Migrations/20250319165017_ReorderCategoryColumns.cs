using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnackHub.Migrations
{
    /// <inheritdoc />
    public partial class ReorderCategoryColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime?>(
                name: "CreationDate",
                table: "Categories",
                type: "datetime2(0)", // ou datetime(0), se preferir
                nullable: true);

            migrationBuilder.AddColumn<DateTime?>(
                name: "UpdateDate",
                table: "Categories",
                type: "datetime2(0)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime?>(
                name: "CreationDate",
                table: "Categories",
                type: "datetime2(0)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime?>(
                name: "UpdateDate",
                table: "Categories",
                type: "datetime2(0)",
                nullable: true);
        }
    }
}
