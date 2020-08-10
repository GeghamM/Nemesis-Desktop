using System;
using System.Collections.Generic;
using System.Text;

namespace BaseEntities
{
    public class Texagrakan
    {
        public Texagrakan(double alpha, double distance, double m, double frontab)
        {
            Alpha = alpha;
            Distance = distance;
            M = m;
            FrontAB = frontab;
        }
        public Texagrakan() { }
        public double Alpha { set; get; }
        public double Distance { set; get; }
        public double M { set; get; }
        public double FrontAB { set; get; }
    }
}
