using System.ComponentModel.DataAnnotations;

namespace HoaDon_Api.Entities
{
    public class LoaiSanPham
    {
        [Key]
        public int LoaiSanPhamID { get; set; }
        public string TenLoaiSanPham { get; set; }
    }
}
