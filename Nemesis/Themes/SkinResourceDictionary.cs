﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Nemesis
{
    public class SkinResourceDictionary : ResourceDictionary
    {
        private Uri _lightSource;
        private Uri _darkSource;
        public Uri LightSource
        {
            get { return _lightSource; }
            set
            {
                _lightSource = value;
                UpdateSource();
            }
        }
        public Uri DarkSource
        {
            get { return _darkSource; }
            set
            {
                _darkSource = value;
                UpdateSource();
            }
        }
        private void UpdateSource()
        {
            var val = App.Skin == Skin.Dark ? DarkSource : LightSource;
            if (val != null && base.Source != val)
                base.Source = val;
        }
    }
}
