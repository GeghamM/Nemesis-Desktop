using System;
using System.Collections.Generic;
using System.Text;

namespace BaseEntities
{
    [Serializable]
    public class CoreInfo
    {
        public int TDistance { get; set; }
        public int pr0 { get; set; }
        public int N0 { get; set; }
        public int pr500 { get; set; }
        public int N500 { get; set; }
        public int pr1000 { get; set; }
        public int N1000 { get; set; }
        public int pr1500 { get; set; }
        public int N1500 { get; set; }
        public int pr2000 { get; set; }
        public int N2000 { get; set; }
        public int pr2500 { get; set; }
        public int N2500 { get; set; }
        public int pr3000 { get; set; }
        public int N3000 { get; set; }
        public double DX { get; set; }
        public int Z { get; set; }
        public float DY { get; set; }
        public float VD { get; set; }
        public float VRV { get; set; }
        public float T { get; set; }
        public float Q { get; set; }
    }
}
