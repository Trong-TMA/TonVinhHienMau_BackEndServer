using Microsoft.EntityFrameworkCore.Migrations;

namespace TonVinhHienMau.Migrations
{
    public partial class newDbNguoihienmau : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NhomMau",
                table: "NguoiHienMau",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NhomMau",
                table: "NguoiHienMau");
        }
    }
}
