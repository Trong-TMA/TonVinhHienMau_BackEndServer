using System;
using System.Collections.Generic;
using TonVinhHienMau.Data;
using TonVinhHienMau.Models.ViewModels;

namespace TonVinhHienMau.Services.Service
{
    public interface IHienMau
    {
        public string getHightTV(AppDbContext _context, Guid NguoiHienMauId);
        public string NonUnicode(string text);
    }
}
