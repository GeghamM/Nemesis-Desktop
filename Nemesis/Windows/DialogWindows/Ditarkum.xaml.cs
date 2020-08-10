using ActionsLayer;
using BaseEntities;
using BaseService;
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
    /// Логика взаимодействия для Ditarkum.xaml
    /// </summary>
    public partial class Ditarkum : Window
    {
        public static int RefCount { get; set; }
        public DitarkumDataModel TaskData { get; set; }
        public DitarkumDataModel NewTaskData { get; set; }
        static Ditarkum()
        {
            RefCount = 0;
        }
        public Ditarkum()
        {
            RefCount++;
            InitializeComponent();
            NewTaskData = null;
            this.Title = "Դիտարկում " + RefCount;

        }

        private void HandleLables()
        {
            if (TaskData.TaskInfo.Positions != "Դասակի Կազմով")
            {
                Label1.Content = Label1.Content.ToString().Remove(0, 4);
                Label2.Content = Label2.Content.ToString().Remove(0, 4);
                Label2_1.Content = Label2_1.Content.ToString().Remove(0, 4);
                Label3.Content = Label3.Content.ToString().Remove(0, 4);
                Label4.Content = Label4.Content.ToString().Remove(0, 4);
                Label4_1.Content = Label4_1.Content.ToString().Remove(0, 4);
                Label5.Content = Label5.Content.ToString().Remove(0, 4);
                Label6.Content = Label6.Content.ToString().Remove(0, 4);
                Label6_1.Content = Label6_1.Content.ToString().Remove(0, 4);
            }
        }

        public Window SetTaskData(DitarkumDataModel ddm)
        {
            TaskData = ddm;
            if (TaskData.TaskInfo.Positions != "Դասակի Կազմով")
            {
                this.Height = 280;
                this.MinHeight = 280;
                this.MaxHeight = 280;
                M1_Dasaknerov.Visibility = Visibility.Collapsed;
                M2_Dasaknerov.Visibility = Visibility.Collapsed;
                M3_Dasaknerov.Visibility = Visibility.Collapsed;
            }
            if (TaskData.TaskInfo.TaskType == "Զուգորդված դիտարկում")
            {
                ParallelPanel.Visibility = Visibility.Visible;
                DistancePanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                AlphaLable.Content = "ԱՆԿՅՈՒՆ";
                ParallelPanel.Visibility = Visibility.Collapsed;
                DistancePanel.Visibility = Visibility.Visible;
            }
            HandleLables();

            if(TaskData.TaskInfo.Paytucich != "РГМ-2 (ОФ-462)")
            {
                this.Width = 910;
                this.MaxWidth = 910;
                this.MinWidth = 910;
                Poxrak11.Visibility = Visibility.Visible;
                Poxrak12.Visibility = Visibility.Visible;
                Poxrak21.Visibility = Visibility.Visible;
                Poxrak22.Visibility = Visibility.Visible;
                Poxrak31.Visibility = Visibility.Visible;
                Poxrak32.Visibility = Visibility.Visible;
            }

            return this;
        }
        private void HandleAlpha1Label(object sender, RoutedEventArgs e)
        {
            AlphaLeaderText1.Content = (sender as RadioButton).Content;
        }
        private void ParallelHandleAlpha1Label(object sender, RoutedEventArgs e)
        {
            ParallelAlphaLeaderText1.Content = (sender as RadioButton).Content;
        }
        private void HandleDistance1Label(object sender, RoutedEventArgs e)
        {
            DistanceLeaderText1.Content = (sender as RadioButton).Content;
        }
        private void CountNewTexakayum(double Front, double DeltaAlpha, double DeltaDistance, double DeltaParallelAlpha)
        {
            int Dist1 = 0, Dist2 = 0;
            BaseEntities.Point point = null;
            Texagrakan NewTexagrakan = null;
            if (TaskData.TaskInfo.TaskType == "Զուգորդված դիտարկում")
            {
                point = Service.GetPointByParallelView(
                    TaskData.ParallelDitaket.Name,
                    TaskData.ParallelHramanatarakan.Alpha + DeltaAlpha,
                    TaskData.ParallelHramanatarakan.M,
                    TaskData.Ditaket.Name,
                    TaskData.Hramanatarakan.Alpha + DeltaParallelAlpha,
                    TaskData.Hramanatarakan.M,

                    ref Dist1,
                    ref Dist2
                    );
                NewTexagrakan = new Texagrakan
                {
                    Alpha = TaskData.Hramanatarakan.Alpha + DeltaParallelAlpha,
                    Distance = TaskData.TaskInfo.TaskType == "Զուգորդված դիտարկում" ? Dist2 : TaskData.Hramanatarakan.Distance + DeltaDistance,
                    M = TaskData.Hramanatarakan.M
                };
            }
            else
            {
                NewTexagrakan = new Texagrakan
                {
                    Alpha = TaskData.Hramanatarakan.Alpha + DeltaAlpha,
                    Distance = TaskData.TaskInfo.TaskType == "Զուգորդված դիտարկում" ? Dist2 : TaskData.Hramanatarakan.Distance + DeltaDistance,
                    M = TaskData.Hramanatarakan.M
                };
            }

            BaseEntities.Point NewPoint = TaskData.TaskInfo.TaskType == "Զուգորդված դիտարկում" ? new BaseEntities.Point(point.X, point.Y, point.H) : Service.GetPoint(TaskData.Ditaket, NewTexagrakan);
            Target NewTarget = new Target
            {
                Deepness = TaskData.Target.Deepness,
                Front = TaskData.Target.Front,
                H = NewPoint.H,
                X = NewPoint.X,
                Y = NewPoint.Y
            };
            double FrontAB = Math.Round(((Math.Asin(NewTarget.Front / NewTexagrakan.Distance)) * 3000) / Math.PI);
            Battarey bat1 = null, bat2 = null, bat3 = null;
            if (TaskData.Battary1Texakayum != null)
            {
                switch (TaskData.Battary1Texakayum.Core)
                {
                    case "6": TaskData.TaskInfo.SelectedCore = "Լիցք 6"; break;
                    case "5": TaskData.TaskInfo.SelectedCore = "Լիցք 5"; break;
                    case "4": TaskData.TaskInfo.SelectedCore = "Լիցք 4"; break;
                    case "3": TaskData.TaskInfo.SelectedCore = "Լիցք 3"; break;
                    case "2": TaskData.TaskInfo.SelectedCore = "Լիցք 2"; break;
                    case "1": TaskData.TaskInfo.SelectedCore = "Լիցք 1"; break;
                    case "Քչացված": TaskData.TaskInfo.SelectedCore = "Լիցք քչացված"; break;
                    case "Լրիվ": TaskData.TaskInfo.SelectedCore = "Լիցք լրիվ"; break;
                    case "###": TaskData.TaskInfo.SelectedCore = "Ավտոմատ լիցք"; break;
                }
                if (TaskData.TaskInfo.Positions == "Դասակի Կազմով")
                {
                    bat1 = Service.GetBattareyData(
                        Owin.Martakarg.GetKD(1, 1),
                        Owin.Martakarg.GetKD(1, 2),
                        TaskData.Ditaket,
                        NewTarget,
                        TaskData.TaskInfo,
                        null,
                        NewTexagrakan
                    );
                }
                else
                {
                    bat1 = Service.GetBattareyData(
                         Owin.Martakarg.GetKD(1, 0),
                         Owin.Martakarg.GetKD(1, 0),
                         TaskData.Ditaket,
                         NewTarget,
                         TaskData.TaskInfo,
                         null,
                         NewTexagrakan
                 );
                }
            }
            if (TaskData.Battary2Texakayum != null)
            {
                switch (TaskData.Battary2Texakayum.Core)
                {
                    case "6": TaskData.TaskInfo.SelectedCore = "Լիցք 6"; break;
                    case "5": TaskData.TaskInfo.SelectedCore = "Լիցք 5"; break;
                    case "4": TaskData.TaskInfo.SelectedCore = "Լիցք 4"; break;
                    case "3": TaskData.TaskInfo.SelectedCore = "Լիցք 3"; break;
                    case "2": TaskData.TaskInfo.SelectedCore = "Լիցք 2"; break;
                    case "1": TaskData.TaskInfo.SelectedCore = "Լիցք 1"; break;
                    case "Քչացված": TaskData.TaskInfo.SelectedCore = "Լիցք քչացված"; break;
                    case "Լրիվ": TaskData.TaskInfo.SelectedCore = "Լիցք լրիվ"; break;
                    case "###": TaskData.TaskInfo.SelectedCore = "Ավտոմատ լիցք"; break;
                }
                if (TaskData.TaskInfo.Positions == "Դասակի Կազմով")
                {
                    bat2 = Service.GetBattareyData(
                        Owin.Martakarg.GetKD(2, 1),
                        Owin.Martakarg.GetKD(2, 2),
                        TaskData.Ditaket,
                        NewTarget,
                        TaskData.TaskInfo,
                        null,
                        NewTexagrakan
                    );
                }
                else
                {
                    bat2 = Service.GetBattareyData(
                         Owin.Martakarg.GetKD(2, 0),
                         Owin.Martakarg.GetKD(2, 0),
                         TaskData.Ditaket,
                         NewTarget,
                         TaskData.TaskInfo,
                         null,
                         NewTexagrakan
                 );
                }
            }
            if (TaskData.Battary3Texakayum != null)
            {
                switch (TaskData.Battary3Texakayum.Core)
                {
                    case "6": TaskData.TaskInfo.SelectedCore = "Լիցք 6"; break;
                    case "5": TaskData.TaskInfo.SelectedCore = "Լիցք 5"; break;
                    case "4": TaskData.TaskInfo.SelectedCore = "Լիցք 4"; break;
                    case "3": TaskData.TaskInfo.SelectedCore = "Լիցք 3"; break;
                    case "2": TaskData.TaskInfo.SelectedCore = "Լիցք 2"; break;
                    case "1": TaskData.TaskInfo.SelectedCore = "Լիցք 1"; break;
                    case "Քչացված": TaskData.TaskInfo.SelectedCore = "Լիցք քչացված"; break;
                    case "Լրիվ": TaskData.TaskInfo.SelectedCore = "Լիցք լրիվ"; break;
                    case "###": TaskData.TaskInfo.SelectedCore = "Ավտոմատ լիցք"; break;
                }
                if (TaskData.TaskInfo.Positions == "Դասակի Կազմով")
                {
                    bat3 = Service.GetBattareyData(
                        Owin.Martakarg.GetKD(3, 1),
                        Owin.Martakarg.GetKD(3, 2),
                        TaskData.Ditaket,
                        NewTarget,
                        TaskData.TaskInfo,
                        null,
                        NewTexagrakan
                    );
                }
                else
                {
                    bat3 = Service.GetBattareyData(
                         Owin.Martakarg.GetKD(3, 0),
                         Owin.Martakarg.GetKD(3, 0),
                         TaskData.Ditaket,
                         NewTarget,
                         TaskData.TaskInfo,
                         null,
                         NewTexagrakan
                 );
                }
            }
            double FrontMeter = Math.Round(NewTexagrakan.Distance * Front / 955);
            if (bat1 != null)
            {
                M1_DeltaPricel1.Text = TaskData.Battary1Texakayum.Core != "###" ? (TaskData.Battary1Texakayum.Pricel1 - bat1.First.Pricel).ToString() : "-";
                M1_DeltaFront1.Text = Front != -1 ? Helper.FormatAngleWithSigne(((FrontMeter - NewTarget.Front) / (4 * bat1.First.Distance))*10) : "";
                if (M1_DeltaPricel1.Text[0] != '-') M1_DeltaPricel1.Text = "+" + M1_DeltaPricel1.Text;
                M1_DeltaDavarot1.Text = TaskData.Battary1Texakayum.Core != "###" ? Helper.FormatAngleWithSigne(TaskData.Battary1Texakayum.Davarot1 - bat1.First.DavarotHU) : "-";
                if (TaskData.TaskInfo.Paytucich != "РГМ-2 (ОФ-462)") M1_DeltaPoxrak1.Text = Math.Round(TaskData.Battary1Texakayum.Poxrak1 - bat1.First.Paytucich,1).ToString();
                if (TaskData.TaskInfo.Positions == "Դասակի Կազմով")
                {
                    M1_DeltaPricel2.Text = TaskData.Battary1Texakayum.Core != "###" ? (TaskData.Battary1Texakayum.Pricel2 - bat1.Second.Pricel).ToString() : "-";
                    if (M1_DeltaPricel2.Text[0] != '-') M1_DeltaPricel2.Text = "+" + M1_DeltaPricel2.Text;
                    M1_DeltaDavarot2.Text = TaskData.Battary1Texakayum.Core != "###" ? Helper.FormatAngleWithSigne(TaskData.Battary1Texakayum.Davarot2 - bat1.Second.DavarotHU) : "-";
                    if (TaskData.TaskInfo.Paytucich != "РГМ-2 (ОФ-462)") M1_DeltaPoxrak2.Text = Math.Round(TaskData.Battary1Texakayum.Poxrak2 - bat1.Second.Paytucich, 1).ToString();
                }
            }
            if (bat2 != null)
            {
                M2_DeltaPricel1.Text = TaskData.Battary2Texakayum.Core != "###" ? (TaskData.Battary2Texakayum.Pricel1 - bat2.First.Pricel).ToString() : "-";
                M2_DeltaFront1.Text = Front != -1 ? Helper.FormatAngleWithSigne(((FrontMeter - NewTarget.Front) / (4 * bat2.First.Distance)) * 10) : "";
                if (M2_DeltaPricel1.Text[0] != '-') M2_DeltaPricel1.Text = "+" + M2_DeltaPricel1.Text;
                M2_DeltaDavarot1.Text = TaskData.Battary2Texakayum.Core != "###" ? Helper.FormatAngleWithSigne(TaskData.Battary2Texakayum.Davarot1 - bat2.First.DavarotHU) : "-";
                if (TaskData.TaskInfo.Paytucich != "РГМ-2 (ОФ-462)") M2_DeltaPoxrak1.Text = Math.Round(TaskData.Battary2Texakayum.Poxrak1 - bat2.First.Paytucich, 1).ToString();
                if (TaskData.TaskInfo.Positions == "Դասակի Կազմով")
                {
                    M2_DeltaPricel2.Text = TaskData.Battary2Texakayum.Core != "###" ? (TaskData.Battary2Texakayum.Pricel2 - bat2.Second.Pricel).ToString() : "-";
                    if (M2_DeltaPricel2.Text[0] != '-') M2_DeltaPricel2.Text = "+" + M2_DeltaPricel2.Text;
                    M2_DeltaDavarot2.Text = TaskData.Battary2Texakayum.Core != "###" ? Helper.FormatAngleWithSigne(TaskData.Battary2Texakayum.Davarot2 - bat2.Second.DavarotHU) : "-";
                    if (TaskData.TaskInfo.Paytucich != "РГМ-2 (ОФ-462)") M2_DeltaPoxrak2.Text = Math.Round(TaskData.Battary2Texakayum.Poxrak2 - bat2.Second.Paytucich, 1).ToString();

                }
            }
            if (bat3 != null)
            {
                M3_DeltaPricel1.Text = TaskData.Battary3Texakayum.Core != "###" ? (TaskData.Battary3Texakayum.Pricel1 - bat3.First.Pricel).ToString() : "-";
                M3_DeltaFront1.Text = Front != -1 ? Helper.FormatAngleWithSigne(((FrontMeter - NewTarget.Front) / (4 * bat3.First.Distance)) * 10) : "";
                if (M3_DeltaPricel1.Text[0] != '-') M3_DeltaPricel1.Text = "+" + M3_DeltaPricel1.Text;
                M3_DeltaDavarot1.Text = TaskData.Battary3Texakayum.Core != "###" ? Helper.FormatAngleWithSigne(TaskData.Battary3Texakayum.Davarot1 - bat3.First.DavarotHU) : "-";
                if (TaskData.TaskInfo.Paytucich != "РГМ-2 (ОФ-462)") M3_DeltaPoxrak1.Text = Math.Round(TaskData.Battary3Texakayum.Poxrak1 - bat3.First.Paytucich, 1).ToString();
                if (TaskData.TaskInfo.Positions == "Դասակի Կազմով")
                {
                    M3_DeltaPricel2.Text = TaskData.Battary3Texakayum.Core != "###" ? (TaskData.Battary3Texakayum.Pricel2 - bat3.Second.Pricel).ToString() : "-";
                    if (M3_DeltaPricel2.Text[0] != '-') M3_DeltaPricel2.Text = "+" + M3_DeltaPricel2.Text;
                    M3_DeltaDavarot2.Text = TaskData.Battary3Texakayum.Core != "###" ? Helper.FormatAngleWithSigne(TaskData.Battary3Texakayum.Davarot2 - bat3.Second.DavarotHU) : "-";
                    if (TaskData.TaskInfo.Paytucich != "РГМ-2 (ОФ-462)") M3_DeltaPoxrak2.Text = Math.Round(TaskData.Battary3Texakayum.Poxrak2 - bat3.Second.Paytucich, 1).ToString();
                }
            }
            NewTaskData = TaskData;
            //NewTaskData = new DitarkumDataModel()
            //{
            //    Ditaket = TaskData.Ditaket,
            //    ParallelDitaket = TaskData.ParallelDitaket,
            //    ParallelHramanatarakan = TaskData.ParallelHramanatarakan,
            //    Hramanatarakan = TaskData.Hramanatarakan,

            //    Target = TaskData.Target,

            //    TaskInfo = TaskData.TaskInfo,

            //    Battary1Texakayum = bat1 != null ? new PaytyunTexakayum
            //    {
            //        Core = bat1.Core,
            //        Davarot1 = bat1.First.DavarotHU + 2 * Helper.GetAngleFromString(M1_DeltaDavarot1.Text),
            //        Pricel1 = bat1.First.Pricel + 2 * double.Parse(M1_DeltaPricel1.Text),
            //        Davarot2 = TaskData.TaskInfo.Positions == "Դասակի Կազմով" ? bat1.Second.DavarotHU + 2 * Helper.GetAngleFromString(M1_DeltaDavarot2.Text) : 0,
            //        Pricel2 = TaskData.TaskInfo.Positions == "Դասակի Կազմով" ? bat1.Second.Pricel + 2 * double.Parse(M1_DeltaPricel2.Text) : 0,
            //        Ku = bat1.KU
            //    } : null,

            //    Battary2Texakayum = bat2 != null ? new PaytyunTexakayum
            //    {
            //        Core = bat2.Core,
            //        Davarot1 = bat1.First.DavarotHU + 2 * Helper.GetAngleFromString(M2_DeltaDavarot1.Text),
            //        Pricel1 = bat1.First.Pricel + 2 * double.Parse(M2_DeltaPricel1.Text),
            //        Davarot2 = TaskData.TaskInfo.Positions == "Դասակի Կազմով" ? bat1.Second.DavarotHU + 2 * Helper.GetAngleFromString(M2_DeltaDavarot2.Text) : 0,
            //        Pricel2 = TaskData.TaskInfo.Positions == "Դասակի Կազմով" ? bat1.Second.Pricel + 2 * double.Parse(M2_DeltaPricel2.Text) : 0,
            //        Ku = bat2.KU
            //    } : null,

            //    Battary3Texakayum = bat3 != null ? new PaytyunTexakayum
            //    {
            //        Core = bat3.Core,
            //        Davarot1 = bat1.First.DavarotHU + 2 * Helper.GetAngleFromString(M3_DeltaDavarot1.Text),
            //        Pricel1 = bat1.First.Pricel + 2 * double.Parse(M3_DeltaPricel1.Text),
            //        Davarot2 = TaskData.TaskInfo.Positions == "Դասակի Կազմով" ? bat1.Second.DavarotHU + 2 * Helper.GetAngleFromString(M3_DeltaDavarot2.Text) : 0,
            //        Pricel2 = TaskData.TaskInfo.Positions == "Դասակի Կազմով" ? bat1.Second.Pricel + 2 * double.Parse(M3_DeltaPricel2.Text) : 0,
            //        Ku = bat3.KU
            //    } : null,
            //};
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (TaskData.TaskInfo.TaskType != "Զուգորդված դիտարկում")
                {
                    if (((bool)DeltaAlphaAj.IsChecked || (bool)DeltaAlphaDzax.IsChecked)
                        && ((bool)DeltaDistanceMinus.IsChecked || (bool)DeltaDistancePlus.IsChecked))
                    {
                        if (!String.IsNullOrWhiteSpace(DeltaAlpha1.Text) && !String.IsNullOrWhiteSpace(DeltaDistance1.Text))
                        {
                            double DeltaAlpha = (bool)DeltaAlphaAj.IsChecked ? 0.01 * double.Parse(DeltaAlpha1.Text) : -0.01 * double.Parse(DeltaAlpha1.Text);
                            double DeltaDistance = (bool)DeltaDistancePlus.IsChecked ? double.Parse(DeltaDistance1.Text) : -1 * double.Parse(DeltaDistance1.Text);
                            double Front;
                            try { Front = Helper.GetAngleFromString(DeltaFront.Text); }
                            catch { Front = -1; }
                            CountNewTexakayum(Front, DeltaAlpha, DeltaDistance, 0);

                        }
                    }
                }
                else
                {
                    if (((bool)DeltaAlphaAj.IsChecked || (bool)DeltaAlphaDzax.IsChecked)
                        && ((bool)ParallelDeltaAlphaAj.IsChecked || (bool)ParallelDeltaAlphaDzax.IsChecked))
                    {
                        if (!String.IsNullOrWhiteSpace(DeltaAlpha1.Text) && !String.IsNullOrWhiteSpace(ParallelDeltaAlpha1.Text))
                        {
                            double DeltaAlpha = (bool)DeltaAlphaAj.IsChecked ? 0.01 * double.Parse(DeltaAlpha1.Text) : -0.01 * double.Parse(DeltaAlpha1.Text);
                            double DeltaParallelAlpha = (bool)ParallelDeltaAlphaAj.IsChecked ? double.Parse(ParallelDeltaAlpha1.Text) : -0.01 * double.Parse(ParallelDeltaAlpha1.Text);
                            double Front;
                            try { Front = Helper.GetAngleFromString(DeltaFront.Text); }
                            catch { Front = -1; }
                            CountNewTexakayum(Front, DeltaAlpha, 0, DeltaParallelAlpha);
                        }
                    }
                }
            }
        }
        private void NewDitarkum(object sender, MouseButtonEventArgs e)
        {
            try
            {

                if (NewTaskData.Battary1Texakayum == null && NewTaskData.Battary2Texakayum == null && NewTaskData.Battary3Texakayum == null)
                {
                    throw new InvalidOperationException();
                }
                new Ditarkum().SetTaskData(NewTaskData).ShowDialog();
                RefCount--;

            }
            catch (Exception)
            {
                MessageBox.Show("Հնարավոր չէ դիտարկում կատարել", "Կիսատ Խնդիր");
            }
        }
    }
}
