using BaseEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace DataAccessLayer
{
    public static class DataWriter
    {
        private static BinaryFormatter binFormat = new BinaryFormatter();
        public static bool SaveMartakarg(BaseEntities.Martakarg mk)
        {

            try
            {
                using (Stream fStream = new FileStream("Data\\Martakarg", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, mk);
                }
                Owin.Martakarg = mk;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool SaveAlphas(BaseEntities.Alphas a)
        {
            try
            {
                using (Stream fStream = new FileStream("Data\\Alphas", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, a);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool SaveTargets(string s)
        {
            var targets = new List<BaseEntities.Target>();

            if (File.Exists("Data\\Targets"))
            {
                using (Stream fStream = File.OpenRead("Data\\Targets"))
                {
                    targets = (List<BaseEntities.Target>)binFormat.Deserialize(fStream);
                }
            }

            string[] text = s.Split('\n');
            List<Target> Targets = new List<Target>();
            int a;
            foreach (var l in text)
            {
                var t = l.Split('\t');
                Targets.Add(new Target(t[0], t[1], int.TryParse(t[2], out a) ? int.Parse(t[2]) : 0, int.TryParse(t[3], out a) ? int.Parse(t[3]) : 0, int.TryParse(t[4], out a) ? int.Parse(t[4]) : 0, int.TryParse(t[5], out a) ? int.Parse(t[5]) : 0, int.TryParse(t[6], out a) ? int.Parse(t[6]) : 0));

            }

            foreach (var target in Targets)
            {
                AddTarget(target);
            }

            return true;
        }

        public static bool AddTarget(BaseEntities.Target t, bool SaveNum = false)
        {
            try
            {
                if (!File.Exists("Data\\Targets"))
                {
                    using (Stream fStream = new FileStream("Data\\Targets", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                    {
                        binFormat.Serialize(fStream, new List<Target>());
                    }
                }
                List<Target> targets = new List<Target>();
                using (Stream fStream = File.OpenRead("Data\\Targets"))
                {
                    targets = (List<Target>)binFormat.Deserialize(fStream);
                }
                if (SaveNum == false)
                {
                    if (targets.Count == 0) t.Num = 1;
                    else t.Num = targets.Select(i => i.Num).Max() + 1;
                }
                else
                {
                    t.Num = targets.Where(ot => ot.ID == t.ID).First().Num;
                }
                targets = targets.Where(ot => ot.ID != t.ID).ToList();
                targets.Add(t);
                using (Stream fStream = new FileStream("Data\\Targets", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, targets.OrderBy(tn => tn.ID).ToList());
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool DeleteTarget(string Number)
        {
            try
            {
                if (!File.Exists("Data\\Targets"))
                {
                    using (Stream fStream = new FileStream("Data\\Targets", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                    {
                        binFormat.Serialize(fStream, new List<Target>());
                    }
                }
                List<Target> targets = new List<Target>();
                using (Stream fStream = File.OpenRead("Data\\Targets"))
                {
                    targets = (List<Target>)binFormat.Deserialize(fStream);
                }
                targets = targets.Where(ot => ot.ID != Number).ToList();
                using (Stream fStream = new FileStream("Data\\Targets", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, targets.OrderBy(tn => tn.ID).ToList());
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool ClearTargets()
        {
            try
            {
                using (Stream fStream = new FileStream("Data\\Targets", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, new List<Target>());
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ClearAXKTargets()
        {
            try
            {
                using (Stream fStream = new FileStream("Data\\AXKTargets", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, new List<AXK>());
                }
                return true;
            }
            catch
            {
                return false;
            }
        }



        ///////////////////////////////// AXK /////////////////////////////////////
        public static bool AddAXKTarget(BaseEntities.AXK t, bool SaveNum = false)
        {
            try
            {
                if (!File.Exists("Data\\AXKTargets"))
                {
                    using (Stream fStream = new FileStream("Data\\AXKTargets", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                    {
                        binFormat.Serialize(fStream, new List<BaseEntities.AXK>());
                    }
                }
                List<AXK> targets = new List<AXK>();
                using (Stream fStream = File.OpenRead("Data\\AXKTargets"))
                {
                    targets = (List<AXK>)binFormat.Deserialize(fStream);
                }
                if (SaveNum == false)
                {
                    if (targets.Count == 0) t.Num = 1;
                    else t.Num = targets.Select(i => i.Num).Max() + 1;
                }
                else
                {
                    t.Num = targets.Where(ot => ot.ID == t.ID).First().Num;
                }
                targets = targets.Where(ot => ot.ID != t.ID).ToList();
                targets.Add(t);
                using (Stream fStream = new FileStream("Data\\AXKTargets", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, targets.OrderBy(tn => tn.ID).ToList());
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool DeleteAXKTarget(string Number)
        {
            try
            {
                if (!File.Exists("Data\\AXKTargets"))
                {
                    using (Stream fStream = new FileStream("Data\\AXKTargets", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                    {
                        binFormat.Serialize(fStream, new List<AXK>());
                    }
                }
                List<AXK> targets = new List<AXK>();
                using (Stream fStream = File.OpenRead("Data\\AXKTargets"))
                {
                    targets = (List<AXK>)binFormat.Deserialize(fStream);
                }
                targets = targets.Where(ot => ot.ID != Number).ToList();
                using (Stream fStream = new FileStream("Data\\AXKTargets", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, targets.OrderBy(tn => tn.ID).ToList());
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool SaveCameras (List<Camera> cams)
        {
            try {
                using (Stream fStream = new FileStream("Data\\Cameras", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, cams);
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
