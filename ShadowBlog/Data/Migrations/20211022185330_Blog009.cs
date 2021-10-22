using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShadowBlog.Data.Migrations
{
    public partial class Blog009 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContentType",
                table: "AspNetUsers",
                newName: "ImageType");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Blogs",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageType",
                table: "Blogs",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ImageType",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "ImageType",
                table: "AspNetUsers",
                newName: "ContentType");
        }
    }
}
