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
                values: new object[] { "b5097497-b525-4486-b15b-25b27d44b45d", "AQAAAAEAACcQAAAAEJ6ouT4HKevsjjYJhdDCqG4Yav2501Yq6KoKBMOyDORHn7aDH3P6HDKV9PydvSR8KA==", "208cfc88-38ab-44c4-a2bd-3d4204b0ed41" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59eb1c04-6312-4090-b38a-4fea4a03a51f", "AQAAAAEAACcQAAAAENPtwLxpJzXDUvy5x2HFRFbvP5/OI9KcfSJkd0Fqk03QkFDr2M4+/IzgKcXolZkU4w==", "e8f03b75-4df7-4d1d-a010-395164a126e7" });
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
