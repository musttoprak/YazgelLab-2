using System.ComponentModel.DataAnnotations;

namespace yazgel_proje.Models
{
    public class Ogretmen
    {
        public Ogretmen(int ogretmenId, string ogretmenAdi)
        {
            this.ogretmenId = ogretmenId;
            this.ogretmenAdi = ogretmenAdi;
        }
        [Key]
        public int ogretmenId { get; set; }
        public string ogretmenAdi { get; set; }

       
    }
}
