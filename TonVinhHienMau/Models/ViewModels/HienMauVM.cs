using System;

namespace TonVinhHienMau.Models.ViewModels
{
    public class HienMauVM
    {
        public Guid Id { get; set; }
        public string HoTen { get; set; }

        public bool? GioiTinh { get; set; }

        public int NamSinh { get; set; }

        public string NgheNghiep { get; set; }

        public string DiaChi { get; set; }
        public string NhomMau { get; set; }
        
        public string TV { get; set; }

    }
}
