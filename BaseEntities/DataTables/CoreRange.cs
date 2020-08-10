using System;
using System.Collections.Generic;
using System.Text;

namespace BaseEntities
{
    [Serializable]
    public class CoreRange
    {
        public string Name { get; set; }
        public int distance { get; set; }

        public int AutoMin { get; set; }
        public int AutoMax { get; set; }
        public int ManualMin { get; set; }
        public int ManualMax { get; set; }

        public int MortorAutoMin { get; set; }
        public int MortorAutoMax { get; set; }
        public int MortorManualMin { get; set; }
        public int MortorManualMax { get; set; }
    }
}
