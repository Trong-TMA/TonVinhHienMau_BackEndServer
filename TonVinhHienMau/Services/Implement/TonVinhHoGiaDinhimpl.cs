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
            string DiachiBefo = null;
            string DiachiAfter = null;
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
                var quanhe = worksheet.Cells[row, 21].Value?.ToString().Trim();

                var mqh = worksheet.Cells[row, 21].Value?.ToString().Trim();

                
                if (hoten != null & namsinh != null)
                {
                    STTBefo = stt;
                    DiachiBefo = diachi;
                    if (STTBefo != null)
                    {
                        STTAfter = stt;
                        DiachiAfter = diachi;
                    }
                    var result = new NguoiHienMauVm()
                    {
                        Id = Guid.NewGuid(),
                        Stt = STTAfter,
                        HoTen = hoten,
                        Code = NonUnicode(hoten) + NonUnicode(namsinh) + NonUnicode(nhomau),
                        GioiTinh = Convert.ToBoolean(gioitinh),
                        NamSinh = Convert.ToInt32(namsinh),
                        NgheNghiep = nghenghiep,
                        DiaChi = DiachiAfter,
                        NhomMau = nhomau,
                        TV = tv_canhan, 
                        Note = quanhe
                    };

                    listresult.Add(result);
                }
            }
            return listresult;
        }
        List<HoGiaDinhVm> ITonVinhHoGiaDinh.ImportExcel(AppDbContext _context, IFormFile file, Guid DonviId, Guid DotTonVinhId)
        {
            List<NguoiHienMauVm> listnguoiHienMauVms = new List<NguoiHienMauVm>();

            List<HoGiaDinhVm> items = new List<HoGiaDinhVm>();

            List<HoGiaDinhVm> listresult = new List<HoGiaDinhVm>();



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
                int stt = 1;
                foreach(var item in listnguoiHienMauVms)
                {
                    HoGiaDinhVm hoGiaDinhVm = new HoGiaDinhVm() { Stt = stt.ToString()};
                    hoGiaDinhVm.NguoiHienMausVm = new List<NguoiHienMauVm>();

                    if (hoGiaDinhVm.Stt.Equals(item.Stt))
                    {
                        hoGiaDinhVm.NguoiHienMausVm.Add(item);
                        items.Add(hoGiaDinhVm);
                        continue;
                        
                    }
                    else
                    {
                        stt++;
                        hoGiaDinhVm.Stt = stt.ToString();
                        hoGiaDinhVm.NguoiHienMausVm.Add(item);
                        items.Add(hoGiaDinhVm);
                        continue;
                    }
                }

                HoGiaDinhVm hoGiaDinh = new HoGiaDinhVm();

                for(int i = 0; i < items.Count; i++) 
                {

                    if (hoGiaDinh.NguoiHienMausVm == null)
                    {
                        hoGiaDinh.NguoiHienMausVm = new List<NguoiHienMauVm>();
                        hoGiaDinh.TenGiaDinh = "Nhà ông/bà: " + items[i].NguoiHienMausVm.FirstOrDefault().HoTen;
                        hoGiaDinh.ChuHoId = items[i].NguoiHienMausVm.FirstOrDefault().Id;
                        hoGiaDinh.NguoiHienMausVm.AddRange(items[i].NguoiHienMausVm);
                        continue;
                    }
                    else
                    {
                        if (items[i].Stt.Equals(items[i - 1].Stt))
                        {
                           hoGiaDinh.NguoiHienMausVm.AddRange(items[i].NguoiHienMausVm);
                            if ((i+1) == items.Count)
                            {
                                listresult.Add(hoGiaDinh);
                            }
                        }
                        else
                        {
                            listresult.Add(hoGiaDinh);
                            hoGiaDinh = new HoGiaDinhVm();
                            hoGiaDinh.NguoiHienMausVm = new List<NguoiHienMauVm>();
                            hoGiaDinh.TenGiaDinh = "Nhà ông/bà: " + items[i].NguoiHienMausVm.FirstOrDefault().HoTen;
                            hoGiaDinh.ChuHoId = items[i].NguoiHienMausVm.FirstOrDefault().Id;
                            hoGiaDinh.NguoiHienMausVm.AddRange(items[i].NguoiHienMausVm);
                        }
                        
                    }
                }

                foreach(var item in listresult)
                {
                    item.TV = SumTV(item.NguoiHienMausVm);
                }
                foreach (var item in listresult)
                {
                    item.TVDX = getHightTVExcel(item);
                }
            }
            return listresult;
        }


        public string getHightTVExcel(HoGiaDinhVm hoGiaDinhVm)
        {
            string result = null;
            if (Int32.Parse(hoGiaDinhVm.TV) >= 5 && Int32.Parse(hoGiaDinhVm.TV) < 10)
            {
                result = "Tôn vinh mức 5";
            }
            if (Int32.Parse(hoGiaDinhVm.TV) >= 10 && Int32.Parse(hoGiaDinhVm.TV) < 15)
            {
                result = "Tôn vinh mức 10";
            }
            if (Int32.Parse(hoGiaDinhVm.TV) >= 15 && Int32.Parse(hoGiaDinhVm.TV) < 20)
            {
                result = "Tôn vinh mức 15";
            }
            if (Int32.Parse(hoGiaDinhVm.TV) >= 20 && Int32.Parse(hoGiaDinhVm.TV) < 30)
            {
                result = "Tôn vinh mức 20";
            }
            if (Int32.Parse(hoGiaDinhVm.TV) >= 30 && Int32.Parse(hoGiaDinhVm.TV) < 40)
            {
                result = "Tôn vinh mức 30";
            }
            if (Int32.Parse(hoGiaDinhVm.TV) >= 40 && Int32.Parse(hoGiaDinhVm.TV) < 50)
            {
                result = "Tôn vinh mức 40";
            }
            if (Int32.Parse(hoGiaDinhVm.TV) >= 50 && Int32.Parse(hoGiaDinhVm.TV) < 60)
            {
                result = "Tôn vinh mức 50";
            }
            if (Int32.Parse(hoGiaDinhVm.TV) >= 60 && Int32.Parse(hoGiaDinhVm.TV) < 70)
            {
                result = "Tôn vinh mức 60";
            }
            if (Int32.Parse(hoGiaDinhVm.TV) >= 70 && Int32.Parse(hoGiaDinhVm.TV) < 80)
            {
                result = "Tôn vinh mức 70";
            }
            if (Int32.Parse(hoGiaDinhVm.TV) >= 80 && Int32.Parse(hoGiaDinhVm.TV) < 90)
            {
                result = "Tôn vinh mức 80";
            }
            if (Int32.Parse(hoGiaDinhVm.TV) >= 90 && Int32.Parse(hoGiaDinhVm.TV) < 100)
            {
                result = "Tôn vinh mức 90";
            }
            if (Int32.Parse(hoGiaDinhVm.TV) >= 100)
            {
                result = "Tôn vinh mức 100";
            }
            return result; 

        }

        public string SumTV(List<NguoiHienMauVm> nguoiHienMauVms)
        {
            int SUM = 0;
            foreach(var item in nguoiHienMauVms)
            {
                SUM = SUM + Int32.Parse(item.TV);
            }
            return SUM.ToString();
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
