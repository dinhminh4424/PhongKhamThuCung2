using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhongKhamThuCung.Migrations
{
    /// <inheritdoc />
    public partial class CapNhatLaiBang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DanhGia_KhachHang_MaKH",
                table: "DanhGia");

            migrationBuilder.DropForeignKey(
                name: "FK_ThuCung_KhachHang_MaKhachHang",
                table: "ThuCung");

            migrationBuilder.DropIndex(
                name: "IX_ThuCung_MaKhachHang",
                table: "ThuCung");

            migrationBuilder.DropIndex(
                name: "IX_DanhGia_MaKH",
                table: "DanhGia");

            migrationBuilder.DropColumn(
                name: "MaKhachHang",
                table: "ThuCung");

            migrationBuilder.DropColumn(
                name: "MaKH",
                table: "DanhGia");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ThuCung",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DanhGia",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThuCung_UserId",
                table: "ThuCung",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_UserId",
                table: "DanhGia",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGia_AspNetUsers_UserId",
                table: "DanhGia",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ThuCung_AspNetUsers_UserId",
                table: "ThuCung",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DanhGia_AspNetUsers_UserId",
                table: "DanhGia");

            migrationBuilder.DropForeignKey(
                name: "FK_ThuCung_AspNetUsers_UserId",
                table: "ThuCung");

            migrationBuilder.DropIndex(
                name: "IX_ThuCung_UserId",
                table: "ThuCung");

            migrationBuilder.DropIndex(
                name: "IX_DanhGia_UserId",
                table: "DanhGia");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ThuCung");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DanhGia");

            migrationBuilder.AddColumn<int>(
                name: "MaKhachHang",
                table: "ThuCung",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaKH",
                table: "DanhGia",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Age",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThuCung_MaKhachHang",
                table: "ThuCung",
                column: "MaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_MaKH",
                table: "DanhGia",
                column: "MaKH");

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGia_KhachHang_MaKH",
                table: "DanhGia",
                column: "MaKH",
                principalTable: "KhachHang",
                principalColumn: "MaKhachHang");

            migrationBuilder.AddForeignKey(
                name: "FK_ThuCung_KhachHang_MaKhachHang",
                table: "ThuCung",
                column: "MaKhachHang",
                principalTable: "KhachHang",
                principalColumn: "MaKhachHang");
        }
    }
}
