using System.Threading.Tasks;
using TonVinhHienMau.Data;
using TonVinhHienMau.Models.ViewModels;

namespace TonVinhHienMau.Services.Service
{
    public interface ITonVinhHienMau
    {
        Task<NguoiHienMauVm> CheckExist(AppDbContext _context, string Hoten, int NamSinh);
    }
}
