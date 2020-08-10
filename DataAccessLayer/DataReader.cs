using BaseEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataAccessLayer
{
    public static class DataReader
    {
        private static readonly BinaryFormatter binFormat = new BinaryFormatter();
        public static Martakarg GetMartakarg()
        {
            Martakarg mk = new Martakarg();
            if (File.Exists("Data\\Martakarg"))
            {
                using (Stream fStream = File.OpenRead("Data\\Martakarg"))
                {
                    mk = (Martakarg)binFormat.Deserialize(fStream);
                }
                Owin.Martakarg = mk;
            }
            else return new Martakarg();
            return mk;
        }
        public static Alphas GetAlphas()
        {
            Alphas a = new Alphas();
            if (File.Exists("Data\\Alphas"))
            {
                using (Stream fStream = File.OpenRead("Data\\Alphas"))
                {
                    a = (Alphas)binFormat.Deserialize(fStream);
                }
            }
            else return new Alphas();
            return a;
        }
        public static List<Camera> GetCameras()
        {
            List<Camera> cams = new List<Camera>();
            if (File.Exists("Data\\Cameras"))
            {
                using (Stream fStream = File.OpenRead("Data\\Cameras"))
                {
                    cams = (List<Camera>)binFormat.Deserialize(fStream);
                    Owin.Martakarg.Cameraner = cams;
                }
            }
            else return new List<Camera>();
            return cams;
        }
        public static List<AXK> GetAXKTargets()
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            List<AXK> targets = new List<AXK>();
            if (File.Exists("Data\\AXKTargets"))
            {
                using (Stream fStream = File.OpenRead("Data\\AXKTargets"))
                {
                    targets = (List<AXK>)binFormat.Deserialize(fStream);
                }
            }
            else targets = new List<AXK>();
            targets = targets.OrderBy(i => i.Num).ToList();
            Owin.AXKTargets = targets;
            return targets;
        }
        public static List<Target> GetTargets()
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            List<Target> targets = new List<Target>();
            if (File.Exists("Data\\Targets"))
            {
                using (Stream fStream = File.OpenRead("Data\\Targets"))
                {
                    targets = (List<Target>)binFormat.Deserialize(fStream);
                }
            }
            else targets = new List<Target>();
            targets = targets.OrderBy(i => i.Num).ToList();
            Owin.Targets = targets;
            return targets;
        }
    }
}