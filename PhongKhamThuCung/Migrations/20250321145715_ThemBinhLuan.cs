using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhongKhamThuCung.Migrations
{
    /// <inheritdoc />
    public partial class ThemBinhLuan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diem",
                table: "DanhGia");

            migrationBuilder.DropColumn(
                name: "MoTaNgan",
                table: "DanhGia");

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayBinhLuan",
                table: "DanhGia",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayBinhLuan",
                table: "DanhGia");

            migrationBuilder.AddColumn<int>(
                name: "Diem",
                table: "DanhGia",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MoTaNgan",
                table: "DanhGia",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
