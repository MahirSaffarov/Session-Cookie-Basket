using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneToMany.Migrations
{
    public partial class AddDataCustomerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Age", "CreatedDate", "FullName", "SoftDeleted" },
                values: new object[] { 1, 16, new DateTime(2023, 5, 14, 6, 9, 40, 407, DateTimeKind.Local).AddTicks(9272), "Resul", false });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Age", "CreatedDate", "FullName", "SoftDeleted" },
                values: new object[] { 2, 25, new DateTime(2023, 5, 14, 6, 9, 40, 407, DateTimeKind.Local).AddTicks(9281), "Novreste", false });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Age", "CreatedDate", "FullName", "SoftDeleted" },
                values: new object[] { 3, 19, new DateTime(2023, 5, 14, 6, 9, 40, 407, DateTimeKind.Local).AddTicks(9282), "Musa", false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
