using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataAccessLayer;
using Nemesis.Windows.Partials;

namespace Nemesis
{
    public partial class Martakarg : Window
    {
        public static string SourcePath
        {
            get
            {
                return Directory.GetCurrentDirectory();
            }
        }
        public static bool HasReference { set; get; }
        public Martakarg()
        {
            HasReference = true;
            InitializeComponent();
            if (!(DataReader.GetMartakarg() == null))
                InitializeMartakarg(DataReader.GetMartakarg());
            //Save.Source = new ImageSourceValueSerializer().ConvertFromString("");
            InatializeImageSources();
        }
        private void InatializeImageSources()
        {
            /////////////////////////////////////////////////////////////////////////////////////
            BitmapImage save = new BitmapImage();
            save.BeginInit();
            save.UriSource = new Uri(SourcePath + "\\Images\\Save.ico");
            save.EndInit();
            Save.Source = save;
            /////////////////////////////////////////////////////////////////////////////////////
            BitmapImage Alpha = new BitmapImage();
            Alpha.BeginInit();
            Alpha.UriSource = new Uri(SourcePath + "\\Images\\Alpha.ico");
            Alpha.EndInit();
            Alphas.Source = Alpha;
            /////////////////////////////////////////////////////////////////////////////////////
            BitmapImage Camera = new BitmapImage();
            Camera.BeginInit();
            Camera.UriSource = new Uri(SourcePath + "\\Images\\Camera.ico");
            Camera.EndInit();
            Cameras.Source = Camera;
            /////////////////////////////////////////////////////////////////////////////////////
        }
        protected override void OnClosed(EventArgs e)
        {
            HasReference = false;
            base.OnClosed(e);
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            BaseEntities.Martakarg mk = new BaseEntities.Martakarg
            {
                Ditaketer = ReadDitaketer(),
                KrakayinDirqer = ReadKrakayinDirqer()
            };
            DataWriter.SaveMartakarg(mk);
            MessageBox.Show("Մարտակարգը հաջողությամբ պահպանված է", "Done");
        }
        private void Alphas_Click(object sender, MouseButtonEventArgs e)
        {
            Window alpha = new Alphas();
            alpha.ShowDialog();
        }
        private void Cameras_Click(object sender, MouseButtonEventArgs e)
        {
            Window Camera = new Cameras();
            Camera.ShowDialog();
        }
    }
}
