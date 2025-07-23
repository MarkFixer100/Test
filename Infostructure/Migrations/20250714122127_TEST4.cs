using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infostructure.Migrations
{
    /// <inheritdoc />
    public partial class TEST4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Cart");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CartItem",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Cart",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }
    }
}
