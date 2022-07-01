using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TonVinhHienMau.Migrations
{
    public partial class NewHoGiaDinhdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiaDinhId",
                table: "MoiQuanHe");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "MoiQuanHe",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MaChuHo",
                table: "HoGiaDinh",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "MoiQuanHe");

            migrationBuilder.DropColumn(
                name: "MaChuHo",
                table: "HoGiaDinh");

            migrationBuilder.AddColumn<Guid>(
                name: "GiaDinhId",
                table: "MoiQuanHe",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
