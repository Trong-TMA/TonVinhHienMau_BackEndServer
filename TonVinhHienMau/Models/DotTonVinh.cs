using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TonVinhHienMau.Models
{
    public class DotTonVinh
    {
        [Key]
        public Guid Id {get; set;}
        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string TenDotTonVinh { get; set;}
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string MaDotTonVinh { get; set;}
        public bool? IsDeleted { get; set; }
    }
}
