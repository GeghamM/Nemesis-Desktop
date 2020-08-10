using System;
using System.Collections.Generic;
using System.Text;

namespace BaseEntities
{
    [Serializable]
    public class Level
    {
        public List<int> M { get; set; }
        public int Distance { get; set; }
        public int Dpr { get; set; }
        public List<int> Values { get; set; }
    }
}
