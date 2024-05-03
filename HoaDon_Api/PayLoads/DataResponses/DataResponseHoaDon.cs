using HoaDon_Api.Entities;

namespace HoaDon_Api.PayLoads.DataResponses
{
    public class DataResponseHoaDon
    {
        public string TenKhachHang { get; set; }
        public string TenHoaDon { get; set; }
        public string MaGiaoDich { get; set; }
        public DateTime ThoiGianTao { get; set; }
        public DateTime? ThoiGianCapNhat { get; set; }
        public string GhiChu { get; set; }
        public double? ThanhTien { get; set; }
        public IQueryable<DataRespomseChiTietHoaDon> DataResChiTietHoaDons { get; set; }

    }
}
