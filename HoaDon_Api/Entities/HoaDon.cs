using System.ComponentModel.DataAnnotations;

namespace HoaDon_Api.Entities
{
    public class HoaDon
    {
        [Key]
        public int HoaDonID { get; set; }
        public int KhachHangId { get; set; }
        public string TenHoaDon { get; set; }
        public string MaGiaoDich { get; set; }
        public DateTime ThoiGianTao { get; set; }
        public DateTime? ThoiGianCapNhat { get; set; }
        public string GhiChu { get; set; }
        public double? ThanhTien { get; set; }
        public virtual KhachHang? KhachHang { get; set; }
        public virtual List<ChiTietHoaDon>? ChiTietHoaDons { get; set; }
    }
}
