using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseEntities
{
    [Serializable]
    public class Martakarg
    {
        public List<Ditaket> Ditaketer { get; set; }
        public List<KrakayinDirq> KrakayinDirqer { get; set; }
        public List<Camera> Cameraner { get; set; }

        public KrakayinDirq GetKD(int Martkoc, int Dasak)
        {
            switch (Martkoc)
            {
                case 1:
                    if (Dasak == 1) return KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄ1ՀԴ").First();
                    if (Dasak == 2) return KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄ2ՀԴ").First();
                    else return KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄՊահեստային").First();

                case 2:
                    if (Dasak == 1) return KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄ1ՀԴ").First();
                    if (Dasak == 2) return KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄ2ՀԴ").First();
                    else return KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄՊահեստային").First();

                case 3:
                    if (Dasak == 1) return KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄ1ՀԴ").First();
                    if (Dasak == 2) return KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄ2ՀԴ").First();
                    else return KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄՊահեստային").First();

            }
            return null;
        }

        public KrakayinDirq GetKDbyName(string Kd, int Dasak)
        {

            switch (Kd)
            {
                case "1 ՀրՄ Հիմնական":
                    if (Dasak == 1) return KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄ1ՀԴ").First();
                    if (Dasak == 2) return KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄ2ՀԴ").First();
                    else return null;
                case "1 ՀրՄ Դասակի Կազմով":
                    if (Dasak == 1) return KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄ1ՀԴ").First();
                    if (Dasak == 2) return KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄ2ՀԴ").First();
                    else return null;

                case "1 ՀրՄ Պահեստային":
                    return KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄՊահեստային").First();
                case "1 ՀրՄ Մարտկոցի Կազմով":
                    return KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄՊահեստային").First();

                case "2 ՀրՄ Հիմնական":
                    if (Dasak == 1) return KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄ1ՀԴ").First();
                    if (Dasak == 2) return KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄ2ՀԴ").First();
                    else return null;
                case "2 ՀրՄ Դասակի Կազմով":
                    if (Dasak == 1) return KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄ1ՀԴ").First();
                    if (Dasak == 2) return KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄ2ՀԴ").First();
                    else return null;

                case "2 ՀրՄ Պահեստային":
                    return KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄՊահեստային").First();
                case "2 ՀրՄ Մարտկոցի Կազմով":
                    return KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄՊահեստային").First();

                case "3 ՀրՄ Հիմնական":
                    if (Dasak == 1) return KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄ1ՀԴ").First();
                    if (Dasak == 2) return KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄ2ՀԴ").First();
                    else return null;
                case "3 ՀրՄ Դասակի Կազմով":
                    if (Dasak == 1) return KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄ1ՀԴ").First();
                    if (Dasak == 2) return KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄ2ՀԴ").First();
                    else return null;

                case "3 ՀրՄ Պահեստային":
                    return KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄՊահեստային").First();
                case "3 ՀրՄ Մարտկոցի Կազմով":
                    return KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄՊահեստային").First();

            }
            return null;
        }

        public Ditaket GetDitaket(string dkname)
        {
            if (dkname.Contains("Տեսախցիկ"))
                return Cameraner.Where(cam => cam.Name == dkname).First().ToDitaket();
            return Ditaketer.Where(dk => dk.Name == dkname).First();
        }
    }
}
