using AppCoreV2.Records.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.Hesap
{
    public class Sehir : RecordBase
    {
        public string Adi { get; set; }
        public int UlkeId { get; set; }
        public Ulke Ulke { get; set; }
        public List<KullaniciDetay> KullaniciDetaylar { get; set; }
    }
}
