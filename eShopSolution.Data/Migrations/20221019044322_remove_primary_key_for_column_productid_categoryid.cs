using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.Data.Migrations
{
    public partial class remove_primary_key_for_column_productid_categoryid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInCategories",
                table: "ProductInCategories");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductInCategories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInCategories",
                table: "ProductInCategories",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "dde87d7d-4e23-41d7-84c5-74851ce31acd");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "157da36b-7aa7-4007-9bd5-8e7bd8e2c7d6", "AQAAAAEAACcQAAAAELY7gey9TPWvyeYi/sX2vbzj+JlHP5MH94eRC5VIRMLitRTfoEZMSl2nfUbNzkuU8Q==" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInCategories_CategoryId",
                table: "ProductInCategories",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInCategories",
                table: "ProductInCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductInCategories_CategoryId",
                table: "ProductInCategories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductInCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInCategories",
                table: "ProductInCategories",
                columns: new[] { "CategoryId", "ProductId" });

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
    }
}
