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

        // Loc du lieu
        [HttpGet]
        public IActionResult DSHoaDon()
        {
            var dsHd = _hoaDonService.LayHoaDon().OrderByDescending(c => c.Data.ThoiGianTao);
            return Ok(dsHd);
        }
        [HttpGet("dshdnamthang")]
        public IActionResult DSHoaDonTheoNamThang(int year,int month)
        {
            var dsHd = _hoaDonService.LayHoaDonTheoNamThang(year,month);
            return Ok(dsHd);
        }

        [HttpGet("dshdngay")]
        public IActionResult DSHoaDonTheoNgay(DateTime startDate, DateTime enddate)
        {
            var dsHd = _hoaDonService.LayHoaDonTheoNgay(startDate, enddate);
            return Ok(dsHd);
        }

        [HttpGet("dshdTien")]
        public IActionResult DSHoaDonTheoTien(double start, double end)
        {
            var dsHd = _hoaDonService.LayHoaDonTheoTien(start, end);
            return Ok(dsHd);
        }

        [HttpGet("layHDtheoTenHoacMGD")]
        public IActionResult HoaDonbyTenOrMGD(string text)
        {
            
            return Ok(_hoaDonService.TimHoaDonTheoMaHoacten(text));
        }

    }
}
