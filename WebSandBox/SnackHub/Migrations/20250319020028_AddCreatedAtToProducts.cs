using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnackHub.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAtToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Products",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Products");
        }
    }
}
