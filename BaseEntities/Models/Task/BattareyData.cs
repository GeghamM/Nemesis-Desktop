using System;
using System.Collections.Generic;
using System.Text;

namespace BaseEntities
{
    public class Battarey
    {
        public string Core { get; set; }
        public double KMH { get; set; }
        public double KU { get; set; }
        public double SHU { get; set; }
        public double PS { get; set; }
        public double DeltaX { get; set; }
        public double DeltaN { get; set; }
        public string KDDirection { get; set; }
        public int Ttrichqayin { get; set; }
        public double Catk { get; set; }
        public double punj { get; set; }
        public int Z { get; set; }
        public Dasak First { get; set; }
        public Dasak Second { get; set; }
        public Battarey()
        {
            First = new Dasak();
            Second = new Dasak();
        }

        public void InitializeTexagrakan(Texagrakan tex1, Texagrakan tex2)
        {
            First.Distance = tex1.Distance;
            First.Davarot = tex1.Alpha;

            Second.Distance = tex2.Distance;
            Second.Davarot = tex2.Alpha;
        }

    }

    public class Dasak
    {
        public double Pricel { get; set; }
        public double Paytucich { get; set; }
        public double Level { get; set; }
        public double DavarotHU { get; set; }
        public double Distance { get; set; }
        public double DistanceHashvarkayin { get; set; }
        public double Davarot { get; set; }
        public double DavarotHashvarkayin { get; set; }

    }
}
