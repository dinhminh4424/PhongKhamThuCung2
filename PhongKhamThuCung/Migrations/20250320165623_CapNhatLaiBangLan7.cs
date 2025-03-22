using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhongKhamThuCung.Migrations
{
    /// <inheritdoc />
    public partial class CapNhatLaiBangLan7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietUuDai");

            migrationBuilder.DropTable(
                name: "HoaDon");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    MaHoaDon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaLH = table.Column<int>(type: "int", nullable: true),
                    DaThanhToan = table.Column<double>(type: "float", nullable: true),
                    NgayLap = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThanhTien = table.Column<double>(type: "float", nullable: true),
                    TinhTrang = table.Column<bool>(type: "bit", nullable: false),
                    TongTien = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.MaHoaDon);
                    table.ForeignKey(
                        name: "FK_HoaDon_LichHen_MaLH",
                        column: x => x.MaLH,
                        principalTable: "LichHen",
                        principalColumn: "MaLichHen");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietUuDai",
                columns: table => new
                {
                    MaChiTietUuDai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHoaDon = table.Column<int>(type: "int", nullable: true),
                    MaUuDai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietUuDai", x => x.MaChiTietUuDai);
                    table.ForeignKey(
                        name: "FK_ChiTietUuDai_HoaDon_MaHoaDon",
                        column: x => x.MaHoaDon,
                        principalTable: "HoaDon",
                        principalColumn: "MaHoaDon");
                    table.ForeignKey(
                        name: "FK_ChiTietUuDai_UuDai_MaUuDai",
                        column: x => x.MaUuDai,
                        principalTable: "UuDai",
                        principalColumn: "MaUuDai");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietUuDai_MaHoaDon",
                table: "ChiTietUuDai",
                column: "MaHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietUuDai_MaUuDai",
                table: "ChiTietUuDai",
                column: "MaUuDai");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaLH",
                table: "HoaDon",
                column: "MaLH",
                unique: true,
                filter: "[MaLH] IS NOT NULL");
        }
    }
}
