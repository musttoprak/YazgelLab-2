using System.ComponentModel.DataAnnotations;

namespace yazgel_proje.Models
{
    public class Kisit
    {
        public Kisit(int kisitId, string gun, string saat, string sinif, int ogretmenId) { 
        
            this.kisitId = kisitId;
            this.gun = gun;
            this.saat = saat;
            this.sinif = sinif;
            this.ogretmenId = ogretmenId;
        }


        [Key]
        public int kisitId { get; set; }
        public string gun { get; set; }
        public string saat { get; set; }
        public string sinif { get; set; }

        public Ogretmen Ogretmen { get; set; }
        public int ogretmenId { get; set; }

    }
}
