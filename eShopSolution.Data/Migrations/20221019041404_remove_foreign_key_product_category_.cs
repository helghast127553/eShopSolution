using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.Data.Migrations
{
    public partial class remove_foreign_key_product_category_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "0c430149-f847-4aa1-a9c6-578cd41c9931");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bc2b5b17-cb62-4287-97cb-861068fdb246", "AQAAAAEAACcQAAAAEMw7xKixNFyttPh9Wp2lQtYqRMHZ1qZ3MxQtOPted/jQE1znprs/EXekedIanVIPEA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "a3a17c56-1b8f-4538-863f-0ffec53e1c7a");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b1c950e2-883d-4ab8-ac2a-5229a45d936d", "AQAAAAEAACcQAAAAEGY2qPhFr0m2uDDA9wovd9VXXKMHnhPUet+YnPfAB/VKLwPQXd+bvqpOkv5d0TDhiw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
