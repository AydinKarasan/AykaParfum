using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class UrunKampanya
    {
        [Key]
        [Column(Order = 0)]
        public int UrunId { get; set; }
        public Urun Urun { get; set; }
        [Key]
        [Column(Order = 1)]
        public int KampanyaId { get; set; }
        public Kampanya Kampanya { get; set; }
    }
}
