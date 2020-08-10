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
        public static AXKReturn HandleAXKData(string Ditaket1, string Ditaket2, TaskInfo tInfo, AXKTarget target, Kayficents kfs = null)
        {
            var retval = new AXKReturn();
            string type = tInfo.AXKType;
            string texamas = tInfo.Texamas;

            BaseEntities.Point DK1 = Owin.Martakarg.GetDitaket(Ditaket1);
            BaseEntities.Point DK2 = Owin.Martakarg.GetDitaket(Ditaket2);
            BaseEntities.Point p1 = null, p2 = null;
            BaseEntities.Point Tp1 = new BaseEntities.Point(), Tp2 = new BaseEntities.Point(), Tp3 = new BaseEntities.Point();

            Texagrakan tex1;
            Texagrakan tex2;
            if (type == "Բևեռային")
            {
                tex1 = new Texagrakan(double.Parse(Helper.RepresentStrings(target.Beverayin.Alpha1)), double.Parse(target.Beverayin.Dist1), double.Parse(target.Beverayin.M1), 0);
                tex2 = new Texagrakan(double.Parse(Helper.RepresentStrings(target.Beverayin.Alpha2)), double.Parse(target.Beverayin.Dist2), double.Parse(target.Beverayin.M2), 0);
                p1 = GetPoint(DK1, tex1);
                p2 = GetPoint(DK2, tex2);
                retval.P2 = p2;
                retval.P1 = p1;

            }
            else if (type == "ՈՒղղանկյուն")
            {
                p1 = new BaseEntities.Point(int.Parse(target.Uxankyun.X1), int.Parse(target.Uxankyun.Y1), int.Parse(target.Uxankyun.H1));
                p2 = new BaseEntities.Point(int.Parse(target.Uxankyun.X2), int.Parse(target.Uxankyun.Y2), int.Parse(target.Uxankyun.H2));
                tex1 = GetTexagrakan(DK1, new Target("", "", p1.X, p1.Y, p1.H, 0, 0));
                tex2 = GetTexagrakan(DK2, new Target("", "", p2.X, p2.Y, p2.H, 0, 0));
                retval.Tex1 = tex1;
                retval.Tex2 = tex2;
            }

            int Dist = (int)Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2)) + 1;
            double axkalpha = Math.Acos((p2.X - p1.X) / (double)Dist) * 9.55414;
            axkalpha = p2.Y - p1.Y < 0 ? 60 - axkalpha : axkalpha;
            BaseEntities.Point T1 = new BaseEntities.Point
            {
                H = (int)(p2.H > p1.H ? p1.H + ((p2.H - p1.H) / 4) : p2.H + ((p1.H - p2.H) / 4))
            },
            T2 = new BaseEntities.Point
            {
                H = (int)(p2.H > p1.H ? p1.H + (2 * ((p2.H - p1.H) / 4)) : p2.H + (2 * ((p1.H - p2.H) / 4)))
            },
            T3 = new BaseEntities.Point
            {
                H = (int)(p2.H > p1.H ? p1.H + (3 * ((p2.H - p1.H) / 4)) : p2.H + (3 * ((p1.H - p2.H) / 4)))
            };

            int k1 = 50, k2 = 150, k3 = 250, kd = 300;
            if (tInfo.TaskType == "ԱԱԿ") { k1 = 100; k2 = 300; k3 = 500; kd = 600; }
            if (texamas == "Եկրորդ")
            {
                k1 += kd;
                k2 += kd;
                k3 += kd;
            }




            Tp1.X = (int)(p1.X + k1 * Math.Cos(axkalpha / 30.0 * Math.PI));
            Tp1.Y = (int)(p1.Y + k1 * Math.Sin(axkalpha / 30.0 * Math.PI));
            Tp1.H = T1.H;

            Tp2.X = (int)(p1.X + k2 * Math.Cos(axkalpha / 30.0 * Math.PI));
            Tp2.Y = (int)(p1.Y + k2 * Math.Sin(axkalpha / 30.0 * Math.PI));
            Tp2.H = T2.H;

            Tp3.X = (int)(p1.X + k3 * Math.Cos(axkalpha / 30.0 * Math.PI));
            Tp3.Y = (int)(p1.Y + k3 * Math.Sin(axkalpha / 30.0 * Math.PI));
            Tp3.H = T3.H;

            retval.T1 = new Target(new Point(Tp1.X, Tp1.Y, Tp1.H));
            retval.T2 = new Target(new Point(Tp2.X, Tp2.Y, Tp2.H));
            retval.T3 = new Target(new Point(Tp3.X, Tp3.Y, Tp3.H));

            double Dist2 = Math.Round(Math.Sqrt(Math.Pow((Tp2.X - DK1.X), 2) + Math.Pow((Tp2.Y - DK1.Y), 2)));
            Battarey Bat1, Bat2, Bat3;
            if (tInfo.Positions == "Դասակի Կազմով")
            {

                Bat1 = GetBattareyData(
                    Owin.Martakarg.GetKD(1, 1),
                    Owin.Martakarg.GetKD(1, 2),
                    Owin.Martakarg.GetDitaket(Ditaket2),
                    new Target(Tp1),
                    tInfo
                    );

                Bat2 = GetBattareyData(
                       Owin.Martakarg.GetKD(2, 1),
                       Owin.Martakarg.GetKD(2, 2),
                       Owin.Martakarg.GetDitaket(Ditaket2),
                       new Target(Tp2),
                       tInfo
                       );

                Bat3 = GetBattareyData(
                       Owin.Martakarg.GetKD(3, 1),
                       Owin.Martakarg.GetKD(3, 2),
                       Owin.Martakarg.GetDitaket(Ditaket2),
                       new Target(Tp3),
                       tInfo
                       );
            }
            else
            {
                Bat1 = GetBattareyData(
                   Owin.Martakarg.GetKD(1, 0),
                   Owin.Martakarg.GetKD(1, 0),
                   Owin.Martakarg.GetDitaket(Ditaket2),
                   new Target(Tp1),
                   tInfo
                   );

                Bat2 = GetBattareyData(
                       Owin.Martakarg.GetKD(2, 0),
                       Owin.Martakarg.GetKD(2, 0),
                       Owin.Martakarg.GetDitaket(Ditaket2),
                       new Target(Tp2),
                       tInfo
                       );

                Bat3 = GetBattareyData(
                       Owin.Martakarg.GetKD(3, 0),
                       Owin.Martakarg.GetKD(3, 0),
                       Owin.Martakarg.GetDitaket(Ditaket2),
                       new Target(Tp3),
                       tInfo
                       );
            }
            if (tInfo.Positions == "Դասակի Կազմով")
            {
                HandleAXKAngle(Tp1, Owin.Martakarg.GetKD(1, 1), tInfo, ref Bat1);
                HandleAXKAngle(Tp2, Owin.Martakarg.GetKD(2, 1), tInfo, ref Bat2);
                HandleAXKAngle(Tp3, Owin.Martakarg.GetKD(3, 1), tInfo, ref Bat3);
            }
            else
            {
                HandleAXKAngle(Tp1, Owin.Martakarg.GetKD(1, 0), tInfo, ref Bat1);
                HandleAXKAngle(Tp2, Owin.Martakarg.GetKD(2, 0), tInfo, ref Bat2);
                HandleAXKAngle(Tp3, Owin.Martakarg.GetKD(3, 0), tInfo, ref Bat3);
            }

            retval.Bat1 = Bat1;
            retval.Bat2 = Bat2;
            retval.Bat3 = Bat3;
            retval.Front = Dist;

            return retval;

        }

        public static void HandleAXKAngle(BaseEntities.Point target, KrakayinDirq kd, TaskInfo tf, ref Battarey Bat)
        {

            double alphahram = Math.Acos((target.X - Owin.Martakarg.GetKD(1, 1).X) / Bat.First.Distance) * 9.55414;
            alphahram = target.Y - Owin.Martakarg.GetKD(1, 1).Y < 0 ? 60 - alphahram : alphahram;

            if (alphahram > 30)
            {
                if (!(Math.Abs(60 - (kd.HU + Bat.First.DavarotHU + alphahram)) > 7.5 && Math.Abs(60 - (kd.HU + Bat.First.DavarotHU + alphahram)) < 22.5))
                {
                    Bat.punj = -10;
                    Bat.Catk = Math.Round(50 / Bat.DeltaX) + 1;
                    if (tf.Positions == "Դասակի Կազմով")
                    {
                        Bat.KMH = Math.Round((double)kd.Dis / (0.1 * Bat.First.Distance), 2);
                    }
                    else
                    {
                        Bat.KMH = Math.Round((double)kd.Dis / (0.4 * Bat.First.Distance), 2);
                    }

                }
                else
                {
                    Bat.Catk = -10;
                    Bat.punj = Math.Round(200 / (0.004 * Bat.First.Distance));
                }
            }
            else
            {
                if (!(Math.Abs(kd.HU + Bat.First.DavarotHU - alphahram) > 7.5 && Math.Abs(kd.HU + Bat.First.DavarotHU - alphahram) < 22.5))
                {
                    Bat.punj = -10;
                    Bat.Catk = Math.Round(50 / Bat.DeltaX) + 1;
                    if (tf.Positions == "Դասակի Կազմով")
                    {
                        Bat.KMH = Math.Round((double)kd.Dis / (0.1 * Bat.First.Distance), 2);
                    }
                    else
                    {
                        Bat.KMH = Math.Round((double)kd.Dis / (0.4 * Bat.First.Distance), 2);
                    }
                }
                else
                {
                    Bat.Catk = -10;
                    Bat.punj = Math.Round(200 / (0.004 * Bat.First.Distance));
                }
            }
        }

    }
}
