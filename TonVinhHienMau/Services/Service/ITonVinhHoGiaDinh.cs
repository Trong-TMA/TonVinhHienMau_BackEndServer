using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using TonVinhHienMau.Data;
using TonVinhHienMau.Models;
using TonVinhHienMau.Models.ViewModels;

namespace TonVinhHienMau.Services.Service
{
    public interface ITonVinhHoGiaDinh
    {
        public List<HoGiaDinh> ImportExcel(AppDbContext _context, IFormFile file, Guid DonviId, Guid DotTonVinhId);
    }
}
