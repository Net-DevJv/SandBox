using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnackHub.Migrations
{
    /// <inheritdoc />
    public partial class CreationDateAndUpdateDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Products",
                newName: "UpdateDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Products",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Products",
                newName: "CreatedAt");
        }
    }
}
