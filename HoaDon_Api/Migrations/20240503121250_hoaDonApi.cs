using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoaDon_Api.Migrations
{
    public partial class hoaDonApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "khachHangs",
                columns: table => new
                {
                    KhachHangID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_khachHangs", x => x.KhachHangID);
                });

            migrationBuilder.CreateTable(
                name: "loaiSanPhams",
                columns: table => new
                {
                    LoaiSanPhamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loaiSanPhams", x => x.LoaiSanPhamID);
                });

            migrationBuilder.CreateTable(
                name: "hoaDons",
                columns: table => new
                {
                    HoaDonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhachHangId = table.Column<int>(type: "int", nullable: false),
                    TenHoaDon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaGiaoDich = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThanhTien = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoaDons", x => x.HoaDonID);
                    table.ForeignKey(
                        name: "FK_hoaDons_khachHangs_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "khachHangs",
                        principalColumn: "KhachHangID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sanPhams",
                columns: table => new
                {
                    SanPhamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiSanPhamID = table.Column<int>(type: "int", nullable: false),
                    TenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaThanh = table.Column<double>(type: "float", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayHetHan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KyHieuSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sanPhams", x => x.SanPhamId);
                    table.ForeignKey(
                        name: "FK_sanPhams_loaiSanPhams_LoaiSanPhamID",
                        column: x => x.LoaiSanPhamID,
                        principalTable: "loaiSanPhams",
                        principalColumn: "LoaiSanPhamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chiTietHoaDons",
                columns: table => new
                {
                    ChiTietHoaDonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoaDonID = table.Column<int>(type: "int", nullable: false),
                    SanPhamID = table.Column<int>(type: "int", nullable: false),
                    DVT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    ThanhTien = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chiTietHoaDons", x => x.ChiTietHoaDonID);
                    table.ForeignKey(
                        name: "FK_chiTietHoaDons_hoaDons_HoaDonID",
                        column: x => x.HoaDonID,
                        principalTable: "hoaDons",
                        principalColumn: "HoaDonID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chiTietHoaDons_sanPhams_SanPhamID",
                        column: x => x.SanPhamID,
                        principalTable: "sanPhams",
                        principalColumn: "SanPhamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_chiTietHoaDons_HoaDonID",
                table: "chiTietHoaDons",
                column: "HoaDonID");

            migrationBuilder.CreateIndex(
                name: "IX_chiTietHoaDons_SanPhamID",
                table: "chiTietHoaDons",
                column: "SanPhamID");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDons_KhachHangId",
                table: "hoaDons",
                column: "KhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_sanPhams_LoaiSanPhamID",
                table: "sanPhams",
                column: "LoaiSanPhamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chiTietHoaDons");

            migrationBuilder.DropTable(
                name: "hoaDons");

            migrationBuilder.DropTable(
                name: "sanPhams");

            migrationBuilder.DropTable(
                name: "khachHangs");

            migrationBuilder.DropTable(
                name: "loaiSanPhams");
        }
    }
}
