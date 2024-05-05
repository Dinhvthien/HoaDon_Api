using HoaDon_Api.DataContext;
using HoaDon_Api.Entities;
using HoaDon_Api.PayLoads.Converters;
using HoaDon_Api.PayLoads.DataRequests;
using HoaDon_Api.PayLoads.DataResponses;
using HoaDon_Api.PayLoads.Responses;
using HoaDon_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HoaDon_Api.Services.Implements
{
    public class HoaDonService : IHoaDonService
    {
        private readonly AppDbContext _context;
        private readonly ResponseObject<DataResponseHoaDon> _responseObject;
        private readonly ResponseList<DataResponseHoaDon> _responseList;
        public static int page_size { get; set; } = 5;
        private readonly HoaDonConverter _hdConverter;

        public HoaDonService(ResponseObject<DataResponseHoaDon> responseObject, ResponseList<DataResponseHoaDon> responseList)
        {
            _context = new AppDbContext();
            _responseObject = responseObject;
            _hdConverter = new HoaDonConverter();
            _responseList = responseList;
        }

        public ResponseObject<DataResponseHoaDon> SuaHoaDon(int HoaDonId, Request_SuaHoaDon request_SuaHoaDon)
        {
            var hoaDon = _context.hoaDons.SingleOrDefault(c => c.HoaDonID == HoaDonId);
            if (hoaDon is null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Khong tim thấy hóa đơn", null);
            }
            hoaDon.TenHoaDon = request_SuaHoaDon.TenHoaDon;
            hoaDon.ThoiGianCapNhat = DateTime.Now;
            hoaDon.GhiChu = request_SuaHoaDon.GhiChu;
            hoaDon.ChiTietHoaDons = UpdateListHoaDon(hoaDon.HoaDonID, request_SuaHoaDon.ChiTietHoaDons);
            double tongtien = 0;
            foreach (var item in hoaDon.ChiTietHoaDons)
            {
                tongtien += item.ThanhTien;
            }
            hoaDon.ThanhTien = tongtien;
            _context.hoaDons.Update(hoaDon);
            _context.SaveChanges();
            return _responseObject.ResponseSuccses("sua hoa don thanh cong", _hdConverter.EntityToDTO(hoaDon));
        }

        private List<ChiTietHoaDon> UpdateListHoaDon(int hoaDonID, List<Request_SuaChiTietHoaDon> chiTietHoaDons)
        {
            var hoaDon = _context.hoaDons.SingleOrDefault(c => c.HoaDonID == hoaDonID);
            if (hoaDon is null)
            {
                return null;
            }
            List<ChiTietHoaDon> cthd = _context.chiTietHoaDons.Where(c => c.HoaDonID == hoaDonID).ToList();

            if (cthd.Count == 0)
            {
                throw new Exception("Chua co chi tiet hoa don");
            }

            foreach (var chiTietHoaDon in cthd)
            {
                foreach (var chiTietHoaDonnew in chiTietHoaDons)
                {
                    var sanPham = _context.sanPhams.SingleOrDefault(c => c.SanPhamId == chiTietHoaDonnew.SanPhamID);
                    if (sanPham is null)
                    {
                        throw new Exception("San pham khong ton tai");
                    }
                    if (chiTietHoaDonnew.SoLuong <= 0)
                    {
                        throw new Exception("So luong phai lon hon 0");
                    }
                    chiTietHoaDon.SanPhamID = chiTietHoaDonnew.SanPhamID;
                    chiTietHoaDon.DVT = chiTietHoaDonnew.DVT;
                    chiTietHoaDon.SoLuong = chiTietHoaDonnew.SoLuong;
                    chiTietHoaDon.ThanhTien = sanPham.GiaThanh * chiTietHoaDonnew.SoLuong;
                }
            }
            _context.chiTietHoaDons.UpdateRange(cthd);
            _context.SaveChanges();
            return cthd;
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
                      c.ThoiGianTao.Day == NgayHienTai.Day) + 1;

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

        public ResponseObject<DataResponseHoaDon> XoaHoaDon(int HoaDonId)
        {
            var hoaDon = _context.hoaDons.SingleOrDefault(c => c.HoaDonID == HoaDonId);
            if (hoaDon is null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Khong tim thấy hóa đơn", null);
            }
            _context.hoaDons.Remove(hoaDon);
            _context.SaveChanges();
            return _responseObject.ResponseSuccses("xoa hoa don thanh cong", _hdConverter.EntityToDTO(hoaDon));
        }


        // Loc du lieu
        public List<ResponseList<DataResponseHoaDon>> LayHoaDon(int page)
        {
            page = (page <= 0) ? 1 : page;
            var list = _context.hoaDons.AsQueryable().Skip((page-1)* page_size).Take(page_size);
            List<ResponseList<DataResponseHoaDon>> responseObjects = new List<ResponseList<DataResponseHoaDon>>();
            foreach (var item in list)
            {
                var responseObject = _responseList.ResponseSuccses(_hdConverter.EntityToDTO(item));
                responseObjects.Add(responseObject);
            }
            return responseObjects;
        }

        public List<ResponseList<DataResponseHoaDon>> LayHoaDonTheoNamThang(int year, int month, int page)
        {
            page = (page <= 0) ? 1 : page;
            var list = _context.hoaDons.AsQueryable().Skip((page - 1) * page_size).Take(page_size);

            if (year == 0 && month != 0)
            {
                list = list.Where(c => c.ThoiGianTao.Month == month);
            }
            if (month == 0 && year != 0)
            {
                list = list.Where(c => c.ThoiGianTao.Year == year);
            }
            if (year != 0 && month != 0)
            {
                list = list.Where(c => c.ThoiGianTao.Year == year && c.ThoiGianTao.Month == month);
            }
            if (year == 0 && month == 0)
            {
                list = list;
            }
            List<ResponseList<DataResponseHoaDon>> responseObjects = new List<ResponseList<DataResponseHoaDon>>();
            foreach (var item in list)
            {
                var responseObject = _responseList.ResponseSuccses(_hdConverter.EntityToDTO(item));
                responseObjects.Add(responseObject);
            }
            return responseObjects;
        }

        public List<ResponseList<DataResponseHoaDon>> LayHoaDonTheoNgay(DateTime Ngaydau, DateTime ngaykt, int page)
        {
            page = (page <= 0) ? 1 : page;
            var list = _context.hoaDons.AsQueryable().Skip((page - 1) * page_size).Take(page_size);
            if (Ngaydau == DateTime.MinValue && ngaykt == DateTime.MinValue)
            {
                list = list;
            }
            if (Ngaydau > ngaykt)
            {
                list = null;
            }
            if (Ngaydau < ngaykt)
            {
                list = list.Where(c => c.ThoiGianTao.Date <= ngaykt.Date && c.ThoiGianTao.Date >= Ngaydau.Date);
            }
            if (Ngaydau == ngaykt)
            {
                list = list.Where(c => c.ThoiGianTao.Date == ngaykt.Date);
            }
            List<ResponseList<DataResponseHoaDon>> responseObjects = new List<ResponseList<DataResponseHoaDon>>();
            foreach (var item in list)
            {
                var responseObject = _responseList.ResponseSuccses(_hdConverter.EntityToDTO(item));
                responseObjects.Add(responseObject);
            }
            return responseObjects;
        }

        public List<ResponseList<DataResponseHoaDon>> LayHoaDonTheoTien(double start, double end, int page)
        {
            page = (page <= 0) ? 1 : page;
            var list = _context.hoaDons.AsQueryable().Skip((page - 1) * page_size).Take(page_size);
            if (start > end)
            {
                list = null;
            }
            if (start == end)
            {
                list = list.Where(c => c.ThanhTien == end);
            }
            else
            {
                list = list.Where(c => c.ThanhTien >= start && c.ThanhTien < end);
            }
            List<ResponseList<DataResponseHoaDon>> responseObjects = new List<ResponseList<DataResponseHoaDon>>();
            foreach (var item in list)
            {
                var responseObject = _responseList.ResponseSuccses(_hdConverter.EntityToDTO(item));
                responseObjects.Add(responseObject);
            }
            return responseObjects;
        }

        public List<ResponseList<DataResponseHoaDon>> TimHoaDonTheoMaHoacten(string text, int page)
        {
            page = (page <= 0) ? 1 : page;
            var list = _context.hoaDons.AsQueryable().Skip((page - 1) * page_size).Take(page_size);
            if (!string.IsNullOrEmpty(text))
            {
                list = list.Where(c => c.TenHoaDon == text || c.MaGiaoDich == text);
            }
            List<ResponseList<DataResponseHoaDon>> responseObjects = new List<ResponseList<DataResponseHoaDon>>();
            foreach (var item in list)
            {
                var responseObject = _responseList.ResponseSuccses(_hdConverter.EntityToDTO(item));
                responseObjects.Add(responseObject);
            }
            return responseObjects;
        }
    }
}
