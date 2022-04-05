using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TonVinhHienMau.Data;

namespace TonVinhHienMau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TonVinhNguoiHienMauController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TonVinhNguoiHienMauController> _logger;

        public TonVinhNguoiHienMauController(AppDbContext context,
            ILogger<TonVinhNguoiHienMauController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {

            return Ok();
        }
    }
}
