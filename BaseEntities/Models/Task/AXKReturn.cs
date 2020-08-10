using System;
using System.Collections.Generic;
using System.Text;

namespace BaseEntities
{
    public class AXKReturn
    {
        public Point P1 { get; set; }
        public Point P2 { get; set; }

        public Texagrakan Tex1 { get; set; }
        public Texagrakan Tex2 { get; set; }

        public Battarey Bat1 { get; set; }
        public Battarey Bat2 { get; set; }
        public Battarey Bat3 { get; set; }

        public Target T1 { get; set; }
        public Target T2 { get; set; }
        public Target T3 { get; set; }

        public int Front { get; set; }
    }
}
