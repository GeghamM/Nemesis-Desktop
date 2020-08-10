using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using BaseEntities;

namespace DataAccessLayer
{
    public static class Core
    {
        public static List<CoreInfo> CoreAutoSelection(string selectedcore, string shootType, int distance1, int distance2, int height, string paytucich, ref string core)
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            List<CoreRange> licqs = new List<CoreRange>();
            string L = "";
            string path = "Data\\CoreRange\\";
            switch (paytucich)
            {
                case "Т-7 (С-463)":
                    path += "T-7";
                    if (selectedcore == "Լիցք 4" || selectedcore == "Լիցք 5" || selectedcore == "Լիցք 6")
                    {
                        selectedcore = "Լիցք 3";
                    }
                    break;
                case "РГМ-2 (ОФ-540)":
                    path += "D-20";
                    if (selectedcore == "Լիցք քչացված")
                    {
                        selectedcore = "Լիցք լրիվ";
                    }
                    break;
                case "Т-90 (3С4)":
                    path += "T-90";
                    if (selectedcore == "Լիցք 5" || selectedcore == "Լիցք 6")
                    {
                        selectedcore = "Լիցք 4";
                    }
                    break;
                case "РГМ-2 (Д-1)":
                    path += "D-1";
                    if (selectedcore == "Լիցք քչացված")
                    {
                        selectedcore = "Լիցք լրիվ";
                    }
                    break;
                default:
                    path += "RGM";
                    break;
            }
            using (Stream fStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
            {
                licqs = (List<CoreRange>)binFormat.Deserialize(fStream);
            }
            CoreRange Licq = new CoreRange();
            switch (selectedcore)
            {
                case "Ավտոմատ լիցք":
                    if (shootType == "Գետնամերձ")
                    {
                        Licq = licqs.Where(l =>
                        l.AutoMin <= distance1 &&
                        distance1 <= l.AutoMax &&
                        Math.Abs(height - l.distance) <= 250).FirstOrDefault();

                    }
                    else if (shootType == "Մարտիրային")
                    {
                        Licq = licqs.Where(l =>
                        l.MortorAutoMin >= distance1 &&
                        distance1 >= l.MortorAutoMax &&
                        Math.Abs(height - l.distance) <= 250).FirstOrDefault();

                    }
                    L = Licq != null ? Licq.Name == "Q" ? "Քչացված" : Licq.Name == "L" ? "Լրիվ" : Licq.Name : "###";
                    break;
                case "Լիցք լրիվ":
                    Licq = licqs.Where(l =>
                        l.Name == "L" &&
                         Math.Abs(height - l.distance) <= 250).FirstOrDefault();
                    L = Licq != null ? Licq.Name == "Q" ? "Քչացված" : Licq.Name == "L" ? "Լրիվ" : Licq.Name : "###";
                    break;
                case "Լիցք քչացված":
                    Licq = licqs.Where(l =>
                        l.Name == "Q" &&
                         Math.Abs(height - l.distance) <= 250).FirstOrDefault();
                    L = Licq != null ? Licq.Name == "Q" ? "Քչացված" : Licq.Name == "L" ? "Լրիվ" : Licq.Name : "###";
                    break;
                case "Լիցք 1":
                    Licq = licqs.Where(l =>
                        l.Name == "1" &&
                         Math.Abs(height - l.distance) <= 250).FirstOrDefault();
                    L = Licq != null ? Licq.Name == "Q" ? "Քչացված" : Licq.Name == "L" ? "Լրիվ" : Licq.Name : "###";
                    break;
                case "Լիցք 2":
                    Licq = licqs.Where(l =>
                        l.Name == "2" &&
                         Math.Abs(height - l.distance) <= 250).FirstOrDefault();
                    L = Licq != null ? Licq.Name == "Q" ? "Քչացված" : Licq.Name == "L" ? "Լրիվ" : Licq.Name : "###";
                    break;
                case "Լիցք 3":
                    Licq = licqs.Where(l =>
                        l.Name == "3" &&
                         Math.Abs(height - l.distance) <= 250).FirstOrDefault();
                    L = Licq != null ? Licq.Name == "Q" ? "Քչացված" : Licq.Name == "L" ? "Լրիվ" : Licq.Name : "###";
                    break;
                case "Լիցք 4":
                    Licq = licqs.Where(l =>
                        l.Name == "4" &&
                         Math.Abs(height - l.distance) <= 250).FirstOrDefault();
                    L = Licq != null ? Licq.Name == "Q" ? "Քչացված" : Licq.Name == "L" ? "Լրիվ" : Licq.Name : "###";
                    break;
                case "Լիցք 5":
                    Licq = licqs.Where(l =>
                        l.Name == "5" &&
                         Math.Abs(height - l.distance) <= 250).FirstOrDefault();
                    L = Licq != null ? Licq.Name == "Q" ? "Քչացված" : Licq.Name == "L" ? "Լրիվ" : Licq.Name : "###";
                    break;
                case "Լիցք 6":
                    Licq = licqs.Where(l =>
                        l.Name == "6" &&
                         Math.Abs(height - l.distance) <= 250).FirstOrDefault();
                    L = Licq != null ? Licq.Name == "Q" ? "Քչացված" : Licq.Name == "L" ? "Լրիվ" : Licq.Name : "###";
                    break;
            }
            core = L;

            return GetCoreInfo(Licq, selectedcore, shootType, distance1, distance2, paytucich);
        }

        public static List<CoreInfo> GetCoreInfo(CoreRange Core, string selectedcore, string shootType, double DDTEX1, double DDTEX2, string paytucich)
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            List<CoreRange> licqs = new List<CoreRange>();
            List<CoreInfo> _l = new List<CoreInfo>();
            List<CoreInfo> _q = new List<CoreInfo>();
            List<CoreInfo> _1 = new List<CoreInfo>();
            List<CoreInfo> _2 = new List<CoreInfo>();
            List<CoreInfo> _3 = new List<CoreInfo>();
            List<CoreInfo> _4 = new List<CoreInfo>();
            List<CoreInfo> _5 = new List<CoreInfo>();
            List<CoreInfo> _6 = new List<CoreInfo>();

            List<CoreInfo> _lM = new List<CoreInfo>();
            List<CoreInfo> _qM = new List<CoreInfo>();
            List<CoreInfo> _1M = new List<CoreInfo>();
            List<CoreInfo> _2M = new List<CoreInfo>();
            List<CoreInfo> _3M = new List<CoreInfo>();
            List<CoreInfo> _4M = new List<CoreInfo>();
            List<CoreInfo> _5M = new List<CoreInfo>();
            List<CoreInfo> _6M = new List<CoreInfo>();

            var source = "Data\\Core\\";
            if (paytucich == "В-90 (ОФ-462)")
            {
                source += "V-90\\";
            }
            if (paytucich == "Т-7 (С-463)")
            {
                source += "T-7\\";
            }
            if (paytucich == "РГМ-2 (ОФ-540)")
            {
                source += "D-20\\";
            }if (paytucich == "Т-90 (3С4)")
            {
                source += "T-90\\";
            }
            if (paytucich == "РГМ-2 (Д-1)")
            {
                source += "D-1\\";
            }
            #region Read All CoreMakingTables
            {
                using (Stream fStream = new FileStream(source + "CoreL", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                {
                    _l = (List<CoreInfo>)binFormat.Deserialize(fStream);
                }
                if (paytucich != "РГМ-2 (ОФ-540)" && paytucich != "РГМ-2 (Д-1)")
                    using (Stream fStream = new FileStream(source + "CoreQ", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                    {
                        _q = (List<CoreInfo>)binFormat.Deserialize(fStream);
                    }
                using (Stream fStream = new FileStream(source + "Core1", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                {
                    _1 = (List<CoreInfo>)binFormat.Deserialize(fStream);
                }
                using (Stream fStream = new FileStream(source + "Core2", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                {
                    _2 = (List<CoreInfo>)binFormat.Deserialize(fStream);
                }
                using (Stream fStream = new FileStream(source + "Core3", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                {
                    _3 = (List<CoreInfo>)binFormat.Deserialize(fStream);
                }
                if (paytucich != "Т-7 (С-463)")
                    using (Stream fStream = new FileStream(source + "Core4", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                    {
                        _4 = (List<CoreInfo>)binFormat.Deserialize(fStream);
                    }
                using (Stream fStream = new FileStream(source + "CoreLM", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                {
                    _lM = (List<CoreInfo>)binFormat.Deserialize(fStream);
                }
                if (paytucich != "РГМ-2 (ОФ-540)" && paytucich != "РГМ-2 (Д-1)")
                    using (Stream fStream = new FileStream(source + "CoreQM", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                    {
                        _qM = (List<CoreInfo>)binFormat.Deserialize(fStream);
                    }
                using (Stream fStream = new FileStream(source + "Core1M", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                {
                    _1M = (List<CoreInfo>)binFormat.Deserialize(fStream);
                }
                using (Stream fStream = new FileStream(source + "Core2M", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                {
                    _2M = (List<CoreInfo>)binFormat.Deserialize(fStream);
                }
                using (Stream fStream = new FileStream(source + "Core3M", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                {
                    _3M = (List<CoreInfo>)binFormat.Deserialize(fStream);
                }
                if (paytucich != "Т-7 (С-463)")
                    using (Stream fStream = new FileStream(source + "Core4M", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                    {
                        _4M = (List<CoreInfo>)binFormat.Deserialize(fStream);
                    }
                if (paytucich == "РГМ-2 (ОФ-540)" || paytucich == "РГМ-2 (Д-1)")
                {
                    using (Stream fStream = new FileStream(source + "Core5", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                    {
                        _5 = (List<CoreInfo>)binFormat.Deserialize(fStream);
                    }
                    using (Stream fStream = new FileStream(source + "Core5M", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                    {
                        _5M = (List<CoreInfo>)binFormat.Deserialize(fStream);
                    }
                    using (Stream fStream = new FileStream(source + "Core6", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                    {
                        _6 = (List<CoreInfo>)binFormat.Deserialize(fStream);
                    }
                    using (Stream fStream = new FileStream(source + "Core6M", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                    {
                        _6M = (List<CoreInfo>)binFormat.Deserialize(fStream);
                    }
                }
            }
            #endregion

            CoreInfo GN1 = new CoreInfo();
            CoreInfo GN2 = new CoreInfo();
            int closestDistanse1 = 0, closestDistanse2 = 0;
            if (shootType == "Գետնամերձ")
            {
                if (Core != null)
                {
                    switch (Core.Name)
                    {
                        case "L":
                            closestDistanse1 = GetClosestDistance(DDTEX1, _l);
                            closestDistanse2 = GetClosestDistance(DDTEX2, _l);
                            GN1 = _l.Where(n => n.TDistance == closestDistanse1).Single();
                            GN2 = _l.Where(n => n.TDistance == closestDistanse2).Single();
                            break;
                        case "Q":
                            closestDistanse1 = GetClosestDistance(DDTEX1, _q);
                            closestDistanse2 = GetClosestDistance(DDTEX2, _q);
                            GN1 = _q.Where(n => n.TDistance == closestDistanse1).Single();
                            GN2 = _q.Where(n => n.TDistance == closestDistanse2).Single();
                            break;
                        case "1":
                            closestDistanse1 = GetClosestDistance(DDTEX1, _1);
                            closestDistanse2 = GetClosestDistance(DDTEX2, _1);
                            GN1 = _1.Where(n => n.TDistance == closestDistanse1).Single();
                            GN2 = _1.Where(n => n.TDistance == closestDistanse2).Single();
                            break;
                        case "2":
                            closestDistanse1 = GetClosestDistance(DDTEX1, _2);
                            closestDistanse2 = GetClosestDistance(DDTEX2, _2);
                            GN1 = _2.Where(n => n.TDistance == closestDistanse1).Single();
                            GN2 = _2.Where(n => n.TDistance == closestDistanse2).Single();
                            break;
                        case "3":
                            closestDistanse1 = GetClosestDistance(DDTEX1, _3);
                            closestDistanse2 = GetClosestDistance(DDTEX2, _3);
                            GN1 = _3.Where(n => n.TDistance == closestDistanse1).Single();
                            GN2 = _3.Where(n => n.TDistance == closestDistanse2).Single();
                            break;
                        case "4":
                            closestDistanse1 = GetClosestDistance(DDTEX1, _4);
                            closestDistanse2 = GetClosestDistance(DDTEX2, _4);
                            GN1 = _4.Where(n => n.TDistance == closestDistanse1).Single();
                            GN2 = _4.Where(n => n.TDistance == closestDistanse2).Single();
                            break;
                        case "5":
                            closestDistanse1 = GetClosestDistance(DDTEX1, _5);
                            closestDistanse2 = GetClosestDistance(DDTEX2, _5);
                            GN1 = _5.Where(n => n.TDistance == closestDistanse1).Single();
                            GN2 = _5.Where(n => n.TDistance == closestDistanse2).Single();
                            break;
                        case "6":
                            closestDistanse1 = GetClosestDistance(DDTEX1, _6);
                            closestDistanse2 = GetClosestDistance(DDTEX2, _6);
                            GN1 = _6.Where(n => n.TDistance == closestDistanse1).Single();
                            GN2 = _6.Where(n => n.TDistance == closestDistanse2).Single();
                            break;
                    }
                }
            }

            if (shootType == "Մարտիրային")
            {
                if (Core != null)
                {
                    switch (Core.Name)
                    {
                        case "L":
                            closestDistanse1 = GetClosestDistance(DDTEX1, _lM);
                            closestDistanse2 = GetClosestDistance(DDTEX2, _lM);
                            GN1 = _lM.Where(n => n.TDistance == closestDistanse1).Single();
                            GN2 = _lM.Where(n => n.TDistance == closestDistanse2).Single();
                            break;
                        case "Q":
                            closestDistanse1 = GetClosestDistance(DDTEX1, _qM);
                            closestDistanse2 = GetClosestDistance(DDTEX2, _qM);
                            GN1 = _qM.Where(n => n.TDistance == closestDistanse1).Single();
                            GN2 = _qM.Where(n => n.TDistance == closestDistanse2).Single();
                            break;
                        case "1":
                            closestDistanse1 = GetClosestDistance(DDTEX1, _1M);
                            closestDistanse2 = GetClosestDistance(DDTEX2, _1M);
                            GN1 = _1M.Where(n => n.TDistance == closestDistanse1).Single();
                            GN2 = _1M.Where(n => n.TDistance == closestDistanse2).Single();
                            break;
                        case "2":
                            closestDistanse1 = GetClosestDistance(DDTEX1, _2M);
                            closestDistanse2 = GetClosestDistance(DDTEX2, _2M);
                            GN1 = _2M.Where(n => n.TDistance == closestDistanse1).Single();
                            GN2 = _2M.Where(n => n.TDistance == closestDistanse2).Single();
                            break;
                        case "3":
                            closestDistanse1 = GetClosestDistance(DDTEX1, _3M);
                            closestDistanse2 = GetClosestDistance(DDTEX2, _3M);
                            GN1 = _3M.Where(n => n.TDistance == closestDistanse1).Single();
                            GN2 = _3M.Where(n => n.TDistance == closestDistanse2).Single();
                            break;
                        case "4":
                            closestDistanse1 = GetClosestDistance(DDTEX1, _4M);
                            closestDistanse2 = GetClosestDistance(DDTEX2, _4M);
                            GN1 = _4M.Where(n => n.TDistance == closestDistanse1).Single();
                            GN2 = _4M.Where(n => n.TDistance == closestDistanse2).Single();
                            break;
                        case "5":
                            closestDistanse1 = GetClosestDistance(DDTEX1, _5M);
                            closestDistanse2 = GetClosestDistance(DDTEX2, _5M);
                            GN1 = _5M.Where(n => n.TDistance == closestDistanse1).Single();
                            GN2 = _5M.Where(n => n.TDistance == closestDistanse2).Single();
                            break;
                        case "6":
                            closestDistanse1 = GetClosestDistance(DDTEX1, _6M);
                            closestDistanse2 = GetClosestDistance(DDTEX2, _6M);
                            GN1 = _6M.Where(n => n.TDistance == closestDistanse1).Single();
                            GN2 = _6M.Where(n => n.TDistance == closestDistanse2).Single();
                            break;
                    }
                }
            }

            List<CoreInfo> GNS = new List<CoreInfo>{GN1, GN2};
            if (selectedcore != "Ավտոմատ լիցք")
            {
                if (shootType == "Գետնամերձ")
                {
                    if (DDTEX1 > Core.ManualMax || DDTEX1 < Core.ManualMin)
                    {
                        GNS[0] = null;
                    }
                    if (DDTEX2 > Core.ManualMax || DDTEX2 < Core.ManualMin)
                    {
                        GNS[1] = null;
                    }
                }
                if (shootType == "Մարտիրային")
                {
                    if (DDTEX1 < Core.MortorManualMax || DDTEX1 > Core.MortorManualMin)
                    {
                        GNS[0] = null;
                    }
                    if (DDTEX2 < Core.MortorManualMax || DDTEX2 > Core.MortorManualMin)
                    {
                        GNS[1] = null;
                    }
                }
            }
            return GNS;

        }

        static int GetClosestDistance(double distance, List<CoreInfo> table)
        {
            int D = table[0].TDistance;
            double mindif = Math.Abs(table[0].TDistance - distance);
            foreach (var d in table)
            {
                double CurentDif = Math.Abs(d.TDistance - distance);
                if (mindif > CurentDif)
                {
                    mindif = CurentDif;
                    D = d.TDistance;
                }
            }
            return D;
        }

        public static double GetLevel(int distance, string core, string shootingType, int hkd, int ht, string paytucich)
        {
            int hDif = ht - hkd;
            if (hDif == 0)
            {
                return 30;
            }

            BinaryFormatter binFormat = new BinaryFormatter();
            List<Level> lvl = new List<Level>();
            switch (shootingType)
            {
                case "Գետնամերձ":
                    if (paytucich != "РГМ-2 (ОФ-540)")
                    {
                        switch (core)
                        {
                            case "Լրիվ":

                                if (hDif > 0)
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_LB", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                else
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_LC", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                break;
                            case "Քչացված":
                                if (hDif > 0)
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_QB", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                else
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_QC", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                break;
                            case "1":
                                if (hDif > 0)
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_1B", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                else
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_1C", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                break;
                            case "2":
                                if (hDif > 0)
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_2B", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                else
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_2C", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                break;
                            case "3":
                                if (hDif > 0)
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_3B", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                else
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_3C", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                break;
                            case "4":
                                if (hDif > 0)
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_4B", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                else
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_4C", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                break;

                        }
                    }
                    else
                    {
                        switch (core)
                        {
                            case "Լրիվ":

                                if (hDif > 0)
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\D20\\LvL_LB", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                else
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\D20\\LvL_LC", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                break;
                            case "1":

                                if (hDif > 0)
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\D20\\LvL_1B", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                else
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\D20\\LvL_1C", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                break;
                            case "2":

                                if (hDif > 0)
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\D20\\LvL_2B", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                else
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\D20\\LvL_2C", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                break;
                            case "3":

                                if (hDif > 0)
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\D20\\LvL_3B", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                else
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\D20\\LvL_3C", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                break;
                            case "4":

                                if (hDif > 0)
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\D20\\LvL_4B", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                else
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\D20\\LvL_4C", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                break;
                            case "5":

                                if (hDif > 0)
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\D20\\LvL_5B", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                else
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\D20\\LvL_5C", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                break;
                            case "6":

                                if (hDif > 0)
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\D20\\LvL_6B", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                else
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\D20\\LvL_6C", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case "Մարտիրային":
                    if (paytucich != "РГМ-2 (ОФ-540)")
                    {
                        switch (core)
                        {
                            case "Լրիվ":
                                if (hDif > 0)
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_LMB", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                else
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_LMC", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                break;
                            case "Քչացված":
                                if (hDif > 0)
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_QMB", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                else
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_QMC", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                break;
                            case "1":
                                if (hDif > 0)
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_1MB", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                else
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_1MC", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                break;
                            case "2":
                                if (hDif > 0)
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_2MB", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                else
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_2MC", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                break;
                            case "3":
                                if (hDif > 0)
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_3MB", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                else
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_3MC", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                break;
                            case "4":
                                if (hDif > 0)
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_4MB", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                else
                                {
                                    using (Stream fStream = new FileStream("Data\\Levels\\LvL_4MC", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                                    {
                                        lvl = (List<Level>)binFormat.Deserialize(fStream);
                                    }
                                }
                                break;

                        }

                        break;
                    }
                    else
                    {
                        throw new FormatException();
                    }

            }




            int UP1, UP2, DOWN1, DOWN2, UP, DOWN;
            int mindist = Getindex(lvl.Select(l => l.Distance).ToList(), distance);
            if (Math.Abs(hDif) < 50)
            {
                if (lvl.Select(l => l.Distance).ToList().Contains(distance))
                {
                    int vl = lvl.Where(l => l.Distance == lvl[mindist].Distance).Single().Values[0];
                    return 30 + Math.Round((((double)hDif) / 50 * vl) / 100, 2);
                }
                UP = lvl.Where(l => l.Distance == lvl[mindist].Distance).Single().Values[0];
                DOWN = lvl.Where(l => l.Distance == lvl[mindist + 1].Distance).Single().Values[0];
                int pr;
                if (UP == DOWN) pr = UP;
                else pr = ((Math.Abs(distance) - lvl[mindist].Distance) / (Math.Abs(lvl[mindist].Distance - lvl[mindist + 1].Distance) / Math.Abs(UP - DOWN))) + Math.Min(UP, DOWN);
                return 30 + Math.Round(((((double)hDif / 50) * pr)) / 100, 2);
            }
            int minh = Getindex(lvl.Where(l => l.Distance == lvl[mindist].Distance).Single().M, Math.Abs(hDif));
            if (lvl.Select(l => l.Distance).ToList().Contains(distance) && lvl.First().M.Contains(Math.Abs(hDif)))
            {           // exact mach
                UP1 = lvl.Where(l => l.Distance == lvl[mindist].Distance).Single().Values[minh];
                UP2 = lvl.Where(l => l.Distance == lvl[mindist].Distance).Single().Values[minh];
                DOWN1 = lvl.Where(l => l.Distance == lvl[mindist].Distance).Single().Values[minh];
                DOWN2 = lvl.Where(l => l.Distance == lvl[mindist].Distance).Single().Values[minh];
            }
            else if (lvl.Select(l => l.Distance).ToList().Contains(distance))
            {      // distance maches
                UP1 = lvl.Where(l => l.Distance == lvl[mindist].Distance).Single().Values[minh];
                UP2 = lvl.Where(l => l.Distance == lvl[mindist].Distance).Single().Values[minh + 1];
                DOWN1 = lvl.Where(l => l.Distance == lvl[mindist].Distance).Single().Values[minh];
                DOWN2 = lvl.Where(l => l.Distance == lvl[mindist].Distance).Single().Values[minh + 1];
            }
            else if (lvl.First().M.Contains(Math.Abs(hDif)))
            {      // hdif maches
                UP1 = lvl.Where(l => l.Distance == lvl[mindist].Distance).Single().Values[minh];
                UP2 = lvl.Where(l => l.Distance == lvl[mindist].Distance).Single().Values[minh];
                DOWN1 = lvl.Where(l => l.Distance == lvl[mindist + 1].Distance).Single().Values[minh];
                DOWN2 = lvl.Where(l => l.Distance == lvl[mindist + 1].Distance).Single().Values[minh];
            }
            else
            {
                UP1 = lvl.Where(l => l.Distance == lvl[mindist].Distance).Single().Values[minh];
                UP2 = lvl.Where(l => l.Distance == lvl[mindist].Distance).Single().Values[minh + 1];
                DOWN1 = lvl.Where(l => l.Distance == lvl[mindist + 1].Distance).Single().Values[minh];
                DOWN2 = lvl.Where(l => l.Distance == lvl[mindist + 1].Distance).Single().Values[minh + 1];
            }

            if (hDif == 0) return 30;

            double retval;
            if (UP1 == UP2) UP = UP1;
            else UP = ((Math.Abs(hDif) - lvl[mindist].M[minh]) / ((lvl[mindist].M[minh + 1] - lvl[mindist].M[minh]) / Math.Abs(UP2 - UP1))) + UP1;
            if (DOWN1 == DOWN2) DOWN = DOWN1;
            else DOWN = ((Math.Abs(hDif) - lvl[mindist].M[minh]) / ((lvl[mindist].M[minh + 1] - lvl[mindist].M[minh]) / Math.Abs(DOWN2 - DOWN1))) + DOWN1;
            if (UP == DOWN) retval = UP;
            else retval = ((Math.Abs(distance) - lvl[mindist].Distance) / (Math.Abs(lvl[mindist].Distance - lvl[mindist + 1].Distance) / Math.Abs(UP - DOWN))) + Math.Min(UP, DOWN);
            if (hDif < 0)
            {
                retval = 30 - (Math.Abs(retval) / 100);
            }
            else
            {
                retval = 30 + (Math.Abs(retval) / 100);
            }
            return retval;
        }

        public static int Getindex(List<int> ints, int val)
        {
            try
            {
                if (ints.Contains(val)) return ints.IndexOf(val);
                for (int i = 0; i < ints.Count; i++)
                {
                    if (val > ints[i] && val < ints[i + 1]) return i;
                    if (i > 0)
                    {
                        if (val < ints[i] && val > ints[i - 1]) return i - 1;
                    }
                    if (val < ints[i] && val > ints[i + 1]) return i;
                    if (i > 0)
                    {
                        if (val < ints[i] && val > ints[i - 1]) return i - 1;
                    }

                }
            }
            catch
            {
                return -1;
            }
            return -1;
        }
    }
}
