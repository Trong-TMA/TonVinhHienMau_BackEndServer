using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TonVinhHienMau.Data;
using TonVinhHienMau.Models;
using TonVinhHienMau.Models.ViewModels;
using TonVinhHienMau.Services.Service;

namespace TonVinhHienMau.Services.Implement
{
    public class TonVinhHienMauimpl : ITonVinhHienMau
    {
        public List<NguoiHienMauVm> ImportExcel(AppDbContext _context, IFormFile file, Guid? donviId, Guid? dotTonVinhId)
        {
            List<NguoiHienMauVm> listresult = new List<NguoiHienMauVm>();
            if (file?.Length > 0)
            {
                var DonVi = _context.DonVi.FirstOrDefault(u => u.Id.Equals(donviId));
                var NamTV = _context.DotTonVinh.FirstOrDefault(u => u.Id.Equals(dotTonVinhId));
                var stream = file.OpenReadStream();

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.First();
                    var rowCount = worksheet.Dimension.Rows;
                    int rowStart = 5;
                    listresult.AddRange(ReadExcel(worksheet, rowCount, rowStart, DonVi.MaDonVi, NamTV.MaDotTonVinh));
                }
            }
            return listresult ;
        }
        private List<NguoiHienMauVm> ReadExcel(ExcelWorksheet worksheet, int rowCount, int rowStart, string? maDonVi, string? maDotTonVinh)
        {
            List<NguoiHienMauVm> listresult = new List<NguoiHienMauVm>();
            for (var row = rowStart; row <= rowCount; row++)
            {
                var hoten = worksheet.Cells[row, 2].Value?.ToString().Trim();
                var gioitinh = worksheet.Cells[row, 3].Value?.ToString().Trim();
                var namsinh = worksheet.Cells[row, 4].Value?.ToString().Trim();
                var nghenghiep = worksheet.Cells[row, 5].Value?.ToString().Trim();
                var diachi = worksheet.Cells[row, 6].Value?.ToString().Trim();
                var tv_5 = worksheet.Cells[row, 7].Value?.ToString().Trim();
                var tv_10 = worksheet.Cells[row, 8].Value?.ToString().Trim();
                var tv_15 = worksheet.Cells[row, 9].Value?.ToString().Trim();
                var tv_20 = worksheet.Cells[row, 10].Value?.ToString().Trim();
                var tv_30 = worksheet.Cells[row, 11].Value?.ToString().Trim();
                var tv_40 = worksheet.Cells[row, 12].Value?.ToString().Trim();
                var tv_50 = worksheet.Cells[row, 13].Value?.ToString().Trim();
                var tv_60 = worksheet.Cells[row, 14].Value?.ToString().Trim();
                var tv_70 = worksheet.Cells[row, 15].Value?.ToString().Trim();
                var tv_80 = worksheet.Cells[row, 16].Value?.ToString().Trim();
                var tv_90 = worksheet.Cells[row, 17].Value?.ToString().Trim();
                var tv_100 = worksheet.Cells[row, 18].Value?.ToString().Trim();
                if(hoten != null & namsinh!= null)
                {
                    var result = new NguoiHienMauVm()
                    {
                        HoTen = hoten,
                        GioiTinh = Convert.ToBoolean(gioitinh),
                        NamSinh = Convert.ToInt32(namsinh),
                        NgheNghiep = nghenghiep,
                        TV_5 = tv_5,
                        TV_10 = tv_10,
                        TV_15 = tv_15,
                        TV_20 = tv_20,
                        TV_30 = tv_30,
                        TV_40 = tv_40,
                        TV_50 = tv_50,
                        TV_60 = tv_60,
                        TV_70 = tv_70,
                        TV_80 = tv_80,
                        TV_90 = tv_90,
                        TV_100 = tv_100
                    };
                    listresult.Add(result);
                }
            }
            return listresult;
        }
    }
}


