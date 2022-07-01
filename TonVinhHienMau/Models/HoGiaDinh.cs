using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TonVinhHienMau.Models
{
    public class HoGiaDinh
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string TenGiaDinh { get; set; }

        public virtual ICollection<NguoiHienMau> NguoiHienMaus { get; set; }

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


        public string MaChuHo { get; set; }


        [ForeignKey("DonViId")]
        public virtual DonVi DonVis { get; set; }

        public Guid DotTonVinhId { get; set; }
        [ForeignKey("DotTonVinhId")]
        public virtual DotTonVinh DotTonVinhs { get; set; }


        

    }
}
