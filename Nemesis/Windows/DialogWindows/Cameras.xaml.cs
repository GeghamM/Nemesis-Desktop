using BaseEntities;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Nemesis.Windows.Partials
{
    /// <summary>
    /// Логика взаимодействия для Cameras.xaml
    /// </summary>
    public partial class Cameras : Window
    {
        private bool IsViewLoaded { get; set; }
        public Cameras()
        {
            IsViewLoaded = false;
            InitializeComponent();
            LoadCameras(DataReader.GetCameras());
            IsViewLoaded = true;
        }

        private void TrySaveAlphas(object sender, TextChangedEventArgs e)
        {
            DataWriter.SaveCameras(ReadCamerasFromView());
        }

        private List<Camera> ReadCamerasFromView()
        {
            var cams = new List<Camera>
            {
                new Camera()
                {
                    Name = "Տեսախցիկ 1",
                    X = int.TryParse(Camera1_X.Text, out _) ? int.Parse(Camera1_X.Text) : 0,
                    Y = int.TryParse(Camera1_Y.Text, out _) ? int.Parse(Camera1_Y.Text) : 0,
                    H = int.TryParse(Camera1_H.Text, out _) ? int.Parse(Camera1_H.Text) : 0,
                    IsPlus = Camera1_isplus.IsChecked == true ? true : false,
                    P = double.TryParse(Camera1_P.Text, out _) ? double.Parse(Camera1_P.Text) : 0
                },
                new Camera()
                {
                    Name = "Տեսախցիկ 2",
                    X = int.TryParse(Camera2_X.Text, out _) ? int.Parse(Camera2_X.Text) : 0,
                    Y = int.TryParse(Camera2_Y.Text, out _) ? int.Parse(Camera2_Y.Text) : 0,
                    H = int.TryParse(Camera2_H.Text, out _) ? int.Parse(Camera2_H.Text) : 0,
                    IsPlus = Camera2_isplus.IsChecked == true ? true : false,
                    P = double.TryParse(Camera2_P.Text, out _) ? double.Parse(Camera2_P.Text) : 0
                },
                new Camera()
                {
                    Name = "Տեսախցիկ 3",
                    X = int.TryParse(Camera3_X.Text, out _) ? int.Parse(Camera3_X.Text) : 0,
                    Y = int.TryParse(Camera3_Y.Text, out _) ? int.Parse(Camera3_Y.Text) : 0,
                    H = int.TryParse(Camera3_H.Text, out _) ? int.Parse(Camera3_H.Text) : 0,
                    IsPlus = Camera3_isplus.IsChecked == true ? true : false,
                    P = double.TryParse(Camera3_P.Text, out _) ? double.Parse(Camera3_P.Text) : 0
                },
                new Camera()
                {
                    Name = "Տեսախցիկ 4",
                    X = int.TryParse(Camera4_X.Text, out _) ? int.Parse(Camera4_X.Text) : 0,
                    Y = int.TryParse(Camera4_Y.Text, out _) ? int.Parse(Camera4_Y.Text) : 0,
                    H = int.TryParse(Camera4_H.Text, out _) ? int.Parse(Camera4_H.Text) : 0,
                    IsPlus = Camera4_isplus.IsChecked == true ? true : false,
                    P = double.TryParse(Camera4_P.Text, out _) ? double.Parse(Camera4_P.Text) : 0
                }
            };

            return cams;
        }

        private void LoadCameras(List<Camera> cams)
        {
            try
            {
                Camera1_X.Text = cams[0].X.ToString();
                Camera1_Y.Text = cams[0].Y.ToString();
                Camera1_H.Text = cams[0].H.ToString();
                if (cams[0].IsPlus) Camera1_isplus.IsChecked = true; else Camera1_isminus.IsChecked = true;
                Camera1_P.Text = cams[0].P.ToString();

                Camera2_X.Text = cams[1].X.ToString();
                Camera2_Y.Text = cams[1].Y.ToString();
                Camera2_H.Text = cams[1].H.ToString();
                if (cams[1].IsPlus) Camera2_isplus.IsChecked = true; else Camera2_isminus.IsChecked = true;
                Camera2_P.Text = cams[1].P.ToString();

                Camera3_X.Text = cams[2].X.ToString();
                Camera3_Y.Text = cams[2].Y.ToString();
                Camera3_H.Text = cams[2].H.ToString();
                if (cams[2].IsPlus) Camera3_isplus.IsChecked = true; else Camera3_isminus.IsChecked = true;
                Camera3_P.Text = cams[2].P.ToString();

                Camera4_X.Text = cams[3].X.ToString();
                Camera4_Y.Text = cams[3].Y.ToString();
                Camera4_H.Text = cams[3].H.ToString();
                if (cams[3].IsPlus) Camera4_isplus.IsChecked = true; else Camera4_isminus.IsChecked = true;
                Camera4_P.Text = cams[3].P.ToString();
            }
            catch
            { }
        }

        private void CheckChanged(object sender, RoutedEventArgs e)
        {
            DataWriter.SaveCameras(ReadCamerasFromView());
        }
    }
}
