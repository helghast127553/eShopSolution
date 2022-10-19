using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.Data.Migrations
{
    public partial class changedatabasedesign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time_Updated",
                table: "ProductImages",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time_Created",
                table: "ProductImages",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time_Updated",
                table: "ProductImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time_Created",
                table: "ProductImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "97215833-8846-4ce1-b2bf-d1fcb666d8ae");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2f7262d7-7eda-4324-af4d-5c4b556b9b4f", "AQAAAAEAACcQAAAAEOLZDWZtcIflEbPzC/EI2AM0Dg0GM/4VeUXfqhud3JCWeWhK+dA90vMkPfL8093SJA==" });
        }
    }
}
