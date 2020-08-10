using ActionsLayer;
using BaseEntities;
using BaseService;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

namespace Nemesis.Windows.PrintWindows
{
    /// <summary>
    /// Логика взаимодействия для Hranot.xaml
    /// </summary>
    public partial class Hranot : Window
    {
        DialogData Data { get; set; }
        private TaskInfo Info { set; get; }
        public Hranot(TaskInfo Info)
        {
            this.Info = Info;
            Info.AXKType = "ՈՒղղանկյուն";
            Data = new DialogData();
            InitializeComponent();
            IsHranot1.IsChecked = true;
        }

        public void ScreenCapture(int res, string name)
        {
            Window win = HRANOT;
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)(res * win.Width), (int)(res * win.Height), res * 96, res * 96, PixelFormats.Pbgra32);
            bmp.Render(win);

            string PicPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GunNote");
            if (!Directory.Exists(PicPath))
                Directory.CreateDirectory(PicPath);

            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));

            string filePath = System.IO.Path.Combine(PicPath, string.Format("{0:MMddyyyyhhmmss}.png", name.Replace("/", "-")));
            using (Stream stream = File.Create(filePath))
            {
                encoder.Save(stream);
            }
            //DrawingVisual vis = new DrawingVisual();
            //using (var dc = vis.RenderOpen())
            //{
            //    dc.DrawImage(bmp, new Rect { Width = bmp.Width, Height = bmp.Height });
            //}
            //var pdialog = new PrintDialog();
            //if (pdialog.ShowDialog() == true)
            //{
            //    pdialog.PrintVisual(vis, "My Image");
            //}

        }

        private void HranotKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.P:
                    ScreenCapture(4, "Սկսած " + T1_Number.Text + "-ից");
                    break;
                case Key.LeftShift:
                    if (MainPanel.HorizontalAlignment == HorizontalAlignment.Right)
                        MainPanel.HorizontalAlignment = HorizontalAlignment.Left;
                    else
                        MainPanel.HorizontalAlignment = HorizontalAlignment.Right;
                    break;
                case Key.T:
                    var t = new SobDialog(Data);
                    t.Submit += value => Data = value;
                    t.ShowDialog();
                    try { SetAlphas(); HandleTargets(); } catch { }
                    break;
                case Key.Right:
                    try
                    {
                        if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                        {
                            AXK ats = TargetManager.GetNextAXKTarget(T10_Number.Text);
                            Data.StartTarget = ats.ID;
                        }
                        else
                        {
                            Target ts = TargetManager.GetNextTarget(T10_Number.Text);
                            Data.StartTarget = ts.ID;
                        }
                        HandleTargets();
                    }
                    catch { }
                    break;
            }

        }

        private void HandleTargets()
        {
            LableKD.Content = Data.KD;
            //LableDK.Content = Data.DK;
            bool IsDasaknerov = !Data.KD.Contains("Մարտկոցի Կազմով");
            if (!IsDasaknerov)
            {
                Info.Positions = "Մարտկոցի Կազմով";
            }
            else
            {
                Info.Positions = "Դասակի Կազմով";
            }
            Ditaket dk = Owin.Martakarg.GetDitaket(Data.DK);
            KrakayinDirq kd1, kd2;
            if (!IsDasaknerov)
            {
                kd1 = Owin.Martakarg.GetKDbyName(Data.KD, 0);
                kd2 = Owin.Martakarg.GetKDbyName(Data.KD, 0);
            }
            else
            {
                kd1 = Owin.Martakarg.GetKDbyName(Data.KD, 1);
                kd2 = Owin.Martakarg.GetKDbyName(Data.KD, 2);
            }
            if (Info.TaskType == "Պլանային (Նշ. ցանկից)")
            {
                var firsttarget = DataReader.GetTargets().FirstOrDefault();


                var target1 = TargetManager.GetTargetByNumber(Data.StartTarget, out _);
                var target2 = TargetManager.GetNextTarget(target1.ID);
                var target3 = TargetManager.GetNextTarget(target2.ID);
                var target4 = TargetManager.GetNextTarget(target3.ID);
                var target5 = TargetManager.GetNextTarget(target4.ID);
                var target6 = TargetManager.GetNextTarget(target5.ID);
                var target7 = TargetManager.GetNextTarget(target6.ID);
                var target8 = TargetManager.GetNextTarget(target7.ID);
                var target9 = TargetManager.GetNextTarget(target8.ID);
                var target10 = TargetManager.GetNextTarget(target9.ID);


                Battarey bat1, bat2, bat3, bat4, bat5, bat6, bat7, bat8, bat9, bat10;
                bat1 = Service.GetBattareyData(kd1, kd2, dk, target1, Info);
                bat2 = Service.GetBattareyData(kd1, kd2, dk, target2, Info);
                bat3 = Service.GetBattareyData(kd1, kd2, dk, target3, Info);
                bat4 = Service.GetBattareyData(kd1, kd2, dk, target4, Info);
                bat5 = Service.GetBattareyData(kd1, kd2, dk, target5, Info);
                bat6 = Service.GetBattareyData(kd1, kd2, dk, target6, Info);
                bat7 = Service.GetBattareyData(kd1, kd2, dk, target7, Info);
                bat8 = Service.GetBattareyData(kd1, kd2, dk, target8, Info);
                bat9 = Service.GetBattareyData(kd1, kd2, dk, target9, Info);
                bat10 = Service.GetBattareyData(kd1, kd2, dk, target10, Info);

                ClearView();
                InitializeTarget1Data(target1, bat1);
                if (firsttarget.ID != target2.ID)
                {
                    InitializeTarget2Data(target2, bat2);
                    if (firsttarget.ID != target3.ID)
                    {
                        InitializeTarget3Data(target3, bat3);
                        if (firsttarget.ID != target4.ID)
                        {
                            InitializeTarget4Data(target4, bat4);
                            if (firsttarget.ID != target5.ID)
                            {
                                InitializeTarget5Data(target5, bat5);
                                if (firsttarget.ID != target6.ID)
                                {
                                    InitializeTarget6Data(target6, bat6);
                                    if (firsttarget.ID != target7.ID)
                                    {
                                        InitializeTarget7Data(target7, bat7);
                                        if (firsttarget.ID != target8.ID)
                                        {
                                            InitializeTarget8Data(target8, bat8);
                                            if (firsttarget.ID != target9.ID)
                                            {
                                                InitializeTarget9Data(target9, bat9);
                                                if (firsttarget.ID != target10.ID)
                                                {
                                                    InitializeTarget10Data(target10, bat10);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
            {
                var firsttarget = DataReader.GetAXKTargets().FirstOrDefault();
                var target1 = TargetManager.GetAXKTargetByNumber(Data.StartTarget, out _);
                var target2 = TargetManager.GetNextAXKTarget(target1.ID);
                var target3 = TargetManager.GetNextAXKTarget(target2.ID);
                var target4 = TargetManager.GetNextAXKTarget(target3.ID);
                var target5 = TargetManager.GetNextAXKTarget(target4.ID);
                var target6 = TargetManager.GetNextAXKTarget(target5.ID);
                var target7 = TargetManager.GetNextAXKTarget(target6.ID);
                var target8 = TargetManager.GetNextAXKTarget(target7.ID);
                var target9 = TargetManager.GetNextAXKTarget(target8.ID);
                var target10 = TargetManager.GetNextAXKTarget(target9.ID);

                AXKReturn bat1 = Service.HandleAXKData(dk.Name, dk.Name, Info, new AXKTarget(new Uxankyun()
                {
                    X1 = target1.Right.X.ToString(),
                    Y1 = target1.Right.Y.ToString(),
                    H1 = target1.Right.H.ToString(),
                    X2 = target1.Left.X.ToString(),
                    Y2 = target1.Left.Y.ToString(),
                    H2 = target1.Left.H.ToString(),
                })),
                bat2 = Service.HandleAXKData(dk.Name, dk.Name, Info, new AXKTarget(new Uxankyun()
                {
                    X1 = target2.Right.X.ToString(),
                    Y1 = target2.Right.Y.ToString(),
                    H1 = target2.Right.H.ToString(),
                    X2 = target2.Left.X.ToString(),
                    Y2 = target2.Left.Y.ToString(),
                    H2 = target2.Left.H.ToString(),
                })),
                bat3 = Service.HandleAXKData(dk.Name, dk.Name, Info, new AXKTarget(new Uxankyun()
                {
                    X1 = target3.Right.X.ToString(),
                    Y1 = target3.Right.Y.ToString(),
                    H1 = target3.Right.H.ToString(),
                    X2 = target3.Left.X.ToString(),
                    Y2 = target3.Left.Y.ToString(),
                    H2 = target3.Left.H.ToString(),
                })),
                bat4 = Service.HandleAXKData(dk.Name, dk.Name, Info, new AXKTarget(new Uxankyun()
                {
                    X1 = target4.Right.X.ToString(),
                    Y1 = target4.Right.Y.ToString(),
                    H1 = target4.Right.H.ToString(),
                    X2 = target4.Left.X.ToString(),
                    Y2 = target4.Left.Y.ToString(),
                    H2 = target4.Left.H.ToString(),
                })),
                bat5 = Service.HandleAXKData(dk.Name, dk.Name, Info, new AXKTarget(new Uxankyun()
                {
                    X1 = target5.Right.X.ToString(),
                    Y1 = target5.Right.Y.ToString(),
                    H1 = target5.Right.H.ToString(),
                    X2 = target5.Left.X.ToString(),
                    Y2 = target5.Left.Y.ToString(),
                    H2 = target5.Left.H.ToString(),
                })),
                bat6 = Service.HandleAXKData(dk.Name, dk.Name, Info, new AXKTarget(new Uxankyun()
                {
                    X1 = target6.Right.X.ToString(),
                    Y1 = target6.Right.Y.ToString(),
                    H1 = target6.Right.H.ToString(),
                    X2 = target6.Left.X.ToString(),
                    Y2 = target6.Left.Y.ToString(),
                    H2 = target6.Left.H.ToString(),
                })),
                bat7 = Service.HandleAXKData(dk.Name, dk.Name, Info, new AXKTarget(new Uxankyun()
                {
                    X1 = target7.Right.X.ToString(),
                    Y1 = target7.Right.Y.ToString(),
                    H1 = target7.Right.H.ToString(),
                    X2 = target7.Left.X.ToString(),
                    Y2 = target7.Left.Y.ToString(),
                    H2 = target7.Left.H.ToString(),
                })),
                bat8 = Service.HandleAXKData(dk.Name, dk.Name, Info, new AXKTarget(new Uxankyun()
                {
                    X1 = target8.Right.X.ToString(),
                    Y1 = target8.Right.Y.ToString(),
                    H1 = target8.Right.H.ToString(),
                    X2 = target8.Left.X.ToString(),
                    Y2 = target8.Left.Y.ToString(),
                    H2 = target8.Left.H.ToString(),
                })),
                bat9 = Service.HandleAXKData(dk.Name, dk.Name, Info, new AXKTarget(new Uxankyun()
                {
                    X1 = target9.Right.X.ToString(),
                    Y1 = target9.Right.Y.ToString(),
                    H1 = target9.Right.H.ToString(),
                    X2 = target9.Left.X.ToString(),
                    Y2 = target9.Left.Y.ToString(),
                    H2 = target9.Left.H.ToString(),
                })),
                bat10 = Service.HandleAXKData(dk.Name, dk.Name, Info, new AXKTarget(new Uxankyun()
                {
                    X1 = target10.Right.X.ToString(),
                    Y1 = target10.Right.Y.ToString(),
                    H1 = target10.Right.H.ToString(),
                    X2 = target10.Left.X.ToString(),
                    Y2 = target10.Left.Y.ToString(),
                    H2 = target10.Left.H.ToString(),
                }));


                Battarey b1 = new Battarey(), b2 = new Battarey(), b3 = new Battarey(), b4 = new Battarey(), b5 = new Battarey(),
                    b6 = new Battarey(), b7 = new Battarey(), b8 = new Battarey(), b9 = new Battarey(), b10 = new Battarey();
                Target t1 = new Target(), t2 = new Target(), t3 = new Target(), t4 = new Target(), t5 = new Target(),
                    t6 = new Target(), t7 = new Target(), t8 = new Target(), t9 = new Target(), t10 = new Target();
                if (Data.KD == "1 ՀրՄ Դասակի Կազմով" || Data.KD == "1 ՀրՄ Մարտկոցի Կազմով")
                {
                    b1 = bat1.Bat1; t1 = bat1.T1;
                    b2 = bat2.Bat1; t2 = bat2.T1;
                    b3 = bat3.Bat1; t3 = bat3.T1;
                    b4 = bat4.Bat1; t4 = bat4.T1;
                    b5 = bat5.Bat1; t5 = bat5.T1;
                    b6 = bat6.Bat1; t6 = bat6.T1;
                    b7 = bat7.Bat1; t7 = bat7.T1;
                    b8 = bat8.Bat1; t8 = bat8.T1;
                    b9 = bat9.Bat1; t9 = bat9.T1;
                    b10 = bat10.Bat1; t10 = bat10.T1;

                }
                else if (Data.KD == "2 ՀրՄ Դասակի Կազմով" || Data.KD == "2 ՀրՄ Մարտկոցի Կազմով")
                {
                    b1 = bat1.Bat2; t1 = bat1.T2;
                    b2 = bat2.Bat2; t2 = bat2.T2;
                    b3 = bat3.Bat2; t3 = bat3.T2;
                    b4 = bat4.Bat2; t4 = bat4.T2;
                    b5 = bat5.Bat2; t5 = bat5.T2;
                    b6 = bat6.Bat2; t6 = bat6.T2;
                    b7 = bat7.Bat2; t7 = bat7.T2;
                    b8 = bat8.Bat2; t8 = bat8.T2;
                    b9 = bat9.Bat2; t9 = bat9.T2;
                    b10 = bat10.Bat2; t10 = bat10.T2;
                }
                else if (Data.KD == "3 ՀրՄ Դասակի Կազմով" || Data.KD == "3 ՀրՄ Մարտկոցի Կազմով")
                {
                    b1 = bat1.Bat3; t1 = bat1.T3;
                    b2 = bat2.Bat2; t2 = bat2.T3;
                    b3 = bat3.Bat3; t3 = bat3.T3;
                    b4 = bat4.Bat3; t4 = bat4.T3;
                    b5 = bat5.Bat3; t5 = bat5.T3;
                    b6 = bat6.Bat3; t6 = bat6.T1;
                    b7 = bat7.Bat3; t7 = bat7.T1;
                    b8 = bat8.Bat3; t8 = bat8.T1;
                    b9 = bat9.Bat3; t9 = bat9.T1;
                    b10 = bat10.Bat3; t10 = bat10.T1;
                }

                t1.ID = target1.ID; t2.ID = target2.ID; t3.ID = target3.ID; t4.ID = target4.ID; t5.ID = target5.ID;
                t6.ID = target6.ID; t7.ID = target7.ID; t8.ID = target8.ID; t9.ID = target9.ID; t10.ID = target10.ID;
                t1.Description = target1.Description; t2.Description = target2.Description; t3.Description = target3.Description; t4.Description = target4.Description; t5.Description = target5.Description;
                t6.Description = target6.Description; t7.Description = target7.Description; t8.Description = target8.Description; t9.Description = target9.Description; t10.Description = target10.Description;
                ClearView();
                InitializeTarget1Data(t1, b1);
                if (firsttarget.ID != target2.ID)
                {
                    InitializeTarget2Data(t2, b2);
                    if (firsttarget.ID != target3.ID)
                    {
                        InitializeTarget3Data(t3, b3);
                        if (firsttarget.ID != target4.ID)
                        {
                            InitializeTarget4Data(t4, b4);
                            if (firsttarget.ID != target5.ID)
                            {
                                InitializeTarget5Data(t5, b5);
                                if (firsttarget.ID != target6.ID)
                                {
                                    InitializeTarget6Data(t6, b6);
                                    if (firsttarget.ID != target7.ID)
                                    {
                                        InitializeTarget7Data(t7, b7);
                                        if (firsttarget.ID != target8.ID)
                                        {
                                            InitializeTarget8Data(t8, b8);
                                            if (firsttarget.ID != target9.ID)
                                            {
                                                InitializeTarget9Data(t9, b9);
                                                if (firsttarget.ID != target10.ID)
                                                {
                                                    InitializeTarget10Data(t10, b10);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void InitializeTarget1Data(Target target, Battarey bat)
        {
            T1_Number.Text = target.ID;
            T1_Desc.Text = target.Description;
            T1_Paytucich.Text = Info.Paytucich.Split(' ')[0];
            T1_Licq.Text = bat.Core;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    if ((bool)(IsHranot1.IsChecked) || (bool)(IsHranot2.IsChecked))
                    { 
                        T1_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel - bat.Catk).ToString() : bat.First.Pricel.ToString();
                    } else if ((bool)(IsHranot3.IsChecked) || (bool)(IsHranot4.IsChecked))
                    {
                        T1_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel + bat.Catk).ToString() : bat.First.Pricel.ToString();
                    }
                }
                else
                {
                    T1_Pricel1.Text = bat.First.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T1_Pricel2.Text = (bat.First.Pricel + bat.Catk).ToString();
                        T1_Pricel3.Text = (bat.First.Pricel - bat.Catk).ToString();
                    }
                }
                T1_LVL.Text = Helper.FormatAngle(bat.First.Level);
                T1_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.First.DavarotHU);
            }
            else
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    T1_Pricel1.Text = bat.Catk != -10 ? (bat.Second.Pricel + bat.Catk).ToString() : bat.Second.Pricel.ToString();
                }
                else
                {
                    T1_Pricel1.Text = bat.Second.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T1_Pricel2.Text = (bat.Second.Pricel + bat.Catk).ToString();
                        T1_Pricel3.Text = (bat.Second.Pricel - bat.Catk).ToString();
                    }
                }
                T1_LVL.Text = Helper.FormatAngle(bat.Second.Level);
                T1_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.Second.DavarotHU);
            }
            if (!Info.Paytucich.Contains("РГМ"))
            {
                if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
                    T1_Pricel2.Text = Math.Round(bat.First.Paytucich, 1).ToString();
                else
                    T1_Pricel2.Text = Math.Round(bat.Second.Paytucich, 1).ToString();
                PricelLabel.Text = "Նշանաթիվ, Փողրակ";
            }
            else PricelLabel.Text = "Նշանաթիվ";

            double kmh_k = new double(), punj_k = new double();
            if (Info.Positions == "Մարտկոցի Կազմով")
            {
                if ((bool)IsHranot1.IsChecked) { kmh_k = 1; punj_k = 0; }
                else if ((bool)IsHranot2.IsChecked) { kmh_k = 0; punj_k = 0; }
                else if ((bool)IsHranot3.IsChecked) { kmh_k = -1; punj_k = 0; }
                else if ((bool)IsHranot4.IsChecked) { kmh_k = -2; punj_k = 0; }
            }
            else
            {
                if ((bool)IsHranot1.IsChecked) { kmh_k = -1; punj_k = 1.5; }
                else if ((bool)IsHranot2.IsChecked) { kmh_k = 0; punj_k = 0.5; }
                else if ((bool)IsHranot3.IsChecked) { kmh_k = 0; punj_k = -0.5; }
                else if ((bool)IsHranot4.IsChecked) { kmh_k = 1; punj_k = -1.5; }
            }

            double.TryParse(Helper.RepresentStrings(HNK.Text), out double Hnk);
            double.TryParse(Helper.RepresentStrings(PNK.Text), out double Pnk);
            double.TryParse(Helper.RepresentStrings(GNK.Text), out double Gnk);
            double DavarotHU;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                Hnk = Hnk < Math.Abs(bat.First.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.First.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.First.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.First.DavarotHU;
            }
            else
            {
                Hnk = Hnk < Math.Abs(bat.Second.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.Second.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.Second.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.Second.DavarotHU;
            }
            bat.punj = bat.punj == -10 ? 0 : bat.punj;

            double data1 = DavarotHU + Hnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data2 = DavarotHU + Pnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data3 = DavarotHU + Gnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100);

            T1_HNK.Text = Helper.FormatAngle(data1 > 60 ? data1 - 60 : data1);
            T1_PNK.Text = Helper.FormatAngle(data2 > 60 ? data2 - 60 : data2);
            T1_GNK.Text = Helper.FormatAngle(data3 > 60 ? data3 - 60 : data3);
        }

        private void InitializeTarget2Data(Target target, Battarey bat)
        {
            T2_Number.Text = target.ID;
            T2_Desc.Text = target.Description;
            T2_Paytucich.Text = Info.Paytucich.Split(' ')[0];
            T2_Licq.Text = bat.Core;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    if ((bool)(IsHranot1.IsChecked) || (bool)(IsHranot2.IsChecked))
                    {
                        T2_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel - bat.Catk).ToString() : bat.First.Pricel.ToString();
                    }
                    else if ((bool)(IsHranot3.IsChecked) || (bool)(IsHranot4.IsChecked))
                    {
                        T2_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel + bat.Catk).ToString() : bat.First.Pricel.ToString();
                    }
                }
                else
                {
                    T2_Pricel1.Text = bat.First.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T2_Pricel2.Text = (bat.First.Pricel + bat.Catk).ToString();
                        T2_Pricel3.Text = (bat.First.Pricel - bat.Catk).ToString();
                    }
                }
                T2_LVL.Text = Helper.FormatAngle(bat.First.Level);
                T2_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.First.DavarotHU);
            }
            else
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    T2_Pricel1.Text = bat.Catk != -10 ? (bat.Second.Pricel + bat.Catk).ToString() : bat.Second.Pricel.ToString();
                }
                else
                {
                    T2_Pricel1.Text = bat.Second.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T2_Pricel2.Text = (bat.Second.Pricel + bat.Catk).ToString();
                        T2_Pricel3.Text = (bat.Second.Pricel - bat.Catk).ToString();
                    }
                }
                T2_LVL.Text = Helper.FormatAngle(bat.Second.Level);
                T2_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.Second.DavarotHU);
            }
            if (!Info.Paytucich.Contains("РГМ"))
            {
                if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
                    T2_Pricel2.Text = Math.Round(bat.First.Paytucich, 1).ToString();
                else
                    T2_Pricel2.Text = Math.Round(bat.Second.Paytucich, 1).ToString();
            }


            double kmh_k = new double(), punj_k = new double();
            if ((bool)IsHranot1.IsChecked) { kmh_k = -1; punj_k = 1.5; }
            else if ((bool)IsHranot2.IsChecked) { kmh_k = 0; punj_k = 0.5; }
            else if ((bool)IsHranot3.IsChecked) { kmh_k = 0; punj_k = -0.5; }
            else if ((bool)IsHranot4.IsChecked) { kmh_k = 1; punj_k = -1.5; }

            double.TryParse(Helper.RepresentStrings(HNK.Text), out double Hnk);
            double.TryParse(Helper.RepresentStrings(PNK.Text), out double Pnk);
            double.TryParse(Helper.RepresentStrings(GNK.Text), out double Gnk);
            double DavarotHU;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                Hnk = Hnk < Math.Abs(bat.First.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.First.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.First.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.First.DavarotHU;
            }
            else
            {
                Hnk = Hnk < Math.Abs(bat.Second.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.Second.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.Second.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.Second.DavarotHU;
            }
            bat.punj = bat.punj == -10 ? 0 : bat.punj;

            double data1 = DavarotHU + Hnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data2 = DavarotHU + Pnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data3 = DavarotHU + Gnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100);

            T2_HNK.Text = Helper.FormatAngle(data1 > 60 ? data1 - 60 : data1);
            T2_PNK.Text = Helper.FormatAngle(data2 > 60 ? data2 - 60 : data2);
            T2_GNK.Text = Helper.FormatAngle(data3 > 60 ? data3 - 60 : data3);
        }

        private void InitializeTarget3Data(Target target, Battarey bat)
        {
            T3_Number.Text = target.ID;
            T3_Desc.Text = target.Description;
            T3_Paytucich.Text = Info.Paytucich.Split(' ')[0];
            T3_Licq.Text = bat.Core;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    if ((bool)(IsHranot1.IsChecked) || (bool)(IsHranot2.IsChecked))
                    {
                        T3_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel - bat.Catk).ToString() : bat.First.Pricel.ToString();
                    }
                    else if ((bool)(IsHranot3.IsChecked) || (bool)(IsHranot4.IsChecked))
                    {
                        T3_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel + bat.Catk).ToString() : bat.First.Pricel.ToString();
                    }
                }
                else
                {
                    T3_Pricel1.Text = bat.First.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T3_Pricel2.Text = (bat.First.Pricel + bat.Catk).ToString();
                        T3_Pricel3.Text = (bat.First.Pricel - bat.Catk).ToString();
                    }
                }
                T3_LVL.Text = Helper.FormatAngle(bat.First.Level);
                T3_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.First.DavarotHU);
            }
            else
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    T3_Pricel1.Text = bat.Catk != -10 ? (bat.Second.Pricel + bat.Catk).ToString() : bat.Second.Pricel.ToString();
                }
                else
                {
                    T3_Pricel1.Text = bat.Second.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T3_Pricel2.Text = (bat.Second.Pricel + bat.Catk).ToString();
                        T3_Pricel3.Text = (bat.Second.Pricel - bat.Catk).ToString();
                    }
                }
                T3_LVL.Text = Helper.FormatAngle(bat.Second.Level);
                T3_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.Second.DavarotHU);
            }

            if (!Info.Paytucich.Contains("РГМ"))
            {
                if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
                    T3_Pricel2.Text = Math.Round(bat.First.Paytucich, 1).ToString();
                else
                    T3_Pricel2.Text = Math.Round(bat.Second.Paytucich, 1).ToString();
            }


            double kmh_k = new double(), punj_k = new double();
            if ((bool)IsHranot1.IsChecked) { kmh_k = -1; punj_k = 1.5; }
            else if ((bool)IsHranot2.IsChecked) { kmh_k = 0; punj_k = 0.5; }
            else if ((bool)IsHranot3.IsChecked) { kmh_k = 0; punj_k = -0.5; }
            else if ((bool)IsHranot4.IsChecked) { kmh_k = 1; punj_k = -1.5; }

            double.TryParse(Helper.RepresentStrings(HNK.Text), out double Hnk);
            double.TryParse(Helper.RepresentStrings(PNK.Text), out double Pnk);
            double.TryParse(Helper.RepresentStrings(GNK.Text), out double Gnk);
            double DavarotHU;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                Hnk = Hnk < Math.Abs(bat.First.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.First.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.First.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.First.DavarotHU;
            }
            else
            {
                Hnk = Hnk < Math.Abs(bat.Second.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.Second.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.Second.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.Second.DavarotHU;
            }
            bat.punj = bat.punj == -10 ? 0 : bat.punj;

            double data1 = DavarotHU + Hnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data2 = DavarotHU + Pnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data3 = DavarotHU + Gnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100);

            T3_HNK.Text = Helper.FormatAngle(data1 > 60 ? data1 - 60 : data1);
            T3_PNK.Text = Helper.FormatAngle(data2 > 60 ? data2 - 60 : data2);
            T3_GNK.Text = Helper.FormatAngle(data3 > 60 ? data3 - 60 : data3);
        }

        private void InitializeTarget4Data(Target target, Battarey bat)
        {
            T4_Number.Text = target.ID;
            T4_Desc.Text = target.Description;
            T4_Paytucich.Text = Info.Paytucich.Split(' ')[0];
            T4_Licq.Text = bat.Core;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    if ((bool)(IsHranot1.IsChecked) || (bool)(IsHranot2.IsChecked))
                    {
                        T4_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel - bat.Catk).ToString() : bat.First.Pricel.ToString();
                    }
                    else if ((bool)(IsHranot3.IsChecked) || (bool)(IsHranot4.IsChecked))
                    {
                        T4_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel + bat.Catk).ToString() : bat.First.Pricel.ToString();
                    }
                }
                else
                {
                    T4_Pricel1.Text = bat.First.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T4_Pricel2.Text = (bat.First.Pricel + bat.Catk).ToString();
                        T4_Pricel3.Text = (bat.First.Pricel - bat.Catk).ToString();
                    }
                }
                T4_LVL.Text = Helper.FormatAngle(bat.First.Level);
                T4_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.First.DavarotHU);
            }
            else
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    T4_Pricel1.Text = bat.Catk != -10 ? (bat.Second.Pricel + bat.Catk).ToString() : bat.Second.Pricel.ToString();
                }
                else
                {
                    T4_Pricel1.Text = bat.Second.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T4_Pricel2.Text = (bat.Second.Pricel + bat.Catk).ToString();
                        T4_Pricel3.Text = (bat.Second.Pricel - bat.Catk).ToString();
                    }
                }
                T4_LVL.Text = Helper.FormatAngle(bat.Second.Level);
                T4_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.Second.DavarotHU);
            }

            if (!Info.Paytucich.Contains("РГМ"))
            {
                if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
                    T4_Pricel2.Text = Math.Round(bat.First.Paytucich, 1).ToString();
                else
                    T4_Pricel2.Text = Math.Round(bat.Second.Paytucich, 1).ToString();
            }


            double kmh_k = new double(), punj_k = new double();
            if ((bool)IsHranot1.IsChecked) { kmh_k = -1; punj_k = 1.5; }
            else if ((bool)IsHranot2.IsChecked) { kmh_k = 0; punj_k = 0.5; }
            else if ((bool)IsHranot3.IsChecked) { kmh_k = 0; punj_k = -0.5; }
            else if ((bool)IsHranot4.IsChecked) { kmh_k = 1; punj_k = -1.5; }

            double.TryParse(Helper.RepresentStrings(HNK.Text), out double Hnk);
            double.TryParse(Helper.RepresentStrings(PNK.Text), out double Pnk);
            double.TryParse(Helper.RepresentStrings(GNK.Text), out double Gnk);
            double DavarotHU;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                Hnk = Hnk < Math.Abs(bat.First.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.First.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.First.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.First.DavarotHU;
            }
            else
            {
                Hnk = Hnk < Math.Abs(bat.Second.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.Second.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.Second.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.Second.DavarotHU;
            }
            bat.punj = bat.punj == -10 ? 0 : bat.punj;

            double data1 = DavarotHU + Hnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data2 = DavarotHU + Pnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data3 = DavarotHU + Gnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100);

            T4_HNK.Text = Helper.FormatAngle(data1 > 60 ? data1 - 60 : data1);
            T4_PNK.Text = Helper.FormatAngle(data2 > 60 ? data2 - 60 : data2);
            T4_GNK.Text = Helper.FormatAngle(data3 > 60 ? data3 - 60 : data3);
        }

        private void InitializeTarget5Data(Target target, Battarey bat)
        {
            T5_Number.Text = target.ID;
            T5_Desc.Text = target.Description;
            T5_Paytucich.Text = Info.Paytucich.Split(' ')[0];
            T5_Licq.Text = bat.Core;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    if ((bool)(IsHranot1.IsChecked) || (bool)(IsHranot2.IsChecked))
                    {
                        T5_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel - bat.Catk).ToString() : bat.First.Pricel.ToString();
                    }
                    else if ((bool)(IsHranot3.IsChecked) || (bool)(IsHranot4.IsChecked))
                    {
                        T5_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel + bat.Catk).ToString() : bat.First.Pricel.ToString();
                    }
                }
                else
                {
                    T5_Pricel1.Text = bat.First.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T5_Pricel2.Text = (bat.First.Pricel + bat.Catk).ToString();
                        T5_Pricel3.Text = (bat.First.Pricel - bat.Catk).ToString();
                    }
                }
                T5_LVL.Text = Helper.FormatAngle(bat.First.Level);
                T5_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.First.DavarotHU);
            }
            else
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    T5_Pricel1.Text = bat.Catk != -10 ? (bat.Second.Pricel + bat.Catk).ToString() : bat.Second.Pricel.ToString();
                }
                else
                {
                    T5_Pricel1.Text = bat.Second.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T5_Pricel2.Text = (bat.Second.Pricel + bat.Catk).ToString();
                        T5_Pricel3.Text = (bat.Second.Pricel - bat.Catk).ToString();
                    }
                }
                T5_LVL.Text = Helper.FormatAngle(bat.Second.Level);
                T5_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.Second.DavarotHU);
            }

            if (!Info.Paytucich.Contains("РГМ"))
            {
                if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
                    T5_Pricel2.Text = Math.Round(bat.First.Paytucich, 1).ToString();
                else
                    T5_Pricel2.Text = Math.Round(bat.Second.Paytucich, 1).ToString();
            }


            double kmh_k = new double(), punj_k = new double();
            if ((bool)IsHranot1.IsChecked) { kmh_k = -1; punj_k = 1.5; }
            else if ((bool)IsHranot2.IsChecked) { kmh_k = 0; punj_k = 0.5; }
            else if ((bool)IsHranot3.IsChecked) { kmh_k = 0; punj_k = -0.5; }
            else if ((bool)IsHranot4.IsChecked) { kmh_k = 1; punj_k = -1.5; }

            double.TryParse(Helper.RepresentStrings(HNK.Text), out double Hnk);
            double.TryParse(Helper.RepresentStrings(PNK.Text), out double Pnk);
            double.TryParse(Helper.RepresentStrings(GNK.Text), out double Gnk);
            double DavarotHU;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                Hnk = Hnk < Math.Abs(bat.First.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.First.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.First.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.First.DavarotHU;
            }
            else
            {
                Hnk = Hnk < Math.Abs(bat.Second.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.Second.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.Second.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.Second.DavarotHU;
            }
            bat.punj = bat.punj == -10 ? 0 : bat.punj;

            double data1 = DavarotHU + Hnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data2 = DavarotHU + Pnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data3 = DavarotHU + Gnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100);

            T5_HNK.Text = Helper.FormatAngle(data1 > 60 ? data1 - 60 : data1);
            T5_PNK.Text = Helper.FormatAngle(data2 > 60 ? data2 - 60 : data2);
            T5_GNK.Text = Helper.FormatAngle(data3 > 60 ? data3 - 60 : data3);
        }

        private void InitializeTarget6Data(Target target, Battarey bat)
        {
            T6_Number.Text = target.ID;
            T6_Desc.Text = target.Description;
            T6_Paytucich.Text = Info.Paytucich.Split(' ')[0];
            T6_Licq.Text = bat.Core;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    if ((bool)(IsHranot1.IsChecked) || (bool)(IsHranot2.IsChecked))
                    {
                        T6_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel - bat.Catk).ToString() : bat.First.Pricel.ToString();
                    }
                    else if ((bool)(IsHranot3.IsChecked) || (bool)(IsHranot4.IsChecked))
                    {
                        T6_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel + bat.Catk).ToString() : bat.First.Pricel.ToString();
                    }
                }
                else
                {
                    T6_Pricel1.Text = bat.First.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T6_Pricel2.Text = (bat.First.Pricel + bat.Catk).ToString();
                        T6_Pricel3.Text = (bat.First.Pricel - bat.Catk).ToString();
                    }
                }
                T6_LVL.Text = Helper.FormatAngle(bat.First.Level);
                T6_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.First.DavarotHU);
            }
            else
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    T6_Pricel1.Text = bat.Catk != -10 ? (bat.Second.Pricel + bat.Catk).ToString() : bat.Second.Pricel.ToString();
                }
                else
                {
                    T6_Pricel1.Text = bat.Second.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T6_Pricel2.Text = (bat.Second.Pricel + bat.Catk).ToString();
                        T6_Pricel3.Text = (bat.Second.Pricel - bat.Catk).ToString();
                    }
                }
                T6_LVL.Text = Helper.FormatAngle(bat.Second.Level);
                T6_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.Second.DavarotHU);
            }

            if (!Info.Paytucich.Contains("РГМ"))
            {
                if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
                    T6_Pricel2.Text = Math.Round(bat.First.Paytucich, 1).ToString();
                else
                    T6_Pricel2.Text = Math.Round(bat.Second.Paytucich, 1).ToString();
            }


            double kmh_k = new double(), punj_k = new double();
            if ((bool)IsHranot1.IsChecked) { kmh_k = -1; punj_k = 1.5; }
            else if ((bool)IsHranot2.IsChecked) { kmh_k = 0; punj_k = 0.5; }
            else if ((bool)IsHranot3.IsChecked) { kmh_k = 0; punj_k = -0.5; }
            else if ((bool)IsHranot4.IsChecked) { kmh_k = 1; punj_k = -1.5; }

            double.TryParse(Helper.RepresentStrings(HNK.Text), out double Hnk);
            double.TryParse(Helper.RepresentStrings(PNK.Text), out double Pnk);
            double.TryParse(Helper.RepresentStrings(GNK.Text), out double Gnk);
            double DavarotHU;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                Hnk = Hnk < Math.Abs(bat.First.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.First.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.First.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.First.DavarotHU;
            }
            else
            {
                Hnk = Hnk < Math.Abs(bat.Second.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.Second.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.Second.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.Second.DavarotHU;
            }
            bat.punj = bat.punj == -10 ? 0 : bat.punj;

            double data1 = DavarotHU + Hnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data2 = DavarotHU + Pnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data3 = DavarotHU + Gnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100);

            T6_HNK.Text = Helper.FormatAngle(data1 > 60 ? data1 - 60 : data1);
            T6_PNK.Text = Helper.FormatAngle(data2 > 60 ? data2 - 60 : data2);
            T6_GNK.Text = Helper.FormatAngle(data3 > 60 ? data3 - 60 : data3);
        }

        private void InitializeTarget7Data(Target target, Battarey bat)
        {
            T7_Number.Text = target.ID;
            T7_Desc.Text = target.Description;
            T7_Paytucich.Text = Info.Paytucich.Split(' ')[0];
            T7_Licq.Text = bat.Core;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    if ((bool)(IsHranot1.IsChecked) || (bool)(IsHranot2.IsChecked))
                    {
                        T7_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel - bat.Catk).ToString() : bat.First.Pricel.ToString();
                    }
                    else if ((bool)(IsHranot3.IsChecked) || (bool)(IsHranot4.IsChecked))
                    {
                        T7_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel + bat.Catk).ToString() : bat.First.Pricel.ToString();
                    }
                }
                else
                {
                    T7_Pricel1.Text = bat.First.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T7_Pricel2.Text = (bat.First.Pricel + bat.Catk).ToString();
                        T7_Pricel3.Text = (bat.First.Pricel - bat.Catk).ToString();
                    }
                }
                T7_LVL.Text = Helper.FormatAngle(bat.First.Level);
                T7_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.First.DavarotHU);
            }
            else
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    T7_Pricel1.Text = bat.Catk != -10 ? (bat.Second.Pricel + bat.Catk).ToString() : bat.Second.Pricel.ToString();
                }
                else
                {
                    T7_Pricel1.Text = bat.Second.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T7_Pricel2.Text = (bat.Second.Pricel + bat.Catk).ToString();
                        T7_Pricel3.Text = (bat.Second.Pricel - bat.Catk).ToString();
                    }
                }
                T7_LVL.Text = Helper.FormatAngle(bat.Second.Level);
                T7_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.Second.DavarotHU);
            }

            if (!Info.Paytucich.Contains("РГМ"))
            {
                if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
                    T7_Pricel2.Text = Math.Round(bat.First.Paytucich, 1).ToString();
                else
                    T7_Pricel2.Text = Math.Round(bat.Second.Paytucich, 1).ToString();
            }


            double kmh_k = new double(), punj_k = new double();
            if ((bool)IsHranot1.IsChecked) { kmh_k = -1; punj_k = 1.5; }
            else if ((bool)IsHranot2.IsChecked) { kmh_k = 0; punj_k = 0.5; }
            else if ((bool)IsHranot3.IsChecked) { kmh_k = 0; punj_k = -0.5; }
            else if ((bool)IsHranot4.IsChecked) { kmh_k = 1; punj_k = -1.5; }

            double.TryParse(Helper.RepresentStrings(HNK.Text), out double Hnk);
            double.TryParse(Helper.RepresentStrings(PNK.Text), out double Pnk);
            double.TryParse(Helper.RepresentStrings(GNK.Text), out double Gnk);
            double DavarotHU;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                Hnk = Hnk < Math.Abs(bat.First.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.First.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.First.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.First.DavarotHU;
            }
            else
            {
                Hnk = Hnk < Math.Abs(bat.Second.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.Second.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.Second.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.Second.DavarotHU;
            }
            bat.punj = bat.punj == -10 ? 0 : bat.punj;

            double data1 = DavarotHU + Hnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data2 = DavarotHU + Pnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data3 = DavarotHU + Gnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100);

            T7_HNK.Text = Helper.FormatAngle(data1 > 60 ? data1 - 60 : data1);
            T7_PNK.Text = Helper.FormatAngle(data2 > 60 ? data2 - 60 : data2);
            T7_GNK.Text = Helper.FormatAngle(data3 > 60 ? data3 - 60 : data3);
        }

        private void InitializeTarget8Data(Target target, Battarey bat)
        {
            T8_Number.Text = target.ID;
            T8_Desc.Text = target.Description;
            T8_Paytucich.Text = Info.Paytucich.Split(' ')[0];
            T8_Licq.Text = bat.Core;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    if ((bool)(IsHranot1.IsChecked) || (bool)(IsHranot2.IsChecked))
                    {
                        T8_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel - bat.Catk).ToString() : bat.First.Pricel.ToString();
                    }
                    else if ((bool)(IsHranot3.IsChecked) || (bool)(IsHranot4.IsChecked))
                    {
                        T8_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel + bat.Catk).ToString() : bat.First.Pricel.ToString();
                    }
                }
                else
                {
                    T8_Pricel1.Text = bat.First.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T8_Pricel2.Text = (bat.First.Pricel + bat.Catk).ToString();
                        T8_Pricel3.Text = (bat.First.Pricel - bat.Catk).ToString();
                    }
                }
                T8_LVL.Text = Helper.FormatAngle(bat.First.Level);
                T8_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.First.DavarotHU);
            }
            else
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    T8_Pricel1.Text = bat.Catk != -10 ? (bat.Second.Pricel + bat.Catk).ToString() : bat.Second.Pricel.ToString();
                }
                else
                {
                    T8_Pricel1.Text = bat.Second.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T8_Pricel2.Text = (bat.Second.Pricel + bat.Catk).ToString();
                        T8_Pricel3.Text = (bat.Second.Pricel - bat.Catk).ToString();
                    }
                }
                T8_LVL.Text = Helper.FormatAngle(bat.Second.Level);
                T8_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.Second.DavarotHU);
            }

            if (!Info.Paytucich.Contains("РГМ"))
            {
                if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
                    T8_Pricel2.Text = Math.Round(bat.First.Paytucich, 1).ToString();
                else
                    T8_Pricel2.Text = Math.Round(bat.Second.Paytucich, 1).ToString();
            }


            double kmh_k = new double(), punj_k = new double();
            if ((bool)IsHranot1.IsChecked) { kmh_k = -1; punj_k = 1.5; }
            else if ((bool)IsHranot2.IsChecked) { kmh_k = 0; punj_k = 0.5; }
            else if ((bool)IsHranot3.IsChecked) { kmh_k = 0; punj_k = -0.5; }
            else if ((bool)IsHranot4.IsChecked) { kmh_k = 1; punj_k = -1.5; }

            double.TryParse(Helper.RepresentStrings(HNK.Text), out double Hnk);
            double.TryParse(Helper.RepresentStrings(PNK.Text), out double Pnk);
            double.TryParse(Helper.RepresentStrings(GNK.Text), out double Gnk);
            double DavarotHU;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                Hnk = Hnk < Math.Abs(bat.First.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.First.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.First.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.First.DavarotHU;
            }
            else
            {
                Hnk = Hnk < Math.Abs(bat.Second.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.Second.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.Second.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.Second.DavarotHU;
            }
            bat.punj = bat.punj == -10 ? 0 : bat.punj;

            double data1 = DavarotHU + Hnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data2 = DavarotHU + Pnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data3 = DavarotHU + Gnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100);

            T8_HNK.Text = Helper.FormatAngle(data1 > 60 ? data1 - 60 : data1);
            T8_PNK.Text = Helper.FormatAngle(data2 > 60 ? data2 - 60 : data2);
            T8_GNK.Text = Helper.FormatAngle(data3 > 60 ? data3 - 60 : data3);
        }

        private void InitializeTarget9Data(Target target, Battarey bat)
        {
            T9_Number.Text = target.ID;
            T9_Desc.Text = target.Description;
            T9_Paytucich.Text = Info.Paytucich.Split(' ')[0];
            T9_Licq.Text = bat.Core;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    if ((bool)(IsHranot1.IsChecked) || (bool)(IsHranot2.IsChecked))
                    {
                        T9_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel - bat.Catk).ToString() : bat.First.Pricel.ToString();
                    }
                    else if ((bool)(IsHranot3.IsChecked) || (bool)(IsHranot4.IsChecked))
                    {
                        T9_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel + bat.Catk).ToString() : bat.First.Pricel.ToString();
                    }
                }
                else
                {
                    T9_Pricel1.Text = bat.First.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T9_Pricel2.Text = (bat.First.Pricel + bat.Catk).ToString();
                        T9_Pricel3.Text = (bat.First.Pricel - bat.Catk).ToString();
                    }
                }
                T9_LVL.Text = Helper.FormatAngle(bat.First.Level);
                T9_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.First.DavarotHU);
            }
            else
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    T9_Pricel1.Text = bat.Catk != -10 ? (bat.Second.Pricel + bat.Catk).ToString() : bat.Second.Pricel.ToString();
                }
                else
                {
                    T9_Pricel1.Text = bat.Second.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T9_Pricel2.Text = (bat.Second.Pricel + bat.Catk).ToString();
                        T9_Pricel3.Text = (bat.Second.Pricel - bat.Catk).ToString();
                    }
                }
                T9_LVL.Text = Helper.FormatAngle(bat.Second.Level);
                T9_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.Second.DavarotHU);
            }

            if (!Info.Paytucich.Contains("РГМ"))
            {
                if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
                    T9_Pricel2.Text = Math.Round(bat.First.Paytucich, 1).ToString();
                else
                    T9_Pricel2.Text = Math.Round(bat.Second.Paytucich, 1).ToString();
            }


            double kmh_k = new double(), punj_k = new double();
            if ((bool)IsHranot1.IsChecked) { kmh_k = -1; punj_k = 1.5; }
            else if ((bool)IsHranot2.IsChecked) { kmh_k = 0; punj_k = 0.5; }
            else if ((bool)IsHranot3.IsChecked) { kmh_k = 0; punj_k = -0.5; }
            else if ((bool)IsHranot4.IsChecked) { kmh_k = 1; punj_k = -1.5; }

            double.TryParse(Helper.RepresentStrings(HNK.Text), out double Hnk);
            double.TryParse(Helper.RepresentStrings(PNK.Text), out double Pnk);
            double.TryParse(Helper.RepresentStrings(GNK.Text), out double Gnk);
            double DavarotHU;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                Hnk = Hnk < Math.Abs(bat.First.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.First.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.First.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.First.DavarotHU;
            }
            else
            {
                Hnk = Hnk < Math.Abs(bat.Second.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.Second.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.Second.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.Second.DavarotHU;
            }
            bat.punj = bat.punj == -10 ? 0 : bat.punj;

            double data1 = DavarotHU + Hnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data2 = DavarotHU + Pnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data3 = DavarotHU + Gnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100);

            T9_HNK.Text = Helper.FormatAngle(data1 > 60 ? data1 - 60 : data1);
            T9_PNK.Text = Helper.FormatAngle(data2 > 60 ? data2 - 60 : data2);
            T9_GNK.Text = Helper.FormatAngle(data3 > 60 ? data3 - 60 : data3);
        }

        private void InitializeTarget10Data(Target target, Battarey bat)
        {
            T10_Number.Text = target.ID;
            T10_Desc.Text = target.Description;
            T10_Paytucich.Text = Info.Paytucich.Split(' ')[0];
            T10_Licq.Text = bat.Core;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    if ((bool)(IsHranot1.IsChecked) || (bool)(IsHranot2.IsChecked))
                    {
                        T10_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel - bat.Catk).ToString() : bat.First.Pricel.ToString();
                    }
                    else if ((bool)(IsHranot3.IsChecked) || (bool)(IsHranot4.IsChecked))
                    {
                        T10_Pricel1.Text = bat.Catk != -10 ? (bat.First.Pricel + bat.Catk).ToString() : bat.First.Pricel.ToString();
                    }
                }
                else
                {
                    T10_Pricel1.Text = bat.First.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T10_Pricel2.Text = (bat.First.Pricel + bat.Catk).ToString();
                        T10_Pricel3.Text = (bat.First.Pricel - bat.Catk).ToString();
                    }
                }
                T10_LVL.Text = Helper.FormatAngle(bat.First.Level);
                T10_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.First.DavarotHU);
            }
            else
            {
                if (Info.TaskType == "ԱԱԿ" || Info.TaskType == "ՇԱԿ")
                {
                    T10_Pricel1.Text = bat.Catk != -10 ? (bat.Second.Pricel + bat.Catk).ToString() : bat.Second.Pricel.ToString();
                }
                else
                {
                    T10_Pricel1.Text = bat.Second.Pricel.ToString();
                    if (bat.Catk > 0)
                    {
                        T10_Pricel2.Text = (bat.Second.Pricel + bat.Catk).ToString();
                        T10_Pricel3.Text = (bat.Second.Pricel - bat.Catk).ToString();
                    }
                }
                T10_LVL.Text = Helper.FormatAngle(bat.Second.Level);
                T10_DavarotHU.Text = Helper.FormatAngleWithSigne(bat.Second.DavarotHU);
            }

            if (!Info.Paytucich.Contains("РГМ"))
            {
                if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
                    T10_Pricel2.Text = Math.Round(bat.First.Paytucich, 1).ToString();
                else
                    T10_Pricel2.Text = Math.Round(bat.Second.Paytucich, 1).ToString();
            }


            double kmh_k = new double(), punj_k = new double();
            if ((bool)IsHranot1.IsChecked) { kmh_k = -1; punj_k = 1.5; }
            else if ((bool)IsHranot2.IsChecked) { kmh_k = 0; punj_k = 0.5; }
            else if ((bool)IsHranot3.IsChecked) { kmh_k = 0; punj_k = -0.5; }
            else if ((bool)IsHranot4.IsChecked) { kmh_k = 1; punj_k = -1.5; }

            double.TryParse(Helper.RepresentStrings(HNK.Text), out double Hnk);
            double.TryParse(Helper.RepresentStrings(PNK.Text), out double Pnk);
            double.TryParse(Helper.RepresentStrings(GNK.Text), out double Gnk);
            double DavarotHU;
            if (Info.Positions == "Մարտկոցի Կազմով" || (bool)IsHranot1.IsChecked || (bool)IsHranot2.IsChecked)
            {
                Hnk = Hnk < Math.Abs(bat.First.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.First.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.First.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.First.DavarotHU;
            }
            else
            {
                Hnk = Hnk < Math.Abs(bat.Second.DavarotHU) ? Hnk + 60 : Hnk;
                Pnk = Pnk < Math.Abs(bat.Second.DavarotHU) ? Pnk + 60 : Pnk;
                Gnk = Gnk < Math.Abs(bat.Second.DavarotHU) ? Gnk + 60 : Gnk;
                DavarotHU = bat.Second.DavarotHU;
            }
            bat.punj = bat.punj == -10 ? 0 : bat.punj;

            double data1 = DavarotHU + Hnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data2 = DavarotHU + Pnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100),
                data3 = DavarotHU + Gnk + (kmh_k * bat.KMH) + (punj_k * bat.punj / 100);

            T10_HNK.Text = Helper.FormatAngle(data1 > 60 ? data1 - 60 : data1);
            T10_PNK.Text = Helper.FormatAngle(data2 > 60 ? data2 - 60 : data2);
            T10_GNK.Text = Helper.FormatAngle(data3 > 60 ? data3 - 60 : data3);
        }
        private void ClearView()
        {
            T1_DavarotHU.Text = ""; T1_Desc.Text = ""; T1_GNK.Text = ""; T1_HNK.Text = ""; T1_Licq.Text = ""; T1_LVL.Text = "";
            T1_Number.Text = ""; T1_Paytucich.Text = ""; T1_PNK.Text = ""; T1_Pricel1.Text = ""; T1_Pricel2.Text = ""; T1_Pricel3.Text = "";

            T2_DavarotHU.Text = ""; T2_Desc.Text = ""; T2_GNK.Text = ""; T2_HNK.Text = ""; T2_Licq.Text = ""; T2_LVL.Text = "";
            T2_Number.Text = ""; T2_Paytucich.Text = ""; T2_PNK.Text = ""; T2_Pricel1.Text = ""; T2_Pricel2.Text = ""; T2_Pricel3.Text = "";

            T3_DavarotHU.Text = ""; T3_Desc.Text = ""; T3_GNK.Text = ""; T3_HNK.Text = ""; T3_Licq.Text = ""; T3_LVL.Text = "";
            T3_Number.Text = ""; T3_Paytucich.Text = ""; T3_PNK.Text = ""; T3_Pricel1.Text = ""; T3_Pricel2.Text = ""; T3_Pricel3.Text = "";

            T4_DavarotHU.Text = ""; T4_Desc.Text = ""; T4_GNK.Text = ""; T4_HNK.Text = ""; T4_Licq.Text = ""; T4_LVL.Text = "";
            T4_Number.Text = ""; T4_Paytucich.Text = ""; T4_PNK.Text = ""; T4_Pricel1.Text = ""; T4_Pricel2.Text = ""; T4_Pricel3.Text = "";

            T5_DavarotHU.Text = ""; T5_Desc.Text = ""; T5_GNK.Text = ""; T5_HNK.Text = ""; T5_Licq.Text = ""; T5_LVL.Text = "";
            T5_Number.Text = ""; T5_Paytucich.Text = ""; T5_PNK.Text = ""; T5_Pricel1.Text = ""; T5_Pricel2.Text = ""; T5_Pricel3.Text = "";

            T6_DavarotHU.Text = ""; T6_Desc.Text = ""; T6_GNK.Text = ""; T6_HNK.Text = ""; T6_Licq.Text = ""; T6_LVL.Text = "";
            T6_Number.Text = ""; T6_Paytucich.Text = ""; T6_PNK.Text = ""; T6_Pricel1.Text = ""; T6_Pricel2.Text = ""; T6_Pricel3.Text = "";

            T7_DavarotHU.Text = ""; T7_Desc.Text = ""; T7_GNK.Text = ""; T7_HNK.Text = ""; T7_Licq.Text = ""; T7_LVL.Text = "";
            T7_Number.Text = ""; T7_Paytucich.Text = ""; T7_PNK.Text = ""; T7_Pricel1.Text = ""; T7_Pricel2.Text = ""; T7_Pricel3.Text = "";

            T8_DavarotHU.Text = ""; T8_Desc.Text = ""; T8_GNK.Text = ""; T8_HNK.Text = ""; T8_Licq.Text = ""; T8_LVL.Text = "";
            T8_Number.Text = ""; T8_Paytucich.Text = ""; T8_PNK.Text = ""; T8_Pricel1.Text = ""; T8_Pricel2.Text = ""; T8_Pricel3.Text = "";

            T9_DavarotHU.Text = ""; T9_Desc.Text = ""; T9_GNK.Text = ""; T9_HNK.Text = ""; T9_Licq.Text = ""; T9_LVL.Text = "";
            T9_Number.Text = ""; T9_Paytucich.Text = ""; T9_PNK.Text = ""; T9_Pricel1.Text = ""; T9_Pricel2.Text = ""; T9_Pricel3.Text = "";

            T10_DavarotHU.Text = ""; T10_Desc.Text = ""; T10_GNK.Text = ""; T10_HNK.Text = ""; T10_Licq.Text = ""; T10_LVL.Text = "";
            T10_Number.Text = ""; T10_Paytucich.Text = ""; T10_PNK.Text = ""; T10_Pricel1.Text = ""; T10_Pricel2.Text = ""; T10_Pricel3.Text = "";
        }

        private HnkPnkGnk SelectAlphas()
        {
            var angles = new HnkPnkGnk();
            var Alphas = DataReader.GetAlphas();
            switch (Data.KD)
            {
                case "1 ՀրՄ Դասակի Կազմով":
                    angles = Alphas.Himnakan_1HrM;
                    break;
                case "1 ՀրՄ Մարտկոցի Կազմով":
                    angles = Alphas.Pahestayin_1HrM;
                    break;
                case "2 ՀրՄ Դասակի Կազմով":
                    angles = Alphas.Himnakan_2HrM;
                    break;
                case "2 ՀրՄ Մարտկոցի Կազմով":
                    angles = Alphas.Pahestayin_2HrM;
                    break;
                case "3 ՀրՄ Դասակի Կազմով":
                    angles = Alphas.Himnakan_3HrM;
                    break;
                case "3 ՀրՄ Մարտկոցի Կազմով":
                    angles = Alphas.Pahestayin_3HrM;
                    break;
            }
            return angles;
        }

        private void SetAlphas()
        {
            var alphas = SelectAlphas();
            if ((bool)IsHranot1.IsChecked)
            {
                HNK.Text = Helper.FormatAngle(alphas.HNK.First);
                PNK.Text = Helper.FormatAngle(alphas.PNK.First);
                GNK.Text = Helper.FormatAngle(alphas.GNK.First);
            }
            else if ((bool)IsHranot2.IsChecked)
            {
                HNK.Text = Helper.FormatAngle(alphas.HNK.Second);
                PNK.Text = Helper.FormatAngle(alphas.PNK.Second);
                GNK.Text = Helper.FormatAngle(alphas.GNK.Second);
            }
            else if ((bool)IsHranot3.IsChecked)
            {
                HNK.Text = Helper.FormatAngle(alphas.HNK.Third);
                PNK.Text = Helper.FormatAngle(alphas.PNK.Third);
                GNK.Text = Helper.FormatAngle(alphas.GNK.Third);
            }
            else if ((bool)IsHranot4.IsChecked)
            {
                HNK.Text = Helper.FormatAngle(alphas.HNK.Fourth);
                PNK.Text = Helper.FormatAngle(alphas.PNK.Fourth);
                GNK.Text = Helper.FormatAngle(alphas.GNK.Fourth);
            }


        }

        private void HandleReaquest(object sender, RoutedEventArgs e)
        {
            try { SetAlphas(); HandleTargets(); } catch { }
        }
    }
}
