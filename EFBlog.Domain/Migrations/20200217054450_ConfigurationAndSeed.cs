using Microsoft.EntityFrameworkCore.Migrations;

namespace EFBlog.Domain.Migrations
{
    public partial class ConfigurationAndSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.RenameTable(
                name: "Blogs",
                newName: "Blog");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blog",
                table: "Blog",
                column: "BlogId");

            migrationBuilder.InsertData(
                table: "Blog",
                columns: new[] { "BlogId", "Url" },
                values: new object[,]
                {
                    { 1, "https://devblogs.microsoft.com/aspnet/" },
                    { 2, "https://devblogs.microsoft.com/aspnet/category/aspnetcore/" },
                    { 3, "https://wakeupandcode.com/aspnetcore/" },
                    { 4, "https://www.stevejgordon.co.uk/" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Blog",
                table: "Blog");

            migrationBuilder.DeleteData(
                table: "Blog",
                keyColumn: "BlogId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Blog",
                keyColumn: "BlogId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Blog",
                keyColumn: "BlogId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Blog",
                keyColumn: "BlogId",
                keyValue: 4);

            migrationBuilder.RenameTable(
                name: "Blog",
                newName: "Blogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                column: "BlogId");
        }
    }
}
