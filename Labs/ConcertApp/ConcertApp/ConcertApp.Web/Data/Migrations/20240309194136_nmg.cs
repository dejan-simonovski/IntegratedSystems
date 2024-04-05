using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcertApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class nmg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Concert",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcertName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcertPrice = table.Column<double>(type: "float", nullable: false),
                    ConcertLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concert", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Concert");
        }
    }
}
