using System;
using System.Collections.Generic;

namespace TonVinhHienMau.Models.ViewModels
{
    public class HoGiaDinhVm
    {
        public string Stt { get; set; }
        public string TenGiaDinh { get; set; }

        public List<NguoiHienMauVm> NguoiHienMausVm{ get; set; }
        public string TV { get; set;}
        public string TVDX { get; set;}
        public string TV_5 { get; set; }
        public string TV_10 { get; set; }
        public string TV_15 { get; set; }
        public string TV_20 { get; set; }

        public string TV_30 { get; set; }
        public string TV_40 { get; set; }

        public string TV_50 { get; set; }

        public string TV_60 { get; set; }

        public string TV_70 { get; set; }

        public string TV_80 { get; set; }

        public string TV_90 { get; set; }

        public string TV_100 { get; set; }

        public Guid DonViId { get; set; }

        public Guid ChuHoId { get; set; }

        public Guid DotTonVinhId { get; set; }

    }
}
