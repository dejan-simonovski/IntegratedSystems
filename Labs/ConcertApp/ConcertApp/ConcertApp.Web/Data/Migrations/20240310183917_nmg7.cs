using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcertApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class nmg7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcertImgUrl",
                table: "Concert",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcertImgUrl",
                table: "Concert");
        }
    }
}
