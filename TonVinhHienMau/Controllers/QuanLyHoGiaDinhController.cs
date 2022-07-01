using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using TonVinhHienMau.Data;
using TonVinhHienMau.Services.Service;

namespace TonVinhHienMau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuanLyHoGiaDinhController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<QuanLyHoGiaDinhController> _logger;
        private readonly ITonVinhHoGiaDinh _tonVinhHoGiaDinh;
        public QuanLyHoGiaDinhController(AppDbContext context,
            ILogger<QuanLyHoGiaDinhController> logger,
            ITonVinhHoGiaDinh tonVinhHoGiaDinh)
        {
            _context = context;
            _logger = logger;
            _tonVinhHoGiaDinh = tonVinhHoGiaDinh;
        }

        [HttpPost("Import")]
        [Consumes("multipart/form-data")]
        public IActionResult Import(Guid DotTonVinhId, Guid DonviId, IFormFile file)
        {
            var result = _tonVinhHoGiaDinh.ImportExcel(_context, file, DonviId, DotTonVinhId);
            return new JsonResult(result);
        }

        
    }
}
