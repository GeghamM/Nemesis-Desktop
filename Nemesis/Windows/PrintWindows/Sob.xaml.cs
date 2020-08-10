using BaseEntities;
using DataAccessLayer;
using System;
using System.Collections.Generic;
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
using ActionsLayer;
using BaseService;

namespace Nemesis.Windows.PrintWindows
{
    /// <summary>
    /// Логика взаимодействия для Sob.xaml
    /// </summary>
    public partial class Sob : Window
    {
        DialogData Data;
        public TaskInfo SobInfo { get; set; }
        public Sob(TaskInfo info)
        {
            InitializeComponent();
            Data = new DialogData();
            SobInfo = info;
        }
        public void ScreenCapture(int res, string name)
        {
            Window win = SOB;
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)(res * win.Width), (int)(res * win.Height), res * 96, res * 96, PixelFormats.Pbgra32);
            bmp.Render(win);
            string PicPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SobNote");
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
        private void SobKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.P:
                    ScreenCapture(8, T1.Text + " - " + T5.Text);
                    break;
                case Key.T:
                    var t = new SobDialog(Data);
                    t.Submit += value => Data = value;
                    t.ShowDialog();
                    HandleSob(Data);
                    break;
                case Key.Right:
                    try
                    {
                        if (SobInfo.TaskType == "ԱԱԿ" || SobInfo.TaskType == "ՇԱԿ")
                        {
                            AXK ats = TargetManager.GetNextAXKTarget(T5.Text);
                            Data.StartTarget = ats.ID;
                        }
                        else
                        {
                            Target ts = TargetManager.GetNextTarget(T5.Text);
                            Data.StartTarget = ts.ID;
                        }
                        HandleSob(Data);
                    }
                    catch { }
                    break;
            }
        }
        private void HandleSob(DialogData Data)
        {
            LableDK.Content = Data.DK;
            LableKD.Content = Data.KD;
            try
            {
                bool IsDasaknerov = !Data.KD.Contains("Մարտկոցի Կազմով");
                Ditaket dk = Owin.Martakarg.GetDitaket(Data.DK);
                KrakayinDirq kd1 = null, kd2 = null;
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
                if (SobInfo.TaskType == "Պլանային (Նշ. ցանկից)")
                {
                    var firsttarget = DataReader.GetTargets().FirstOrDefault();
                    /////////////////////////////////////////////////////////////////////////////////////
                    var target1 = TargetManager.GetTargetByNumber(Data.StartTarget, out _);
                    var target2 = TargetManager.GetNextTarget(target1.ID);
                    var target3 = TargetManager.GetNextTarget(target2.ID);
                    var target4 = TargetManager.GetNextTarget(target3.ID);
                    var target5 = TargetManager.GetNextTarget(target4.ID);
                    /////////////////////////////////////////////////////////////////////////////////////
                    Battarey bat1, bat2, bat3, bat4, bat5;
                    bat1 = Service.GetBattareyData(kd1, kd2, dk, target1, SobInfo);
                    bat2 = Service.GetBattareyData(kd1, kd2, dk, target2, SobInfo);
                    bat3 = Service.GetBattareyData(kd1, kd2, dk, target3, SobInfo);
                    bat4 = Service.GetBattareyData(kd1, kd2, dk, target4, SobInfo);
                    bat5 = Service.GetBattareyData(kd1, kd2, dk, target5, SobInfo);
                    /////////////////////////////////////////////////////////////////////////////////////
                    ClearView();
                    InitializeTarget1Data(target1, bat1, dk, kd1, kd2);
                    if (firsttarget.ID != target2.ID)
                    {
                        InitializeTarget2Data(target2, bat2, dk, kd1, kd2);
                        if (firsttarget.ID != target3.ID)
                        {
                            InitializeTarget3Data(target3, bat3, dk, kd1, kd2);
                            if (firsttarget.ID != target4.ID)
                            {
                                InitializeTarget4Data(target4, bat4, dk, kd1, kd2);
                                if (firsttarget.ID != target5.ID)
                                {
                                    InitializeTarget5Data(target5, bat5, dk, kd1, kd2);
                                }
                            }
                        }
                    }
                }
                else if (SobInfo.TaskType == "ԱԱԿ" || SobInfo.TaskType == "ՇԱԿ")
                {
                    var firsttarget = DataReader.GetAXKTargets().FirstOrDefault();
                    /////////////////////////////////////////////////////////////////////////////////////
                    var target1 = TargetManager.GetAXKTargetByNumber(Data.StartTarget, out _);
                    var target2 = TargetManager.GetNextAXKTarget(target1.ID);
                    var target3 = TargetManager.GetNextAXKTarget(target2.ID);
                    var target4 = TargetManager.GetNextAXKTarget(target3.ID);
                    var target5 = TargetManager.GetNextAXKTarget(target4.ID);
                    /////////////////////////////////////////////////////////////////////////////////////
                    AXKReturn bat1 = new AXKReturn(), bat2 = new AXKReturn(), bat3 = new AXKReturn(), bat4 = new AXKReturn(), bat5 = new AXKReturn();
                    /////////////////////////////////////////////////////////////////////////////////////
                    bat1 = Service.HandleAXKData(dk.Name, dk.Name, SobInfo, new AXKTarget(new Uxankyun()
                    {
                        X1 = target1.Right.X.ToString(),
                        Y1 = target1.Right.Y.ToString(),
                        H1 = target1.Right.H.ToString(),
                        X2 = target1.Left.X.ToString(),
                        Y2 = target1.Left.Y.ToString(),
                        H2 = target1.Left.H.ToString(),
                    }));
                    /////////////////////////////////////////////////////////////////////////////////////
                    bat2 = Service.HandleAXKData(dk.Name, dk.Name, SobInfo, new AXKTarget(new Uxankyun()
                    {
                        X1 = target2.Right.X.ToString(),
                        Y1 = target2.Right.Y.ToString(),
                        H1 = target2.Right.H.ToString(),
                        X2 = target2.Left.X.ToString(),
                        Y2 = target2.Left.Y.ToString(),
                        H2 = target2.Left.H.ToString(),
                    }));
                    /////////////////////////////////////////////////////////////////////////////////////
                    bat3 = Service.HandleAXKData(dk.Name, dk.Name, SobInfo, new AXKTarget(new Uxankyun()
                    {
                        X1 = target3.Right.X.ToString(),
                        Y1 = target3.Right.Y.ToString(),
                        H1 = target3.Right.H.ToString(),
                        X2 = target3.Left.X.ToString(),
                        Y2 = target3.Left.Y.ToString(),
                        H2 = target3.Left.H.ToString(),
                    }));
                    /////////////////////////////////////////////////////////////////////////////////////
                    bat4 = Service.HandleAXKData(dk.Name, dk.Name, SobInfo, new AXKTarget(new Uxankyun()
                    {
                        X1 = target4.Right.X.ToString(),
                        Y1 = target4.Right.Y.ToString(),
                        H1 = target4.Right.H.ToString(),
                        X2 = target4.Left.X.ToString(),
                        Y2 = target4.Left.Y.ToString(),
                        H2 = target4.Left.H.ToString(),
                    }));
                    /////////////////////////////////////////////////////////////////////////////////////
                    bat5 = Service.HandleAXKData(dk.Name, dk.Name, SobInfo, new AXKTarget(new Uxankyun()
                    {
                        X1 = target5.Right.X.ToString(),
                        Y1 = target5.Right.Y.ToString(),
                        H1 = target5.Right.H.ToString(),
                        X2 = target5.Left.X.ToString(),
                        Y2 = target5.Left.Y.ToString(),
                        H2 = target5.Left.H.ToString(),
                    }));
                    /////////////////////////////////////////////////////////////////////////////////////
                    Battarey b1 = new Battarey(), b2 = new Battarey(), b3 = new Battarey(), b4 = new Battarey(), b5 = new Battarey();
                    Target t1 = new Target(), t2 = new Target(), t3 = new Target(), t4 = new Target(), t5 = new Target();
                    if (Data.KD == "1 ՀրՄ Դասակի Կազմով" || Data.KD == "1 ՀրՄ Մարտկոցի Կազմով")
                    {
                        /////////////////////////////////////////////////////////////////////////////////////
                        b1 = bat1.Bat1;
                        t1 = bat1.T1;
                        /////////////////////////////////////////////////////////////////////////////////////
                        b2 = bat2.Bat1;
                        t2 = bat2.T1;
                        /////////////////////////////////////////////////////////////////////////////////////
                        b3 = bat3.Bat1;
                        t3 = bat3.T1;
                        /////////////////////////////////////////////////////////////////////////////////////
                        b4 = bat4.Bat1;
                        t4 = bat4.T1;
                        /////////////////////////////////////////////////////////////////////////////////////
                        b5 = bat5.Bat1;
                        t5 = bat5.T1;
                        /////////////////////////////////////////////////////////////////////////////////////
                    }
                    else if (Data.KD == "2 ՀրՄ Դասակի Կազմով" || Data.KD == "2 ՀրՄ Մարտկոցի Կազմով")
                    {
                        /////////////////////////////////////////////////////////////////////////////////////
                        b1 = bat1.Bat2;
                        t1 = bat1.T2;
                        /////////////////////////////////////////////////////////////////////////////////////
                        b2 = bat2.Bat2;
                        t2 = bat2.T2;
                        /////////////////////////////////////////////////////////////////////////////////////
                        b3 = bat3.Bat2;
                        t3 = bat3.T2;
                        /////////////////////////////////////////////////////////////////////////////////////
                        b4 = bat4.Bat2;
                        t4 = bat4.T2;
                        /////////////////////////////////////////////////////////////////////////////////////
                        b5 = bat5.Bat2;
                        t5 = bat5.T2;
                        /////////////////////////////////////////////////////////////////////////////////////
                    }
                    else if (Data.KD == "3 ՀրՄ Դասակի Կազմով" || Data.KD == "3 ՀրՄ Մարտկոցի Կազմով")
                    {
                        /////////////////////////////////////////////////////////////////////////////////////
                        b1 = bat1.Bat3;
                        t1 = bat1.T3;
                        /////////////////////////////////////////////////////////////////////////////////////
                        b2 = bat2.Bat2;
                        t2 = bat2.T3;
                        /////////////////////////////////////////////////////////////////////////////////////
                        b3 = bat3.Bat3;
                        t3 = bat3.T3;
                        /////////////////////////////////////////////////////////////////////////////////////
                        b4 = bat4.Bat3;
                        t4 = bat4.T3;
                        /////////////////////////////////////////////////////////////////////////////////////
                        b5 = bat5.Bat3;
                        t5 = bat5.T3;
                        /////////////////////////////////////////////////////////////////////////////////////
                    }
                    t1.Front = 200; t2.Front = 200; t3.Front = 200; t4.Front = 200; t5.Front = 200;
                    t1.ID = target1.ID; t2.ID = target2.ID; t3.ID = target3.ID; t4.ID = target4.ID; t5.ID = target5.ID;
                    t1.Description = target1.Description; t2.Description = target2.Description; t3.Description = target3.Description; t4.Description = target4.Description; t5.Description = target5.Description;
                    ClearView();
                    InitializeTarget1Data(t1, b1, dk, kd1, kd2);
                    if (firsttarget.ID != target2.ID)
                    {
                        InitializeTarget2Data(t2, b2, dk, kd1, kd2);
                        if (firsttarget.ID != target3.ID)
                        {
                            InitializeTarget3Data(t3, b3, dk, kd1, kd2);
                            if (firsttarget.ID != target4.ID)
                            {
                                InitializeTarget4Data(t4, b4, dk, kd1, kd2);
                                if (firsttarget.ID != target5.ID)
                                {
                                    InitializeTarget5Data(t5, b5, dk, kd1, kd2);
                                }
                            }
                        }
                    }
                }
            }
            catch
            { }
        }
        private void ClearView()
        {
            //////////////////////////////////////////////Target 1 Data
            T1.Text = ""; T1C1.Text = ""; T1C2.Text = ""; T1C3.Text = ""; T1C4.Text = ""; T1C5.Text = ""; T1C6.Text = ""; T1C7.Text = "";
            T1C8.Text = ""; T1C9.Text = ""; T1C10.Text = ""; T1C11.Text = ""; T1C12.Text = ""; T1C13.Text = ""; T1C14.Text = ""; T1C15_1.Text = "";
            T1C15_2.Text = ""; T1C16_1.Text = ""; T1C16_2.Text = ""; T1C17_1.Text = ""; T1C17_2.Text = ""; T1C18_1.Text = ""; T1C18_2.Text = ""; T1C19.Text = "";
            T1C20.Text = ""; T1C21_1.Text = ""; T1C21_2.Text = ""; T1C22_1.Text = ""; T1C22_2.Text = ""; T1C23_1.Text = "";
            T1C23_2.Text = ""; T1C24_1.Text = ""; T1C24_2.Text = ""; T1C25.Text = ""; T1C26.Text = ""; T1C27_1.Text = ""; T1C27_2.Text = ""; T1C28_1.Text = "";
            T1C28_2.Text = ""; T1C29_1.Text = ""; T1C29_2.Text = ""; T1C30_1.Text = ""; T1C30_2.Text = ""; T1C31_1.Text = ""; T1C31_2.Text = "";
            T1C32_1.Text = ""; T1C32_2.Text = ""; T1C33_1.Text = ""; T1C33_2.Text = ""; T1C34.Text = ""; T1C35.Text = ""; T1C36.Text = ""; T1C37.Text = "";
            T1C38.Text = ""; T1C39.Text = ""; T1C40_1.Text = ""; T1C40_2.Text = ""; T1C41_1.Text = ""; T1C41_2.Text = ""; T1C42_1.Text = ""; T1C42_2.Text = "";
            T1C43_1.Text = ""; T1C43_2.Text = "";
            //////////////////////////////////////////////Target 2 Data
            T2.Text = ""; T2C1.Text = ""; T2C2.Text = ""; T2C3.Text = ""; T2C4.Text = ""; T2C5.Text = ""; T2C6.Text = ""; T2C7.Text = "";
            T2C8.Text = ""; T2C9.Text = ""; T2C10.Text = ""; T2C11.Text = ""; T2C12.Text = ""; T2C13.Text = ""; T2C14.Text = ""; T2C15_1.Text = "";
            T2C15_2.Text = ""; T2C16_1.Text = ""; T2C16_2.Text = ""; T2C17_1.Text = ""; T2C17_2.Text = ""; T2C18_1.Text = ""; T2C18_2.Text = ""; T2C19.Text = "";
            T2C20.Text = ""; T2C21_1.Text = ""; T2C21_2.Text = ""; T2C22_1.Text = ""; T2C22_2.Text = ""; T2C23_1.Text = "";
            T2C23_2.Text = ""; T2C24_1.Text = ""; T2C24_2.Text = ""; T2C25.Text = ""; T2C26.Text = ""; T2C28_1.Text = ""; T2C28_2.Text = ""; T2C29_1.Text = "";
            T2C29_2.Text = ""; T2C30_1.Text = ""; T2C30_2.Text = ""; T2C31_1.Text = ""; T2C31_2.Text = ""; T2C32_1.Text = ""; T2C32_2.Text = ""; T2C33_1.Text = "";
            T2C33_2.Text = ""; T2C34.Text = ""; T2C35.Text = ""; T2C36.Text = ""; T2C37.Text = ""; T2C38.Text = ""; T2C39.Text = ""; T2C40_1.Text = "";
            T2C40_2.Text = ""; T2C41_1.Text = ""; T2C41_2.Text = ""; T2C42_1.Text = ""; T2C42_2.Text = ""; T2C43_1.Text = ""; T2C43_2.Text = "";
            T2C27_1.Text = ""; T2C27_2.Text = "";
            //////////////////////////////////////////////Target 3 Data
            T3.Text = ""; T3C1.Text = ""; T3C2.Text = ""; T3C3.Text = ""; T3C4.Text = ""; T3C5.Text = ""; T3C6.Text = ""; T3C7.Text = "";
            T3C8.Text = ""; T3C9.Text = ""; T3C10.Text = ""; T3C11.Text = ""; T3C12.Text = ""; T3C13.Text = ""; T3C14.Text = ""; T3C15_1.Text = "";
            T3C15_2.Text = ""; T3C16_1.Text = ""; T3C16_2.Text = ""; T3C17_1.Text = ""; T3C17_2.Text = ""; T3C18_1.Text = ""; T3C18_2.Text = ""; T3C19.Text = "";
            T3C20.Text = ""; T3C21_1.Text = ""; T3C21_2.Text = ""; T3C22_1.Text = ""; T3C22_2.Text = ""; T3C23_1.Text = "";
            T3C23_2.Text = ""; T3C24_1.Text = ""; T3C24_2.Text = ""; T3C25.Text = ""; T3C26.Text = ""; T3C28_1.Text = ""; T3C28_2.Text = ""; T3C29_1.Text = "";
            T3C29_2.Text = ""; T3C30_1.Text = ""; T3C30_2.Text = ""; T3C31_1.Text = ""; T3C31_2.Text = ""; T3C32_1.Text = ""; T3C32_2.Text = ""; T3C33_1.Text = "";
            T3C33_2.Text = ""; T3C34.Text = ""; T3C35.Text = ""; T3C36.Text = ""; T3C37.Text = ""; T3C38.Text = ""; T3C39.Text = ""; T3C40_1.Text = "";
            T3C40_2.Text = ""; T3C41_1.Text = ""; T3C41_2.Text = ""; T3C42_1.Text = ""; T3C42_2.Text = ""; T3C43_1.Text = ""; T3C43_2.Text = "";
            T3C27_1.Text = ""; T3C27_2.Text = "";
            //////////////////////////////////////////////Target 4 Data
            T4.Text = ""; T4C1.Text = ""; T4C2.Text = ""; T4C3.Text = ""; T4C4.Text = ""; T4C5.Text = ""; T4C6.Text = ""; T4C7.Text = "";
            T4C8.Text = ""; T4C9.Text = ""; T4C10.Text = ""; T4C11.Text = ""; T4C12.Text = ""; T4C13.Text = ""; T4C14.Text = ""; T4C15_1.Text = "";
            T4C15_2.Text = ""; T4C16_1.Text = ""; T4C16_2.Text = ""; T4C17_1.Text = ""; T4C17_2.Text = ""; T4C18_1.Text = ""; T4C18_2.Text = ""; T4C19.Text = "";
            T4C20.Text = ""; T4C21_1.Text = ""; T4C21_2.Text = ""; T4C22_1.Text = ""; T4C22_2.Text = ""; T4C23_1.Text = "";
            T4C23_2.Text = ""; T4C24_1.Text = ""; T4C24_2.Text = ""; T4C25.Text = ""; T4C26.Text = ""; T4C28_1.Text = ""; T4C28_2.Text = ""; T4C29_1.Text = "";
            T4C29_2.Text = ""; T4C30_1.Text = ""; T4C30_2.Text = ""; T4C31_1.Text = ""; T4C31_2.Text = ""; T4C32_1.Text = ""; T4C32_2.Text = ""; T4C33_1.Text = "";
            T4C33_2.Text = ""; T4C34.Text = ""; T4C35.Text = ""; T4C36.Text = ""; T4C37.Text = ""; T4C38.Text = ""; T4C39.Text = ""; T4C40_1.Text = "";
            T4C40_2.Text = ""; T4C41_1.Text = ""; T4C41_2.Text = ""; T4C42_1.Text = ""; T4C42_2.Text = ""; T4C43_1.Text = ""; T4C43_2.Text = "";
            T4C27_1.Text = ""; T4C27_2.Text = "";
            //////////////////////////////////////////////Target 5 Data
            T5.Text = ""; T5C1.Text = ""; T5C2.Text = ""; T5C3.Text = ""; T5C4.Text = ""; T5C5.Text = ""; T5C6.Text = ""; T5C7.Text = "";
            T5C8.Text = ""; T5C9.Text = ""; T5C10.Text = ""; T5C11.Text = ""; T5C12.Text = ""; T5C13.Text = ""; T5C14.Text = ""; T5C15_1.Text = "";
            T5C15_2.Text = ""; T5C16_1.Text = ""; T5C16_2.Text = ""; T5C17_1.Text = ""; T5C17_2.Text = ""; T5C18_1.Text = ""; T5C18_2.Text = ""; T5C19.Text = "";
            T5C20.Text = ""; T5C21_1.Text = ""; T5C21_2.Text = ""; T5C22_1.Text = ""; T5C22_2.Text = ""; T5C23_1.Text = "";
            T5C23_2.Text = ""; T5C24_1.Text = ""; T5C24_2.Text = ""; T5C25.Text = ""; T5C26.Text = ""; T5C28_1.Text = ""; T5C28_2.Text = ""; T5C29_1.Text = "";
            T5C29_2.Text = ""; T5C30_1.Text = ""; T5C30_2.Text = ""; T5C31_1.Text = ""; T5C31_2.Text = ""; T5C32_1.Text = ""; T5C32_2.Text = ""; T5C33_1.Text = "";
            T5C33_2.Text = ""; T5C34.Text = ""; T5C35.Text = ""; T5C36.Text = ""; T5C37.Text = ""; T5C38.Text = ""; T5C39.Text = ""; T5C40_1.Text = "";
            T5C40_2.Text = ""; T5C41_1.Text = ""; T5C41_2.Text = ""; T5C42_1.Text = ""; T5C42_2.Text = ""; T5C43_1.Text = ""; T5C43_2.Text = "";
            T5C27_1.Text = ""; T5C27_2.Text = "";
            /////////////////////////////////////////////////////////////////////////////////////
        }

        private void InitializeTarget1Data(Target t, Battarey bat, Ditaket dk, KrakayinDirq kd1, KrakayinDirq kd2)
        {
            var tex = Service.GetTexagrakan(dk, t);
            T1.Text = t.ID.ToString();
            T1C1.Text = t.Description.ToString();
            T1C2.Text = t.X.ToString();
            T1C3.Text = t.Y.ToString();
            T1C4.Text = t.H.ToString();
            T1C5.Text = t.Front.ToString();
            T1C6.Text = t.Deepness.ToString();
            if (SobInfo.Paytucich == "Т-90 (3С4)" || SobInfo.Paytucich == "Т-7 (С-463)")
            { T1C9.Text = "Լուսային"; }
            else { T1C9.Text = "ԲՖ"; }
            T1C10.Text = SobInfo.Paytucich.Split(' ')[0];
            T1C11.Text = bat.Core;
            T1C15_1.Text = Math.Round(bat.First.Distance).ToString();
            T1C15_2.Text = Math.Round(bat.Second.Distance).ToString();
            T1C18_1.Text = (t.H - kd1.H).ToString();
            T1C18_2.Text = (t.H - kd2.H).ToString();
            T1C19.Text = Helper.FormatAngle(tex.Alpha);
            T1C20.Text = Math.Round(tex.Distance).ToString();
            T1C25.Text = Helper.FormatAngle(bat.KMH);
            T1C26.Text = bat.punj == -10 ? "Թևային" : Helper.FormatAngle(bat.punj / 100);
            T1C8.Text = bat.Catk == -10 ? "Ճակատային" : bat.Catk != 0 && SobInfo.Paytucich.Contains("РГМ") ? bat.Catk.ToString() : "";
            T1C27_1.Text = Helper.FormatAngleWithSigne(bat.First.Davarot).ToString();
            T1C27_2.Text = Helper.FormatAngleWithSigne(bat.Second.Davarot).ToString();
            T1C30_1.Text = bat.First.Pricel.ToString();
            T1C30_2.Text = bat.Second.Pricel.ToString();
            if (!SobInfo.Paytucich.Contains("РГМ-2"))
            {
                T1C31_1.Text = Math.Round(bat.First.Paytucich, 1).ToString();
                T1C31_2.Text = Math.Round(bat.Second.Paytucich, 1).ToString();
            }
            T1C32_1.Text = Helper.FormatAngle(bat.First.Level);
            T1C32_2.Text = Helper.FormatAngle(bat.Second.Level);
            T1C33_1.Text = Helper.FormatAngleWithSigne(bat.First.DavarotHU).ToString();
            T1C33_2.Text = Helper.FormatAngleWithSigne(bat.Second.DavarotHU).ToString();
            T1C34.Text = bat.KU != 0 ? bat.KU.ToString() : "";
            T1C35.Text = bat.SHU != 0 ? Helper.FormatAngle(bat.SHU) : "";
            T1C36.Text = bat.DeltaX.ToString();
            if (!SobInfo.Paytucich.Contains("РГМ-2"))
            {
                T1C37.Text = bat.DeltaN.ToString();
            }
            T1C38.Text = Helper.FormatAngle(Math.Round(bat.PS, 2));
            T1C39.Text = bat.KDDirection;
            if (bat.Core == "###")
            {
                T1C8.Text = "";
                T1C9.Text = "";
                T1C10.Text = "";
                T1C18_1.Text = "";
                T1C18_2.Text = "";
                T1C25.Text = "";
                T1C26.Text = "";
                T1C30_1.Text = "";
                T1C30_2.Text = "";
                T1C31_1.Text = "";
                T1C31_2.Text = "";
                T1C32_1.Text = "";
                T1C32_2.Text = "";
                T1C33_1.Text = "";
                T1C33_2.Text = "";
                T1C34.Text = "";
                T1C35.Text = "";
                T1C36.Text = "";
                T1C37.Text = "";
                T1C38.Text = "";
                T1C39.Text = "";
            }
            if (Data.KD.Contains("Մարտկոցի Կազմով"))
            {
                /////////////////////////////////////////////////////////////////////////////////////
                T1C15_1.Style = FullStyleExample.Style;
                T1C16_1.Style = FullStyleExample.Style;
                T1C17_1.Style = FullStyleExample.Style;
                T1C18_1.Style = FullStyleExample.Style;
                T1C21_1.Style = FullStyleExample.Style;
                T1C22_1.Style = FullStyleExample.Style;
                T1C23_1.Style = FullStyleExample.Style;
                T1C24_1.Style = FullStyleExample.Style;
                T1C27_1.Style = FullStyleExample.Style;
                T1C28_1.Style = FullStyleExample.Style;
                T1C29_1.Style = FullStyleExample.Style;
                T1C30_1.Style = FullStyleExample.Style;
                T1C31_1.Style = FullStyleExample.Style;
                T1C32_1.Style = FullStyleExample.Style;
                T1C33_1.Style = FullStyleExample.Style;
                /////////////////////////////////////////////////////////////////////////////////////
                T1C15_2.Visibility = Visibility.Collapsed;
                T1C16_2.Visibility = Visibility.Collapsed;
                T1C17_2.Visibility = Visibility.Collapsed;
                T1C18_2.Visibility = Visibility.Collapsed;
                T1C21_2.Visibility = Visibility.Collapsed;
                T1C22_2.Visibility = Visibility.Collapsed;
                T1C23_2.Visibility = Visibility.Collapsed;
                T1C24_2.Visibility = Visibility.Collapsed;
                T1C27_2.Visibility = Visibility.Collapsed;
                T1C28_2.Visibility = Visibility.Collapsed;
                T1C29_2.Visibility = Visibility.Collapsed;
                T1C30_2.Visibility = Visibility.Collapsed;
                T1C31_2.Visibility = Visibility.Collapsed;
                T1C32_2.Visibility = Visibility.Collapsed;
                T1C33_2.Visibility = Visibility.Collapsed;
                /////////////////////////////////////////////////////////////////////////////////////
            }
            else
            {
                /////////////////////////////////////////////////////////////////////////////////////
                T1C15_1.Style = HalfStyleExample.Style;
                T1C16_1.Style = HalfStyleExample.Style;
                T1C17_1.Style = HalfStyleExample.Style;
                T1C18_1.Style = HalfStyleExample.Style;
                T1C21_1.Style = HalfStyleExample.Style;
                T1C22_1.Style = HalfStyleExample.Style;
                T1C23_1.Style = HalfStyleExample.Style;
                T1C24_1.Style = HalfStyleExample.Style;
                T1C27_1.Style = HalfStyleExample.Style;
                T1C28_1.Style = HalfStyleExample.Style;
                T1C29_1.Style = HalfStyleExample.Style;
                T1C30_1.Style = HalfStyleExample.Style;
                T1C31_1.Style = HalfStyleExample.Style;
                T1C32_1.Style = HalfStyleExample.Style;
                T1C33_1.Style = HalfStyleExample.Style;
                /////////////////////////////////////////////////////////////////////////////////////
                T1C15_2.Visibility = Visibility.Visible;
                T1C16_2.Visibility = Visibility.Visible;
                T1C17_2.Visibility = Visibility.Visible;
                T1C18_2.Visibility = Visibility.Visible;
                T1C21_2.Visibility = Visibility.Visible;
                T1C22_2.Visibility = Visibility.Visible;
                T1C23_2.Visibility = Visibility.Visible;
                T1C24_2.Visibility = Visibility.Visible;
                T1C27_2.Visibility = Visibility.Visible;
                T1C28_2.Visibility = Visibility.Visible;
                T1C29_2.Visibility = Visibility.Visible;
                T1C30_2.Visibility = Visibility.Visible;
                T1C31_2.Visibility = Visibility.Visible;
                T1C32_2.Visibility = Visibility.Visible;
                T1C33_2.Visibility = Visibility.Visible;
                /////////////////////////////////////////////////////////////////////////////////////
            }

        }

        private void InitializeTarget2Data(Target t, Battarey bat, Ditaket dk, KrakayinDirq kd1, KrakayinDirq kd2)
        {
            var tex = Service.GetTexagrakan(dk, t);
            T2.Text = t.ID.ToString();
            T2C1.Text = t.Description.ToString();
            T2C2.Text = t.X.ToString();
            T2C3.Text = t.Y.ToString();
            T2C4.Text = t.H.ToString();
            T2C5.Text = t.Front.ToString();
            T2C6.Text = t.Deepness.ToString();
            if (SobInfo.Paytucich == "Т-90 (3С4)" || SobInfo.Paytucich == "Т-7 (С-463)")
            { T2C9.Text = "Լուսային"; }
            else { T2C9.Text = "ԲՖ"; }
            T2C10.Text = SobInfo.Paytucich.Split(' ')[0];
            T2C11.Text = bat.Core;
            T2C15_1.Text = Math.Round(bat.First.Distance).ToString();
            T2C15_2.Text = Math.Round(bat.Second.Distance).ToString();
            T2C18_1.Text = (t.H - kd1.H).ToString();
            T2C18_2.Text = (t.H - kd2.H).ToString();
            T2C19.Text = Helper.FormatAngle(tex.Alpha);
            T2C20.Text = Math.Round(tex.Distance).ToString();
            T2C25.Text = Helper.FormatAngle(bat.KMH);
            T2C26.Text = bat.punj == -10 ? "Թևային" : Helper.FormatAngle(bat.punj / 100);
            T2C8.Text = bat.Catk == -10 ? "Ճակատային" : bat.Catk != 0 && SobInfo.Paytucich.Contains("РГМ") ? bat.Catk.ToString() : "";
            T2C27_1.Text = Helper.FormatAngleWithSigne(bat.First.Davarot).ToString();
            T2C27_2.Text = Helper.FormatAngleWithSigne(bat.Second.Davarot).ToString();
            T2C30_1.Text = bat.First.Pricel.ToString();
            T2C30_2.Text = bat.Second.Pricel.ToString();
            if (!SobInfo.Paytucich.Contains("РГМ-2"))
            {
                T2C31_1.Text = Math.Round(bat.First.Paytucich, 1).ToString();
                T2C31_2.Text = Math.Round(bat.Second.Paytucich, 1).ToString();
            }
            T2C32_1.Text = Helper.FormatAngle(bat.First.Level);
            T2C32_2.Text = Helper.FormatAngle(bat.Second.Level);
            T2C33_1.Text = Helper.FormatAngleWithSigne(bat.First.DavarotHU).ToString();
            T2C33_2.Text = Helper.FormatAngleWithSigne(bat.Second.DavarotHU).ToString();
            T2C34.Text = bat.KU != 0 ? bat.KU.ToString() : "";
            T2C35.Text = bat.SHU != 0 ? Helper.FormatAngle(bat.SHU) : "";
            T2C36.Text = bat.DeltaX.ToString();
            if (!SobInfo.Paytucich.Contains("РГМ-2"))
            {
                T2C37.Text = bat.DeltaN.ToString();
            }
            T2C38.Text = Helper.FormatAngle(Math.Round(bat.PS, 2)).ToString();
            T2C39.Text = bat.KDDirection;
            if (bat.Core == "###")
            {
                T2C8.Text = "";
                T2C9.Text = "";
                T2C10.Text = "";
                T2C18_1.Text = "";
                T2C18_2.Text = "";
                T2C25.Text = "";
                T2C26.Text = "";
                T2C30_1.Text = "";
                T2C30_2.Text = "";
                T2C31_1.Text = "";
                T2C31_2.Text = "";
                T2C32_1.Text = "";
                T2C32_2.Text = "";
                T2C33_1.Text = "";
                T2C33_2.Text = "";
                T2C34.Text = "";
                T2C35.Text = "";
                T2C36.Text = "";
                T2C37.Text = "";
                T2C38.Text = "";
                T2C39.Text = "";
            }

            if (Data.KD.Contains("Մարտկոցի Կազմով"))
            {
                /////////////////////////////////////////////////////////////////////////////////////
                T2C15_1.Style = FullStyleExample.Style;
                T2C16_1.Style = FullStyleExample.Style;
                T2C17_1.Style = FullStyleExample.Style;
                T2C18_1.Style = FullStyleExample.Style;
                T2C21_1.Style = FullStyleExample.Style;
                T2C22_1.Style = FullStyleExample.Style;
                T2C23_1.Style = FullStyleExample.Style;
                T2C24_1.Style = FullStyleExample.Style;
                T2C27_1.Style = FullStyleExample.Style;
                T2C28_1.Style = FullStyleExample.Style;
                T2C29_1.Style = FullStyleExample.Style;
                T2C30_1.Style = FullStyleExample.Style;
                T2C31_1.Style = FullStyleExample.Style;
                T2C32_1.Style = FullStyleExample.Style;
                T2C33_1.Style = FullStyleExample.Style;
                /////////////////////////////////////////////////////////////////////////////////////
                T2C15_2.Visibility = Visibility.Collapsed;
                T2C16_2.Visibility = Visibility.Collapsed;
                T2C17_2.Visibility = Visibility.Collapsed;
                T2C18_2.Visibility = Visibility.Collapsed;
                T2C21_2.Visibility = Visibility.Collapsed;
                T2C22_2.Visibility = Visibility.Collapsed;
                T2C23_2.Visibility = Visibility.Collapsed;
                T2C24_2.Visibility = Visibility.Collapsed;
                T2C27_2.Visibility = Visibility.Collapsed;
                T2C28_2.Visibility = Visibility.Collapsed;
                T2C29_2.Visibility = Visibility.Collapsed;
                T2C30_2.Visibility = Visibility.Collapsed;
                T2C31_2.Visibility = Visibility.Collapsed;
                T2C32_2.Visibility = Visibility.Collapsed;
                T2C33_2.Visibility = Visibility.Collapsed;
                /////////////////////////////////////////////////////////////////////////////////////
            }
            else
            {
                /////////////////////////////////////////////////////////////////////////////////////
                T2C15_1.Style = HalfStyleExample.Style;
                T2C16_1.Style = HalfStyleExample.Style;
                T2C17_1.Style = HalfStyleExample.Style;
                T2C18_1.Style = HalfStyleExample.Style;
                T2C21_1.Style = HalfStyleExample.Style;
                T2C22_1.Style = HalfStyleExample.Style;
                T2C23_1.Style = HalfStyleExample.Style;
                T2C24_1.Style = HalfStyleExample.Style;
                T2C27_1.Style = HalfStyleExample.Style;
                T2C28_1.Style = HalfStyleExample.Style;
                T2C29_1.Style = HalfStyleExample.Style;
                T2C30_1.Style = HalfStyleExample.Style;
                T2C31_1.Style = HalfStyleExample.Style;
                T2C32_1.Style = HalfStyleExample.Style;
                T2C33_1.Style = HalfStyleExample.Style;
                /////////////////////////////////////////////////////////////////////////////////////
                T2C15_2.Visibility = Visibility.Visible;
                T2C16_2.Visibility = Visibility.Visible;
                T2C17_2.Visibility = Visibility.Visible;
                T2C18_2.Visibility = Visibility.Visible;
                T2C21_2.Visibility = Visibility.Visible;
                T2C22_2.Visibility = Visibility.Visible;
                T2C23_2.Visibility = Visibility.Visible;
                T2C24_2.Visibility = Visibility.Visible;
                T2C27_2.Visibility = Visibility.Visible;
                T2C28_2.Visibility = Visibility.Visible;
                T2C29_2.Visibility = Visibility.Visible;
                T2C30_2.Visibility = Visibility.Visible;
                T2C31_2.Visibility = Visibility.Visible;
                T2C32_2.Visibility = Visibility.Visible;
                T2C33_2.Visibility = Visibility.Visible;
                /////////////////////////////////////////////////////////////////////////////////////
            }

        }

        private void InitializeTarget3Data(Target t, Battarey bat, Ditaket dk, KrakayinDirq kd1, KrakayinDirq kd2)
        {
            var tex = Service.GetTexagrakan(dk, t);
            T3.Text = t.ID.ToString();
            T3C1.Text = t.Description.ToString();
            T3C2.Text = t.X.ToString();
            T3C3.Text = t.Y.ToString();
            T3C4.Text = t.H.ToString();
            T3C5.Text = t.Front.ToString();
            T3C6.Text = t.Deepness.ToString();
            if (SobInfo.Paytucich == "Т-90 (3С4)" || SobInfo.Paytucich == "Т-7 (С-463)")
            { T3C9.Text = "Լուսային"; }
            else { T3C9.Text = "ԲՖ"; }
            T3C10.Text = SobInfo.Paytucich.Split(' ')[0];
            T3C11.Text = bat.Core;
            T3C15_1.Text = Math.Round(bat.First.Distance).ToString();
            T3C15_2.Text = Math.Round(bat.Second.Distance).ToString();
            T3C18_1.Text = (t.H - kd1.H).ToString();
            T3C18_2.Text = (t.H - kd2.H).ToString();
            T3C19.Text = Helper.FormatAngle(tex.Alpha);
            T3C20.Text = Math.Round(tex.Distance).ToString();
            T3C25.Text = Helper.FormatAngle(bat.KMH);
            T3C26.Text = bat.punj == -10 ? "Թևային" : Helper.FormatAngle(bat.punj / 100);
            T3C8.Text = bat.Catk == -10 ? "Ճակատային" : bat.Catk != 0 && SobInfo.Paytucich.Contains("РГМ") ? bat.Catk.ToString() : "";
            T3C27_1.Text = Helper.FormatAngleWithSigne(bat.First.Davarot).ToString();
            T3C27_2.Text = Helper.FormatAngleWithSigne(bat.Second.Davarot).ToString();
            T3C30_1.Text = bat.First.Pricel.ToString();
            T3C30_2.Text = bat.Second.Pricel.ToString();
            if (!SobInfo.Paytucich.Contains("РГМ-2"))
            {
                T3C31_1.Text = Math.Round(bat.First.Paytucich, 1).ToString();
                T3C31_2.Text = Math.Round(bat.Second.Paytucich, 1).ToString();
            }
            T3C32_1.Text = Helper.FormatAngle(bat.First.Level);
            T3C32_2.Text = Helper.FormatAngle(bat.Second.Level);
            T3C33_1.Text = Helper.FormatAngleWithSigne(bat.First.DavarotHU).ToString();
            T3C33_2.Text = Helper.FormatAngleWithSigne(bat.Second.DavarotHU).ToString();
            T3C34.Text = bat.KU != 0 ? bat.KU.ToString() : "";
            T3C35.Text = bat.SHU != 0 ? Helper.FormatAngle(bat.SHU) : "";
            T3C36.Text = bat.DeltaX.ToString();
            if (!SobInfo.Paytucich.Contains("РГМ-2"))
            {
                T3C37.Text = bat.DeltaN.ToString();
            }
            T3C38.Text = Helper.FormatAngle(Math.Round(bat.PS, 2)).ToString();
            T3C39.Text = bat.KDDirection;
            if (bat.Core == "###")
            {
                T3C8.Text = "";
                T3C9.Text = "";
                T3C10.Text = "";
                T3C18_1.Text = "";
                T3C18_2.Text = "";
                T3C25.Text = "";
                T3C26.Text = "";
                T3C30_1.Text = "";
                T3C30_2.Text = "";
                T3C31_1.Text = "";
                T3C31_2.Text = "";
                T3C32_1.Text = "";
                T3C32_2.Text = "";
                T3C33_1.Text = "";
                T3C33_2.Text = "";
                T3C34.Text = "";
                T3C35.Text = "";
                T3C36.Text = "";
                T3C37.Text = "";
                T3C38.Text = "";
                T3C39.Text = "";
            }
            if (Data.KD.Contains("Մարտկոցի Կազմով"))
            {
                /////////////////////////////////////////////////////////////////////////////////////
                T3C15_1.Style = FullStyleExample.Style;
                T3C16_1.Style = FullStyleExample.Style;
                T3C17_1.Style = FullStyleExample.Style;
                T3C18_1.Style = FullStyleExample.Style;
                T3C21_1.Style = FullStyleExample.Style;
                T3C22_1.Style = FullStyleExample.Style;
                T3C23_1.Style = FullStyleExample.Style;
                T3C24_1.Style = FullStyleExample.Style;
                T3C27_1.Style = FullStyleExample.Style;
                T3C28_1.Style = FullStyleExample.Style;
                T3C29_1.Style = FullStyleExample.Style;
                T3C30_1.Style = FullStyleExample.Style;
                T3C31_1.Style = FullStyleExample.Style;
                T3C32_1.Style = FullStyleExample.Style;
                T3C33_1.Style = FullStyleExample.Style;
                /////////////////////////////////////////////////////////////////////////////////////
                T3C15_2.Visibility = Visibility.Collapsed;
                T3C16_2.Visibility = Visibility.Collapsed;
                T3C17_2.Visibility = Visibility.Collapsed;
                T3C18_2.Visibility = Visibility.Collapsed;
                T3C21_2.Visibility = Visibility.Collapsed;
                T3C22_2.Visibility = Visibility.Collapsed;
                T3C23_2.Visibility = Visibility.Collapsed;
                T3C24_2.Visibility = Visibility.Collapsed;
                T3C27_2.Visibility = Visibility.Collapsed;
                T3C28_2.Visibility = Visibility.Collapsed;
                T3C29_2.Visibility = Visibility.Collapsed;
                T3C30_2.Visibility = Visibility.Collapsed;
                T3C31_2.Visibility = Visibility.Collapsed;
                T3C32_2.Visibility = Visibility.Collapsed;
                T3C33_2.Visibility = Visibility.Collapsed;
                /////////////////////////////////////////////////////////////////////////////////////
            }
            else
            {
                /////////////////////////////////////////////////////////////////////////////////////
                T3C15_1.Style = HalfStyleExample.Style;
                T3C16_1.Style = HalfStyleExample.Style;
                T3C17_1.Style = HalfStyleExample.Style;
                T3C18_1.Style = HalfStyleExample.Style;
                T3C21_1.Style = HalfStyleExample.Style;
                T3C22_1.Style = HalfStyleExample.Style;
                T3C23_1.Style = HalfStyleExample.Style;
                T3C24_1.Style = HalfStyleExample.Style;
                T3C27_1.Style = HalfStyleExample.Style;
                T3C28_1.Style = HalfStyleExample.Style;
                T3C29_1.Style = HalfStyleExample.Style;
                T3C30_1.Style = HalfStyleExample.Style;
                T3C31_1.Style = HalfStyleExample.Style;
                T3C32_1.Style = HalfStyleExample.Style;
                T3C33_1.Style = HalfStyleExample.Style;
                /////////////////////////////////////////////////////////////////////////////////////
                T3C15_2.Visibility = Visibility.Visible;
                T3C16_2.Visibility = Visibility.Visible;
                T3C17_2.Visibility = Visibility.Visible;
                T3C18_2.Visibility = Visibility.Visible;
                T3C21_2.Visibility = Visibility.Visible;
                T3C22_2.Visibility = Visibility.Visible;
                T3C23_2.Visibility = Visibility.Visible;
                T3C24_2.Visibility = Visibility.Visible;
                T3C27_2.Visibility = Visibility.Visible;
                T3C28_2.Visibility = Visibility.Visible;
                T3C29_2.Visibility = Visibility.Visible;
                T3C30_2.Visibility = Visibility.Visible;
                T3C31_2.Visibility = Visibility.Visible;
                T3C32_2.Visibility = Visibility.Visible;
                T3C33_2.Visibility = Visibility.Visible;
                /////////////////////////////////////////////////////////////////////////////////////
            }
        }

        private void InitializeTarget4Data(Target t, Battarey bat, Ditaket dk, KrakayinDirq kd1, KrakayinDirq kd2)
        {
            var tex = Service.GetTexagrakan(dk, t);
            T4.Text = t.ID.ToString();
            T4C1.Text = t.Description.ToString();
            T4C2.Text = t.X.ToString();
            T4C3.Text = t.Y.ToString();
            T4C4.Text = t.H.ToString();
            T4C5.Text = t.Front.ToString();
            T4C6.Text = t.Deepness.ToString();
            if (SobInfo.Paytucich == "Т-90 (3С4)" || SobInfo.Paytucich == "Т-7 (С-463)")
            { T4C9.Text = "Լուսային"; }
            else { T4C9.Text = "ԲՖ"; }
            T4C10.Text = SobInfo.Paytucich.Split(' ')[0];
            T4C11.Text = bat.Core;
            T4C15_1.Text = Math.Round(bat.First.Distance).ToString();
            T4C15_2.Text = Math.Round(bat.Second.Distance).ToString();
            T4C18_1.Text = (t.H - kd1.H).ToString();
            T4C18_2.Text = (t.H - kd2.H).ToString();
            T4C19.Text = Helper.FormatAngle(tex.Alpha);
            T4C20.Text = Math.Round(tex.Distance).ToString();
            T4C25.Text = Helper.FormatAngle(bat.KMH);
            T4C26.Text = bat.punj == -10 ? "Թևային" : Helper.FormatAngle(bat.punj / 100);
            T4C8.Text = bat.Catk == -10 ? "Ճակատային" : bat.Catk != 0 && SobInfo.Paytucich.Contains("РГМ") ? bat.Catk.ToString() : "";
            T4C27_1.Text = Helper.FormatAngleWithSigne(bat.First.Davarot).ToString();
            T4C27_2.Text = Helper.FormatAngleWithSigne(bat.Second.Davarot).ToString();
            T4C30_1.Text = bat.First.Pricel.ToString();
            T4C30_2.Text = bat.Second.Pricel.ToString();
            if (!SobInfo.Paytucich.Contains("РГМ-2"))
            {
                T4C31_1.Text = Math.Round(bat.First.Paytucich, 1).ToString();
                T4C31_2.Text = Math.Round(bat.Second.Paytucich, 1).ToString();
            }
            T4C32_1.Text = Helper.FormatAngle(bat.First.Level);
            T4C32_2.Text = Helper.FormatAngle(bat.Second.Level);
            T4C33_1.Text = Helper.FormatAngleWithSigne(bat.First.DavarotHU).ToString();
            T4C33_2.Text = Helper.FormatAngleWithSigne(bat.Second.DavarotHU).ToString();
            T4C34.Text = bat.KU != 0 ? bat.KU.ToString() : "";
            T4C35.Text = bat.SHU != 0 ? Helper.FormatAngle(bat.SHU) : "";
            T4C36.Text = bat.DeltaX.ToString();
            if (!SobInfo.Paytucich.Contains("РГМ-2"))
            {
                T4C37.Text = bat.DeltaN.ToString();
            }
            T4C38.Text = Helper.FormatAngle(Math.Round(bat.PS, 2)).ToString();
            T4C39.Text = bat.KDDirection;
            if (bat.Core == "###")
            {
                T4C8.Text = "";
                T4C9.Text = "";
                T4C10.Text = "";
                T4C18_1.Text = "";
                T4C18_2.Text = "";
                T4C25.Text = "";
                T4C26.Text = "";
                T4C30_1.Text = "";
                T4C30_2.Text = "";
                T4C31_1.Text = "";
                T4C31_2.Text = "";
                T4C32_1.Text = "";
                T4C32_2.Text = "";
                T4C33_1.Text = "";
                T4C33_2.Text = "";
                T4C34.Text = "";
                T4C35.Text = "";
                T4C36.Text = "";
                T4C37.Text = "";
                T4C38.Text = "";
                T4C39.Text = "";
            }
            if (Data.KD.Contains("Մարտկոցի Կազմով"))
            {
                /////////////////////////////////////////////////////////////////////////////////////
                T4C15_1.Style = FullStyleExample.Style;
                T4C16_1.Style = FullStyleExample.Style;
                T4C17_1.Style = FullStyleExample.Style;
                T4C18_1.Style = FullStyleExample.Style;
                T4C21_1.Style = FullStyleExample.Style;
                T4C22_1.Style = FullStyleExample.Style;
                T4C23_1.Style = FullStyleExample.Style;
                T4C24_1.Style = FullStyleExample.Style;
                T4C27_1.Style = FullStyleExample.Style;
                T4C28_1.Style = FullStyleExample.Style;
                T4C29_1.Style = FullStyleExample.Style;
                T4C30_1.Style = FullStyleExample.Style;
                T4C31_1.Style = FullStyleExample.Style;
                T4C32_1.Style = FullStyleExample.Style;
                T4C33_1.Style = FullStyleExample.Style;
                /////////////////////////////////////////////////////////////////////////////////////
                T4C15_2.Visibility = Visibility.Collapsed;
                T4C16_2.Visibility = Visibility.Collapsed;
                T4C17_2.Visibility = Visibility.Collapsed;
                T4C18_2.Visibility = Visibility.Collapsed;
                T4C21_2.Visibility = Visibility.Collapsed;
                T4C22_2.Visibility = Visibility.Collapsed;
                T4C23_2.Visibility = Visibility.Collapsed;
                T4C24_2.Visibility = Visibility.Collapsed;
                T4C27_2.Visibility = Visibility.Collapsed;
                T4C28_2.Visibility = Visibility.Collapsed;
                T4C29_2.Visibility = Visibility.Collapsed;
                T4C30_2.Visibility = Visibility.Collapsed;
                T4C31_2.Visibility = Visibility.Collapsed;
                T4C32_2.Visibility = Visibility.Collapsed;
                T4C33_2.Visibility = Visibility.Collapsed;
                /////////////////////////////////////////////////////////////////////////////////////
            }
            else
            {
                /////////////////////////////////////////////////////////////////////////////////////
                T4C15_1.Style = HalfStyleExample.Style;
                T4C16_1.Style = HalfStyleExample.Style;
                T4C17_1.Style = HalfStyleExample.Style;
                T4C18_1.Style = HalfStyleExample.Style;
                T4C21_1.Style = HalfStyleExample.Style;
                T4C22_1.Style = HalfStyleExample.Style;
                T4C23_1.Style = HalfStyleExample.Style;
                T4C24_1.Style = HalfStyleExample.Style;
                T4C27_1.Style = HalfStyleExample.Style;
                T4C28_1.Style = HalfStyleExample.Style;
                T4C29_1.Style = HalfStyleExample.Style;
                T4C30_1.Style = HalfStyleExample.Style;
                T4C31_1.Style = HalfStyleExample.Style;
                T4C32_1.Style = HalfStyleExample.Style;
                T4C33_1.Style = HalfStyleExample.Style;
                /////////////////////////////////////////////////////////////////////////////////////
                T4C15_2.Visibility = Visibility.Visible;
                T4C16_2.Visibility = Visibility.Visible;
                T4C17_2.Visibility = Visibility.Visible;
                T4C18_2.Visibility = Visibility.Visible;
                T4C21_2.Visibility = Visibility.Visible;
                T4C22_2.Visibility = Visibility.Visible;
                T4C23_2.Visibility = Visibility.Visible;
                T4C24_2.Visibility = Visibility.Visible;
                T4C27_2.Visibility = Visibility.Visible;
                T4C28_2.Visibility = Visibility.Visible;
                T4C29_2.Visibility = Visibility.Visible;
                T4C30_2.Visibility = Visibility.Visible;
                T4C31_2.Visibility = Visibility.Visible;
                T4C32_2.Visibility = Visibility.Visible;
                T4C33_2.Visibility = Visibility.Visible;
                /////////////////////////////////////////////////////////////////////////////////////
            }
        }

        private void InitializeTarget5Data(Target t, Battarey bat, Ditaket dk, KrakayinDirq kd1, KrakayinDirq kd2)
        {
            var tex = Service.GetTexagrakan(dk, t);
            T5.Text = t.ID.ToString();
            T5C1.Text = t.Description.ToString();
            T5C2.Text = t.X.ToString();
            T5C3.Text = t.Y.ToString();
            T5C4.Text = t.H.ToString();
            T5C5.Text = t.Front.ToString();
            T5C6.Text = t.Deepness.ToString();
            if (SobInfo.Paytucich == "Т-90 (3С4)" || SobInfo.Paytucich == "Т-7 (С-463)")
            { T5C9.Text = "Լուսային"; }
            else { T5C9.Text = "ԲՖ"; }
            T5C10.Text = SobInfo.Paytucich.Split(' ')[0];
            T5C11.Text = bat.Core;
            T5C15_1.Text = Math.Round(bat.First.Distance).ToString();
            T5C15_2.Text = Math.Round(bat.Second.Distance).ToString();
            T5C18_1.Text = (t.H - kd1.H).ToString();
            T5C18_2.Text = (t.H - kd2.H).ToString();
            T5C19.Text = Helper.FormatAngle(tex.Alpha);
            T5C20.Text = Math.Round(tex.Distance).ToString();
            T5C25.Text = Helper.FormatAngle(bat.KMH);
            T5C26.Text = bat.punj == -10 ? "Թևային" : Helper.FormatAngle(bat.punj / 100);
            T5C8.Text = bat.Catk == -10 ? "Ճակատային" : bat.Catk != 0 && SobInfo.Paytucich.Contains("РГМ") ? bat.Catk.ToString() : "";
            T5C27_1.Text = Helper.FormatAngleWithSigne(bat.First.Davarot).ToString();
            T5C27_2.Text = Helper.FormatAngleWithSigne(bat.Second.Davarot).ToString();
            T5C30_1.Text = bat.First.Pricel.ToString();
            T5C30_2.Text = bat.Second.Pricel.ToString();
            if (!SobInfo.Paytucich.Contains("РГМ-2"))
            {
                T5C31_1.Text = Math.Round(bat.First.Paytucich, 1).ToString();
                T5C31_2.Text = Math.Round(bat.Second.Paytucich, 1).ToString();
            }
            T5C32_1.Text = Helper.FormatAngle(bat.First.Level);
            T5C32_2.Text = Helper.FormatAngle(bat.Second.Level);
            T5C33_1.Text = Helper.FormatAngleWithSigne(bat.First.DavarotHU).ToString();
            T5C33_2.Text = Helper.FormatAngleWithSigne(bat.Second.DavarotHU).ToString();
            T5C34.Text = bat.KU != 0 ? bat.KU.ToString() : "";
            T5C35.Text = bat.SHU != 0 ? Helper.FormatAngle(bat.SHU) : "";
            T5C36.Text = bat.DeltaX.ToString();
            if (!SobInfo.Paytucich.Contains("РГМ-2"))
            {
                T5C37.Text = bat.DeltaN.ToString();
            }
            T5C38.Text = Helper.FormatAngle(Math.Round(bat.PS, 2)).ToString();
            T5C39.Text = bat.KDDirection;
            if (bat.Core == "###")
            {
                T5C8.Text = "";
                T5C9.Text = "";
                T5C10.Text = "";
                T5C18_1.Text = "";
                T5C18_2.Text = "";
                T5C25.Text = "";
                T5C26.Text = "";
                T5C30_1.Text = "";
                T5C30_2.Text = "";
                T5C31_1.Text = "";
                T5C31_2.Text = "";
                T5C32_1.Text = "";
                T5C32_2.Text = "";
                T5C33_1.Text = "";
                T5C33_2.Text = "";
                T5C34.Text = "";
                T5C35.Text = "";
                T5C36.Text = "";
                T5C37.Text = "";
                T5C38.Text = "";
                T5C39.Text = "";
            }

            if (Data.KD.Contains("Մարտկոցի Կազմով"))
            {
                /////////////////////////////////////////////////////////////////////////////////////
                T5C15_1.Style = FullStyleExample.Style;
                T5C16_1.Style = FullStyleExample.Style;
                T5C17_1.Style = FullStyleExample.Style;
                T5C18_1.Style = FullStyleExample.Style;
                T5C21_1.Style = FullStyleExample.Style;
                T5C22_1.Style = FullStyleExample.Style;
                T5C23_1.Style = FullStyleExample.Style;
                T5C24_1.Style = FullStyleExample.Style;
                T5C27_1.Style = FullStyleExample.Style;
                T5C28_1.Style = FullStyleExample.Style;
                T5C29_1.Style = FullStyleExample.Style;
                T5C30_1.Style = FullStyleExample.Style;
                T5C31_1.Style = FullStyleExample.Style;
                T5C32_1.Style = FullStyleExample.Style;
                T5C33_1.Style = FullStyleExample.Style;
                /////////////////////////////////////////////////////////////////////////////////////
                T5C15_2.Visibility = Visibility.Collapsed;
                T5C16_2.Visibility = Visibility.Collapsed;
                T5C17_2.Visibility = Visibility.Collapsed;
                T5C18_2.Visibility = Visibility.Collapsed;
                T5C21_2.Visibility = Visibility.Collapsed;
                T5C22_2.Visibility = Visibility.Collapsed;
                T5C23_2.Visibility = Visibility.Collapsed;
                T5C24_2.Visibility = Visibility.Collapsed;
                T5C27_2.Visibility = Visibility.Collapsed;
                T5C28_2.Visibility = Visibility.Collapsed;
                T5C29_2.Visibility = Visibility.Collapsed;
                T5C30_2.Visibility = Visibility.Collapsed;
                T5C31_2.Visibility = Visibility.Collapsed;
                T5C32_2.Visibility = Visibility.Collapsed;
                T5C33_2.Visibility = Visibility.Collapsed;
                /////////////////////////////////////////////////////////////////////////////////////
            }
            else
            {
                /////////////////////////////////////////////////////////////////////////////////////
                T5C15_1.Style = HalfStyleExample.Style;
                T5C16_1.Style = HalfStyleExample.Style;
                T5C17_1.Style = HalfStyleExample.Style;
                T5C18_1.Style = HalfStyleExample.Style;
                T5C21_1.Style = HalfStyleExample.Style;
                T5C22_1.Style = HalfStyleExample.Style;
                T5C23_1.Style = HalfStyleExample.Style;
                T5C24_1.Style = HalfStyleExample.Style;
                T5C27_1.Style = HalfStyleExample.Style;
                T5C28_1.Style = HalfStyleExample.Style;
                T5C29_1.Style = HalfStyleExample.Style;
                T5C30_1.Style = HalfStyleExample.Style;
                T5C31_1.Style = HalfStyleExample.Style;
                T5C32_1.Style = HalfStyleExample.Style;
                T5C33_1.Style = HalfStyleExample.Style;
                /////////////////////////////////////////////////////////////////////////////////////
                T5C15_2.Visibility = Visibility.Visible;
                T5C16_2.Visibility = Visibility.Visible;
                T5C17_2.Visibility = Visibility.Visible;
                T5C18_2.Visibility = Visibility.Visible;
                T5C21_2.Visibility = Visibility.Visible;
                T5C22_2.Visibility = Visibility.Visible;
                T5C23_2.Visibility = Visibility.Visible;
                T5C24_2.Visibility = Visibility.Visible;
                T5C27_2.Visibility = Visibility.Visible;
                T5C28_2.Visibility = Visibility.Visible;
                T5C29_2.Visibility = Visibility.Visible;
                T5C30_2.Visibility = Visibility.Visible;
                T5C31_2.Visibility = Visibility.Visible;
                T5C32_2.Visibility = Visibility.Visible;
                T5C33_2.Visibility = Visibility.Visible;
                /////////////////////////////////////////////////////////////////////////////////////
            }

        }
    }
}
