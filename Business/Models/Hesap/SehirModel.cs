using AppCoreV2.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Hesap
{
    public class SehirModel : RecordBase
    {
        [Required]
        [StringLength(100)]
        public string Adi { get; set; }
        public int UlkeId { get; set; }
    }
}
