using BaseEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Nemesis
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    /// 
    public enum Skin { Dark, Light }
    public partial class App : Application
    {

        public static Skin Skin { get
            {
                var settings = new Settings();
                settings.Initialize();
                return settings.IsDarkThemeSelected ? Skin.Dark : Skin.Light;
                //return Skin.Light;
            }
        }
    }
}