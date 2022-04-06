using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TonVinhHienMau.Models
{
    public class DonVi
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string TenDonVi { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string MaDonVi { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string Diachi { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
