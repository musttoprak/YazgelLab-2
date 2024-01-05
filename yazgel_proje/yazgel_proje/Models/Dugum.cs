using Org.BouncyCastle.Asn1.X509;

namespace yazgel_proje.Models
{
    public class DersGraph
    {
        public List<Dugum> graph;

        public DersGraph()
        {
            graph = new List<Dugum>();
        }

        public void AddDers(Kisit dersNode)
        {
            graph.Add(new Dugum(dersNode));
        }

        public int HasConflict(Dugum dugum)
        {       
                string nodeslotkey = $"{dugum.kisit.saat}-{dugum.kisit.gun}-{dugum.kisit.sinif}";
                foreach (var digerleri in graph)
                {
                    if(digerleri.kisit.kisitId!= dugum.kisit.kisitId)
                    {
                        string digerslotkey = $"{digerleri.kisit.saat}-{digerleri.kisit.gun}-{digerleri.kisit.sinif}";

                        if (nodeslotkey == digerslotkey)
                        {
                            return 1;
                           
                        }
                    }
                }
            string nodeslotkeyOgretmen = $"{dugum.kisit.saat}-{dugum.kisit.gun}-{dugum.kisit.ogretmenAdi}";
            foreach (var digerleri in graph)
            {
                if (digerleri.kisit.kisitId != dugum.kisit.kisitId)
                {
                    string digerslotkey = $"{digerleri.kisit.saat}-{digerleri.kisit.gun}-{digerleri.kisit.ogretmenAdi}";
                    if (nodeslotkeyOgretmen == digerslotkey && dugum.kisit.sinif != digerleri.kisit.sinif)
                    {
                        return 2;
                    }
                }
            }


            return 0;
        }
    }

    public class Dugum
    {
        public Kisit kisit { get; set; }
        public List<Dugum> dugumler { get; }

        public Kenar kenarlar { get; }
        public Dugum(Kisit kisit)
        {
            this.kisit = kisit;
            this.kenarlar = new Kenar();
            this.dugumler = new List<Dugum>();
        }
    }

    public class Kenar
    {
        public Kisit ustKisit { get; set; }
        public Kisit altKisit{ get; set; }
    }
}
