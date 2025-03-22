using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhongKhamThuCung.Migrations
{
    /// <inheritdoc />
    public partial class CapNhatLaiBangLan3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LichHen_BacSi_BacSiMaBacSi",
                table: "LichHen");

            migrationBuilder.DropForeignKey(
                name: "FK_ThuCung_AspNetUsers_UserId",
                table: "ThuCung");

            migrationBuilder.DropIndex(
                name: "IX_ThuCung_UserId",
                table: "ThuCung");

            migrationBuilder.DropIndex(
                name: "IX_LichHen_BacSiMaBacSi",
                table: "LichHen");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ThuCung");

            migrationBuilder.DropColumn(
                name: "BacSiMaBacSi",
                table: "LichHen");

            migrationBuilder.DropColumn(
                name: "MaBacSi",
                table: "LichHen");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "LichHen",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaChuyenKhoa",
                table: "DichVu",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LichHen_UserId",
                table: "LichHen",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DichVu_MaChuyenKhoa",
                table: "DichVu",
                column: "MaChuyenKhoa");

            migrationBuilder.AddForeignKey(
                name: "FK_DichVu_ChuyenKhoa_MaChuyenKhoa",
                table: "DichVu",
                column: "MaChuyenKhoa",
                principalTable: "ChuyenKhoa",
                principalColumn: "MaChuyenKhoa");

            migrationBuilder.AddForeignKey(
                name: "FK_LichHen_AspNetUsers_UserId",
                table: "LichHen",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DichVu_ChuyenKhoa_MaChuyenKhoa",
                table: "DichVu");

            migrationBuilder.DropForeignKey(
                name: "FK_LichHen_AspNetUsers_UserId",
                table: "LichHen");

            migrationBuilder.DropIndex(
                name: "IX_LichHen_UserId",
                table: "LichHen");

            migrationBuilder.DropIndex(
                name: "IX_DichVu_MaChuyenKhoa",
                table: "DichVu");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LichHen");

            migrationBuilder.DropColumn(
                name: "MaChuyenKhoa",
                table: "DichVu");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ThuCung",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BacSiMaBacSi",
                table: "LichHen",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaBacSi",
                table: "LichHen",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThuCung_UserId",
                table: "ThuCung",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LichHen_BacSiMaBacSi",
                table: "LichHen",
                column: "BacSiMaBacSi");

            migrationBuilder.AddForeignKey(
                name: "FK_LichHen_BacSi_BacSiMaBacSi",
                table: "LichHen",
                column: "BacSiMaBacSi",
                principalTable: "BacSi",
                principalColumn: "MaBacSi");

            migrationBuilder.AddForeignKey(
                name: "FK_ThuCung_AspNetUsers_UserId",
                table: "ThuCung",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
