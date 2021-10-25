using Microsoft.EntityFrameworkCore.Migrations;

namespace ShadowBlog.Data.Migrations
{
    public partial class _0010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageType",
                table: "Blogs",
                newName: "ContentType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContentType",
                table: "Blogs",
                newName: "ImageType");
        }
    }
}
