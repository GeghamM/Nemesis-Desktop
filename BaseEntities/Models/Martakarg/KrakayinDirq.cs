using System;
using System.Collections.Generic;
using System.Text;

namespace BaseEntities
{
    [Serializable]
    public class KrakayinDirq : Point
    {
        public string Name { get; set; }
        public int HU { get; set; }
        public int Dis { get; set; }

        public KrakayinDirq(string name, int x, int y, int h, int hu, int dis)
        {
            Name = name;
            X = x;
            Y = y;
            H = h;
            HU = hu;
            Dis = dis;
        }

    }
}
