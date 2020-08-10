using System;
using System.Collections.Generic;
using System.Text;
using BaseEntities;
using BaseService;

namespace Nemesis
{
    public partial class Task
    {
        public Target GetTarget()
        {
            var target = new Target
            {
                ID = TargetNumber.Text,
                Description = TargetName.Text
            };
            try { target.X = int.Parse(Target_X.Text); }catch { }
            try { target.Y = int.Parse(Target_Y.Text); } catch { }
            try { target.H = int.Parse(Target_H.Text); } catch { }
            try { target.Front = int.Parse(Target_FrontMeter.Text); } catch { }
            try { target.Deepness = int.Parse(Target_Deepness.Text); } catch { }
            return target;
        }

        protected TaskInfo GetTaskInfo() //private
        {
            return new TaskInfo()
            {
                System = Systems.Text,
                AXKType = AXKType.Text,
                Paytucich = Paytucich.Text,
                Positions = Positions.Text,
                SelectedCore = ShootingCore.Text,
                ShootingType = ShootingMethod.Text,
                TaskType = TaskType.Text,
                Texamas = Texamas.Text,
                Kayficents = IsKayficent.Text
            };
        }

        public Kayficents GetKayficentsFromView()
        {
            try
            {
                var kfs = new Kayficents()
                {
                    K_Davarot = double.Parse(string.IsNullOrWhiteSpace(K_Davarot.Text) ? "0" : Helper.GetAngleFromString(K_Davarot.Text).ToString()),
                    K_Dist = double.Parse(string.IsNullOrWhiteSpace(K_Distance.Text) ? "0" : K_Distance.Text),
                    Z_Reper = double.Parse(string.IsNullOrWhiteSpace(Z_Reper.Text) ? "0" : Helper.GetAngleFromString(Z_Reper.Text).ToString()),
                    Distance1 = double.Parse(string.IsNullOrWhiteSpace(K_Dist1.Text) ? "0" : K_Dist1.Text),
                    Distance2 = double.Parse(string.IsNullOrWhiteSpace(K_Dist2.Text) ? "0" : K_Dist2.Text),
                    Distance3 = double.Parse(string.IsNullOrWhiteSpace(K_Dist3.Text) ? "0" : K_Dist3.Text),
                    DeltaDistance1 = double.Parse(string.IsNullOrWhiteSpace(K_DeltaDistance1.Text) ? "0" : K_DeltaDistance1.Text),
                    DeltaDistance2 = double.Parse(string.IsNullOrWhiteSpace(K_DeltaDistance2.Text) ? "0" : K_DeltaDistance2.Text),
                    DeltaDistance3 = double.Parse(string.IsNullOrWhiteSpace(K_DeltaDistance3.Text) ? "0" : K_DeltaDistance3.Text),
                    DeltaDavarot1 = double.Parse(string.IsNullOrWhiteSpace(K_DeltaDavarot1.Text) ? "0" : Helper.GetAngleFromString(K_DeltaDavarot1.Text).ToString()),
                    DeltaDavarot2 = double.Parse(string.IsNullOrWhiteSpace(K_DeltaDavarot2.Text) ? "0" : Helper.GetAngleFromString(K_DeltaDavarot2.Text).ToString()),
                    DeltaDavarot3 = double.Parse(string.IsNullOrWhiteSpace(K_DeltaDavarot3.Text) ? "0" : Helper.GetAngleFromString(K_DeltaDavarot3.Text).ToString())
                };
                return kfs;
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected AXKTarget GetAXKTarget()
        {
            if (AXKType.Text == "Բևեռային")
                return new AXKTarget(new Beverayin()
                {
                    Alpha1 = Alpha_Hram1.Text,
                    Alpha2 = Alpha_Hram2.Text,
                    Dist1 = D_Hram1.Text,
                    Dist2 = D_Hram2.Text,
                    M1 = M_NSH1.Text,
                    M2 = M_NSH2.Text
                });
            else return new AXKTarget(new Uxankyun()
            {
                X2 = AXK_dzax_X.Text,
                X1 = AXK_aj_X.Text,
                Y2 = AXK_dzax_Y.Text,
                Y1 = AXK_aj_Y.Text,
                H2 = AXK_dzax_H.Text,
                H1 = AXK_aj_H.Text
            });
        }
    }
}
