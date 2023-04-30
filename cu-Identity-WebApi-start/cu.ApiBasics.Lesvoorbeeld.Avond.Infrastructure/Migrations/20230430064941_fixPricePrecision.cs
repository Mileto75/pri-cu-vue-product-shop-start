using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cu.ApiBasics.Lesvoorbeeld.Avond.Infrastructure.Migrations
{
    public partial class fixPricePrecision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d26c8c84-2a9d-4fb2-8868-c0956cee0f6c", "AQAAAAEAACcQAAAAEEInGmXAGmuWdvTGMYU1nf63MkuObO2UUrwru4jBMKfjSfnovK3EJVGoMGVjsnrrCg==", "891baa4c-9cdf-4b6e-b692-ac3996a4545d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ba68c55-a40c-4eab-a99c-0758312c2f2c", "AQAAAAEAACcQAAAAEHPOETqoiS8HyqkDYPv2P2jpESFoopmUJ63I+CI7+JswLh64SsJNT6pljvtDB78fHQ==", "6989b198-41ac-422a-ae38-018a1817c6d2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "276a1a8a-cb71-48ba-aec5-18ad2b18272f", "AQAAAAEAACcQAAAAEMDXa9x66DIUAODuPmyCMBep2KF9Pk7SAQQgyOhJkY9VocooqX5hho4z6H4jC2B8BQ==", "7e61de9f-2599-4165-9d3f-0cf0e591c048" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c84d3200-07d0-4084-a77e-4f1ace971b7e", "AQAAAAEAACcQAAAAEA7Q7mkvzWLOnQF20sOrIT+TMtQjKmrlTXpaUzr6wqQpwwssHPhVsrh6/d+G7BwqGA==", "06cd582d-16b1-4a36-989a-1ff76a41b429" });
        }
    }
}
