using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Cars.Migrations
{
    public partial class m6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Layers_LayerID1",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_LayerID1",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "LayerID1",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<int>(
                name: "LayerID",
                table: "Layers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_LayerID",
                table: "OrderDetails",
                column: "LayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Layers_LayerID",
                table: "OrderDetails",
                column: "LayerID",
                principalTable: "Layers",
                principalColumn: "LayerID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Layers_LayerID",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_LayerID",
                table: "OrderDetails");

            migrationBuilder.AddColumn<long>(
                name: "LayerID1",
                table: "OrderDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "LayerID",
                table: "Layers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_LayerID1",
                table: "OrderDetails",
                column: "LayerID1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Layers_LayerID1",
                table: "OrderDetails",
                column: "LayerID1",
                principalTable: "Layers",
                principalColumn: "LayerID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
