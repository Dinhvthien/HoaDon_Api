using HoaDon_Api.PayLoads.DataRequests;
using HoaDon_Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HoaDon_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonService _hoaDonService;
        public HoaDonController(IHoaDonService hoaDonService)
        {
            _hoaDonService = hoaDonService;
        }
        [HttpPost("ThemHoaDon")]
        public IActionResult ThemHoaDon(Request_ThemHoaDon request)
        {
            return Ok(_hoaDonService.ThemHoaDon(request));
        }

        [HttpPut]
        public IActionResult SuaHoaDon(int HoaDonId,Request_SuaHoaDon request)
        {
            return Ok(_hoaDonService.SuaHoaDon(HoaDonId,request));
        }
        [HttpDelete]
        public IActionResult XoaHoaDon(int HoaDonId)
        {
            return Ok(_hoaDonService.XoaHoaDon(HoaDonId));
        }
    }
}
