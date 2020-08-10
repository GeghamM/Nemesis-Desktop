using System;
using System.Collections.Generic;
using System.Text;

namespace BaseEntities
{
    public class AXKTarget
    {
        public AXKTarget(Beverayin b)
        {
            Beverayin = new Beverayin();
            Uxankyun = new Uxankyun();
            Beverayin.Alpha1 = b.Alpha1;
            Beverayin.Alpha2 = b.Alpha2;
            Beverayin.Dist1 = b.Dist1;
            Beverayin.Dist2 = b.Dist2;
            Beverayin.M1 = b.M1;
            Beverayin.M2 = b.M2;
        }
        public AXKTarget(Uxankyun u)
        {
            Beverayin = new Beverayin();
            Uxankyun = new Uxankyun
            {
                X1 = u.X1,
                X2 = u.X2,
                Y1 = u.Y1,
                Y2 = u.Y2,
                H1 = u.H1,
                H2 = u.H2
            };
        }
        public Beverayin Beverayin { get; set; }
        public Uxankyun Uxankyun { get; set; }
    }

    public class Beverayin
    {
        public static int Num { get; set; }
        public string Alpha1 { get; set; }
        public string Dist1 { get; set; }
        public string M1 { get; set; }
        public string Alpha2 { get; set; }
        public string Dist2 { get; set; }
        public string M2 { get; set; }
    }

    public class Uxankyun
    {
        public string X1 { get; set; }
        public string Y1 { get; set; }
        public string H1 { get; set; }
        public string X2 { get; set; }
        public string Y2 { get; set; }
        public string H2 { get; set; }
    }
}
