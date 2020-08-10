using BaseService;
using BaseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nemesis
{
    public partial class Task
    {
        public bool SetTarget(Target target)
        {
            TargetNumber.Text = target.ID;
            TargetName.Text = target.Description;
            Target_X.Text = target.X.ToString();
            Target_Y.Text = target.Y.ToString();
            Target_H.Text = target.H.ToString();
            Target_FrontMeter.Text = target.Front.ToString();
            Target_Deepness.Text = target.Deepness.ToString();
            return true;
        }

        public bool ReSetTarget()
        {
            TargetName.Text = "";
            Target_X.Text = "";
            Target_Y.Text = "";
            Target_H.Text = "";
            Target_FrontMeter.Text = "";
            Target_Deepness.Text = "";
            TargetFront_AB.Text = "";
            AXK_aj_X.Text = "";
            AXK_aj_Y.Text = "";
            AXK_aj_H.Text = "";
            AXK_dzax_X.Text = "";
            AXK_dzax_Y.Text = "";
            AXK_dzax_H.Text = "";
            AXKF_Frot.Text = "";
            return true;
        }

        public bool ResetBattares()
        {
            Alpha_Hram1.Text = "";
            D_Hram1.Text = "";
            M_NSH1.Text = "";
            Alpha_Hram2.Text = "";
            D_Hram2.Text = "";
            M_NSH2.Text = "";

            M1_Core.Text = "";
            M1_D1_Pricel.Text = "";
            M1_D2_Pricel.Text = "";
            M1_D1_Poxrak.Text = "";
            M1_D2_Poxrak.Text = "";
            M1_D1_LVL.Text = "";
            M1_D2_LVL.Text = "";
            M1_D1_DHU.Text = "";
            M1_D2_DHU.Text = "";
            M1_KMH.Text = "";
            M1_KU.Text = "";
            M1_SHU.Text = "";
            M1_PS.Text = "";
            M1_xHAZ.Text = "";
            M1_nHAZ.Text = "";
            M1_KDN.Text = "";
            M1_Ttrichq.Text = "";
            M1_D1_Dtex.Text = "";
            M1_D2_Dtex.Text = "";
            M1_D1_shrjaberTex.Text = "";
            M1_D2_shrjaberTex.Text = "";
            M1_D1_D_Hashv.Text = "";
            M1_D2_D_Hashv.Text = "";
            M1_D1_shrjaber_Hashv.Text = "";
            M1_D2_shrjaber_Hashv.Text = "";
            M1_Catk.Text = "";
            M1_Hovhar.Text = "";
            M1_Z.Text = "";

            M2_Core.Text = "";
            M2_D1_Pricel.Text = "";
            M2_D2_Pricel.Text = "";
            M2_D1_Poxrak.Text = "";
            M2_D2_Poxrak.Text = "";
            M2_D1_LVL.Text = "";
            M2_D2_LVL.Text = "";
            M2_D1_DHU.Text = "";
            M2_D2_DHU.Text = "";
            M2_KMH.Text = "";
            M2_KU.Text = "";
            M2_SHU.Text = "";
            M2_PS.Text = "";
            M2_xHAZ.Text = "";
            M2_nHAZ.Text = "";
            M2_KDN.Text = "";
            M2_Ttrichq.Text = "";
            M2_D1_Dtex.Text = "";
            M2_D2_Dtex.Text = "";
            M2_D1_shrjaberTex.Text = "";
            M2_D2_shrjaberTex.Text = "";
            M2_D1_D_Hashv.Text = "";
            M2_D2_D_Hashv.Text = "";
            M2_D1_shrjaber_Hashv.Text = "";
            M2_D2_shrjaber_Hashv.Text = "";
            M2_Catk.Text = "";
            M2_Hovhar.Text = "";
            M2_Z.Text = "";

            M3_Core.Text = "";
            M3_D1_Pricel.Text = "";
            M3_D2_Pricel.Text = "";
            M3_D1_Poxrak.Text = "";
            M3_D2_Poxrak.Text = "";
            M3_D1_LVL.Text = "";
            M3_D2_LVL.Text = "";
            M3_D1_DHU.Text = "";
            M3_D2_DHU.Text = "";
            M3_KMH.Text = "";
            M3_KU.Text = "";
            M3_SHU.Text = "";
            M3_PS.Text = "";
            M3_xHAZ.Text = "";
            M3_nHAZ.Text = "";
            M3_KDN.Text = "";
            M3_Ttrichq.Text = "";
            M3_D1_Dtex.Text = "";
            M3_D2_Dtex.Text = "";
            M3_D1_shrjaberTex.Text = "";
            M3_D2_shrjaberTex.Text = "";
            M3_D1_D_Hashv.Text = "";
            M3_D2_D_Hashv.Text = "";
            M3_D1_shrjaber_Hashv.Text = "";
            M3_D2_shrjaber_Hashv.Text = "";
            M3_Catk.Text = "";
            M3_Hovhar.Text = "";
            M3_Z.Text = "";

            return true;
        }

        public bool SetAXKpointLeft(BaseEntities.Point point)
        {
            AXK_dzax_X.Text = point.X.ToString();
            AXK_dzax_Y.Text = point.Y.ToString();
            AXK_dzax_H.Text = point.H.ToString();
            return true;
        }

        public bool SetAXKpointRight(BaseEntities.Point point)
        {
            AXK_aj_X.Text = point.X.ToString();
            AXK_aj_Y.Text = point.Y.ToString();
            AXK_aj_H.Text = point.H.ToString();
            return true;
        }
        public bool SetTexagrakandzax(Texagrakan t)
        {
            Alpha_Hram2.Text = Helper.FormatAngle(t.Alpha);
            D_Hram2.Text = Math.Round(t.Distance).ToString();
            t.M = Math.Round(t.M);
            M_NSH2.Text = t.M.ToString()[0] == '-' ? t.M.ToString() : '+' + t.M.ToString();
            return true;
        }

        public bool SetTexagrakanaj(Texagrakan t)
        {
            Alpha_Hram1.Text = Helper.FormatAngle(t.Alpha);
            D_Hram1.Text = Math.Round(t.Distance).ToString();
            t.M = Math.Round(t.M);
            M_NSH1.Text = t.M.ToString()[0] == '-' ? t.M.ToString() : '+' + t.M.ToString();
            return true;
        }

        public bool SetTexagrakandAch(Texagrakan t)
        {
            Alpha_Hram1.Text = Helper.FormatAngle(t.Alpha);
            D_Hram1.Text = Math.Round(t.Distance).ToString();
            t.M = Math.Round(t.M);
            M_NSH1.Text = t.M.ToString()[0] == '-' ? t.M.ToString() : '+' + t.M.ToString();
            return true;
        }

        public bool SetBattarey1Data(Battarey battarey)
        {
            if (battarey is null) return false;
            M1_Core.Text = battarey.Core != null ? battarey.Core : "-";
            M1_D1_Pricel.Text = battarey.First.Pricel != 0 ? battarey.First.Pricel.ToString() : "-";
            M1_D2_Pricel.Text = battarey.Second.Pricel != 0 ? battarey.Second.Pricel.ToString() : "-";
            M1_D1_Poxrak.Text = battarey.First.Paytucich != 0 ? Math.Round(battarey.First.Paytucich, 1).ToString() : "-";
            M1_D2_Poxrak.Text = battarey.Second.Paytucich != 0 ? Math.Round(battarey.Second.Paytucich, 1).ToString() : "-";
            M1_D1_LVL.Text = battarey.First.Level != 0 ? Helper.FormatAngle(battarey.First.Level) : "-";
            M1_D2_LVL.Text = battarey.Second.Level != 0 ? Helper.FormatAngle(battarey.Second.Level) : "-";
            M1_D1_DHU.Text = battarey.First.DavarotHU != 0 ? Helper.FormatAngleWithSigne(battarey.First.DavarotHU) : "-";
            M1_D2_DHU.Text = battarey.Second.DavarotHU != 0 ? Helper.FormatAngleWithSigne(battarey.Second.DavarotHU) : "-";
            M1_KMH.Text = battarey.KMH != 0 ? Helper.FormatAngle(battarey.KMH) : "-";
            M1_KU.Text = battarey.KU != 0 ? battarey.KU.ToString() : "-";
            M1_SHU.Text = battarey.SHU != 0 ? Helper.FormatAngle(battarey.SHU) : "-";
            M1_PS.Text = battarey.PS != 0 ? Helper.FormatAngle(battarey.PS) : "-";
            M1_xHAZ.Text = battarey.DeltaX.ToString();
            M1_nHAZ.Text = Math.Round(battarey.DeltaN, 1).ToString();
            M1_KDN.Text = battarey.KDDirection;
            M1_Ttrichq.Text = battarey.Ttrichqayin.ToString();
            M1_D1_Dtex.Text = battarey.First.Distance != 0 ? Math.Round(battarey.First.Distance).ToString() : "-";
            M1_D2_Dtex.Text = battarey.Second.Distance != 0 ? Math.Round(battarey.Second.Distance).ToString() : "-";
            M1_D1_shrjaberTex.Text = battarey.First.Davarot != 0 ? Helper.FormatAngleWithSigne(battarey.First.Davarot) : "-";
            M1_D2_shrjaberTex.Text = battarey.Second.Davarot != 0 ? Helper.FormatAngleWithSigne(battarey.Second.Davarot) : "-";
            M1_D1_D_Hashv.Text = battarey.First.DistanceHashvarkayin != 0 ? Math.Round(battarey.First.DistanceHashvarkayin).ToString() : "-";
            M1_D2_D_Hashv.Text = battarey.Second.DistanceHashvarkayin != 0 ? Math.Round(battarey.Second.DistanceHashvarkayin).ToString() : "-";
            M1_D1_shrjaber_Hashv.Text = battarey.First.DavarotHashvarkayin != 0 ? Helper.FormatAngleWithSigne(battarey.First.DavarotHashvarkayin) : "-";
            M1_D2_shrjaber_Hashv.Text = battarey.Second.DavarotHashvarkayin != 0 ? Helper.FormatAngleWithSigne(battarey.Second.DavarotHashvarkayin) : "-";
            M1_Catk.Text = battarey.Catk == -10 ? "Ճակատային" : battarey.Catk.ToString();
            M1_Hovhar.Text = battarey.punj == -10 ? "Թևային" : Helper.FormatAngle(battarey.punj / 100);
            M1_Z.Text = battarey.Z.ToString();
            return true;
        }

        public bool SetBattarey2Data(Battarey battarey)
        {
            M2_Core.Text = battarey.Core != null ? battarey.Core : "-";
            M2_D1_Pricel.Text = battarey.First.Pricel != 0 ? battarey.First.Pricel.ToString() : "-";
            M2_D2_Pricel.Text = battarey.Second.Pricel != 0 ? battarey.Second.Pricel.ToString() : "-";
            M2_D1_Poxrak.Text = battarey.First.Paytucich != 0 ? Math.Round(battarey.First.Paytucich, 1).ToString() : "-";
            M2_D2_Poxrak.Text = battarey.Second.Paytucich != 0 ? Math.Round(battarey.Second.Paytucich, 1).ToString() : "-";
            M2_D1_LVL.Text = battarey.First.Level != 0 ? Helper.FormatAngle(battarey.First.Level) : "-";
            M2_D2_LVL.Text = battarey.Second.Level != 0 ? Helper.FormatAngle(battarey.Second.Level) : "-";
            M2_D1_DHU.Text = battarey.First.DavarotHU != 0 ? Helper.FormatAngleWithSigne(battarey.First.DavarotHU) : "-";
            M2_D2_DHU.Text = battarey.Second.DavarotHU != 0 ? Helper.FormatAngleWithSigne(battarey.Second.DavarotHU) : "-";
            M2_KMH.Text = battarey.KMH != 0 ? Helper.FormatAngle(battarey.KMH) : "-";
            M2_KU.Text = battarey.KU != 0 ? battarey.KU.ToString() : "-";
            M2_SHU.Text = battarey.SHU != 0 ? Helper.FormatAngle(battarey.SHU) : "-";
            M2_PS.Text = battarey.PS != 0 ? Helper.FormatAngle(battarey.PS) : "-";
            M2_xHAZ.Text = battarey.DeltaX.ToString();
            M2_nHAZ.Text = Math.Round(battarey.DeltaN, 1).ToString();
            M2_KDN.Text = battarey.KDDirection;
            M2_Ttrichq.Text = battarey.Ttrichqayin.ToString();
            M2_D1_Dtex.Text = battarey.First.Distance != 0 ? Math.Round(battarey.First.Distance).ToString() : "-";
            M2_D2_Dtex.Text = battarey.Second.Distance != 0 ? Math.Round(battarey.Second.Distance).ToString() : "-";
            M2_D1_shrjaberTex.Text = battarey.First.Davarot != 0 ? Helper.FormatAngleWithSigne(battarey.First.Davarot) : "-";
            M2_D2_shrjaberTex.Text = battarey.Second.Davarot != 0 ? Helper.FormatAngleWithSigne(battarey.Second.Davarot) : "-";
            M2_D1_D_Hashv.Text = battarey.First.DistanceHashvarkayin != 0 ? Math.Round(battarey.First.DistanceHashvarkayin).ToString() : "-";
            M2_D2_D_Hashv.Text = battarey.Second.DistanceHashvarkayin != 0 ? Math.Round(battarey.Second.DistanceHashvarkayin).ToString() : "-";
            M2_D1_shrjaber_Hashv.Text = battarey.First.DavarotHashvarkayin != 0 ? Helper.FormatAngleWithSigne(battarey.First.DavarotHashvarkayin) : "-";
            M2_D2_shrjaber_Hashv.Text = battarey.Second.DavarotHashvarkayin != 0 ? Helper.FormatAngleWithSigne(battarey.Second.DavarotHashvarkayin) : "-";
            M2_Catk.Text = battarey.Catk == -10 ? "Ճակատային" : battarey.Catk.ToString();
            M2_Hovhar.Text = battarey.punj == -10 ? "Թևային" : Helper.FormatAngle(battarey.punj / 100);
            M2_Z.Text = battarey.Z.ToString();
            return true;
        }

        public bool SetBattarey3Data(Battarey battarey)
        {
            M3_Core.Text = battarey.Core != null ? battarey.Core : "-";
            M3_D1_Pricel.Text = battarey.First.Pricel != 0 ? battarey.First.Pricel.ToString() : "-";
            M3_D2_Pricel.Text = battarey.Second.Pricel != 0 ? battarey.Second.Pricel.ToString() : "-";
            M3_D1_Poxrak.Text = battarey.First.Paytucich != 0 ? Math.Round(battarey.First.Paytucich, 1).ToString() : "-";
            M3_D2_Poxrak.Text = battarey.Second.Paytucich != 0 ? Math.Round(battarey.Second.Paytucich, 1).ToString() : "-";
            M3_D1_LVL.Text = battarey.First.Level != 0 ? Helper.FormatAngle(battarey.First.Level) : "-";
            M3_D2_LVL.Text = battarey.Second.Level != 0 ? Helper.FormatAngle(battarey.Second.Level) : "-";
            M3_D1_DHU.Text = battarey.First.DavarotHU != 0 ? Helper.FormatAngleWithSigne(battarey.First.DavarotHU) : "-";
            M3_D2_DHU.Text = battarey.Second.DavarotHU != 0 ? Helper.FormatAngleWithSigne(battarey.Second.DavarotHU) : "-";
            M3_KMH.Text = battarey.KMH != 0 ? Helper.FormatAngle(battarey.KMH) : "-";
            M3_KU.Text = battarey.KU != 0 ? battarey.KU.ToString() : "-";
            M3_SHU.Text = battarey.SHU != 0 ? Helper.FormatAngle(battarey.SHU) : "-";
            M3_PS.Text = battarey.PS != 0 ? Helper.FormatAngle(battarey.PS) : "-";
            M3_xHAZ.Text = battarey.DeltaX.ToString();
            M3_nHAZ.Text = Math.Round(battarey.DeltaN, 1).ToString();
            M3_KDN.Text = battarey.KDDirection;
            M3_Ttrichq.Text = battarey.Ttrichqayin.ToString();
            M3_D1_Dtex.Text = battarey.First.Distance != 0 ? Math.Round(battarey.First.Distance).ToString() : "-";
            M3_D2_Dtex.Text = battarey.Second.Distance != 0 ? Math.Round(battarey.Second.Distance).ToString() : "-";
            M3_D1_shrjaberTex.Text = battarey.First.Davarot != 0 ? Helper.FormatAngleWithSigne(battarey.First.Davarot) : "-";
            M3_D2_shrjaberTex.Text = battarey.Second.Davarot != 0 ? Helper.FormatAngleWithSigne(battarey.Second.Davarot) : "-";
            M3_D1_D_Hashv.Text = battarey.First.DistanceHashvarkayin != 0 ? Math.Round(battarey.First.DistanceHashvarkayin).ToString() : "-";
            M3_D2_D_Hashv.Text = battarey.Second.DistanceHashvarkayin != 0 ? Math.Round(battarey.Second.DistanceHashvarkayin).ToString() : "-";
            M3_D1_shrjaber_Hashv.Text = battarey.First.DavarotHashvarkayin != 0 ? Helper.FormatAngleWithSigne(battarey.First.DavarotHashvarkayin) : "-";
            M3_D2_shrjaber_Hashv.Text = battarey.Second.DavarotHashvarkayin != 0 ? Helper.FormatAngleWithSigne(battarey.Second.DavarotHashvarkayin) : "-";
            M3_Catk.Text = battarey.Catk == -10 ? "Ճակատային" : battarey.Catk.ToString();
            M3_Hovhar.Text = battarey.punj == -10 ? "Թևային" : Helper.FormatAngle(battarey.punj / 100);
            M3_Z.Text = battarey.Z.ToString();
            return true;
        }

        public void SetAxkFront(int front)
        {
            AXKF_Frot.Text = front.ToString();
        }

        public void SetPointToTarget(Point p)
        {
            Target_X.Text = p.X.ToString();
            Target_Y.Text = p.Y.ToString();
            Target_H.Text = p.H.ToString();
        }
    }
}
