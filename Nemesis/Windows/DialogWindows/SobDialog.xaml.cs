using BaseEntities;
using DataAccessLayer;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Nemesis
{
    /// <summary>
    /// Логика взаимодействия для SobDialog.xaml
    /// </summary>
    public partial class SobDialog : Window
    {
        private static string StartTarget { get; set; }
        private static string DK { get; set; }
        private static string KD { get; set; }

        public event Action<DialogData> Submit;
        public SobDialog(DialogData data)
        {

            InitializeComponent();
            InitializeComboBoxes();

            if (!string.IsNullOrWhiteSpace(DK)) Ditaketer.SelectedItem = Ditaketer.Items[GetDKIndex(DK)];
            if (!string.IsNullOrWhiteSpace(KD)) Dirqer.SelectedItem = Dirqer.Items[GetKDIndex(KD)];
            TargetID.Text = StartTarget;
        }

        private int GetDKIndex(string dk)
        {
            switch (dk)
            {
                case "ՀԴՆ Հիմնական":
                    return 0;
                case "1 ՀՄ Հիմնական":
                    return 1;
                case "2 ՀՄ Հիմնական":
                    return 2;
                case "3 ՀՄ Հիմնական":
                    return 3;
                case "ՀԴՆ Պահեստային":
                    return 4;
                case "1 ՀՄ Պահեստային":
                    return 5;
                case "2 ՀՄ Պահեստային":
                    return 6;
                case "3 ՀՄ Պահեստային":
                    return 7;
            }
            return -1;
        }

        private int GetKDIndex(string kd)
        {
            switch (kd)
            {
                case "1 ՀրՄ Դասակի Կազմով":
                    return 0;
                case "1 ՀրՄ Մարտկոցի Կազմով":
                    return 1;
                case "2 ՀրՄ Դասակի Կազմով":
                    return 2;
                case "2 ՀրՄ Մարտկոցի Կազմով":
                    return 3;
                case "3 ՀրՄ Դասակի Կազմով":
                    return 4;
                case "3 ՀրՄ Մարտկոցի Կազմով":
                    return 5;
            }
            return -1;
        }

        private void InitializeComboBoxes()
        {
            Initializer.InitializeDitakets(ref Ditaketer);

            Dirqer.Items.Add(new ComboBoxItem { Content = "1 ՀրՄ Դասակի Կազմով" });
            Dirqer.Items.Add(new ComboBoxItem { Content = "1 ՀրՄ Մարտկոցի Կազմով" });
            Dirqer.Items.Add(new ComboBoxItem { Content = "2 ՀրՄ Դասակի Կազմով" });
            Dirqer.Items.Add(new ComboBoxItem { Content = "2 ՀրՄ Մարտկոցի Կազմով" });
            Dirqer.Items.Add(new ComboBoxItem { Content = "3 ՀրՄ Դասակի Կազմով" });
            Dirqer.Items.Add(new ComboBoxItem { Content = "3 ՀրՄ Մարտկոցի Կազմով" });
            Dirqer.SelectedItem = Dirqer.Items[0];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Submit != null)
            {
                StartTarget = TargetID.Text;
                KD = Dirqer.Text;
                DK = Ditaketer.Text;
                Submit(new DialogData() { DK = Ditaketer.Text, KD = Dirqer.Text, StartTarget = TargetID.Text });
            }
            this.Close();
        }
    }
}
