using AppCoreV2.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Hesap
{
    public class UlkeModel : RecordBase
    {
        [Required]
        [StringLength(100)]
        [DisplayName("Adý")]
        public string Adi { get; set; }
    }
}
