using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TonVinhHienMau.Migrations
{
    public partial class Db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonVi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenDonVi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MaDonVi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Diachi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonVi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DotTonVinh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenDotTonVinh = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MaDotTonVinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DotTonVinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NguoiHienMau",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    NamSinh = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    NgheNghiep = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TV_5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TV_10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TV_15 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TV_20 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TV_30 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TV_40 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TV_50 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TV_60 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TV_70 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TV_80 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TV_90 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TV_100 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonViId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DotTonVinhId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiHienMau", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NguoiHienMau_DonVi_DonViId",
                        column: x => x.DonViId,
                        principalTable: "DonVi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NguoiHienMau_DotTonVinh_DotTonVinhId",
                        column: x => x.DotTonVinhId,
                        principalTable: "DotTonVinh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NguoiHienMau_DonViId",
                table: "NguoiHienMau",
                column: "DonViId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiHienMau_DotTonVinhId",
                table: "NguoiHienMau",
                column: "DotTonVinhId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NguoiHienMau");

            migrationBuilder.DropTable(
                name: "DonVi");

            migrationBuilder.DropTable(
                name: "DotTonVinh");
        }
    }
}
