using HoaDon_Api.Entities;

namespace HoaDon_Api.PayLoads.DataRequests
{
    public class Request_ThemChiTietHoaDon
    {
        public int SanPhamID { get; set; }
        public string DVT { get; set; }
        public int SoLuong { get; set; }
    }
}
