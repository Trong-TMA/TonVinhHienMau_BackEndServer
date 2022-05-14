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
        public List<NguoiHienMauVm> GetAll(AppDbContext _context);
        public List<NguoiHienMauVm> ImportExcel(AppDbContext _context, IFormFile file, Guid DonviId, Guid DotTonVinhId);


    }
}
