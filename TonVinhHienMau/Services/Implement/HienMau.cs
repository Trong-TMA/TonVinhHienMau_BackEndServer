using System;
using System.Collections.Generic;
using System.Linq;
using TonVinhHienMau.Data;
using TonVinhHienMau.Models.ViewModels;
using TonVinhHienMau.Services.Service;

namespace TonVinhHienMau.Services.Implement
{
    public class HienMau : IHienMau
    {
        public string getHightTV(AppDbContext _context, Guid NguoiHienMauId)
        {
            var nguoihien = _context.NguoiHienMau.FirstOrDefault(u=>u.Id.Equals(NguoiHienMauId));
            if (!string.IsNullOrEmpty(nguoihien.TV_100))
            {
                return "Mức 100";
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_90))
            {
                return "Mức 90";
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_80))
            {
                return "Mức 80";
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_70))
            {
                return "Mức 70";
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_60))
            {
                return "Mức 60";
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_50))
            {
                return "Mức 50";
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_40))
            {
                return "Mức 40";
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_30))
            {
                return "Mức 30";
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_20))
            {
                return "Mức 20";
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_15))
            {
                return "Mức 15";
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_10))
            {
                return "Mức 10";
            }
            if (!string.IsNullOrEmpty(nguoihien.TV_5))
            {
                return "Mức 5";
            }
            else
            {
                return null;
            }

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
