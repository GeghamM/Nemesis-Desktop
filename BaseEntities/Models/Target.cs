using System;
using System.Collections.Generic;
using System.Text;

namespace BaseEntities
{
    [Serializable]
    public class Target
    {
        public Target (string id, string desc, int x, int y, int h, int front, int deep, int num = -1)
        {
            Num = num;
            ID = id;
            Description = desc;
            X = x;
            Y = y;
            H = h;
            Front = front;
            Deepness = deep;
        }

        public Target(Point p) 
        {
            X = p.X;
            Y = p.Y;
            H = p.H;
            Front = 1;
            Deepness = 1;
        }
        public Target() { }

        public int Num { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int H { get; set; }
        public int Front { get; set; }
        public int Deepness { get; set; }
    }

}
