using System.ComponentModel.DataAnnotations;

namespace yazgel_proje.Models
{
    public class Kisit
    {
        public Kisit() { }
        public Kisit(int kisitId, string dersAdi,string gun, string saat, string sinif, string ogretmenAdi, int ustKisitId, int altKisitId, int dersProgramiId) { 
        
            this.kisitId = kisitId;
            this.dersAdi = dersAdi;
            this.gun = gun;
            this.saat = saat;
            this.sinif = sinif;
            this.ogretmenAdi = ogretmenAdi;
            this.ustKisitId = ustKisitId;
            this.altKisitId = altKisitId;
            this.dersProgramiId = dersProgramiId;
        }


        [Key]
        public int kisitId { get; set; }
        public string dersAdi { get; set; }
        public string ogretmenAdi { get; set; }
        public string gun { get; set; }
        public string saat { get; set; }
        public string sinif { get; set; }
        public int ustKisitId { get; set; }
        public int altKisitId { get; set; }
        public int dersProgramiId { get; set; }

    }
}
