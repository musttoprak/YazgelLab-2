using System.ComponentModel.DataAnnotations;

namespace yazgel_proje.Models
{
    public class DersProgrami
    {

        public DersProgrami(int dersProgramiId, int ogretmenId, string gun, string saat, string dersAdi, string sinif) {
            this.dersProgramiId = dersProgramiId;
            this.ogretmenId = ogretmenId;
            this.gun = gun;
            this.saat = saat;
            this.dersAdi = dersAdi;
            this.sinif = sinif;
        }

        [Key]
        public int dersProgramiId { get; set; }

        public int ogretmenId { get; set; }
        public Ogretmen Ogretmen { get; set; }
        public string gun { get; set; }

        public string saat { get; set; }

        public string dersAdi { get; set; }

        public string sinif { get; set; }
    }
}
