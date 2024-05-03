using HoaDon_Api.PayLoads.DataRequests;
using HoaDon_Api.PayLoads.DataResponses;
using HoaDon_Api.PayLoads.Responses;

namespace HoaDon_Api.Services.Interfaces
{
    public interface IHoaDonService
    {
        ResponseObject<DataResponseHoaDon> ThemHoaDon(Request_ThemHoaDon request_ThemHoaDon);
    }
}
