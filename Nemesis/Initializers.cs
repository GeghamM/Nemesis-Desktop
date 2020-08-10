using BaseEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Nemesis
{
    public static class Initializers
    {
        public static string SourcePath { get { return Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory().ToString()).ToString()).ToString() + "\\Files"; } }
        private static int IntParse(string s) { return int.TryParse(s, out _) ? int.Parse(s) : -1; }
        public static void InitializeCoreRanges()
        {
            InitCoreRanges("RGM"); InitCoreRanges("T-7"); InitCoreRanges("T-90"); InitCoreRanges("D-20"); InitCoreRanges("D-1");
        }
        private static void InitCoreRanges(string Name)
        {
            try
            {
                string[] text = File.ReadAllLines(Path.Combine(SourcePath, "Cores", Name + ".txt"));

                List<CoreRange> licqs = new List<CoreRange>();
                foreach (var s in text)
                {
                    var t = s.Split('\t');
                    licqs.Add(new CoreRange
                    {
                        Name = t[0],
                        distance = int.Parse(t[1]),
                        AutoMin = int.Parse(t[2]),
                        ManualMin = int.Parse(t[3]),
                        MortorAutoMin = int.Parse(t[4]),
                        MortorManualMin = int.Parse(t[5]),
                        AutoMax = int.Parse(t[6]),
                        ManualMax = int.Parse(t[7]),
                        MortorAutoMax = int.Parse(t[8]),
                        MortorManualMax = int.Parse(t[9]),
                    });
                }

                BinaryFormatter binFormat = new BinaryFormatter();

                using (Stream fStream = new FileStream("Data\\CoreRange\\" + Name, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, licqs);
                }
            } catch { }
        }

        private static void InitCore(string sourse, string name)
        {
            try
            {
                string[] text = File.ReadAllLines(sourse);
                List<CoreInfo> gunN = new List<CoreInfo>();
                BinaryFormatter binFormat = new BinaryFormatter();
                foreach (var s in text)
                {
                    var t = s.Split('\t');
                    gunN.Add(new CoreInfo
                    {
                        TDistance = int.Parse(t[0]),
                        pr0 = int.TryParse(t[1], out _) ? int.Parse(t[1]) : -1,
                        N0 = int.TryParse(t[2], out _) ? int.Parse(t[2]) : -1,
                        pr500 = int.TryParse(t[3], out _) ? int.Parse(t[3]) : -1,
                        N500 = int.TryParse(t[4], out _) ? int.Parse(t[4]) : -1,
                        pr1000 = int.TryParse(t[5], out _) ? int.Parse(t[5]) : -1,
                        N1000 = int.TryParse(t[6], out _) ? int.Parse(t[6]) : -1,
                        pr1500 = int.TryParse(t[7], out _) ? int.Parse(t[7]) : -1,
                        N1500 = int.TryParse(t[8], out _) ? int.Parse(t[8]) : -1,
                        pr2000 = int.TryParse(t[9], out _) ? int.Parse(t[9]) : -1,
                        N2000 = int.TryParse(t[10], out _) ? int.Parse(t[10]) : -1,
                        pr2500 = int.TryParse(t[11], out _) ? int.Parse(t[11]) : -1,
                        N2500 = int.TryParse(t[12], out _) ? int.Parse(t[12]) : -1,
                        pr3000 = int.TryParse(t[13], out _) ? int.Parse(t[13]) : -1,
                        N3000 = int.TryParse(t[14], out _) ? int.Parse(t[14]) : -1,
                        DX = float.TryParse(t[15], out _) ? Math.Round(float.Parse(t[15]), 1) : -1,
                        Z = int.TryParse(t[16], out _) ? int.Parse(t[16]) : -1,
                        DY = float.TryParse(t[17], out _) ? float.Parse(t[17]) : -1,
                        VD = float.TryParse(t[18], out _) ? float.Parse(t[18]) : -1,
                        VRV = float.TryParse(t[19], out _) ? float.Parse(t[19]) : -1,
                        T = float.TryParse(t[20], out _) ? float.Parse(t[20]) : -1,
                        Q = float.TryParse(t[21], out _) ? float.Parse(t[21]) : -1,
                    });
                }
                using (Stream fStream = new FileStream(name, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, gunN);
                }
            }
            catch { }
        }


        public static void InitiazieCores()
        {
            InitCore(SourcePath + "\\L.txt", "Data\\Core\\CoreL");
            InitCore(SourcePath + "\\Q.txt", "Data\\Core\\CoreQ");
            InitCore(SourcePath + "\\1.txt", "Data\\Core\\Core1");
            InitCore(SourcePath + "\\2.txt", "Data\\Core\\Core2");
            InitCore(SourcePath + "\\3.txt", "Data\\Core\\Core3");
            InitCore(SourcePath + "\\4.txt", "Data\\Core\\Core4");
            InitCore(SourcePath + "\\LM.txt", "Data\\Core\\CoreLM");
            InitCore(SourcePath + "\\QM.txt", "Data\\Core\\CoreQM");
            InitCore(SourcePath + "\\1M.txt", "Data\\Core\\Core1M");
            InitCore(SourcePath + "\\2M.txt", "Data\\Core\\Core2M");
            InitCore(SourcePath + "\\3M.txt", "Data\\Core\\Core3M");
            InitCore(SourcePath + "\\4M.txt", "Data\\Core\\Core4M");

            InitCore(SourcePath + "\\V-90\\L.txt", "Data\\Core\\V-90\\CoreL");
            InitCore(SourcePath + "\\V-90\\Q.txt", "Data\\Core\\V-90\\CoreQ");
            InitCore(SourcePath + "\\V-90\\1.txt", "Data\\Core\\V-90\\Core1");
            InitCore(SourcePath + "\\V-90\\2.txt", "Data\\Core\\V-90\\Core2");
            InitCore(SourcePath + "\\V-90\\3.txt", "Data\\Core\\V-90\\Core3");
            InitCore(SourcePath + "\\V-90\\4.txt", "Data\\Core\\V-90\\Core4");
            InitCore(SourcePath + "\\V-90\\LM.txt", "Data\\Core\\V-90\\CoreLM");
            InitCore(SourcePath + "\\V-90\\QM.txt", "Data\\Core\\V-90\\CoreQM");
            InitCore(SourcePath + "\\V-90\\1M.txt", "Data\\Core\\V-90\\Core1M");
            InitCore(SourcePath + "\\V-90\\2M.txt", "Data\\Core\\V-90\\Core2M");
            InitCore(SourcePath + "\\V-90\\3M.txt", "Data\\Core\\V-90\\Core3M");
            InitCore(SourcePath + "\\V-90\\4M.txt", "Data\\Core\\V-90\\Core4M");

            InitCore(SourcePath + "\\T-7\\L.txt", "Data\\Core\\T-7\\CoreL");
            InitCore(SourcePath + "\\T-7\\Q.txt", "Data\\Core\\T-7\\CoreQ");
            InitCore(SourcePath + "\\T-7\\1.txt", "Data\\Core\\T-7\\Core1");
            InitCore(SourcePath + "\\T-7\\2.txt", "Data\\Core\\T-7\\Core2");
            InitCore(SourcePath + "\\T-7\\3.txt", "Data\\Core\\T-7\\Core3");
            InitCore(SourcePath + "\\T-7\\4.txt", "Data\\Core\\T-7\\Core4");
            InitCore(SourcePath + "\\T-7\\LM.txt", "Data\\Core\\T-7\\CoreLM");
            InitCore(SourcePath + "\\T-7\\QM.txt", "Data\\Core\\T-7\\CoreQM");
            InitCore(SourcePath + "\\T-7\\1M.txt", "Data\\Core\\T-7\\Core1M");
            InitCore(SourcePath + "\\T-7\\2M.txt", "Data\\Core\\T-7\\Core2M");
            InitCore(SourcePath + "\\T-7\\3M.txt", "Data\\Core\\T-7\\Core3M");
            InitCore(SourcePath + "\\T-7\\4M.txt", "Data\\Core\\T-7\\Core4M");

            InitCore(SourcePath + "\\T-90\\L.txt", "Data\\Core\\T-90\\CoreL");
            InitCore(SourcePath + "\\T-90\\Q.txt", "Data\\Core\\T-90\\CoreQ");
            InitCore(SourcePath + "\\T-90\\1.txt", "Data\\Core\\T-90\\Core1");
            InitCore(SourcePath + "\\T-90\\2.txt", "Data\\Core\\T-90\\Core2");
            InitCore(SourcePath + "\\T-90\\3.txt", "Data\\Core\\T-90\\Core3");
            InitCore(SourcePath + "\\T-90\\4.txt", "Data\\Core\\T-90\\Core4");
            InitCore(SourcePath + "\\T-90\\LM.txt", "Data\\Core\\T-90\\CoreLM");
            InitCore(SourcePath + "\\T-90\\QM.txt", "Data\\Core\\T-90\\CoreQM");
            InitCore(SourcePath + "\\T-90\\1M.txt", "Data\\Core\\T-90\\Core1M");
            InitCore(SourcePath + "\\T-90\\2M.txt", "Data\\Core\\T-90\\Core2M");
            InitCore(SourcePath + "\\T-90\\3M.txt", "Data\\Core\\T-90\\Core3M");
            InitCore(SourcePath + "\\T-90\\4M.txt", "Data\\Core\\T-90\\Core4M");

            InitCore(SourcePath + "\\D20\\L.txt", "Data\\Core\\D-20\\CoreL");
            InitCore(SourcePath + "\\D20\\1.txt", "Data\\Core\\D-20\\Core1");
            InitCore(SourcePath + "\\D20\\2.txt", "Data\\Core\\D-20\\Core2");
            InitCore(SourcePath + "\\D20\\3.txt", "Data\\Core\\D-20\\Core3");
            InitCore(SourcePath + "\\D20\\4.txt", "Data\\Core\\D-20\\Core4");
            InitCore(SourcePath + "\\D20\\5.txt", "Data\\Core\\D-20\\Core5");
            InitCore(SourcePath + "\\D20\\6.txt", "Data\\Core\\D-20\\Core6");
            InitCore(SourcePath + "\\D20\\LM.txt", "Data\\Core\\D-20\\CoreLM");
            InitCore(SourcePath + "\\D20\\1M.txt", "Data\\Core\\D-20\\Core1M");
            InitCore(SourcePath + "\\D20\\2M.txt", "Data\\Core\\D-20\\Core2M");
            InitCore(SourcePath + "\\D20\\3M.txt", "Data\\Core\\D-20\\Core3M");
            InitCore(SourcePath + "\\D20\\4M.txt", "Data\\Core\\D-20\\Core4M");
            InitCore(SourcePath + "\\D20\\5M.txt", "Data\\Core\\D-20\\Core5M");
            InitCore(SourcePath + "\\D20\\6M.txt", "Data\\Core\\D-20\\Core6M");

            InitCore(SourcePath + "\\D1\\L.txt", "Data\\Core\\D-1\\CoreL");
            InitCore(SourcePath + "\\D1\\1.txt", "Data\\Core\\D-1\\Core1");
            InitCore(SourcePath + "\\D1\\2.txt", "Data\\Core\\D-1\\Core2");
            InitCore(SourcePath + "\\D1\\3.txt", "Data\\Core\\D-1\\Core3");
            InitCore(SourcePath + "\\D1\\4.txt", "Data\\Core\\D-1\\Core4");
            InitCore(SourcePath + "\\D1\\5.txt", "Data\\Core\\D-1\\Core5");
            InitCore(SourcePath + "\\D1\\6.txt", "Data\\Core\\D-1\\Core6");
            InitCore(SourcePath + "\\D1\\LM.txt", "Data\\Core\\D-1\\CoreLM");
            InitCore(SourcePath + "\\D1\\1M.txt", "Data\\Core\\D-1\\Core1M");
            InitCore(SourcePath + "\\D1\\2M.txt", "Data\\Core\\D-1\\Core2M");
            InitCore(SourcePath + "\\D1\\3M.txt", "Data\\Core\\D-1\\Core3M");
            InitCore(SourcePath + "\\D1\\4M.txt", "Data\\Core\\D-1\\Core4M");
            InitCore(SourcePath + "\\D1\\5M.txt", "Data\\Core\\D-1\\Core5M");
            InitCore(SourcePath + "\\D1\\6M.txt", "Data\\Core\\D-1\\Core6M");
        }

        private static void InitLvl(string filePath, string binPath)
        {
            try
            {
                string[] text = File.ReadAllLines(filePath);
                List<Level> Lvls = new List<Level>();
                BinaryFormatter binFormat = new BinaryFormatter();
                foreach (var s in text)
                {
                    var t = s.Split('\t');
                    Lvls.Add(new Level
                    {
                        Dpr = int.Parse(t[0]),
                        Distance = int.Parse(t[1]),
                        M = new List<int>
                    {
                        50,100,150,200,250,300,350,400,450,500,550,600,650,700,750,800,850,
                        900,950,1000,1050,1100,1150,1200,1250,1300,1350,1400,1450,1500
                    },
                        Values = new List<int>
                    {
                        IntParse(t[2]),IntParse(t[3]),IntParse(t[4]),IntParse(t[5]),IntParse(t[6]),
                        IntParse(t[7]),IntParse(t[8]),IntParse(t[9]),IntParse(t[10]),IntParse(t[11]),
                        IntParse(t[12]),IntParse(t[13]),IntParse(t[14]),IntParse(t[15]),IntParse(t[16]),
                        IntParse(t[17]),IntParse(t[18]),IntParse(t[19]),IntParse(t[20]),IntParse(t[21]),
                        IntParse(t[22]),IntParse(t[23]),IntParse(t[24]),IntParse(t[25]),IntParse(t[26]),
                        IntParse(t[27]),IntParse(t[28]),IntParse(t[29]),IntParse(t[30]),IntParse(t[31]),
                    }
                    });
                }
                using (Stream fStream = new FileStream(binPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, Lvls);
                }
            }
            catch { }
        }

        public static void InitiazieLevels()
        {
            InitLvl(SourcePath + "\\Levels\\LB.txt", "Data\\Levels\\LvL_LB");
            InitLvl(SourcePath + "\\\\Levels\\LC.txt", "Data\\Levels\\LvL_LC");
            InitLvl(SourcePath + "\\\\Levels\\LMB.txt", "Data\\Levels\\LvL_LMB");
            InitLvl(SourcePath + "\\\\Levels\\LMC.txt", "Data\\Levels\\LvL_LMC");

            InitLvl(SourcePath + "\\\\Levels\\QB.txt", "Data\\Levels\\LvL_QB");
            InitLvl(SourcePath + "\\\\Levels\\QC.txt", "Data\\Levels\\LvL_QC");
            InitLvl(SourcePath + "\\\\Levels\\QMB.txt", "Data\\Levels\\LvL_QMB");
            InitLvl(SourcePath + "\\\\Levels\\QMC.txt", "Data\\Levels\\LvL_QMC");

            InitLvl(SourcePath + "\\\\Levels\\1B.txt", "Data\\Levels\\LvL_1B");
            InitLvl(SourcePath + "\\\\Levels\\1C.txt", "Data\\Levels\\LvL_1C");
            InitLvl(SourcePath + "\\\\Levels\\1MB.txt", "Data\\Levels\\LvL_1MB");
            InitLvl(SourcePath + "\\\\Levels\\1MC.txt", "Data\\Levels\\LvL_1MC");

            InitLvl(SourcePath + "\\\\Levels\\2B.txt", "Data\\Levels\\LvL_2B");
            InitLvl(SourcePath + "\\\\Levels\\2C.txt", "Data\\Levels\\LvL_2C");
            InitLvl(SourcePath + "\\\\Levels\\2MB.txt", "Data\\Levels\\LvL_2MB");
            InitLvl(SourcePath + "\\\\Levels\\2MC.txt", "Data\\Levels\\LvL_2MC");

            InitLvl(SourcePath + "\\\\Levels\\3B.txt", "Data\\Levels\\LvL_3B");
            InitLvl(SourcePath + "\\\\Levels\\3C.txt", "Data\\Levels\\LvL_3C");
            InitLvl(SourcePath + "\\\\Levels\\3MB.txt", "Data\\Levels\\LvL_3MB");
            InitLvl(SourcePath + "\\\\Levels\\3MC.txt", "Data\\Levels\\LvL_3MC");

            InitLvl(SourcePath + "\\\\Levels\\4B.txt", "Data\\Levels\\LvL_4B");
            InitLvl(SourcePath + "\\\\Levels\\4C.txt", "Data\\Levels\\LvL_4C");
            InitLvl(SourcePath + "\\\\Levels\\4MB.txt", "Data\\Levels\\LvL_4MB");
            InitLvl(SourcePath + "\\\\Levels\\4MC.txt", "Data\\Levels\\LvL_4MC");

            InitLvl(SourcePath + "\\\\Levels\\D20\\LB.txt", "Data\\Levels\\D20\\LvL_LB");
            InitLvl(SourcePath + "\\\\Levels\\D20\\LC.txt", "Data\\Levels\\D20\\LvL_LC");
            InitLvl(SourcePath + "\\\\Levels\\D20\\1B.txt", "Data\\Levels\\D20\\LvL_1B");
            InitLvl(SourcePath + "\\\\Levels\\D20\\1C.txt", "Data\\Levels\\D20\\LvL_1C");
            InitLvl(SourcePath + "\\\\Levels\\D20\\2B.txt", "Data\\Levels\\D20\\LvL_2B");
            InitLvl(SourcePath + "\\\\Levels\\D20\\2C.txt", "Data\\Levels\\D20\\LvL_2C");
            InitLvl(SourcePath + "\\\\Levels\\D20\\3B.txt", "Data\\Levels\\D20\\LvL_3B");
            InitLvl(SourcePath + "\\\\Levels\\D20\\3C.txt", "Data\\Levels\\D20\\LvL_3C");
            InitLvl(SourcePath + "\\\\Levels\\D20\\4B.txt", "Data\\Levels\\D20\\LvL_4B");
            InitLvl(SourcePath + "\\\\Levels\\D20\\4C.txt", "Data\\Levels\\D20\\LvL_4C");
            InitLvl(SourcePath + "\\\\Levels\\D20\\5B.txt", "Data\\Levels\\D20\\LvL_5B");
            InitLvl(SourcePath + "\\\\Levels\\D20\\5C.txt", "Data\\Levels\\D20\\LvL_5C");
            InitLvl(SourcePath + "\\\\Levels\\D20\\6B.txt", "Data\\Levels\\D20\\LvL_6B");
            InitLvl(SourcePath + "\\\\Levels\\D20\\6C.txt", "Data\\Levels\\D20\\LvL_6C");

        }
    }
}
