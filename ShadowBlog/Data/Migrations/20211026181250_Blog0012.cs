using Microsoft.EntityFrameworkCore.Migrations;

namespace ShadowBlog.Data.Migrations
{
    public partial class Blog0012 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageType",
                table: "BlogPosts",
                newName: "ContentType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContentType",
                table: "BlogPosts",
                newName: "ImageType");
        }
    }
}
