using HoaDon_Api.Entities;

namespace HoaDon_Api.PayLoads.DataRequests
{
    public class Request_DSHoaDon
    {
        public int KhachHangId { get; set; }
        public string TenHoaDon { get; set; }
        public string MaGiaoDich { get; set; }
        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
        public string GhiChu { get; set; }
        public double ThanhTien { get; set; }
        public List<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
