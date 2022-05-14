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
        public List<NguoiHienMauVm> GetAll(AppDbContext _context)
        {
            List<NguoiHienMauVm> nguoihienmaus = _context.NguoiHienMau.Select(u => new NguoiHienMauVm()
            {
                HoTen = u.HoTen,
                DiaChi = u.DiaChi,
                GioiTinh = u.GioiTinh,
                NamSinh = u.NamSinh,
                NgheNghiep = u.NgheNghiep,
                TV_5 = u.TV_5,
                TV_10 = u.TV_10,
                TV_15 = u.TV_15,
                TV_20 = u.TV_20,
                TV_30 = u.TV_30,
                TV_40 = u.TV_40,
                TV_50 = u.TV_50,
                TV_60 = u.TV_60,
                TV_70 = u.TV_70,
                TV_80 = u.TV_80,
                TV_90 = u.TV_90,
                TV_100 = u.TV_100,
                NamTV_5 = u.NamTV_5,
                NamTV_10 = u.NamTV_10,
                NamTV_15 = u.NamTV_15,
                NamTV_20 = u.NamTV_20,
                NamTV_30 = u.NamTV_30,
                NamTV_40 = u.NamTV_40,
                NamTV_50 = u.NamTV_50,
                NamTV_60 = u.NamTV_60,
                NamTV_70 = u.NamTV_70,
                NamTV_80 = u.NamTV_80,
                NamTV_90 = u.NamTV_90,
                NamTV_100 = u.NamTV_100,
            }).ToList();
            if (nguoihienmaus != null)
            {
                return nguoihienmaus;
            }
            else
            {
                return null;
            }
        }

        public List<NguoiHienMauVm> ImportExcel(AppDbContext _context, IFormFile file, Guid donviId, Guid dotTonVinhId)
        {

            if (file?.Length > 0)
            {
                var DonVi = _context.DonVi.FirstOrDefault(u => u.Id.Equals(donviId));
                var NamTV = _context.DotTonVinh.FirstOrDefault(u => u.Id.Equals(dotTonVinhId));
                var stream = file.OpenReadStream();

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.First();
                    var rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;
                    int rowStart = 4;
                    return ReadExcel(worksheet, rowCount, colCount, rowStart, DonVi.MaDonVi, NamTV.MaDotTonVinh);
                }
            }
        }
        private List<NguoiHienMauVm> ReadExcel(ExcelWorksheet worksheet, int rowCount, int colCount, int rowStart, string maDonVi, string maDotTonVinh)
        {
            for (var row = rowStart; row <= rowCount; row++)
            {
                
            }
        }
        private RowDataVm getRowData(ExcelWorksheet worksheet,int row, int col)
        {
            var hoten = worksheet.Cells[row, col].Value?.ToString().Trim();
            var gioitinh = worksheet.Cells[row, col].Value?.ToString().Trim();
            var namsinh = worksheet.Cells[row, col].Value?.ToString().Trim();
            var nghenghiep = worksheet.Cells[row, col].Value?.ToString().Trim();
            var diachi = worksheet.Cells[row, col].Value?.ToString().Trim();
            var tv_5 = worksheet.Cells[row, col].Value?.ToString().Trim();
            var tv_10 = worksheet.Cells[row, col].Value?.ToString().Trim();
            var tv_15 = worksheet.Cells[row, col].Value?.ToString().Trim();
            var tv_20 = worksheet.Cells[row, col].Value?.ToString().Trim();
            var tv_30 = worksheet.Cells[row, col].Value?.ToString().Trim();
            var tv_40 = worksheet.Cells[row, col].Value?.ToString().Trim();
            var tv_50 = worksheet.Cells[row, col].Value?.ToString().Trim();
            var tv_60 = worksheet.Cells[row, col].Value?.ToString().Trim();
            var tv_70 = worksheet.Cells[row, col].Value?.ToString().Trim();
            var tv_80 = worksheet.Cells[row, col].Value?.ToString().Trim();
            var tv_90 = worksheet.Cells[row, col].Value?.ToString().Trim();
            var tv_100 = worksheet.Cells[row, col].Value?.ToString().Trim();

            return new RowDataVm()
            {
                HoTen = hoten,
                GioiTinh = Boolean.Parse(gioitinh),
                NamSinh = Int32.Parse(namsinh),
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

        }
    }
}


