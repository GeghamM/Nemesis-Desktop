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
        public static Texagrakan GetTexagrakan(Point From, Target target)
        {
            Texagrakan texagrakan = new Texagrakan();
            texagrakan.Distance = Math.Sqrt(Math.Pow(target.X - From.X, 2) + Math.Pow(target.Y - From.Y, 2));
            texagrakan.Alpha = Math.Round(Math.Acos((target.X - From.X) / texagrakan.Distance) * 9.55414, 2);
            texagrakan.Alpha = target.Y - From.Y < 0 ? 60 - texagrakan.Alpha : texagrakan.Alpha;
            texagrakan.FrontAB = ((Math.Asin(target.Front / texagrakan.Distance)) * 3000) / Math.PI;
            texagrakan.M = (target.H - From.H) * 955 / texagrakan.Distance;

            return texagrakan;
        }

        public static Point GetPoint(Point From, Texagrakan texagrakan)
        {
            Point p = new Point
            {
                X = (int)Math.Round(From.X + (texagrakan.Distance * Math.Cos(texagrakan.Alpha * Math.PI / 30))),
                Y = (int)Math.Round(From.Y + (texagrakan.Distance * Math.Sin(texagrakan.Alpha * Math.PI / 30))),
                H = (int)Math.Round(From.H + texagrakan.Distance * Math.Sin((texagrakan.M * Math.PI) / 3000))
            };

            return p;

        }

    }
}
