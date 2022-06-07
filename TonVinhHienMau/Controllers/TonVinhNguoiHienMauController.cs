using ClosedXML.Excel;
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

        public TonVinhNguoiHienMauController(AppDbContext context,
            ILogger<TonVinhNguoiHienMauController> logger, ITonVinhHienMau tonVinhHienMau)
        {
            _context = context;
            _logger = logger;
            _tonVinhHienMau = tonVinhHienMau;
        }

        [HttpPost("Import")]
        public IActionResult Import(Guid? DonviId, Guid? DotTonVinhId, IFormFile file)
        {
            var result = _tonVinhHienMau.ImportExcel(_context,file,DonviId,DotTonVinhId);
            return new JsonResult(result);
        }

        [HttpPost("ExportALL")]
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
