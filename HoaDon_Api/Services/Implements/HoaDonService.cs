using HoaDon_Api.DataContext;
using HoaDon_Api.Entities;
using HoaDon_Api.PayLoads.Converters;
using HoaDon_Api.PayLoads.DataRequests;
using HoaDon_Api.PayLoads.DataResponses;
using HoaDon_Api.PayLoads.Responses;
using HoaDon_Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HoaDon_Api.Services.Implements
{
    public class HoaDonService : IHoaDonService
    {
        private readonly AppDbContext _context;
        private readonly ResponseObject<DataResponseHoaDon> _responseObject;
        private readonly ResponseObject<DataRespomseChiTietHoaDon> _responseCthdObject;

        private readonly HoaDonConverter _hdConverter;

        public HoaDonService(ResponseObject<DataResponseHoaDon> responseObject, HoaDonConverter hoaDonConverter)
        {
            _context = new AppDbContext();
            _responseObject = responseObject;
            _hdConverter = hoaDonConverter;

        }

        public ResponseObject<DataResponseHoaDon> ThemHoaDon(Request_ThemHoaDon request_ThemHoaDon)
        {
            var KhachHang = _context.khachHangs.SingleOrDefault(c => c.KhachHangID == request_ThemHoaDon.KhachHangId);
            if (KhachHang is null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Khong tim thấy khách hàng", null);
            }
            DateTime NgayHienTai = DateTime.Now;
            int index = _context.hoaDons
             .Count(c => c.ThoiGianTao.Year == NgayHienTai.Year &&
                      c.ThoiGianTao.Month == NgayHienTai.Month &&
                      c.ThoiGianTao.Day == NgayHienTai.Day &&
                      c.ThoiGianTao.Hour == NgayHienTai.Hour &&
                      c.ThoiGianTao.Minute == NgayHienTai.Minute);
            index++;
            HoaDon hoaDon = new HoaDon();
            hoaDon.TenHoaDon = request_ThemHoaDon.TenHoaDon;
            hoaDon.KhachHangId = request_ThemHoaDon.KhachHangId;
            hoaDon.MaGiaoDich = $"{NgayHienTai:yyyyMMdd}_{index:000}";
            hoaDon.ThoiGianTao = DateTime.Now;
            hoaDon.ThoiGianCapNhat = DateTime.Now;
            hoaDon.GhiChu = request_ThemHoaDon.GhiChu;
            hoaDon.ThanhTien = 0;
            hoaDon.ChiTietHoaDons = null;
            _context.hoaDons.Add(hoaDon);
            _context.SaveChanges();
            hoaDon.ChiTietHoaDons = ThemListCTHD(hoaDon.HoaDonID, request_ThemHoaDon.ChiTietHoaDons);
            _context.hoaDons.Update(hoaDon);
            _context.SaveChanges();
            double tongtien = 0;
            foreach (var item in hoaDon.ChiTietHoaDons)
            {
                tongtien += item.ThanhTien;
            }
            hoaDon.ThanhTien = tongtien;
            _context.hoaDons.Update(hoaDon);
            _context.SaveChanges();
            return _responseObject.ResponseSuccses("Them hoa don thanh cong", _hdConverter.EntityToDTO(hoaDon));
        }

        private List<ChiTietHoaDon> ThemListCTHD(int hoaDonID, List<Request_ThemChiTietHoaDon> chiTietHoaDons)
        {
            var hoaDon = _context.hoaDons.SingleOrDefault(c => c.HoaDonID == hoaDonID);
            if (hoaDon is null)
            {
                return null;
            }
            List<ChiTietHoaDon> list = new List<ChiTietHoaDon>();
            foreach (var chiTietHoaDon in chiTietHoaDons)
            {
                ChiTietHoaDon cthd = new ChiTietHoaDon();
                var sanPham = _context.sanPhams.SingleOrDefault(c => c.SanPhamId == chiTietHoaDon.SanPhamID);
                if (sanPham is null)
                {
                    throw new Exception("San pham khong ton tai");
                }
                cthd.HoaDonID = hoaDonID;
                cthd.SanPhamID = chiTietHoaDon.SanPhamID;
                cthd.DVT = chiTietHoaDon.DVT;
                cthd.SoLuong = chiTietHoaDon.SoLuong;
                cthd.ThanhTien = sanPham.GiaThanh * chiTietHoaDon.SoLuong;
                list.Add(cthd);
            }
            _context.chiTietHoaDons.AddRange(list);
            _context.SaveChanges();
            return list;
        }
    }
}
