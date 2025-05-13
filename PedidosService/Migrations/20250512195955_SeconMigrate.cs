using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PedidosService.Migrations
{
    /// <inheritdoc />
    public partial class SeconMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreComida = table.Column<string>(type: "text", nullable: false),
                    Precio = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuDTO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MesaDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumeroMesa = table.Column<string>(type: "text", nullable: false),
                    Disponible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesaDTO", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_MesaId",
                table: "Pedidos",
                column: "MesaId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoMenus_MenuId",
                table: "PedidoMenus",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoMenus_MenuDTO_MenuId",
                table: "PedidoMenus",
                column: "MenuId",
                principalTable: "MenuDTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_MesaDTO_MesaId",
                table: "Pedidos",
                column: "MesaId",
                principalTable: "MesaDTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoMenus_MenuDTO_MenuId",
                table: "PedidoMenus");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_MesaDTO_MesaId",
                table: "Pedidos");

            migrationBuilder.DropTable(
                name: "MenuDTO");

            migrationBuilder.DropTable(
                name: "MesaDTO");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_MesaId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_PedidoMenus_MenuId",
                table: "PedidoMenus");
        }
    }
}
