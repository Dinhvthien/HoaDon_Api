using HoaDon_Api.Entities;
using HoaDon_Api.PayLoads.DataRequests;
using HoaDon_Api.PayLoads.DataResponses;
using HoaDon_Api.PayLoads.Responses;

namespace HoaDon_Api.Services.Interfaces
{
    public interface IHoaDonService
    {
        ResponseObject<DataResponseHoaDon> ThemHoaDon(Request_ThemHoaDon request_ThemHoaDon);
        ResponseObject<DataResponseHoaDon> SuaHoaDon(int HoaDonId, Request_SuaHoaDon request_SuaHoaDon);
        ResponseObject<DataResponseHoaDon> XoaHoaDon(int HoaDonId);


        // LOC DU LIEU 
        public List<ResponseList<DataResponseHoaDon>> LayHoaDon(int page = 1);

        public List<ResponseList<DataResponseHoaDon>> LayHoaDonTheoNamThang(int year, int month, int page);

        //Lấy hóa đơn được tạo từ ngày ... đến ngày
        public List<ResponseList<DataResponseHoaDon>> LayHoaDonTheoNgay(DateTime Ngaydau, DateTime ngaykt, int page);

        // Lấy hóa đơn theo tổng tiền từ XXXX -> XXXX
        public List<ResponseList<DataResponseHoaDon>> LayHoaDonTheoTien(double start, double end, int page);

        //Tìm kiếm hóa đơn theo Mã giao dịch hoặc tên hóa đơn

        public List<ResponseList<DataResponseHoaDon>> TimHoaDonTheoMaHoacten(string text, int page);

    }
}
