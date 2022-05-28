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
    public class DotTonVinhController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DotTonVinhController> _logger;
        public DotTonVinhController(AppDbContext context,
            ILogger<DotTonVinhController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return new JsonResult(_context.DotTonVinh.Where(u=>u.IsDeleted!= true));
        }

        [HttpPost("create")]
        public IActionResult Create(string madottonvinh)
        {
            if (!string.IsNullOrEmpty(madottonvinh))
            {
                DotTonVinh dottv = new DotTonVinh()
                {
                    Id = Guid.NewGuid(),
                    TenDotTonVinh = "Năm " + madottonvinh,
                    MaDotTonVinh = madottonvinh,
                    IsDeleted = false
                };
                _context.DotTonVinh.Add(dottv);
                _context.SaveChanges();
            }
            return new JsonResult("Success");
        }

        [HttpPost("edit")]
        public IActionResult Edit(DottonvinhVM dottonvinhVM)
        {
            var dv = _context.DotTonVinh.FirstOrDefault(u => u.Id.Equals(dottonvinhVM.Id));
            if (!string.IsNullOrEmpty(dottonvinhVM.Name))
            {
                dv.TenDotTonVinh = dottonvinhVM.Name;
                _context.DotTonVinh.Update(dv);
            }
            if (!string.IsNullOrEmpty(dottonvinhVM.Code))
            {
                dv.MaDotTonVinh = dottonvinhVM.Code;
                _context.DotTonVinh.Update(dv);
            }
            _context.SaveChanges();
            return new JsonResult("Success");
        }

        [HttpPut("delete")]
        public IActionResult Delete(Guid DotTonVinhId)
        {
            var dv = _context.DotTonVinh.FirstOrDefault(u => u.Id.Equals(DotTonVinhId));
            dv.IsDeleted = true;
            _context.DotTonVinh.Update(dv);
            _context.SaveChanges();
            return new JsonResult("Success");
        }
    }
}
