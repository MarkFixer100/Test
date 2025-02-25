using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infostructure.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Created", "Description", "Name", "Price", "Updated" },
                values: new object[] { new Guid("d290f1ee-6c54-4b01-90e6-d701748f0851"), new DateTime(2025, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Разработка дизайна", "Web Design", 5000m, new DateTime(2025, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("d290f1ee-6c54-4b01-90e6-d701748f0851"));
        }
    }
}
