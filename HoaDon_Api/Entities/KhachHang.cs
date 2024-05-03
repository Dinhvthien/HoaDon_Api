using System.ComponentModel.DataAnnotations;

namespace HoaDon_Api.Entities
{
    public class KhachHang
    {
        [Key]
        public int KhachHangID { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoDienThoai { get; set; }
        public virtual List<HoaDon>? HoaDons { get; set; }
    }
}
