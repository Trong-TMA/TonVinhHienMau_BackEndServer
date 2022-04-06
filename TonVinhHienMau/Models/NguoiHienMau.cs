﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TonVinhHienMau.Models
{
    public class NguoiHienMau
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string HoTen { get; set; }

        public bool GioiTinh { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public int NamSinh { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string NgheNghiep { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string DiaChi { get; set; }

        public string TV_5 { get; set; }


        public int NamTV_5 { get; set; }


        public string TV_10 { get; set; }


        public int NamTV_10 { get; set; }


        public string TV_15 { get; set; }

        public int NamTV_15 { get; set; }


        public string TV_20 { get; set; }

        public int NamTV_20 { get; set; }

 
        public string TV_30 { get; set; }

        public int NamTV_30 { get; set; }


        public string TV_40 { get; set; }

        public int NamTV_40 { get; set; }



        public string TV_50 { get; set; }

        public int NamTV_50 { get; set; }


        public string TV_60 { get; set; }

        public int NamTV_60 { get; set; }

 
        public string TV_70 { get; set; }
 
        public int NamTV_70 { get; set; }


        public string TV_80 { get; set; }
  
        public int NamTV_80 { get; set; }

  
        public string TV_90 { get; set; }
  
        public int NamTV_90 { get; set; }

 
        public string TV_100 { get; set; }

        public int NamTV_100 { get; set; }

        public Guid? DonViId { get; set; }

        [ForeignKey("DonViId")]
        public virtual DonVi DonVis { get; set; }
    }
}
