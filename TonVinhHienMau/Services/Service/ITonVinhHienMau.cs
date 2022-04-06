using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TonVinhHienMau.Data;
using TonVinhHienMau.Models.ViewModels;

namespace TonVinhHienMau.Services.Service
{
    public interface ITonVinhHienMau
    {
        List<NguoiHienMauVm> GetAll(AppDbContext _context);
        Task<NguoiHienMauVm> CheckExist(AppDbContext _context, string Hoten, int NamSinh);
    }
}
