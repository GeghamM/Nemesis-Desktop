using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using BaseEntities;

namespace Nemesis
{
    public partial class Martakarg
    {
        private List<Ditaket> ReadDitaketer()
        {
            List<Ditaket> dks = new List<Ditaket>();

            try { dks.Add(new Ditaket("ՀԴՆ Հիմնական", int.Parse(HDN_x.Text), int.Parse(HDN_y.Text), int.Parse(HDN_h.Text))); } catch { }
            try { dks.Add(new Ditaket("1 ՀՄ Հիմնական", int.Parse(HRM1_x.Text), int.Parse(HRM1_y.Text), int.Parse(HRM1_h.Text))); } catch { }
            try { dks.Add(new Ditaket("2 ՀՄ Հիմնական", int.Parse(HRM2_x.Text), int.Parse(HRM2_y.Text), int.Parse(HRM2_h.Text))); } catch { }
            try { dks.Add(new Ditaket("3 ՀՄ Հիմնական", int.Parse(HRM3_x.Text), int.Parse(HRM3_y.Text), int.Parse(HRM3_h.Text))); } catch { }

            try { dks.Add(new Ditaket("ՀԴՆ Պահեստային", int.Parse(P_HDN_x.Text), int.Parse(P_HDN_y.Text), int.Parse(P_HDN_h.Text))); } catch { }
            try { dks.Add(new Ditaket("1 ՀՄ Պահեստային", int.Parse(P_HRM1_x.Text), int.Parse(P_HRM1_y.Text), int.Parse(P_HRM1_h.Text))); } catch { }
            try { dks.Add(new Ditaket("2 ՀՄ Պահեստային", int.Parse(P_HRM2_x.Text), int.Parse(P_HRM2_y.Text), int.Parse(P_HRM2_h.Text))); } catch { }
            try { dks.Add(new Ditaket("3 ՀՄ Պահեստային", int.Parse(P_HRM3_x.Text), int.Parse(P_HRM3_y.Text), int.Parse(P_HRM3_h.Text))); } catch { }

            return dks;
        }

        private List< KrakayinDirq> ReadKrakayinDirqer()
        {
            List <KrakayinDirq> kds = new List<KrakayinDirq>();

            try {kds.Add(new KrakayinDirq("1ՀրՄ1ՀԴ", int.Parse(HM1HD1_x.Text), int.Parse(HM1HD1_y.Text), int.Parse(HM1HD1_h.Text), int.Parse(HM1HD1_hu.Text), int.Parse(HM1_dis.Text))); } catch { }
            try {kds.Add(new KrakayinDirq("1ՀրՄ2ՀԴ", int.Parse(HM1HD2_x.Text), int.Parse(HM1HD2_y.Text), int.Parse(HM1HD2_h.Text), int.Parse(HM1HD2_hu.Text), int.Parse(HM1_dis.Text))); } catch { }

            try {kds.Add(new KrakayinDirq("2ՀրՄ1ՀԴ", int.Parse(HM2HD1_x.Text), int.Parse(HM2HD1_y.Text), int.Parse(HM2HD1_h.Text), int.Parse(HM2HD1_hu.Text), int.Parse(HM2_dis.Text))); } catch { }
            try {kds.Add( new KrakayinDirq("2ՀրՄ2ՀԴ", int.Parse(HM2HD2_x.Text), int.Parse(HM2HD2_y.Text), int.Parse(HM2HD2_h.Text), int.Parse(HM2HD2_hu.Text), int.Parse(HM2_dis.Text))); } catch { }

            try {kds.Add(new KrakayinDirq("3ՀրՄ1ՀԴ", int.Parse(HM3HD1_x.Text), int.Parse(HM3HD1_y.Text), int.Parse(HM3HD1_h.Text), int.Parse(HM3HD1_hu.Text), int.Parse(HM3_dis.Text))); } catch { }
            try {kds.Add(new KrakayinDirq("3ՀրՄ2ՀԴ", int.Parse(HM3HD2_x.Text), int.Parse(HM3HD2_y.Text), int.Parse(HM3HD2_h.Text), int.Parse(HM3HD2_hu.Text), int.Parse(HM3_dis.Text))); } catch { }

            try {kds.Add( new KrakayinDirq("1ՀրՄՊահեստային", int.Parse(PHM1_x.Text), int.Parse(PHM1_y.Text), int.Parse(PHM1_h.Text), int.Parse(PHM1_hu.Text), int.Parse(PHM1_dis.Text))); } catch { }
            try {kds.Add( new KrakayinDirq("2ՀրՄՊահեստային", int.Parse(PHM2_x.Text), int.Parse(PHM2_y.Text), int.Parse(PHM2_h.Text), int.Parse(PHM2_hu.Text), int.Parse(PHM2_dis.Text))); } catch { }
            try {kds.Add( new KrakayinDirq("3ՀրՄՊահեստային", int.Parse(PHM3_x.Text), int.Parse(PHM3_y.Text), int.Parse(PHM3_h.Text), int.Parse(PHM3_hu.Text), int.Parse(PHM3_dis.Text))); } catch { }

            return kds;
        }
    }
}
