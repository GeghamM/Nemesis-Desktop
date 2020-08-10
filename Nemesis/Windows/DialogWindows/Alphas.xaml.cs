using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using BaseService;
using DataAccessLayer;

namespace Nemesis.Windows.Partials
{
    /// <summary>
    /// Логика взаимодействия для Alphas.xaml
    /// </summary>
    public partial class Alphas : Window
    {
        public Alphas()
        {
            InitializeComponent();
            SetView(DataReader.GetAlphas());
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Save();
            base.OnClosing(e);
        }

        private void Save()
        {
            DataAccessLayer.DataWriter.SaveAlphas(ReadView());
        }

        private BaseEntities.Alphas ReadView()
        {
            BaseEntities.Alphas View = new BaseEntities.Alphas();

            View.Himnakan_1HrM.HNK.First = Double.Parse(Helper.RepresentStrings(H1_HNK1.Text));
            View.Himnakan_1HrM.HNK.Second = Double.Parse(Helper.RepresentStrings(H1_HNK2.Text));
            View.Himnakan_1HrM.HNK.Third = Double.Parse(Helper.RepresentStrings(H1_HNK3.Text));
            View.Himnakan_1HrM.HNK.Fourth = Double.Parse(Helper.RepresentStrings(H1_HNK4.Text));

            View.Himnakan_1HrM.PNK.First = Double.Parse(Helper.RepresentStrings(H1_PNK1.Text));
            View.Himnakan_1HrM.PNK.Second = Double.Parse(Helper.RepresentStrings(H1_PNK2.Text));
            View.Himnakan_1HrM.PNK.Third = Double.Parse(Helper.RepresentStrings(H1_PNK3.Text));
            View.Himnakan_1HrM.PNK.Fourth = Double.Parse(Helper.RepresentStrings(H1_PNK4.Text));

            View.Himnakan_1HrM.GNK.First = Double.Parse(Helper.RepresentStrings(H1_GNK1.Text));
            View.Himnakan_1HrM.GNK.Second = Double.Parse(Helper.RepresentStrings(H1_GNK2.Text));
            View.Himnakan_1HrM.GNK.Third = Double.Parse(Helper.RepresentStrings(H1_GNK3.Text));
            View.Himnakan_1HrM.GNK.Fourth = Double.Parse(Helper.RepresentStrings(H1_GNK4.Text));

            ///////////////////////////////////////////////////////////////////////////////
            ///
            View.Himnakan_2HrM.HNK.First = Double.Parse(Helper.RepresentStrings(H2_HNK1.Text));
            View.Himnakan_2HrM.HNK.Second = Double.Parse(Helper.RepresentStrings(H2_HNK2.Text));
            View.Himnakan_2HrM.HNK.Third = Double.Parse(Helper.RepresentStrings(H2_HNK3.Text));
            View.Himnakan_2HrM.HNK.Fourth = Double.Parse(Helper.RepresentStrings(H2_HNK4.Text));

            View.Himnakan_2HrM.PNK.First = Double.Parse(Helper.RepresentStrings(H2_PNK1.Text));
            View.Himnakan_2HrM.PNK.Second = Double.Parse(Helper.RepresentStrings(H2_PNK2.Text));
            View.Himnakan_2HrM.PNK.Third = Double.Parse(Helper.RepresentStrings(H2_PNK3.Text));
            View.Himnakan_2HrM.PNK.Fourth = Double.Parse(Helper.RepresentStrings(H2_PNK4.Text));

            View.Himnakan_2HrM.GNK.First = Double.Parse(Helper.RepresentStrings(H2_GNK1.Text));
            View.Himnakan_2HrM.GNK.Second = Double.Parse(Helper.RepresentStrings(H2_GNK2.Text));
            View.Himnakan_2HrM.GNK.Third = Double.Parse(Helper.RepresentStrings(H2_GNK3.Text));
            View.Himnakan_2HrM.GNK.Fourth = Double.Parse(Helper.RepresentStrings(H2_GNK4.Text));

            ///////////////////////////////////////////////////////////////////////////////
            ///
            View.Himnakan_3HrM.HNK.First = Double.Parse(Helper.RepresentStrings(H3_HNK1.Text));
            View.Himnakan_3HrM.HNK.Second = Double.Parse(Helper.RepresentStrings(H3_HNK2.Text));
            View.Himnakan_3HrM.HNK.Third = Double.Parse(Helper.RepresentStrings(H3_HNK3.Text));
            View.Himnakan_3HrM.HNK.Fourth = Double.Parse(Helper.RepresentStrings(H3_HNK4.Text));

            View.Himnakan_3HrM.PNK.First = Double.Parse(Helper.RepresentStrings(H3_PNK1.Text));
            View.Himnakan_3HrM.PNK.Second = Double.Parse(Helper.RepresentStrings(H3_PNK2.Text));
            View.Himnakan_3HrM.PNK.Third = Double.Parse(Helper.RepresentStrings(H3_PNK3.Text));
            View.Himnakan_3HrM.PNK.Fourth = Double.Parse(Helper.RepresentStrings(H3_PNK4.Text));

            View.Himnakan_3HrM.GNK.First = Double.Parse(Helper.RepresentStrings(H3_GNK1.Text));
            View.Himnakan_3HrM.GNK.Second = Double.Parse(Helper.RepresentStrings(H3_GNK2.Text));
            View.Himnakan_3HrM.GNK.Third = Double.Parse(Helper.RepresentStrings(H3_GNK3.Text));
            View.Himnakan_3HrM.GNK.Fourth = Double.Parse(Helper.RepresentStrings(H3_GNK4.Text));

            ////////////////////////
            ///////////////////////
            //////////////////////
            /////////////////////

            View.Pahestayin_1HrM.HNK.First = Double.Parse(Helper.RepresentStrings(P1_HNK1.Text));
            View.Pahestayin_1HrM.HNK.Second = Double.Parse(Helper.RepresentStrings(P1_HNK2.Text));
            View.Pahestayin_1HrM.HNK.Third = Double.Parse(Helper.RepresentStrings(P1_HNK3.Text));
            View.Pahestayin_1HrM.HNK.Fourth = Double.Parse(Helper.RepresentStrings(P1_HNK4.Text));

            View.Pahestayin_1HrM.PNK.First = Double.Parse(Helper.RepresentStrings(P1_PNK1.Text));
            View.Pahestayin_1HrM.PNK.Second = Double.Parse(Helper.RepresentStrings(P1_PNK2.Text));
            View.Pahestayin_1HrM.PNK.Third = Double.Parse(Helper.RepresentStrings(P1_PNK3.Text));
            View.Pahestayin_1HrM.PNK.Fourth = Double.Parse(Helper.RepresentStrings(P1_PNK4.Text));

            View.Pahestayin_1HrM.GNK.First = Double.Parse(Helper.RepresentStrings(P1_GNK1.Text));
            View.Pahestayin_1HrM.GNK.Second = Double.Parse(Helper.RepresentStrings(P1_GNK2.Text));
            View.Pahestayin_1HrM.GNK.Third = Double.Parse(Helper.RepresentStrings(P1_GNK3.Text));
            View.Pahestayin_1HrM.GNK.Fourth = Double.Parse(Helper.RepresentStrings(P1_GNK4.Text));

            ///////////////////////////////////////////////////////////////////////////////
            ///
            View.Pahestayin_2HrM.HNK.First = Double.Parse(Helper.RepresentStrings(P2_HNK1.Text));
            View.Pahestayin_2HrM.HNK.Second = Double.Parse(Helper.RepresentStrings(P2_HNK2.Text));
            View.Pahestayin_2HrM.HNK.Third = Double.Parse(Helper.RepresentStrings(P2_HNK3.Text));
            View.Pahestayin_2HrM.HNK.Fourth = Double.Parse(Helper.RepresentStrings(P2_HNK4.Text));

            View.Pahestayin_2HrM.PNK.First = Double.Parse(Helper.RepresentStrings(P2_PNK1.Text));
            View.Pahestayin_2HrM.PNK.Second = Double.Parse(Helper.RepresentStrings(P2_PNK2.Text));
            View.Pahestayin_2HrM.PNK.Third = Double.Parse(Helper.RepresentStrings(P2_PNK3.Text));
            View.Pahestayin_2HrM.PNK.Fourth = Double.Parse(Helper.RepresentStrings(P2_PNK4.Text));

            View.Pahestayin_2HrM.GNK.First = Double.Parse(Helper.RepresentStrings(P2_GNK1.Text));
            View.Pahestayin_2HrM.GNK.Second = Double.Parse(Helper.RepresentStrings(P2_GNK2.Text));
            View.Pahestayin_2HrM.GNK.Third = Double.Parse(Helper.RepresentStrings(P2_GNK3.Text));
            View.Pahestayin_2HrM.GNK.Fourth = Double.Parse(Helper.RepresentStrings(P2_GNK4.Text));

            ///////////////////////////////////////////////////////////////////////////////
            ///
            View.Pahestayin_3HrM.HNK.First = Double.Parse(Helper.RepresentStrings(P3_HNK1.Text));
            View.Pahestayin_3HrM.HNK.Second = Double.Parse(Helper.RepresentStrings(P3_HNK2.Text));
            View.Pahestayin_3HrM.HNK.Third = Double.Parse(Helper.RepresentStrings(P3_HNK3.Text));
            View.Pahestayin_3HrM.HNK.Fourth = Double.Parse(Helper.RepresentStrings(P3_HNK4.Text));

            View.Pahestayin_3HrM.PNK.First = Double.Parse(Helper.RepresentStrings(P3_PNK1.Text));
            View.Pahestayin_3HrM.PNK.Second = Double.Parse(Helper.RepresentStrings(P3_PNK2.Text));
            View.Pahestayin_3HrM.PNK.Third = Double.Parse(Helper.RepresentStrings(P3_PNK3.Text));
            View.Pahestayin_3HrM.PNK.Fourth = Double.Parse(Helper.RepresentStrings(P3_PNK4.Text));

            View.Pahestayin_3HrM.GNK.First = Double.Parse(Helper.RepresentStrings(P3_GNK1.Text));
            View.Pahestayin_3HrM.GNK.Second = Double.Parse(Helper.RepresentStrings(P3_GNK2.Text));
            View.Pahestayin_3HrM.GNK.Third = Double.Parse(Helper.RepresentStrings(P3_GNK3.Text));
            View.Pahestayin_3HrM.GNK.Fourth = Double.Parse(Helper.RepresentStrings(P3_GNK4.Text));

            return View;
        }

        public void SetView(BaseEntities.Alphas View)
        {

            H1_HNK1.Text = Helper.FormatAngle(View.Himnakan_1HrM.HNK.First);
            H1_HNK2.Text = Helper.FormatAngle(View.Himnakan_1HrM.HNK.Second);
            H1_HNK3.Text = Helper.FormatAngle(View.Himnakan_1HrM.HNK.Third);
            H1_HNK4.Text = Helper.FormatAngle(View.Himnakan_1HrM.HNK.Fourth);

            H1_PNK1.Text = Helper.FormatAngle(View.Himnakan_1HrM.PNK.First);
            H1_PNK2.Text = Helper.FormatAngle(View.Himnakan_1HrM.PNK.Second);
            H1_PNK3.Text = Helper.FormatAngle(View.Himnakan_1HrM.PNK.Third);
            H1_PNK4.Text = Helper.FormatAngle(View.Himnakan_1HrM.PNK.Fourth);

            H1_GNK1.Text = Helper.FormatAngle(View.Himnakan_1HrM.GNK.First);
            H1_GNK2.Text = Helper.FormatAngle(View.Himnakan_1HrM.GNK.Second);
            H1_GNK3.Text = Helper.FormatAngle(View.Himnakan_1HrM.GNK.Third);
            H1_GNK4.Text = Helper.FormatAngle(View.Himnakan_1HrM.GNK.Fourth);

            //////////////////
            H2_HNK1.Text = Helper.FormatAngle(View.Himnakan_2HrM.HNK.First);
            H2_HNK2.Text = Helper.FormatAngle(View.Himnakan_2HrM.HNK.Second);
            H2_HNK3.Text = Helper.FormatAngle(View.Himnakan_2HrM.HNK.Third);
            H2_HNK4.Text = Helper.FormatAngle(View.Himnakan_2HrM.HNK.Fourth);

            H2_PNK1.Text = Helper.FormatAngle(View.Himnakan_2HrM.PNK.First);
            H2_PNK2.Text = Helper.FormatAngle(View.Himnakan_2HrM.PNK.Second);
            H2_PNK3.Text = Helper.FormatAngle(View.Himnakan_2HrM.PNK.Third);
            H2_PNK4.Text = Helper.FormatAngle(View.Himnakan_2HrM.PNK.Fourth);

            H2_GNK1.Text = Helper.FormatAngle(View.Himnakan_2HrM.GNK.First);
            H2_GNK2.Text = Helper.FormatAngle(View.Himnakan_2HrM.GNK.Second);
            H2_GNK3.Text = Helper.FormatAngle(View.Himnakan_2HrM.GNK.Third);
            H2_GNK4.Text = Helper.FormatAngle(View.Himnakan_2HrM.GNK.Fourth);

            //////////////////
            H3_HNK1.Text = Helper.FormatAngle(View.Himnakan_3HrM.HNK.First);
            H3_HNK2.Text = Helper.FormatAngle(View.Himnakan_3HrM.HNK.Second);
            H3_HNK3.Text = Helper.FormatAngle(View.Himnakan_3HrM.HNK.Third);
            H3_HNK4.Text = Helper.FormatAngle(View.Himnakan_3HrM.HNK.Fourth);

            H3_PNK1.Text = Helper.FormatAngle(View.Himnakan_3HrM.PNK.First);
            H3_PNK2.Text = Helper.FormatAngle(View.Himnakan_3HrM.PNK.Second);
            H3_PNK3.Text = Helper.FormatAngle(View.Himnakan_3HrM.PNK.Third);
            H3_PNK4.Text = Helper.FormatAngle(View.Himnakan_3HrM.PNK.Fourth);

            H3_GNK1.Text = Helper.FormatAngle(View.Himnakan_3HrM.GNK.First);
            H3_GNK2.Text = Helper.FormatAngle(View.Himnakan_3HrM.GNK.Second);
            H3_GNK3.Text = Helper.FormatAngle(View.Himnakan_3HrM.GNK.Third);
            H3_GNK4.Text = Helper.FormatAngle(View.Himnakan_3HrM.GNK.Fourth);




            P1_HNK1.Text = Helper.FormatAngle(View.Pahestayin_1HrM.HNK.First);
            P1_HNK2.Text = Helper.FormatAngle(View.Pahestayin_1HrM.HNK.Second);
            P1_HNK3.Text = Helper.FormatAngle(View.Pahestayin_1HrM.HNK.Third);
            P1_HNK4.Text = Helper.FormatAngle(View.Pahestayin_1HrM.HNK.Fourth);

            P1_PNK1.Text = Helper.FormatAngle(View.Pahestayin_1HrM.PNK.First);
            P1_PNK2.Text = Helper.FormatAngle(View.Pahestayin_1HrM.PNK.Second);
            P1_PNK3.Text = Helper.FormatAngle(View.Pahestayin_1HrM.PNK.Third);
            P1_PNK4.Text = Helper.FormatAngle(View.Pahestayin_1HrM.PNK.Fourth);

            P1_GNK1.Text = Helper.FormatAngle(View.Pahestayin_1HrM.GNK.First);
            P1_GNK2.Text = Helper.FormatAngle(View.Pahestayin_1HrM.GNK.Second);
            P1_GNK3.Text = Helper.FormatAngle(View.Pahestayin_1HrM.GNK.Third);
            P1_GNK4.Text = Helper.FormatAngle(View.Pahestayin_1HrM.GNK.Fourth);

            /////////////////
            P2_HNK1.Text = Helper.FormatAngle(View.Pahestayin_2HrM.HNK.First);
            P2_HNK2.Text = Helper.FormatAngle(View.Pahestayin_2HrM.HNK.Second);
            P2_HNK3.Text = Helper.FormatAngle(View.Pahestayin_2HrM.HNK.Third);
            P2_HNK4.Text = Helper.FormatAngle(View.Pahestayin_2HrM.HNK.Fourth);

            P2_PNK1.Text = Helper.FormatAngle(View.Pahestayin_2HrM.PNK.First);
            P2_PNK2.Text = Helper.FormatAngle(View.Pahestayin_2HrM.PNK.Second);
            P2_PNK3.Text = Helper.FormatAngle(View.Pahestayin_2HrM.PNK.Third);
            P2_PNK4.Text = Helper.FormatAngle(View.Pahestayin_2HrM.PNK.Fourth);

            P2_GNK1.Text = Helper.FormatAngle(View.Pahestayin_2HrM.GNK.First);
            P2_GNK2.Text = Helper.FormatAngle(View.Pahestayin_2HrM.GNK.Second);
            P2_GNK3.Text = Helper.FormatAngle(View.Pahestayin_2HrM.GNK.Third);
            P2_GNK4.Text = Helper.FormatAngle(View.Pahestayin_2HrM.GNK.Fourth);

            /////////////////
            P3_HNK1.Text = Helper.FormatAngle(View.Pahestayin_3HrM.HNK.First);
            P3_HNK2.Text = Helper.FormatAngle(View.Pahestayin_3HrM.HNK.Second);
            P3_HNK3.Text = Helper.FormatAngle(View.Pahestayin_3HrM.HNK.Third);
            P3_HNK4.Text = Helper.FormatAngle(View.Pahestayin_3HrM.HNK.Fourth);

            P3_PNK1.Text = Helper.FormatAngle(View.Pahestayin_3HrM.PNK.First);
            P3_PNK2.Text = Helper.FormatAngle(View.Pahestayin_3HrM.PNK.Second);
            P3_PNK3.Text = Helper.FormatAngle(View.Pahestayin_3HrM.PNK.Third);
            P3_PNK4.Text = Helper.FormatAngle(View.Pahestayin_3HrM.PNK.Fourth);

            P3_GNK1.Text = Helper.FormatAngle(View.Pahestayin_3HrM.GNK.First);
            P3_GNK2.Text = Helper.FormatAngle(View.Pahestayin_3HrM.GNK.Second);
            P3_GNK3.Text = Helper.FormatAngle(View.Pahestayin_3HrM.GNK.Third);
            P3_GNK4.Text = Helper.FormatAngle(View.Pahestayin_3HrM.GNK.Fourth);

        }

        private void WindwStateChanged(object sender, EventArgs e)
        {
            Scroller.Height = this.ActualHeight - 130;
        }

        private void WindowSozeChanged(object sender, SizeChangedEventArgs e)
        {
            Scroller.Height = this.ActualHeight - 130;
        }

        private void InitializeImage(object sender, RoutedEventArgs e)
        {
            if (App.Skin != Skin.Dark)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(Directory.GetCurrentDirectory() + "\\Images\\gun.jpg");
                image.EndInit();
                (sender as Image).Source = image;
            }
        }
    }
}
