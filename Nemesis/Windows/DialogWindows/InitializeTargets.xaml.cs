using BaseEntities;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Nemesis
{
    /// <summary>
    /// Логика взаимодействия для InitializeTargets.xaml
    /// </summary>
    public partial class InitializeTargets : Window
    {
        private bool? IsAAK { get; set; }
        public static bool HasReference { set; get; }
        public InitializeTargets(bool? isaak = false)
        {
            IsAAK = isaak;
            HasReference = true;
            InitializeComponent();
        }
        protected override void OnClosed(EventArgs e)
        {
            HasReference = false;
            base.OnClosed(e);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] text = InsertTextBlock.Text.Split('\n');
                if (!(bool)IsAAK)
                {
                    List<Target> Targets = new List<Target>();
                    foreach (var s in text)
                    {
                        var t = s.Split('\t');
                        Targets.Add(
                            new Target(t[0],
                            t[1],
                            int.TryParse(t[2], out _) ? int.Parse(t[2]) : 0,
                            int.TryParse(t[3], out _) ? int.Parse(t[3]) : 0,
                            int.TryParse(t[4], out _) ? int.Parse(t[4]) : 0,
                            int.TryParse(t[5], out _) ? int.Parse(t[5]) : 0,
                            int.TryParse(t[6], out _) ? int.Parse(t[6]) : 0));
                    }

                    foreach (var target in Targets)
                    {
                        DataWriter.AddTarget(target);
                    }
                }
                else
                {
                    List<AXK> Targets = new List<AXK>();
                    int a;
                    foreach (var s in text)
                    {
                        var t = s.Split('\t');
                        Targets.Add(
                            new AXK(t[0],
                            t[1],
                            int.TryParse(t[2], out a) ? int.Parse(t[2]) : 0,
                            int.TryParse(t[3], out a) ? int.Parse(t[3]) : 0,
                            int.TryParse(t[4], out a) ? int.Parse(t[4]) : 0,
                            int.TryParse(t[5], out a) ? int.Parse(t[5]) : 0,
                            int.TryParse(t[6], out a) ? int.Parse(t[6]) : 0,
                            int.TryParse(t[7], out a) ? int.Parse(t[7]) : 0));
                    }

                    foreach (var target in Targets)
                    {
                        DataWriter.AddAXKTarget(target);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Խնդրում ենք մուտքագրել ճիշտ ֆորմատով", "Սխալ ֆորմատ");
            }
            this.Close();
        }

    }
}
