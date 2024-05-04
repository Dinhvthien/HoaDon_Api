namespace HoaDon_Api.PayLoads.DataRequests
{
    public class Request_SuaHoaDon
    {
        public string TenHoaDon { get; set; }
        public string GhiChu { get; set; }
        public List<Request_SuaChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
