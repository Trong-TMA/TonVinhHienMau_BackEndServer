using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TonVinhHienMau.Models
{
    public class MoiQuanHe
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string TenMQH { get; set; }
        public bool IsDelete { get; set; }
    }
}
