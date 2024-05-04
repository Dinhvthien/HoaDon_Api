namespace HoaDon_Api.PayLoads.DataRequests
{
    public class Request_ThemHoaDon
    {
        public int KhachHangId { get; set; }
        public string TenHoaDon { get; set; }
        public string GhiChu { get; set; }
        public List<Request_ThemChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
