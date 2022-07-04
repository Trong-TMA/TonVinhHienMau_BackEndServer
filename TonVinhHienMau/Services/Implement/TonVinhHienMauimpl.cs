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
        private List<NguoiHienMauVm> ReadExcel(ExcelWorksheet worksheet, int rowCount, int rowStart)
        {
            List<NguoiHienMauVm> listresult = new List<NguoiHienMauVm>();
            for (var row = rowStart; row <= rowCount; row++)
            {
                var hoten = worksheet.Cells[row, 2].Value?.ToString().Trim();
                var gioitinh = worksheet.Cells[row, 3].Value?.ToString().Trim();
                var namsinh = worksheet.Cells[row, 4].Value?.ToString().Trim();
                var nghenghiep = worksheet.Cells[row, 5].Value?.ToString().Trim();
                var diachi = worksheet.Cells[row, 6].Value?.ToString().Trim();
                var nhomau = worksheet.Cells[row, 7].Value?.ToString().Trim();
                var tv_5 = worksheet.Cells[row, 8].Value?.ToString().Trim();
                var tv_10 = worksheet.Cells[row, 9].Value?.ToString().Trim();
                var tv_15 = worksheet.Cells[row, 10].Value?.ToString().Trim();
                var tv_20 = worksheet.Cells[row, 11].Value?.ToString().Trim();
                var tv_30 = worksheet.Cells[row, 12].Value?.ToString().Trim();
                var tv_40 = worksheet.Cells[row, 13].Value?.ToString().Trim();
                var tv_50 = worksheet.Cells[row, 14].Value?.ToString().Trim();
                var tv_60 = worksheet.Cells[row, 15].Value?.ToString().Trim();
                var tv_70 = worksheet.Cells[row, 16].Value?.ToString().Trim();
                var tv_80 = worksheet.Cells[row, 17].Value?.ToString().Trim();
                var tv_90 = worksheet.Cells[row, 18].Value?.ToString().Trim();
                var tv_100 = worksheet.Cells[row, 19].Value?.ToString().Trim();
                if (hoten != null & namsinh!= null)
                {
                    var result = new NguoiHienMauVm()
                    {
                        HoTen = hoten,
                        GioiTinh = Convert.ToBoolean(gioitinh),
                        NamSinh = Convert.ToInt32(namsinh),
                        NgheNghiep = nghenghiep,
                        DiaChi = diachi,
                        NhomMau = nhomau,
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
                        TV_100 = tv_100,
                        
                        
                    };
                    listresult.Add(result);
                }
            }
            return listresult;
        }
        public List<NguoiHienMauVm> ImportExcel(AppDbContext _context, IFormFile file, Guid donviId, Guid dotTonVinhId)
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
                    int rowStart = 4;
                    listresult.AddRange(ReadExcel(worksheet, rowCount, rowStart));
                }
                foreach (var item in listresult)
                {
                    NguoiHienMauVm nguoiHienMauVm = new NguoiHienMauVm()
                    {
                        HoTen = item.HoTen,
                        GioiTinh = Convert.ToBoolean(item.GioiTinh),
                        NamSinh = Convert.ToInt32(item.NamSinh),
                        NgheNghiep = item.NgheNghiep,
                        DiaChi = item.DiaChi,
                        NhomMau = item.NhomMau,
                        TVDX = getHightTVExcel(item).ToString(),
                        Note = "",
                    };
                    var MaNguoiHien = NonUnicode(item.HoTen) + NonUnicode(item.NamSinh.ToString()) + NonUnicode(item.NhomMau);
                    var nguoiHM = _context.NguoiHienMau.FirstOrDefault(s => s.MaNguoiHien.Equals(MaNguoiHien) && s.DonViId.Equals(donviId) && s.DotTonVinhId.Equals(dotTonVinhId));

                    
                    

                    if (nguoiHM == null)
                    {
                        item.Note = "Người mới cần đề xuất";
                    }
                    else
                    {
                        item.TV = getHightTV(_context, nguoiHM.Id).ToString();
                        if (Int32.Parse(item.TV) == 0) {
                            item.TV = "Chưa được tôn vinh";
                        }
                        var data = getHightTV(_context, nguoiHM.Id);
                        var excel = getHightTVExcel(item);

                        if (data < excel)
                        {
                            nguoiHM.TV_5 = null;
                            nguoiHM.TV_15 = null;
                            nguoiHM.TV_10 = null;
                            nguoiHM.TV_20 = null;
                            nguoiHM.TV_30 = null;
                            nguoiHM.TV_40 = null;
                            nguoiHM.TV_50 = null;
                            nguoiHM.TV_60 = null;
                            nguoiHM.TV_70 = null;
                            nguoiHM.TV_80 = null;
                            nguoiHM.TV_90 = null;
                            nguoiHM.TV_100 = null;
                            _context.NguoiHienMau.Update(nguoiHM);
                            item.TVDX = excel.ToString();
                            item.Note = "Đã cập nhật mức tôn vinh mới là: " + excel;

                        }                       
                    }
                }
                _context.SaveChanges();
            }
            return listresult ;
        }

        public string NonUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text.Replace(" ", "");
        }

        public int getHightTVExcel(NguoiHienMauVm nguoihien)
        {
            if (!string.IsNullOrEmpty(nguoihien.TV_100))
            {
                return 100;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_90))
            {
                return 90;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_80))
            {
                return 80;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_70))
            {
                return 70;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_60))
            {
                return 60;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_50))
            {
                return 50;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_40))
            {
                return 40;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_30))
            {
                return 30;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_20))
            {
                return 20;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_15))
            {
                return 15;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_10))
            {
                return 10;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_5))
            {
                return 5;
            }
            else
            {
                return 0;
            }

        }

        public int getHightTV(AppDbContext _context, Guid NguoiHienMauId)
        {
            var nguoihien = _context.NguoiHienMau.FirstOrDefault(u => u.Id.Equals(NguoiHienMauId));
            if (!string.IsNullOrEmpty(nguoihien.TV_100))
            {
                return 100;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_90))
            {
                return 90;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_80))
            {
                return 80;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_70))
            {
                return 70;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_60))
            {
                return 60;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_50))
            {
                return 50;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_40))
            {
                return 40;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_30))
            {
                return 30;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_20))
            {
                return 20;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_15))
            {
                return 15;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_10))
            {
                return 10;
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_5))
            {
                return 5;
            }
            else
            {
                return 0;
            }

        }
    }
}


