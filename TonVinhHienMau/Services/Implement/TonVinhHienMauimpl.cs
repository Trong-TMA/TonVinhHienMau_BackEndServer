using System.Threading.Tasks;
using TonVinhHienMau.Data;
using TonVinhHienMau.Models.ViewModels;
using TonVinhHienMau.Services.Service;

namespace TonVinhHienMau.Services.Implement
{
    public class TonVinhHienMauimpl : ITonVinhHienMau
    {
        public Task<NguoiHienMauVm> CheckExist(AppDbContext _context, string Hoten, int NamSinh)
        {
            throw new System.NotImplementedException();
        }
    }
}
