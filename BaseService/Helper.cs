using BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BaseService
{
    public static class Helper
    {

        public static string FormatAngleWithSigne(double angle)
        {
            angle = Math.Round(angle, 2);
            string ang = angle.ToString().Contains(',') || angle.ToString().Contains('.') ? angle.ToString() : angle.ToString() + ",0";
            string a = ang.Replace(",", "-").Replace(".", "-");
            while (a.Split('-').Last().Length <= 1)
            {
                a += "0";
            }
            if (a[0] == '-')
            {
                if (!a.Substring(1).Contains('-'))
                {
                    a += "-00";
                }
            }
            else
            {
                if (!a.Contains('-'))
                {
                    a += "-00";
                }
            }
            if (a[0] != '-') a = '+' + a;
            return a;
        }

        public static string FormatAngle(double angle)
        {
            angle = Math.Round(angle, 2);
            string ang = angle.ToString().Contains(',') || angle.ToString().Contains('.') ? angle.ToString() : angle.ToString() + ",0";
            string a = ang.Replace(",", "-").Replace(".", "-");
            while (a.Split(' ').Last().Length <= 1)
            {
                a += "0";
            }
            while (a.Split('-').Last().Length == 1)
            {
                a += "0";
            }
            if (a[0] == '-')
            {
                if (!a.Substring(1).Contains('-'))
                {
                    a += "-00";
                }
            }
            else
            {
                if (!a.Contains('-'))
                {
                    a += "-00";
                }
            }
            return a;
        }

        public static double GetAngleFromString(string AngleString)
        {
            if (AngleString.Count() - AngleString.Replace("-", "").Count() == 2)
            {
                return double.Parse("-" + AngleString.Remove(0, 1).Replace("-", Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator));
            }
            else
            {
                return double.Parse(AngleString.Replace("-", Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator));
            }
        }

        public static double GetAngleFromCamera(Camera cam, double Azimut)
        {
            if (cam.IsPlus)
            {
                double Pazimut = Azimut - cam.P;
                if (Pazimut < 0)
                {
                    Pazimut += 360;
                }
                return Pazimut / 6;
            }
            else
            {
                double Pazimut = cam.P - Azimut;
                if (Pazimut < 0)
                {
                    Pazimut += 360;
                }
                return Pazimut / 6;
            }
        }

        public static string RepresentStrings(string input)
        {
            return input
                .Replace(" ", "")
                .Replace(".", Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator)
                .Replace(",", Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator)
                .Replace("-", Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
        }
    }
}
