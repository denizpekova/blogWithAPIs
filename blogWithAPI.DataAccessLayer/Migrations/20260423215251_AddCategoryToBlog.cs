using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blogWithAPI.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryToBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "imageURL",
                table: "Blogs",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Blogs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Blogs",
                newName: "imageURL");
        }
    }
}
