using System.ComponentModel.DataAnnotations;

namespace HoaDon_Api.Entities
{
    public class SanPham
    {
        [Key]
        public int SanPhamId { get; set; }
        public int LoaiSanPhamID { get; set; }
        public string TenSanPham { get; set; }
        public double GiaThanh { get; set; }
        public string MoTa { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public string KyHieuSanPham { get; set; }
        public virtual LoaiSanPham? LoaiSanPham { get; set; }
        public virtual List<ChiTietHoaDon>? ChiTietHoaDons { get; set; }
    }
}
