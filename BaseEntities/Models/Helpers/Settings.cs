using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BaseEntities
{
    [Serializable]
    public class Settings
    {
        public bool IsDarkThemeSelected { get; set; }
        public int TargetsPageSize { get; set; }
        [NonSerialized]
        private readonly BinaryFormatter binFormat = new BinaryFormatter();
        public Settings Initialize()
        {
            var Sett = new Settings();
            if (File.Exists("Settings"))
            {
                using (Stream fStream = File.OpenRead("Settings"))
                {
                    Sett = (Settings)binFormat.Deserialize(fStream);
                }
            }
            else { return null; }
            IsDarkThemeSelected = Sett.IsDarkThemeSelected;
            TargetsPageSize = Sett.TargetsPageSize;
            return this;
        }

        public bool Save()
        {
            try
            {
                using (Stream fStream = new FileStream("Settings", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, this);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
