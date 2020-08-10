using ActionsLayer;
using BaseEntities;
using BaseService;
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

namespace Nemesis
{
    public partial class Tools : Window
    {
        public static bool HasReference { set; get; }
        public Tools()
        {
            HasReference = true;
            InitializeComponent();
            TopoTypeChanged(this, new EventArgs());
        }
        protected override void OnClosed(EventArgs e)
        {
            HasReference = false;
            base.OnClosed(e);
        }
        private void CountAlpha(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Hranot_X.Text)
                && !string.IsNullOrWhiteSpace(Hranot_Y.Text)
                && !string.IsNullOrWhiteSpace(KK_X.Text)
                && !string.IsNullOrWhiteSpace(KK_Y.Text)
                && !string.IsNullOrWhiteSpace(HU.Text)
                )
            {
                try
                {
                    var X_Gun = int.Parse(Hranot_X.Text);
                    var Y_Gun = int.Parse(Hranot_Y.Text);
                    var X_KK = int.Parse(KK_X.Text);
                    var Y_KK = int.Parse(KK_Y.Text);
                    var hu = Helper.GetAngleFromString(HU.Text);
                    var texagrakan = Service.GetTexagrakan(new BaseEntities.Point() { X = X_Gun, Y = Y_Gun, H = 0 }, new BaseEntities.Target() { X = X_KK, Y = Y_KK, H = 0 });
                    Angle.Text = Helper.FormatAngle(texagrakan.Alpha);
                    Distance.Text = Math.Round(texagrakan.Distance).ToString();
                    double Betta = 0;
                    if (hu + 30 < texagrakan.Alpha)
                    {
                        Betta = hu + 90 - texagrakan.Alpha;
                    }
                    else
                    {
                        Betta = hu - texagrakan.Alpha + 30;
                    }
                    Betta = Betta > 60 ? Betta - 60 : Betta;
                    Alpha.Text = Helper.FormatAngle(Betta);
                }
                catch
                {
                    Angle.Text = "";
                    Distance.Text = "";
                    Alpha.Text = "";
                }
            }
            else
            {
                Angle.Text = "";
                Distance.Text = "";
                Alpha.Text = "";
            }
        }
        private void Topo1(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(T1_KK_X.Text)
                && !string.IsNullOrWhiteSpace(T1_KK_Y.Text)
                && !string.IsNullOrWhiteSpace(T1_KK_D.Text)
                && !string.IsNullOrWhiteSpace(T1_KK_Alpha.Text))
            {
                try
                {
                    var KKX = int.Parse(T1_KK_X.Text);
                    var KKY = int.Parse(T1_KK_Y.Text);
                    var KKD = int.Parse(T1_KK_D.Text);
                    var KKAlpha = Helper.GetAngleFromString(T1_KK_Alpha.Text);
                    KKAlpha = KKAlpha > 30 ? KKAlpha - 30 : KKAlpha + 30;
                    var P = Service.GetPoint(
                        new BaseEntities.Point() { X = KKX, Y = KKY, H = 0 },
                        new Texagrakan() { Alpha = KKAlpha, Distance = KKD, M = 0 }
                        );
                    T1_DK_X.Text = P.X.ToString();
                    T1_DK_Y.Text = P.Y.ToString();
                }
                catch
                {
                    T1_DK_X.Text = "";
                    T1_DK_Y.Text = "";
                }
            }
            else
            {
                T1_DK_X.Text = "";
                T1_DK_Y.Text = "";
            }
        }
        private void TopoTypeChanged(object sender, EventArgs e)
        {
            switch (TopoType.Text)
            {
                case "Բևեռային եղանակով":
                    TopoPanel1.Visibility = Visibility.Visible;
                    TopoPanel2.Visibility = Visibility.Collapsed;
                    TopoPanel3.Visibility = Visibility.Collapsed;
                    TopoPanel4.Visibility = Visibility.Collapsed;
                    break;
                case "Բևեռային եղանակով 2 քայլով":
                    TopoPanel1.Visibility = Visibility.Collapsed;
                    TopoPanel2.Visibility = Visibility.Visible;
                    TopoPanel3.Visibility = Visibility.Collapsed;
                    TopoPanel4.Visibility = Visibility.Collapsed;
                    break;
                case "2 Դիրեկցիոն անկյուններով":
                    TopoPanel1.Visibility = Visibility.Collapsed;
                    TopoPanel2.Visibility = Visibility.Collapsed;
                    TopoPanel3.Visibility = Visibility.Visible;
                    TopoPanel4.Visibility = Visibility.Collapsed;
                    break;
                case "2 Հեռավորությամբ և անկյամբ":
                    TopoPanel1.Visibility = Visibility.Collapsed;
                    TopoPanel2.Visibility = Visibility.Collapsed;
                    TopoPanel3.Visibility = Visibility.Collapsed;
                    TopoPanel4.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void Topo2_1(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(T2_12_X.Text)
                && !string.IsNullOrWhiteSpace(T2_12_Y.Text)
                && !string.IsNullOrWhiteSpace(T2_12_Alpha.Text)
                && !string.IsNullOrWhiteSpace(T2_12_D.Text)
                )
            {
                try
                {
                    var X12 = int.Parse(T2_12_X.Text);
                    var Y12 = int.Parse(T2_12_Y.Text);
                    var D12 = int.Parse(T2_12_D.Text);
                    var Alpha12 = Helper.GetAngleFromString(T2_12_Alpha.Text);
                    Alpha12 = Alpha12 > 30 ? Alpha12 - 30 : Alpha12 + 30;
                    var P = Service.GetPoint(
                        new BaseEntities.Point() { X = X12, Y = Y12, H = 0 },
                        new Texagrakan() { Alpha = D12, Distance = Alpha12, M = 0 }
                        );
                    T2_X1.Text = P.X.ToString();
                    T2_Y1.Text = P.Y.ToString();
                }
                catch
                {
                    T2_X1.Text = "";
                    T2_Y1.Text = "";
                }
            }
            else
            {
                T2_X1.Text = "";
                T2_Y1.Text = "";
            }
        }
        private void Topo2_2(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(T2_X1.Text)
                && !string.IsNullOrWhiteSpace(T2_Y1.Text)
                && !string.IsNullOrWhiteSpace(T2_1DK_Alpha.Text)
                && !string.IsNullOrWhiteSpace(T2_1DK_D.Text)
                )
            {
                try
                {
                    var X1 = int.Parse(T2_X1.Text);
                    var Y1 = int.Parse(T2_Y1.Text);
                    var DK1D = int.Parse(T2_1DK_D.Text);
                    var DK1Alpha = Helper.GetAngleFromString(T2_1DK_Alpha.Text);

                    var P = Service.GetPoint(
                        new BaseEntities.Point() { X = X1, Y = Y1, H = 0 },
                        new Texagrakan() { Alpha = DK1D, Distance = DK1Alpha, M = 0 }
                        );

                    T2_XDK.Text = P.X.ToString();
                    T2_YDK.Text = P.Y.ToString();
                }
                catch
                {
                    T2_XDK.Text = "";
                    T2_YDK.Text = "";
                }
            }
            else
            {
                T2_XDK.Text = "";
                T2_YDK.Text = "";
            }
        }

        private void Topo3(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(T3_X1.Text)
                && !string.IsNullOrWhiteSpace(T3_Y1.Text)
                && !string.IsNullOrWhiteSpace(T3_Alpha1.Text)
                && !string.IsNullOrWhiteSpace(T3_X2.Text)
                && !string.IsNullOrWhiteSpace(T3_Y2.Text)
                && !string.IsNullOrWhiteSpace(T3_Alpha2.Text)
                )
            {
                try
                {
                    var X1 = int.Parse(T3_X1.Text);
                    var Y1 = int.Parse(T3_Y1.Text);
                    var Alpha1 = Helper.GetAngleFromString(T3_Alpha1.Text);
                    Alpha1 = Alpha1 > 30 ? Alpha1 - 30 : Alpha1 + 30;
                    var X2 = int.Parse(T3_X2.Text);
                    var Y2 = int.Parse(T3_Y2.Text);
                    var Alpha2 = Helper.GetAngleFromString(T3_Alpha2.Text);
                    Alpha2 = Alpha2 > 30 ? Alpha2 - 30 : Alpha2 + 30;
                    var P = Service.GetPointByParallelView(new BaseEntities.Point(X1, Y1, 0), Alpha1, new BaseEntities.Point(X2, Y2, 0), Alpha2);
                    T3_XDK.Text = P.X.ToString();
                    T3_YDK.Text = P.Y.ToString();
                }
                catch
                {
                    T3_XDK.Text = "";
                    T3_YDK.Text = "";
                }
            }
            else
            {
                T3_XDK.Text = "";
                T3_YDK.Text = "";
            }
        }

        private void Topo4(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(T4_KK1_X.Text)
                && !string.IsNullOrWhiteSpace(T4_KK1_Y.Text)
                && !string.IsNullOrWhiteSpace(T4_KK1_D.Text)
                && !string.IsNullOrWhiteSpace(T4_KK2_X.Text)
                && !string.IsNullOrWhiteSpace(T4_KK2_Y.Text)
                && !string.IsNullOrWhiteSpace(T4_KK2_D.Text)
                && !string.IsNullOrWhiteSpace(T4_Alpha.Text)
                )
            {
                try
                {
                    var X1 = double.Parse(T4_KK1_X.Text);
                    var Y1 = double.Parse(T4_KK1_Y.Text);
                    var X2 = double.Parse(T4_KK2_X.Text);
                    var Y2 = double.Parse(T4_KK2_Y.Text);
                    var alpha = Helper.GetAngleFromString(T4_Alpha.Text);
                    var D1 = double.Parse(T4_KK1_D.Text);
                    var D2 = double.Parse(T4_KK2_D.Text);
                    var RadAlpha = alpha * Math.PI / 30;
                    var k1 = (15.0 - alpha / 2) * Math.PI / 30;
                    var k2 = Math.Atan(Math.Abs((D2 - D1) / (D2 + D1) / Math.Tan(RadAlpha/2)));
                    var Alpha1 = (k1 + k2);
                    var Alpha2 = (k1 - k2);
                    var D12_ = D2 * Math.Sin(RadAlpha) / Math.Sin(Alpha1);
                    var D12 = Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
                    if (Math.Abs(D12_ - D12) > 20)
                    {
                        T4_DKX.Text = "Սխալ";
                        T4_DKY.Text = "Տվյալներ";
                        return;
                    }
                    var Alpha12 = Math.Atan(Math.Abs((Y2 - Y1) / (X2 - X1)));
                    var Alpha21 = Service.GetTexagrakan(new BaseEntities.Point() { X = (int)X2, Y = (int)Y2 }, new Target() { X = (int)X1, Y = (int)Y1, H = 0 }).Alpha * Math.PI/30;
                    var Alpha1P = Alpha12 + Alpha1;
                    var Alpha2P = Alpha21 - Alpha2;
                    var XP1 = X1 + D1 * Math.Cos(Alpha1P);
                    var YP1 = Y1 + D1 * Math.Sin(Alpha1P);
                    var XP2 = X2 + D2 * Math.Cos(Alpha2P);
                    var YP2 = Y2 + D2 * Math.Sin(Alpha2P);
                    T4_DKX.Text = Math.Round(((XP1 + XP2) / 2)).ToString();
                    T4_DKY.Text = Math.Round(((YP1 + YP2) / 2)).ToString();
                }
                catch
                {
                    T4_DKX.Text = "";
                    T4_DKY.Text = "";
                }
            }
            else
            {
                T4_DKX.Text = "";
                T4_DKY.Text = "";
            }

        }
    }
}
