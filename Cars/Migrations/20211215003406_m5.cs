using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Cars.Migrations
{
    public partial class m5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LayerID",
                table: "OrderDetails",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LayerID1",
                table: "OrderDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Layers",
                columns: table => new
                {
                    LayerID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Layers", x => x.LayerID);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Layers_LayerID1",
                table: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Layers");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_LayerID1",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "LayerID",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "LayerID1",
                table: "OrderDetails");
        }
    }
}
