using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActionsLayer;
using BaseEntities;
using DataAccessLayer;
using BaseService;

namespace Nemesis
{
    public partial class Task
    {

        public void DKTexagrakanDzax()
        {
            var dk = Owin.Martakarg.GetDitaket(Ditaket2.Text);
            var target = GetTarget();
            Owin.TaskView.DitaketTexagrakandzax = Service.GetTexagrakan(new BaseEntities.Point(dk.X, dk.Y, dk.H), target);
            SetTexagrakandzax(Owin.TaskView.DitaketTexagrakandzax);
        }

        private void HandleFront(string SenderName, string Distance, string ab, string meter)
        {
            double distance, x = 0, y = 0;
            if (double.TryParse(Distance, out distance) || ( double.TryParse(Target_X.Text, out x) && double.TryParse(Target_Y.Text, out y)) )
            {
                if(!double.TryParse(Distance, out distance))
                {
                    var dk = Owin.Martakarg.GetDitaket(Ditaket2.Text);
                    distance = Math.Sqrt(Math.Pow(x - dk.X, 2) + Math.Pow(y - dk.Y, 2));
                    D_Hram2.Text = ((int)distance).ToString();
                }

                double.TryParse(Distance, out double d);
                try
                {

                    if (SenderName == "Target_FrontMeter")
                    {
                        TargetFront_AB.Text = Math.Round(((Math.Asin(double.Parse(meter) / distance)) * 3000) / Math.PI).ToString();
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(ab))
                            Target_FrontMeter.Text = Math.Round(d * double.Parse(ab) / 955).ToString();
                    }
                }
                catch
                {

                }
            }
        }

        public void ParallelView()
        {
            int Dist1=0, Dist2=0;
            var point = Service.GetPointByParallelView(
                Ditaket1.Text,
                double.Parse(Helper.RepresentStrings(Alpha_Hram1.Text)),
                double.Parse(M_NSH1.Text),
                Ditaket2.Text,
                double.Parse(Helper.RepresentStrings(Alpha_Hram2.Text)),
                double.Parse(M_NSH2.Text),
                ref Dist1,
                ref Dist2
                );

            Target_X.Text = point.X.ToString();
            Target_Y.Text = point.Y.ToString();
            Target_H.Text = point.H.ToString();
            D_Hram1.Text = Dist1.ToString();
            D_Hram2.Text = Dist2.ToString();
        }

        public void HandleTaskTypes(string type)
        {
            switch(type)
            {
                case "Պլանային (Նշ. ցանկից)":
                    TargetName.IsEnabled = false;
                    Target_X.IsEnabled = false;
                    Target_Y.IsEnabled = false;
                    Target_H.IsEnabled = false;
                    Target_FrontMeter.IsEnabled = false;
                    Target_Deepness.IsEnabled = false;
                    TargetFront_AB.IsEnabled = false;
                    Alpha_Azimut2.IsEnabled = false;
                    Alpha_Hram2.IsEnabled = false;
                    D_Hram2.IsEnabled = false;
                    M_NSH2.IsEnabled = false;
                    Planayin.Visibility = Visibility.Visible;
                    Ddzax.Visibility = Visibility.Visible;
                    Daj.Visibility = Visibility.Visible;
                    DitaketAj.Visibility = Visibility.Collapsed;
                    AXK_xyh.Visibility = Visibility.Collapsed;
                    Alpha_Hram2_Label.Content = "α հրամ.";
                    D_Hram2_Label.Content = "Д հրամ.";
                    M_NSH2_Label.Content = "M հրամ.";
                    AXKFront.Visibility = Visibility.Collapsed;
                    break;

                case "ՈՒղղանկյուն կոորդինատներով":
                    TargetName.IsEnabled = true;
                    Target_X.IsEnabled = true;
                    Target_Y.IsEnabled = true;
                    Target_H.IsEnabled = true;
                    Target_FrontMeter.IsEnabled = true;
                    Target_Deepness.IsEnabled = true;
                    TargetFront_AB.IsEnabled = true;
                    Alpha_Hram2.IsEnabled = false;
                    Alpha_Azimut2.IsEnabled = false;
                    D_Hram2.IsEnabled = false;
                    M_NSH2.IsEnabled = false;
                    Planayin.Visibility = Visibility.Visible;
                    DitaketAj.Visibility = Visibility.Collapsed;
                    AXK_xyh.Visibility = Visibility.Collapsed;
                    Alpha_Hram2_Label.Content = "α հրամ.";
                    D_Hram2_Label.Content = "Д հրամ.";
                    M_NSH2_Label.Content = "M հրամ.";
                    AXKFront.Visibility = Visibility.Collapsed;
                    break;

                case "Բևեռային եղանակով":
                    TargetName.IsEnabled = true;
                    Target_X.IsEnabled = false;
                    Target_Y.IsEnabled = false;
                    Target_H.IsEnabled = false;
                    Target_FrontMeter.IsEnabled = true;
                    Target_Deepness.IsEnabled = true;
                    TargetFront_AB.IsEnabled = true;
                    Alpha_Hram2.IsEnabled = true;
                    Alpha_Azimut2.IsEnabled = true;
                    D_Hram2.IsEnabled = true;
                    M_NSH2.IsEnabled = true;
                    D_Hram2.IsEnabled = true;
                    D_Hram1.IsEnabled = true;
                    Planayin.Visibility = Visibility.Visible;
                    Ddzax.Visibility = Visibility.Visible;
                    Daj.Visibility = Visibility.Visible;
                    DitaketAj.Visibility = Visibility.Collapsed;
                    AXK_xyh.Visibility = Visibility.Collapsed;
                    Alpha_Hram2_Label.Content = "α հրամ.";
                    D_Hram2_Label.Content = "Д հրամ.";
                    M_NSH2_Label.Content = "M հրամ.";
                    AXKFront.Visibility = Visibility.Collapsed;
                    break;

                case "Զուգորդված դիտարկում":
                    TargetName.IsEnabled = true;
                    Target_X.IsEnabled = false;
                    Target_Y.IsEnabled = false;
                    Target_H.IsEnabled = false;
                    Target_FrontMeter.IsEnabled = true;
                    Target_Deepness.IsEnabled = true;
                    TargetFront_AB.IsEnabled = true;
                    Alpha_Hram2.IsEnabled = true;
                    Alpha_Azimut2.IsEnabled = true;
                    M_NSH2.IsEnabled = true;
                    Alpha_Hram1.IsEnabled = true;
                    M_NSH1.IsEnabled = true;
                    Planayin.Visibility = Visibility.Visible;
                    D_Hram2.IsEnabled = false;
                    D_Hram1.IsEnabled = false;
                    DitaketAj.Visibility = Visibility.Visible;
                    
                    AXK_xyh.Visibility = Visibility.Collapsed;
                    Alpha_Hram2_Label.Content = "α ձախ.";
                    D_Hram2_Label.Content = "Д ձախ.";
                    M_NSH2_Label.Content = "M ձախ.";
                    AXKFront.Visibility = Visibility.Collapsed;
                    break;

                case "ԱԱԿ":
                    TargetName.IsEnabled = false;
                    Target_X.IsEnabled = false;
                    Target_Y.IsEnabled = false;
                    Target_H.IsEnabled = false;
                    Target_FrontMeter.IsEnabled = false;
                    Target_Deepness.IsEnabled = false;
                    TargetFront_AB.IsEnabled = false;
                    Planayin.Visibility = Visibility.Collapsed;
                    AXKFront.Visibility = Visibility.Visible;
                    if (AXKType.Text == "Բևեռային")
                    {
                        Alpha_Hram2.IsEnabled = true;
                        Alpha_Azimut2.IsEnabled = true;
                        D_Hram2.IsEnabled = true;
                        M_NSH2.IsEnabled = true;
                        Alpha_Hram1.IsEnabled = true;
                        Alpha_Azimut1.IsEnabled = true;
                        D_Hram1.IsEnabled = true;
                        M_NSH1.IsEnabled = true;

                        AXK_dzax_X.IsEnabled = false;
                        AXK_dzax_Y.IsEnabled = false;
                        AXK_dzax_H.IsEnabled = false;
                        AXK_aj_X.IsEnabled = false;
                        AXK_aj_Y.IsEnabled = false;
                        AXK_aj_H.IsEnabled = false;

                    }
                    else
                    {
                        Alpha_Hram2.IsEnabled = false;
                        Alpha_Azimut2.IsEnabled = false;
                        D_Hram2.IsEnabled = false;
                        M_NSH2.IsEnabled = false;
                        Alpha_Hram1.IsEnabled = false;
                        Alpha_Azimut1.IsEnabled = false;
                        D_Hram1.IsEnabled = false;
                        M_NSH1.IsEnabled = false;

                        AXK_dzax_X.IsEnabled = true;
                        AXK_dzax_Y.IsEnabled = true;
                        AXK_dzax_H.IsEnabled = true;
                        AXK_aj_X.IsEnabled = true;
                        AXK_aj_Y.IsEnabled = true;
                        AXK_aj_H.IsEnabled = true;
                    }

                    Ddzax.Visibility = Visibility.Visible;
                    Daj.Visibility = Visibility.Visible;
                    DitaketAj.Visibility = Visibility.Visible;
                    AXK_xyh.Visibility = Visibility.Visible;
                    Alpha_Hram2_Label.Content = "α ձախ.";
                    D_Hram2_Label.Content = "Д ձախ.";
                    M_NSH2_Label.Content = "M ձախ.";
                    break;

                case "ՇԱԿ":
                    TargetName.IsEnabled = false;
                    Target_X.IsEnabled = false;
                    Target_Y.IsEnabled = false;
                    Target_H.IsEnabled = false;
                    Target_FrontMeter.IsEnabled = false;
                    Target_Deepness.IsEnabled = false;
                    TargetFront_AB.IsEnabled = false;
                    Planayin.Visibility = Visibility.Collapsed;
                    AXKFront.Visibility = Visibility.Visible;
                    if (AXKType.Text == "Բևեռային")
                    {
                        Alpha_Hram2.IsEnabled = true;
                        D_Hram2.IsEnabled = true;
                        M_NSH2.IsEnabled = true;
                        Alpha_Hram1.IsEnabled = true;
                        D_Hram1.IsEnabled = true;
                        M_NSH1.IsEnabled = true;

                        AXK_dzax_X.IsEnabled = false;
                        AXK_dzax_Y.IsEnabled = false;
                        AXK_dzax_H.IsEnabled = false;
                        AXK_aj_X.IsEnabled = false;
                        AXK_aj_Y.IsEnabled = false;
                        AXK_aj_H.IsEnabled = false;

                    }
                    else
                    {
                        Alpha_Hram2.IsEnabled = false;
                        D_Hram2.IsEnabled = false;
                        M_NSH2.IsEnabled = false;
                        Alpha_Hram1.IsEnabled = false;
                        D_Hram1.IsEnabled = false;
                        M_NSH1.IsEnabled = false;

                        AXK_dzax_X.IsEnabled = true;
                        AXK_dzax_Y.IsEnabled = true;
                        AXK_dzax_H.IsEnabled = true;
                        AXK_aj_X.IsEnabled = true;
                        AXK_aj_Y.IsEnabled = true;
                        AXK_aj_H.IsEnabled = true;
                    }

                    Ddzax.Visibility = Visibility.Visible;
                    Daj.Visibility = Visibility.Visible;
                    DitaketAj.Visibility = Visibility.Visible;
                    AXK_xyh.Visibility = Visibility.Visible;
                    Alpha_Hram2_Label.Content = "α ձախ.";
                    D_Hram2_Label.Content = "Д ձախ.";
                    M_NSH2_Label.Content = "M ձախ.";
                    break;
            }
        }
        public void HandlePaytucichTypes(string type)
        {
            switch(type)
            {
                case "РГМ-2 (ОФ-462)":
                    Poxrak1.Visibility = Visibility.Collapsed;
                    Poxrak2.Visibility = Visibility.Collapsed;
                    Poxrak3.Visibility = Visibility.Collapsed;
                    Nhaz1.Visibility = Visibility.Collapsed;
                    Nhaz2.Visibility = Visibility.Collapsed;
                    Nhaz3.Visibility = Visibility.Collapsed;
                    catk1.Visibility = Visibility.Visible;
                    catk2.Visibility = Visibility.Visible;
                    catk3.Visibility = Visibility.Visible;
                    break;
                case "В-90 (ОФ-462)":
                    Poxrak1.Visibility = Visibility.Visible;
                    Poxrak2.Visibility = Visibility.Visible;
                    Poxrak3.Visibility = Visibility.Visible;
                    PoxrakLabel1.Content = "ՊԱՅԹՈՒՑԻՉ";
                    PoxrakLabel2.Content = "ՊԱՅԹՈՒՑԻՉ";
                    PoxrakLabel3.Content = "ՊԱՅԹՈՒՑԻՉ";
                    Nhaz1.Visibility = Visibility.Visible;
                    Nhaz2.Visibility = Visibility.Visible;
                    Nhaz3.Visibility = Visibility.Visible;
                    catk1.Visibility = Visibility.Collapsed;
                    catk2.Visibility = Visibility.Collapsed;
                    catk3.Visibility = Visibility.Collapsed;
                    break;
                case "Т-90 (3С4)":
                    Poxrak1.Visibility = Visibility.Visible;
                    Poxrak2.Visibility = Visibility.Visible;
                    Poxrak3.Visibility = Visibility.Visible;
                    PoxrakLabel1.Content = "ՓՈՂՐԱԿ";
                    PoxrakLabel2.Content = "ՓՈՂՐԱԿ";
                    PoxrakLabel3.Content = "ՓՈՂՐԱԿ";
                    Nhaz1.Visibility = Visibility.Visible;
                    Nhaz2.Visibility = Visibility.Visible;
                    Nhaz3.Visibility = Visibility.Visible;
                    catk1.Visibility = Visibility.Collapsed;
                    catk2.Visibility = Visibility.Collapsed;
                    catk3.Visibility = Visibility.Collapsed;
                    break;
                case "Т-7 (С-463)":
                    Poxrak1.Visibility = Visibility.Visible;
                    Poxrak2.Visibility = Visibility.Visible;
                    Poxrak3.Visibility = Visibility.Visible;
                    PoxrakLabel1.Content = "ՓՈՂՐԱԿ";
                    PoxrakLabel2.Content = "ՓՈՂՐԱԿ";
                    PoxrakLabel3.Content = "ՓՈՂՐԱԿ";
                    Nhaz1.Visibility = Visibility.Visible;
                    Nhaz2.Visibility = Visibility.Visible;
                    Nhaz3.Visibility = Visibility.Visible;
                    catk1.Visibility = Visibility.Collapsed;
                    catk2.Visibility = Visibility.Collapsed;
                    catk3.Visibility = Visibility.Collapsed;
                    break;

                //case "ДТМ-75":
                //    Poxrak1.Visibility = Visibility.Visible;
                //    Poxrak2.Visibility = Visibility.Visible;
                //    Poxrak3.Visibility = Visibility.Visible;
                //    PoxrakLabel1.Content = "ՓՈՂՐԱԿ";
                //    PoxrakLabel2.Content = "ՓՈՂՐԱԿ";
                //    PoxrakLabel3.Content = "ՓՈՂՐԱԿ";
                //    Nhaz1.Visibility = Visibility.Visible;
                //    Nhaz2.Visibility = Visibility.Visible;
                //    Nhaz3.Visibility = Visibility.Visible;
                //    catk1.Visibility = Visibility.Collapsed;
                //    catk2.Visibility = Visibility.Collapsed;
                //    catk3.Visibility = Visibility.Collapsed;
                //    break;

                //case "АР-30":
                //    Poxrak1.Visibility = Visibility.Collapsed;
                //    Poxrak2.Visibility = Visibility.Collapsed;
                //    Poxrak3.Visibility = Visibility.Collapsed;
                //    Nhaz1.Visibility = Visibility.Collapsed;
                //    Nhaz2.Visibility = Visibility.Collapsed;
                //    Nhaz3.Visibility = Visibility.Collapsed;
                //    catk1.Visibility = Visibility.Visible;
                //    catk2.Visibility = Visibility.Visible;
                //    catk3.Visibility = Visibility.Visible;
                //    break;
            }
        }

        
    }
}