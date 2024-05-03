using HoaDon_Api.DataContext;
using HoaDon_Api.Entities;
using HoaDon_Api.PayLoads.DataResponses;

namespace HoaDon_Api.PayLoads.Converters
{
    public class HoaDonConverter
    {
        private readonly AppDbContext _context;
        private readonly ChiTietHoaDonConverter _CTHDConverter;
        public HoaDonConverter()
        {
            _context = new AppDbContext();
            _CTHDConverter = new ChiTietHoaDonConverter();
        }
        public DataResponseHoaDon EntityToDTO(HoaDon hoaDon)
        {
            return new DataResponseHoaDon()
            {
                TenKhachHang = _context.khachHangs.FirstOrDefault(c => c.KhachHangID == hoaDon.KhachHangId).HoTen,
                TenHoaDon = hoaDon.TenHoaDon,
                GhiChu = hoaDon.GhiChu,
                MaGiaoDich = hoaDon.MaGiaoDich,
                ThoiGianTao = hoaDon.ThoiGianTao,
                ThoiGianCapNhat = hoaDon.ThoiGianCapNhat,
                ThanhTien = hoaDon.ThanhTien,
                DataResChiTietHoaDons = _context.chiTietHoaDons.Where(x=>x.HoaDonID == hoaDon.HoaDonID).Select(x=> _CTHDConverter.DataRespomseChiTietHoaDon(x))
            };
        }
    }
}
