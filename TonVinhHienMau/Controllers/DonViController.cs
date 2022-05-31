using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using TonVinhHienMau.Data;
using TonVinhHienMau.Models;
using TonVinhHienMau.Models.ViewModels;

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
            return new JsonResult(_context.DonVi.Where(u=>u.IsDeleted!= true).OrderBy(u => u.TenDonVi));
        }

        [HttpPost("create")]
        public IActionResult Create(DonViVM donViVM)
        {
            if (!string.IsNullOrEmpty(donViVM.TenDonVi) && !string.IsNullOrEmpty(donViVM.MaDonVi))
            {
                DonVi dv = new DonVi()
                {
                    Id = Guid.NewGuid(),
                    TenDonVi = donViVM.TenDonVi,
                    MaDonVi = donViVM.MaDonVi,
                    Diachi = donViVM.Diachi,
                    IsDeleted = false
                };
                _context.DonVi.Add(dv);
                _context.SaveChanges();
            }
            return new JsonResult("Success");
        }

        [HttpPost("edit")]
        public IActionResult Edit(DonViVM donViVM)
        {
            var dv = _context.DonVi.FirstOrDefault(u => u.Id.Equals(donViVM.Id));
            if (!string.IsNullOrEmpty(donViVM.TenDonVi))
            {
                dv.TenDonVi = donViVM.TenDonVi;
                _context.DonVi.Update(dv);
            }
            if (!string.IsNullOrEmpty(donViVM.MaDonVi))
            {
                dv.MaDonVi = donViVM.MaDonVi;
                _context.DonVi.Update(dv);
            }
            if (!string.IsNullOrEmpty(donViVM.Diachi))
            {
                dv.Diachi = donViVM.Diachi;
                _context.DonVi.Update(dv);
            }
            _context.SaveChanges();
            return new JsonResult("Success");
        }

        [HttpPut("delete")]
        public IActionResult Delete(Guid DonViId)
        {
            var dv = _context.DonVi.FirstOrDefault(u => u.Id.Equals(DonViId));
            if(dv != null)
            {
                dv.IsDeleted = true;
                _context.DonVi.Update(dv);
                _context.SaveChanges();
            }
            return new JsonResult("Success");
        }

    }
}
