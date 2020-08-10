using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseEntities
{
    [Serializable]
    public class AXK
    {
        public AXK(string id,string description, int x1, int y1, int h1, int x2, int y2, int h2)
        {
            Right = new Point();
            Left = new Point();
            ID = id;
            Description = description;
            Right.X = x1;
            Right.Y = y1;
            Right.H = h1;
            Left.X = x2;
            Left.Y = y2;
            Left.H = h2;
        }
        public AXK()
        {

        }
        public int Num { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
        public Point Right { get; set; }
        public Point Left { get; set; }
    }
}
