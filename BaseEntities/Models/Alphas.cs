using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseEntities
{
    [Serializable]
    public class HnkPnkGnk
    {
        public HnkPnkGnk()
        {
            HNK = new AlphaRow();
            PNK = new AlphaRow();
            GNK = new AlphaRow();
        }
        public AlphaRow HNK { get; set; }
        public AlphaRow PNK { get; set; }
        public AlphaRow GNK { get; set; }
    }
    [Serializable]
    public class AlphaRow
    {
        public double First { get; set; }
        public double Second { get; set; }
        public double Third { get; set; }
        public double Fourth { get; set; }
    }
    [Serializable]
    public class Alphas
    {
        public Alphas()
        {
            Himnakan_1HrM = new HnkPnkGnk();
            Himnakan_2HrM = new HnkPnkGnk();
            Himnakan_3HrM = new HnkPnkGnk();
            Pahestayin_1HrM = new HnkPnkGnk();
            Pahestayin_2HrM = new HnkPnkGnk();
            Pahestayin_3HrM = new HnkPnkGnk();
        }
        public HnkPnkGnk Himnakan_1HrM { get; set; }
        public HnkPnkGnk Himnakan_2HrM { get; set; }
        public HnkPnkGnk Himnakan_3HrM { get; set; }
        public HnkPnkGnk Pahestayin_1HrM { get; set; }
        public HnkPnkGnk Pahestayin_2HrM { get; set; }
        public HnkPnkGnk Pahestayin_3HrM { get; set; }
    }

}
