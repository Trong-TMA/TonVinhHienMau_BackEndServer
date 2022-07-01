using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using TonVinhHienMau.Data;
using TonVinhHienMau.Models;
using TonVinhHienMau.Models.ViewModels;
using TonVinhHienMau.Services.Service;

namespace TonVinhHienMau.Services.Implement
{
    public class TonVinhHoGiaDinhimpl : ITonVinhHoGiaDinh
    {
        private readonly AppDbContext _context;

        private List<NguoiHienMauVm> ReadExcel(ExcelWorksheet worksheet, int rowCount, int rowStart)
        {
            List<NguoiHienMauVm> listresult = new List<NguoiHienMauVm>();
            string STTBefo = null;
            string STTAfter = null;
            for (var row = rowStart; row <= rowCount; row++)
            {
                var stt = worksheet.Cells[row, 1].Value?.ToString().Trim();
                var hoten = worksheet.Cells[row, 2].Value?.ToString().Trim();
                var gioitinh = worksheet.Cells[row, 3].Value?.ToString().Trim();
                var namsinh = worksheet.Cells[row, 4].Value?.ToString().Trim();
                var nghenghiep = worksheet.Cells[row, 5].Value?.ToString().Trim();
                var diachi = worksheet.Cells[row, 6].Value?.ToString().Trim();
                var nhomau = worksheet.Cells[row, 7].Value?.ToString().Trim();
                
                var tv_canhan = worksheet.Cells[row, 8].Value?.ToString().Trim();
                var tv_5 = worksheet.Cells[row, 9].Value?.ToString().Trim();
                var tv_10 = worksheet.Cells[row, 10].Value?.ToString().Trim();
                var tv_15 = worksheet.Cells[row, 11].Value?.ToString().Trim();
                var tv_20 = worksheet.Cells[row, 12].Value?.ToString().Trim();
                var tv_30 = worksheet.Cells[row, 13].Value?.ToString().Trim();
                var tv_40 = worksheet.Cells[row, 14].Value?.ToString().Trim();
                var tv_50 = worksheet.Cells[row, 15].Value?.ToString().Trim();
                var tv_60 = worksheet.Cells[row, 16].Value?.ToString().Trim();
                var tv_70 = worksheet.Cells[row, 17].Value?.ToString().Trim();
                var tv_80 = worksheet.Cells[row, 18].Value?.ToString().Trim();
                var tv_90 = worksheet.Cells[row, 19].Value?.ToString().Trim();
                var tv_100 = worksheet.Cells[row, 20].Value?.ToString().Trim();

                var mqh = worksheet.Cells[row, 21].Value?.ToString().Trim();

                
                if (hoten != null & namsinh != null)
                {
                    STTBefo = stt;
                    if (STTBefo != null)
                    {
                        STTAfter = stt;
                    }
                    var result = new NguoiHienMauVm()
                    {
                        Stt = STTAfter,
                        HoTen = hoten,
                        Code = NonUnicode(hoten) + NonUnicode(namsinh) + NonUnicode(nhomau),
                        GioiTinh = Convert.ToBoolean(gioitinh),
                        NamSinh = Convert.ToInt32(namsinh),
                        NgheNghiep = nghenghiep,
                        DiaChi = diachi,
                        NhomMau = nhomau,
                    };

                    listresult.Add(result);
                }
            }
            return listresult;
        }
        List<HoGiaDinh> ITonVinhHoGiaDinh.ImportExcel(AppDbContext _context, IFormFile file, Guid DonviId, Guid DotTonVinhId)
        {
            List<NguoiHienMauVm> listnguoiHienMauVms = new List<NguoiHienMauVm>();

            List<HoGiaDinh> listresult = new List<HoGiaDinh>();



            if (file?.Length > 0)
            {
                var DonVi = _context.DonVi.FirstOrDefault(u => u.Id.Equals(DonviId));
                var NamTV = _context.DotTonVinh.FirstOrDefault(u => u.Id.Equals(DotTonVinhId));
                var stream = file.OpenReadStream();

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.First();
                    var rowCount = worksheet.Dimension.Rows;
                    int rowStart = 4;
                    listnguoiHienMauVms.AddRange(ReadExcel(worksheet, rowCount, rowStart));
                }

                for(int i = 1; i <= listnguoiHienMauVms.Count; i++)
                {

                    var check = _context.HoGiaDinh.FirstOrDefault(u=>u.MaChuHo.Equals(listnguoiHienMauVms[i].Code));
                    if (check == null)
                    {
                        HoGiaDinh hoGiaDinh = new HoGiaDinh()
                        {
                            Id = Guid.NewGuid(),
                        };

                        if (hoGiaDinh.NguoiHienMaus == null)
                        {
                            hoGiaDinh.NguoiHienMaus.Add(createNguoiHienMau(listnguoiHienMauVms[i], DonviId, DotTonVinhId));
                            hoGiaDinh.MaChuHo = listnguoiHienMauVms[i].Code;
                            listresult.Add(hoGiaDinh);
                            continue;
                        }

                        if (listnguoiHienMauVms[i].Stt.Equals(listnguoiHienMauVms[i - 1].Stt))
                        {
                            foreach(var item in listresult)
                            {
                                var kt = item.NguoiHienMaus.Where(u => u.MaNguoiHien.Equals(listnguoiHienMauVms[i].Code));

                                if (kt != null)
                                {
                                    item.NguoiHienMaus.Add(createNguoiHienMau(listnguoiHienMauVms[i], DonviId, DotTonVinhId));
                                }
                            }
                        }
                        else
                        {
                            HoGiaDinh hoGiaDinhNew = new HoGiaDinh()
                            {
                                Id = Guid.NewGuid(),
                            };
                            hoGiaDinhNew.NguoiHienMaus.Add(createNguoiHienMau(listnguoiHienMauVms[i], DonviId, DotTonVinhId));
                            listresult.Add(hoGiaDinh);
                            continue;
                        }
                    }
                }
            }
            return listresult;
        }

        public NguoiHienMau createNguoiHienMau(NguoiHienMauVm nguoiHienMauVm, Guid DonviId, Guid DotTonVinhId)
        {
            var maNguoiHien = NonUnicode(nguoiHienMauVm.HoTen) + NonUnicode(nguoiHienMauVm.NamSinh.ToString()) + NonUnicode(nguoiHienMauVm.NhomMau);
            NguoiHienMau nguoiHienMau = new NguoiHienMau()
            {
                Id = Guid.NewGuid(),
                MaNguoiHien = maNguoiHien,
                HoTen = nguoiHienMauVm.HoTen,
                GioiTinh = Convert.ToBoolean(nguoiHienMauVm.GioiTinh),
                NamSinh = Convert.ToInt32(nguoiHienMauVm.NamSinh),
                NgheNghiep = nguoiHienMauVm.NgheNghiep,
                DiaChi = nguoiHienMauVm.DiaChi,
                NhomMau = nguoiHienMauVm.NhomMau,
                TV_5 = nguoiHienMauVm.TV_5,
                TV_10 = nguoiHienMauVm.TV_10,
                TV_15 = nguoiHienMauVm.TV_15,
                TV_20 = nguoiHienMauVm.TV_20,
                TV_30 = nguoiHienMauVm.TV_30,
                TV_40 = nguoiHienMauVm.TV_40,
                TV_50 = nguoiHienMauVm.TV_50,
                TV_60 = nguoiHienMauVm.TV_60,
                TV_70 = nguoiHienMauVm.TV_70,
                TV_80 = nguoiHienMauVm.TV_80,
                TV_90 = nguoiHienMauVm.TV_90,
                TV_100 = nguoiHienMauVm.TV_100,
                DonViId = DonviId,
                DotTonVinhId = DotTonVinhId
            };
            return nguoiHienMau;
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
    }
}
