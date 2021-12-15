using Microsoft.EntityFrameworkCore.Migrations;

namespace Cars.Migrations
{
    public partial class m3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Orders",
                type: "boolean",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SystemUserCreate",
                table: "OrderDetails",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "OrderDetails",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<string>(
                name: "SystemUserCreate",
                table: "OrderDetails",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
