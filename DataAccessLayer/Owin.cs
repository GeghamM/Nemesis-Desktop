using BaseEntities;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;

namespace DataAccessLayer
{
    public static class Owin
    {
        public static Martakarg Martakarg { set; get; }
        public static TaskViewModel TaskView { set; get; }
        public static List<Target> Targets { set; get; }
        public static List<AXK> AXKTargets { set; get; }

        static Owin()
        {
            Martakarg = DataReader.GetMartakarg();
            Martakarg.Cameraner = DataReader.GetCameras();
            Targets = DataReader.GetTargets();
            AXKTargets = DataReader.GetAXKTargets();
            TaskView = new TaskViewModel();
        }

    }
}
