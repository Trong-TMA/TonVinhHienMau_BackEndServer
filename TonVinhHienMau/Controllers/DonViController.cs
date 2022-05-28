using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using TonVinhHienMau.Data;
using TonVinhHienMau.Models;

namespace TonVinhHienMau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonViController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DonViController> _logger;
        public DonViController(AppDbContext context,
            ILogger<DonViController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return new JsonResult(_context.DonVi.Where(u=>u.IsDeleted!= true));
        }

        [HttpPost("create")]
        public IActionResult Create(DonVi donVi)
        {

            DonVi dv = new DonVi()
            {
                Id = Guid.NewGuid(),
                TenDonVi = donVi.TenDonVi,
                MaDonVi = donVi.MaDonVi,
                Diachi = donVi.Diachi,
                IsDeleted = false
            };
            _context.DonVi.Add(dv);
            _context.SaveChanges();
            return new JsonResult("Success");
        }

        [HttpPost("edit")]
        public IActionResult Edit(Guid DonViId,string name, string diachi, string madonvi)
        {
            var dv = _context.DonVi.FirstOrDefault(u => u.Id.Equals(DonViId));
            _context.DonVi.Update(dv);
            _context.SaveChanges();
            return new JsonResult("Success");
        }

        [HttpPut("delete")]
        public IActionResult Delete(Guid DonViId)
        {
            var dv = _context.DonVi.FirstOrDefault(u => u.Id.Equals(DonViId));
            dv.IsDeleted = true;
            _context.DonVi.Update(dv);
            _context.SaveChanges();
            return new JsonResult("Success");
        }

    }
}
