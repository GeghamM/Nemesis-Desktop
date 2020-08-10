using System;
using System.Collections.Generic;
using System.Text;

namespace BaseEntities
{
    [Serializable]
    public class Point
    {
        public Point(int x, int y, int h)
        {
            X = x;
            Y = y;
            H = h;
        }
        public Point()
        {
            X = 0;
            Y = 0;
            H = 0;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int H { get; set; }
    }
}
