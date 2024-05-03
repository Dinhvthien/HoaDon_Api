using System.ComponentModel.DataAnnotations;

namespace HoaDon_Api.Entities
{
    public class ChiTietHoaDon
    {
        [Key]
        public int ChiTietHoaDonID { get; set; }
        public int HoaDonID { get; set; }
        public int SanPhamID { get; set; }
        public string DVT { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien { get; set; }
        public virtual HoaDon? HoaDon { get; set; }
        public virtual SanPham? SanPham { get; set; }
    }
}
