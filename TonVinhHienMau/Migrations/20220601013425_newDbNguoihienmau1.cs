using Microsoft.EntityFrameworkCore.Migrations;

namespace TonVinhHienMau.Migrations
{
    public partial class newDbNguoihienmau1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaNguoiHien",
                table: "NguoiHienMau",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaNguoiHien",
                table: "NguoiHienMau");
        }
    }
}
