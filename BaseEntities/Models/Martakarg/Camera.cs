using System;
using System.Collections.Generic;
using System.Text;

namespace BaseEntities
{
    [Serializable]
    public class Camera : Point
    {
        public string Name { get; set; }
        public bool IsPlus { get; set; }
        public double P { get; set; }

        public Ditaket ToDitaket()
        {
            return new Ditaket(Name, X, Y, H);
        }
    }
}
