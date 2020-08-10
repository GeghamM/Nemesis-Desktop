using BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class TargetManager
    {
        public static Target GetTargetByNumber(string number, out int index)
        {
            var target = DataReader.GetTargets().Where(t => t.ID == number);

            if (target.Count() > 0)
            {
                index = Owin.Targets.FindIndex(t => t.ID == number);
                return target.First();
            }
            else
            {
                index = -1;
                return null;
            }
        }

        public static AXK GetAXKTargetByNumber(string number, out int index)
        {
            var target = DataReader.GetAXKTargets().Where(t => t.ID == number);

            if (target.Count() > 0)
            {
                index = Owin.AXKTargets.FindIndex(t => t.ID == number);
                return target.First();
            }
            else
            {
                index = -1;
                return null;
            }
        }

        public static AXK GetNextAXKTarget(string number)
        {
            int index = -1;
            var Next = new AXK();
            var current = GetAXKTargetByNumber(number, out index);
            if (index != -1 && index < Owin.AXKTargets.Count - 1)
            {
                Next = Owin.AXKTargets[index + 1];
            }
            else if (index != -1)
            {
                Next = Owin.AXKTargets[0];
            }
            else
            {
                Next = null;
            }
            return Next;
        }

        public static Target GetNextTarget(string number)
        {
            int index = -1;
            var Next = new Target();
            var current = GetTargetByNumber(number, out index);
            if (index != -1 && index < Owin.Targets.Count-1)
            {
                Next = Owin.Targets[index + 1];
            }
            else if (index != -1)
            {
                Next = Owin.Targets[0];
            }
            else
            {
                Next = null;
            }
            return Next;
        }
        public static Target GetPreviousTarget(string number)
        {
            Target Next;
            GetTargetByNumber(number, out int index);
            if (index != -1 && index > 0)
            {
                Next = Owin.Targets[index - 1];
            }
            else if (index != -1)
            {
                Next = Owin.Targets.Last();
            }
            else
            {
                Next = null;
            }
            return Next;
        }
    }
}
