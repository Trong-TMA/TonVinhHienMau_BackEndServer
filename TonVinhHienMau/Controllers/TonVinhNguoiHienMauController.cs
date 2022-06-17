using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using TonVinhHienMau.Data;
using TonVinhHienMau.Models;
using TonVinhHienMau.Models.ViewModels;
using TonVinhHienMau.Services.Service;

namespace TonVinhHienMau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TonVinhNguoiHienMauController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TonVinhNguoiHienMauController> _logger;
        private readonly ITonVinhHienMau _tonVinhHienMau;
        private readonly IHienMau _hienMau;

        public TonVinhNguoiHienMauController(AppDbContext context,
            ILogger<TonVinhNguoiHienMauController> logger, ITonVinhHienMau tonVinhHienMau, IHienMau hienMau)
        {
            _context = context;
            _logger = logger;
            _tonVinhHienMau = tonVinhHienMau;
            _hienMau = hienMau;
        }


        [HttpPost("Import")]
        [Consumes("multipart/form-data")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Import(Guid DotTonVinhId, Guid DonviId, IFormFile file)
        {
            var result = _tonVinhHienMau.ImportExcel(_context,file,DonviId,DotTonVinhId);
            return new JsonResult(result);
        }

        [HttpPost("SaveChanges")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult SaveChanges(Guid DottonvinhId, Guid DonViId, List<NguoiHienMauVm> nguoiHienMauVms)
        {
            foreach (var item in nguoiHienMauVms)
            {
                var MaNguoiHien = _hienMau.NonUnicode(item.HoTen) + _hienMau.NonUnicode(item.NamSinh.ToString()) + _hienMau.NonUnicode(item.NhomMau);
                var nguoiHM = _context.NguoiHienMau.FirstOrDefault(s => s.MaNguoiHien.Equals(MaNguoiHien) && s.DonViId.Equals(DonViId) && s.DotTonVinhId.Equals(DottonvinhId));

                if (nguoiHM == null)
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
                        TV_5 = item.TV_5,
                        TV_10 = item.TV_10,
                        TV_15 = item.TV_15,
                        TV_20 = item.TV_20,
                        TV_30 = item.TV_30,
                        TV_40 = item.TV_40,
                        TV_50 = item.TV_50,
                        TV_60 = item.TV_60,
                        TV_70 = item.TV_70,
                        TV_80 = item.TV_80,
                        TV_90 = item.TV_90,
                        TV_100 = item.TV_100,
                        DonViId = DonViId,
                        DotTonVinhId = DottonvinhId
                    };
                    _context.NguoiHienMau.Add(nguoiHienMau);
                }
                else
                {

                    if (!item.GioiTinh.Equals(nguoiHM.GioiTinh)) { nguoiHM.GioiTinh = item.GioiTinh; _context.NguoiHienMau.Update(nguoiHM); }
                    if (!item.NgheNghiep.Equals(nguoiHM.NgheNghiep)) { nguoiHM.NgheNghiep = item.NgheNghiep; _context.NguoiHienMau.Update(nguoiHM); }
                    if (!item.DiaChi.Equals(nguoiHM.DiaChi)) { nguoiHM.DiaChi = item.DiaChi; _context.NguoiHienMau.Update(nguoiHM); }

                    if (item.TV_5 != nguoiHM.TV_5) { nguoiHM.TV_5 = item.TV_5; _context.NguoiHienMau.Update(nguoiHM);}
                    if (item.TV_10 != nguoiHM.TV_10) { nguoiHM.TV_10 = item.TV_10; _context.NguoiHienMau.Update(nguoiHM); }
                    if (item.TV_15 != nguoiHM.TV_15) { nguoiHM.TV_15 = item.TV_15; _context.NguoiHienMau.Update(nguoiHM); }
                    if (item.TV_20 != nguoiHM.TV_20) { nguoiHM.TV_20 = item.TV_20; _context.NguoiHienMau.Update(nguoiHM); }
                    if (item.TV_30 != nguoiHM.TV_30) { nguoiHM.TV_30 = item.TV_30; _context.NguoiHienMau.Update(nguoiHM); }
                    if (item.TV_40 != nguoiHM.TV_40) { nguoiHM.TV_40 = item.TV_40; _context.NguoiHienMau.Update(nguoiHM); }
                    if (item.TV_50 != nguoiHM.TV_50) { nguoiHM.TV_50 = item.TV_50; _context.NguoiHienMau.Update(nguoiHM); }
                    if (item.TV_60 != nguoiHM.TV_60) { nguoiHM.TV_60 = item.TV_60; _context.NguoiHienMau.Update(nguoiHM); }
                    if (item.TV_70 != nguoiHM.TV_70) { nguoiHM.TV_70 = item.TV_70; _context.NguoiHienMau.Update(nguoiHM); }
                    if (item.TV_80 != nguoiHM.TV_80) { nguoiHM.TV_80 = item.TV_80; _context.NguoiHienMau.Update(nguoiHM); }
                    if (item.TV_90 != nguoiHM.TV_90) { nguoiHM.TV_90 = item.TV_90; _context.NguoiHienMau.Update(nguoiHM); }
                    if (item.TV_100 != nguoiHM.TV_100) { nguoiHM.TV_100 = item.TV_100; _context.NguoiHienMau.Update(nguoiHM); }


                }
                _context.SaveChanges();
            }
            return new JsonResult("Sucess");
        }


        [HttpPost("ExportALL")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Export(Guid? idDonVi)
        {
            var checkdonvi = _context.DonVi.Find(idDonVi);
            if (checkdonvi != null)
            {
                var listNguoiHienMau = _context.NguoiHienMau.ToList();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(Common.ToDataTable(listNguoiHienMau.ToList()));
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh sách người hiến máu của đơn vị"+ checkdonvi.TenDonVi.ToString() + ".xlsx");
                    }
                }
            }
            else
            {
                return Ok("Không tải xuống được");
            }
        }


    }
}
public static class Common
{
    public static DataTable ToDataTable<T>(List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);
        //Get all the properties
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name);
        }
        foreach (T item in items)
        {
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                //inserting property values to datatable rows
                values[i] = Props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }
        //put a breakpoint here and check datatable
        return dataTable;
    }
}
