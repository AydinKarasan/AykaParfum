using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kampanyalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Aciklamasi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kampanyalar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UrunKampanyalar",
                columns: table => new
                {
                    UrunId = table.Column<int>(type: "int", nullable: false),
                    KampanyaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunKampanyalar", x => new { x.UrunId, x.KampanyaId });
                    table.ForeignKey(
                        name: "FK_UrunKampanyalar_Kampanyalar_KampanyaId",
                        column: x => x.KampanyaId,
                        principalTable: "Kampanyalar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UrunKampanyalar_Urunler_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Urunler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrunKampanyalar_KampanyaId",
                table: "UrunKampanyalar",
                column: "KampanyaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrunKampanyalar");

            migrationBuilder.DropTable(
                name: "Kampanyalar");
        }
    }
}
