using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NECOP_Form.Migrations
{
    /// <inheritdoc />
    public partial class AddFileColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Designation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Designation",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Designation");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Designation");
        }
    }
}
