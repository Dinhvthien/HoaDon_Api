using HoaDon_Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace HoaDon_Api.DataContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<HoaDon> hoaDons { get; set; }
        public DbSet<ChiTietHoaDon> chiTietHoaDons { get; set; }
        public DbSet<KhachHang> khachHangs { get; set; }
        public DbSet<LoaiSanPham> loaiSanPhams { get; set; }
        public DbSet<SanPham> sanPhams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer($"Server=DESKTOP-JLCKB92\\THIENCHINTE12;database=WA_API;trusted_connection=true;");
        }
    }
}
