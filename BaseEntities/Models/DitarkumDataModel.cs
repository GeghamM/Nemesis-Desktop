using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseEntities
{
    public class DitarkumDataModel
    {
        public Ditaket Ditaket { get; set; }
        public Ditaket ParallelDitaket { get; set; }
        public Texagrakan Hramanatarakan { get; set; }
        public Texagrakan ParallelHramanatarakan { get; set; }
        public Target Target { get; set; }
        public TaskInfo TaskInfo { get; set; }
        public PaytyunTexakayum Battary1Texakayum { get; set; }
        public PaytyunTexakayum Battary2Texakayum { get; set; }
        public PaytyunTexakayum Battary3Texakayum { get; set; }
    }

    public class PaytyunTexakayum
    {
        public string Core { get; set; }
        public double Davarot1 { get; set; }
        public double Pricel1 { get; set; } 
        public double Davarot2 { get; set; }
        public double Pricel2 { get; set; }
        public double Poxrak1 { get; set; }
        public double Poxrak2 { get; set; }
        public double Ku { get; set; }
    }
}
