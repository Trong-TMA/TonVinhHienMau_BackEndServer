using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


        public Task<NguoiHienMauVm> CheckExist(AppDbContext _context, string Hoten, int NamSinh)
        {
            throw new System.NotImplementedException();
        }
    }
}


