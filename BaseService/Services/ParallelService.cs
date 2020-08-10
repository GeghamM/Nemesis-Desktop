using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BaseEntities;
using DataAccessLayer;
using BaseService;

namespace ActionsLayer
{
    public static partial class Service
    {
        public static Point GetPointByParallelView(string DK1, double AlphaRight, double MRight, string DK2, double AlphaLeft, double MLeft, ref int d1, ref int d2)
        {
            Ditaket RightDK = Owin.Martakarg.GetDitaket(DK1);
            Ditaket LeftDK = Owin.Martakarg.GetDitaket(DK2);

            double D_AjDzax = Math.Sqrt(Math.Pow(LeftDK.X - RightDK.X, 2) + Math.Pow(LeftDK.Y - RightDK.Y, 2));
            double p = Math.Acos((RightDK.X - LeftDK.X) / D_AjDzax) * 30 / Math.PI;
            double AlphaAjDzax = RightDK.Y - LeftDK.Y > 0 ? p : 60 - p;
            double Dzax = AlphaAjDzax - AlphaLeft;
            double Aj = AlphaRight - (AlphaAjDzax > 30 ? AlphaAjDzax - 30 : AlphaAjDzax + 30);
            double y = 3000 - Dzax - Aj;
            double DDzax = -D_AjDzax * Math.Sin(Aj * Math.PI / 30) / Math.Sin(y * Math.PI / 30);
            double DAj = -D_AjDzax * Math.Sin(Dzax * Math.PI / 30) / Math.Sin(y * Math.PI / 30);
            double Hdzax = RightDK.H + DDzax * Math.Sin(MLeft / 100 * Math.PI / 30);
            double Haj = LeftDK.H + DAj * Math.Sin(MRight / 100 * Math.PI / 30);
            double DzaxX = DDzax * Math.Cos(AlphaLeft * Math.PI / 30);
            double DzaxY = DDzax * Math.Sin(AlphaLeft * Math.PI / 30);
            double AjX = DAj * Math.Cos(AlphaRight * Math.PI / 30);
            double AjY = DAj * Math.Sin(AlphaRight * Math.PI / 30);

            double DeltaDzaxX = DzaxX + LeftDK.X;
            double DeltaDzaxY = DzaxY + LeftDK.Y;
            double DeltaAjX = AjX + RightDK.X;
            double DeltaAjY = AjY + RightDK.Y;

            double Xtarget = Math.Round((DeltaDzaxX + DeltaAjX) / 2);
            double Ytarget = Math.Round((DeltaDzaxY + DeltaAjY) / 2);
            double Htarget = Math.Round((Hdzax + Haj) / 2);

            d1 = (int)DAj;
            d2 = (int)DDzax;

            return new Point((int)Xtarget, (int)Ytarget, (int)Htarget);

        }

        public static Point GetPointByParallelView(Point Point1, double Alpha1, Point Point2, double Alpha2)
        {

            double D_AjDzax = Math.Sqrt(Math.Pow(Point1.X - Point2.X, 2) + Math.Pow(Point1.Y - Point2.Y, 2));
            double p = Math.Acos((Point2.X - Point1.X) / D_AjDzax) * 30 / Math.PI;
            double AlphaAjDzax = Point2.Y - Point1.Y > 0 ? p : 60 - p;
            double Dzax = AlphaAjDzax - Alpha1;
            double Aj = Alpha2 - (AlphaAjDzax > 30 ? AlphaAjDzax - 30 : AlphaAjDzax + 30);
            double y = 3000 - Dzax - Aj;
            double DDzax = -D_AjDzax * Math.Sin(Aj * Math.PI / 30) / Math.Sin(y * Math.PI / 30);
            double DAj = -D_AjDzax * Math.Sin(Dzax * Math.PI / 30) / Math.Sin(y * Math.PI / 30);
            double DzaxX = DDzax * Math.Cos(Alpha1 * Math.PI / 30);
            double DzaxY = DDzax * Math.Sin(Alpha1 * Math.PI / 30);
            double AjX = DAj * Math.Cos(Alpha2 * Math.PI / 30);
            double AjY = DAj * Math.Sin(Alpha2 * Math.PI / 30);

            double DeltaDzaxX = DzaxX + Point1.X;
            double DeltaDzaxY = DzaxY + Point1.Y;
            double DeltaAjX = AjX + Point2.X;
            double DeltaAjY = AjY + Point2.Y;

            double Xtarget = Math.Round((DeltaDzaxX + DeltaAjX) / 2);
            double Ytarget = Math.Round((DeltaDzaxY + DeltaAjY) / 2);

            return new Point((int)Xtarget, (int)Ytarget, 0);

        }
    }
}
