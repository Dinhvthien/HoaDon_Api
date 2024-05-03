using HoaDon_Api.DataContext;
using HoaDon_Api.Entities;
using HoaDon_Api.PayLoads.DataResponses;

namespace HoaDon_Api.PayLoads.Converters
{
    public class ChiTietHoaDonConverter
    {
        private readonly AppDbContext _context;
        public ChiTietHoaDonConverter()
        {
            _context = new AppDbContext();
        }
        public DataRespomseChiTietHoaDon DataRespomseChiTietHoaDon(ChiTietHoaDon chiTietHoaDon)
        {
            return new DataRespomseChiTietHoaDon()
            {
                DVT = chiTietHoaDon.DVT,
                SoLuong = chiTietHoaDon.SoLuong,
                TenSanPham = _context.sanPhams.SingleOrDefault(c => c.SanPhamId == chiTietHoaDon.SanPhamID).TenSanPham,
                ThanhTien = chiTietHoaDon.ThanhTien
            };
        }
    }
}
