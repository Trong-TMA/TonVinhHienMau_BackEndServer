using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using TonVinhHienMau.Data;
using TonVinhHienMau.Models;
using TonVinhHienMau.Models.ViewModels;
using TonVinhHienMau.Services.Service;

namespace TonVinhHienMau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuanLyNguoiHienMauController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TonVinhNguoiHienMauController> _logger;
        private readonly IHienMau _hienMau;

        public QuanLyNguoiHienMauController(AppDbContext context,
           ILogger<TonVinhNguoiHienMauController> logger, ITonVinhHienMau tonVinhHienMau, IHienMau hienMau)
        {
            _context = context;
            _logger = logger;
            _hienMau = hienMau;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var nguoihienmau = _context.NguoiHienMau.Select(u=> new HienMauVM()
            {
                Id = u.Id,
                HoTen = u.HoTen,
                GioiTinh = u.GioiTinh,
                NamSinh = u.NamSinh,
                NgheNghiep = u.NgheNghiep,
                DiaChi = u.DiaChi,
                NhomMau = u.NhomMau,
                TV = _hienMau.getHightTV(_context,u.Id),
            }).OrderBy(u=>u.HoTen);
            return new JsonResult(nguoihienmau);
        }


        [HttpPost("AddFromExceltoData")]
        [Consumes("multipart/form-data")]
        public IActionResult importPeople(Guid DottonvinhId, Guid DonViId, IFormFile file)
        {
            var DonVi =  _context.DonVi.Where(u => u.Id.Equals(DonViId) && u.IsDeleted != true).FirstOrDefault();
            var Dottonvinh = _context.DotTonVinh.Where(u => u.Id.Equals(DottonvinhId) && u.IsDeleted != true).FirstOrDefault();
            List<NguoiHienMauVm> list_result = new List<NguoiHienMauVm>();

            if (file?.Length > 0)
            {
                // convert to a stream
                var stream = file.OpenReadStream();
                using (var package = new ExcelPackage(stream))
                {


                    var worksheet = package.Workbook.Worksheets.First();
                    var rowCount = worksheet.Dimension.Rows;
                    for (var row = 4; row <= rowCount; row++)
                    {
                        var hoten = worksheet.Cells[row, 2].Value?.ToString().Trim();
                        var gioitinh = worksheet.Cells[row, 3].Value?.ToString().Trim();
                        var namsinh = worksheet.Cells[row, 4].Value?.ToString().Trim();
                        var nghenghiep = worksheet.Cells[row, 5].Value?.ToString().Trim();
                        var diachi = worksheet.Cells[row, 6].Value?.ToString().Trim();
                        var nhomau = worksheet.Cells[row, 7].Value?.ToString().Trim();

                        if (hoten != null && namsinh != null && nhomau !=null)
                        {
                            NguoiHienMauVm nguoiHienMauVm = new NguoiHienMauVm()
                            {
                                HoTen = hoten,
                                GioiTinh = Convert.ToBoolean(gioitinh),
                                NamSinh = Convert.ToInt32(namsinh),
                                NgheNghiep = nghenghiep,
                                DiaChi = diachi,
                                NhomMau = nhomau,
                                Note = "",
                            };
                            var MaNguoiHien = _hienMau.NonUnicode(hoten) + _hienMau.NonUnicode(namsinh) + _hienMau.NonUnicode(nhomau);
                            var nguoiHM = _context.NguoiHienMau.FirstOrDefault(s => s.MaNguoiHien.Equals(MaNguoiHien) && s.DonViId.Equals(DonViId) && s.DotTonVinhId.Equals(DottonvinhId));

                            if (nguoiHM == null)
                            {
                                nguoiHienMauVm.Note = "Thêm mới";
                                list_result.Add(nguoiHienMauVm);
                            }
                            else
                            {
                                if (!nguoiHienMauVm.GioiTinh.Equals(nguoiHM.GioiTinh)) { nguoiHM.GioiTinh = nguoiHienMauVm.GioiTinh; nguoiHienMauVm.Note = nguoiHienMauVm.Note + "Đã cập nhật giới tính"; }
                                if (!nguoiHienMauVm.NgheNghiep.Equals(nguoiHM.NgheNghiep)) { nguoiHM.NgheNghiep = nguoiHienMauVm.NgheNghiep; nguoiHienMauVm.Note = nguoiHienMauVm.Note + "Đã cập nhật nghề nghiệp"; }
                                if (!nguoiHienMauVm.DiaChi.Equals(nguoiHM.DiaChi)) { nguoiHM.DiaChi = nguoiHienMauVm.DiaChi; _context.NguoiHienMau.Update(nguoiHM); nguoiHienMauVm.Note = nguoiHienMauVm.Note + "Đã cập nhật địa chỉ"; }
                                list_result.Add(nguoiHienMauVm);
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

            }
            return new JsonResult(list_result);
        }


        [HttpPost("SaveChanges")]
        public IActionResult importPeople(Guid DottonvinhId, Guid DonViId, List<NguoiHienMauVm> nguoiHienMauVms)
        {
            foreach(var item in nguoiHienMauVms)
            {
                NguoiHienMau nguoiHienMau = new NguoiHienMau()
                {
                    Id = Guid.NewGuid(),
                    MaNguoiHien = _hienMau.NonUnicode(item.HoTen) + _hienMau.NonUnicode(item.NamSinh.ToString()) + _hienMau.NonUnicode(item.NhomMau),
                    HoTen = item.HoTen,
                    GioiTinh = Convert.ToBoolean(item.GioiTinh),
                    NamSinh = Convert.ToInt32(item.NamSinh),
                    NgheNghiep = item.NgheNghiep,
                    DiaChi = item.DiaChi,
                    NhomMau = item.NhomMau,
                    TV_5 = "0",
                    TV_10 = "0",
                    TV_15 = "0",
                    TV_20 = "0",
                    TV_30 = "0",
                    TV_40 = "0",
                    TV_50 = "0",
                    TV_60 = "0",
                    TV_70 = "0",
                    TV_80 = "0",
                    TV_90 = "0",
                    TV_100 = "0",
                    DonViId = DonViId,
                    DotTonVinhId = DottonvinhId
                };
                var nguoiHM = _context.NguoiHienMau.FirstOrDefault(s => s.MaNguoiHien.Equals(nguoiHienMau.MaNguoiHien) && s.DonViId.Equals(DonViId) && s.DotTonVinhId.Equals(DottonvinhId));

                if (nguoiHM == null)
                {
                    _context.NguoiHienMau.Add(nguoiHienMau);
                }
                else
                {
                    if (!item.GioiTinh.Equals(nguoiHM.GioiTinh)) { nguoiHM.GioiTinh = item.GioiTinh; _context.NguoiHienMau.Update(nguoiHM); }
                    if (!item.NgheNghiep.Equals(nguoiHM.NgheNghiep)) { nguoiHM.NgheNghiep = item.NgheNghiep; _context.NguoiHienMau.Update(nguoiHM); }
                    if (!item.DiaChi.Equals(nguoiHM.DiaChi)) { nguoiHM.DiaChi = item.DiaChi; _context.NguoiHienMau.Update(nguoiHM); }
                }
                _context.SaveChanges();
            }
            return new JsonResult("Sucess");
        }

        [HttpPost("Create")]
        public IActionResult CreateNguoiHienMau(Guid DottonvinhId, Guid DonViId, NguoiHienMauVm hienMauVM)
        {
            var maNguoiHien = _hienMau.NonUnicode(hienMauVM.HoTen) + _hienMau.NonUnicode(hienMauVM.NamSinh.ToString()) + _hienMau.NonUnicode(hienMauVM.NhomMau);
            var check = _context.NguoiHienMau.FirstOrDefault(u=>u.MaNguoiHien.Equals(maNguoiHien));
            if(check == null)
            {
                NguoiHienMau nguoiHienMau = new NguoiHienMau()
                {
                    Id = Guid.NewGuid(),
                    MaNguoiHien = maNguoiHien,
                    HoTen = hienMauVM.HoTen,
                    GioiTinh = Convert.ToBoolean(hienMauVM.GioiTinh),
                    NamSinh = Convert.ToInt32(hienMauVM.NamSinh),
                    NgheNghiep = hienMauVM.NgheNghiep,
                    DiaChi = hienMauVM.DiaChi,
                    NhomMau = hienMauVM.NhomMau,
                    TV_5 = hienMauVM.TV_5,
                    TV_10 = hienMauVM.TV_10,
                    TV_15 = hienMauVM.TV_15,
                    TV_20 = hienMauVM.TV_20,
                    TV_30 = hienMauVM.TV_30,
                    TV_40 = hienMauVM.TV_40,
                    TV_50 = hienMauVM.TV_50,
                    TV_60 = hienMauVM.TV_60,
                    TV_70 = hienMauVM.TV_70,
                    TV_80 = hienMauVM.TV_80,
                    TV_90 = hienMauVM.TV_90,
                    TV_100 = hienMauVM.TV_100,
                    DonViId = DonViId,
                    DotTonVinhId = DottonvinhId
                };
                _context.NguoiHienMau.Add(nguoiHienMau);
                _context.SaveChanges();
                return new JsonResult("Sucess");

            }
            else
            {
                return new JsonResult("Existed");
            }
        }

        [HttpPost("Edit")]
        public IActionResult EditNguoiHienMau(HienMauVM hienMauVM)
        {
            var ng = _context.NguoiHienMau.FirstOrDefault(u=>u.Id.Equals(hienMauVM.Id));
            if(ng == null)
            {
                if (!ng.HoTen.Equals(hienMauVM.HoTen))
                {
                    ng.HoTen = hienMauVM.HoTen; 
                }
                if (!ng.GioiTinh.Equals(hienMauVM.GioiTinh))
                {
                    ng.GioiTinh = hienMauVM.GioiTinh;
                }
                if (!ng.NamSinh.Equals(hienMauVM.NamSinh))
                {
                    ng.NamSinh = hienMauVM.NamSinh;
                }
                if (!ng.NgheNghiep.Equals(hienMauVM.NgheNghiep))
                {
                    ng.NgheNghiep = hienMauVM.NgheNghiep;
                }
                if (!ng.DiaChi.Equals(hienMauVM.DiaChi))
                {
                    ng.DiaChi = hienMauVM.DiaChi;
                }
                if (!ng.NhomMau.Equals(hienMauVM.NhomMau))
                {
                    ng.NamSinh = hienMauVM.NamSinh;
                }
                ng.MaNguoiHien = _hienMau.NonUnicode(hienMauVM.HoTen) + _hienMau.NonUnicode(hienMauVM.NamSinh.ToString()) + _hienMau.NonUnicode(hienMauVM.NhomMau);
                _context.NguoiHienMau.Update(ng);
                _context.SaveChanges();
            }
            return new JsonResult("Sucess");
        }

        [HttpPut("Delete")]
        public IActionResult DeleteNguoiHienMau(Guid NguoihienmauId)
        {
            var ng = _context.NguoiHienMau.FirstOrDefault(u => u.Id.Equals(NguoihienmauId));
            if (ng != null)
            {
                _context.NguoiHienMau.Remove(ng);
                _context.SaveChanges();
            }
            return new JsonResult("Sucess");
        }

        [HttpGet("Search")]
        public IActionResult Search(string searchString,bool gioitinh,string namsinh)
        {
            
            var nguoihienau = _context.NguoiHienMau.ToList();
            nguoihienau = nguoihienau.Where(u=>u.GioiTinh.Equals(gioitinh)).ToList();
            if (searchString != "null")
            {
                nguoihienau = nguoihienau.Where(u =>
                    u.HoTen.ToLower().Contains(searchString.ToLower())
                    || u.NgheNghiep.ToLower().Contains(searchString.ToLower())
                    || u.DiaChi.ToLower().Contains(searchString.ToLower())
                    || u.NhomMau.ToLower().Contains(searchString.ToLower())
                ).ToList();
            }
            if (namsinh != "Invalid date" && namsinh != DateTime.Now.Year.ToString())
            {        
                nguoihienau = nguoihienau.Where(u=>u.NamSinh.Equals(Int32.Parse(namsinh))).ToList();
            }
            return new JsonResult(nguoihienau.Select(u => new HienMauVM()
                {
                    Id = u.Id,
                    HoTen = u.HoTen,
                    GioiTinh = u.GioiTinh,
                    NamSinh = u.NamSinh,
                    NgheNghiep = u.NgheNghiep,
                    DiaChi = u.DiaChi,
                    NhomMau = u.NhomMau,
                    TV = _hienMau.getHightTV(_context, u.Id),
                }).OrderBy(u => u.HoTen)
            );
        }

    }
}
