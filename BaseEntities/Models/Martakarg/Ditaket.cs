using System;
using System.Collections.Generic;
using System.Text;

namespace BaseEntities
{
    [Serializable]
    public class Ditaket : Point
    {
        public string Name { get; set; }
        public Ditaket(string name,int x, int y, int h)
        {
            Name = name;
            X = x;
            Y = y;
            H = h;
        }
        //public Point ToPoint()
        //{
        //    return new Point(X, Y, H);
        //}
        
    }
}
