using System.ComponentModel.DataAnnotations;

namespace yazgel_proje.Models
{
    public class DersProgrami
    {
        public DersProgrami()
        {

        }
        public DersProgrami(int dersProgramiId, string ogretmenAdi, string dersAdi, string sinif) {
            this.dersProgramiId = dersProgramiId;
            this.ogretmenAdi = ogretmenAdi;
            this.dersAdi = dersAdi;
            this.sinif = sinif;
        }

        [Key]
        public int dersProgramiId { get; set; }

        public string ogretmenAdi { get; set; }

        public string dersAdi { get; set; }

        public string sinif { get; set; }


    }
}
