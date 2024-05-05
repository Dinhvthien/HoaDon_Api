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
        public IActionResult SuaHoaDon(int HoaDonId, Request_SuaHoaDon request)
        {
            return Ok(_hoaDonService.SuaHoaDon(HoaDonId, request));
        }
        [HttpDelete]
        public IActionResult XoaHoaDon(int HoaDonId)
        {
            return Ok(_hoaDonService.XoaHoaDon(HoaDonId));
        }

        // Loc du lieu
        [HttpGet]
        public IActionResult DSHoaDon(int page)
        {
            var dsHd = _hoaDonService.LayHoaDon(page).OrderByDescending(c => c.Data.ThoiGianTao);
            return Ok(dsHd);
        }
        [HttpGet("dshdnamthang")]
        public IActionResult DSHoaDonTheoNamThang(int year, int month, int page)
        {
            try
            {
                if (month > 12 || month < 1)
                {
                    return BadRequest("Ban nhap thong tin sai");
                }
                var dsHd = _hoaDonService.LayHoaDonTheoNamThang(year, month, page);
                return Ok(dsHd);
            }
            catch (Exception)
            {
                return BadRequest("Co loi xay ra");
            }
        }

        [HttpGet("dshdngay")]
        public IActionResult DSHoaDonTheoNgay(DateTime startDate, DateTime enddate, int page)
        {
            try
            {
                var dsHd = _hoaDonService.LayHoaDonTheoNgay(startDate, enddate, page);
                return Ok(dsHd);
            }
            catch (Exception)
            {
                return BadRequest("Co loi xay ra");
            }
        }

        [HttpGet("dshdTien")]
        public IActionResult DSHoaDonTheoTien(double start, double end, int page)
        {
            try
            {
                if (start > end)
                {
                    return BadRequest("So tien ban nhap khong chinh xac");
                }
                if (start <=0 && end <=0)
                {
                     return BadRequest("So tien ban nhap khong chinh xac");
                }
                var dsHd = _hoaDonService.LayHoaDonTheoTien(start, end, page);
                return Ok(dsHd);
            }
            catch (Exception)
            {

                return BadRequest("Co loi xay ra");
            }
        }

        [HttpGet("layHDtheoTenHoacMGD")]
        public IActionResult HoaDonbyTenOrMGD(string search, int page)
        {
            try
            {
                return Ok(_hoaDonService.TimHoaDonTheoMaHoacten(search, page));
            }
            catch (Exception)
            {
                return BadRequest("Co loi xay ra");
            }
        }

    }
}
