using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhongKhamThuCung.Migrations
{
    /// <inheritdoc />
    public partial class CapNhatLaiBangLan9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "LichHen",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "LichHen");
        }
    }
}
