using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nemesis
{
    public partial class Martakarg
    {
        public void InitializeMartakarg(BaseEntities.Martakarg mk)
        {
            //Հիմնական Դիտակետեր
            try
            {
                HDN_x.Text = mk.Ditaketer.Where(d => d.Name == "ՀԴՆ Հիմնական").First().X.ToString();
                HDN_y.Text = mk.Ditaketer.Where(d => d.Name == "ՀԴՆ Հիմնական").First().Y.ToString();
                HDN_h.Text = mk.Ditaketer.Where(d => d.Name == "ՀԴՆ Հիմնական").First().H.ToString();

                HRM1_x.Text = mk.Ditaketer.Where(d => d.Name == "1 ՀՄ Հիմնական").First().X.ToString();
                HRM1_y.Text = mk.Ditaketer.Where(d => d.Name == "1 ՀՄ Հիմնական").First().Y.ToString();
                HRM1_h.Text = mk.Ditaketer.Where(d => d.Name == "1 ՀՄ Հիմնական").First().H.ToString();

                HRM2_x.Text = mk.Ditaketer.Where(d => d.Name == "2 ՀՄ Հիմնական").First().X.ToString();
                HRM2_y.Text = mk.Ditaketer.Where(d => d.Name == "2 ՀՄ Հիմնական").First().Y.ToString();
                HRM2_h.Text = mk.Ditaketer.Where(d => d.Name == "2 ՀՄ Հիմնական").First().H.ToString();

                HRM3_x.Text = mk.Ditaketer.Where(d => d.Name == "3 ՀՄ Հիմնական").First().X.ToString();
                HRM3_y.Text = mk.Ditaketer.Where(d => d.Name == "3 ՀՄ Հիմնական").First().Y.ToString();
                HRM3_h.Text = mk.Ditaketer.Where(d => d.Name == "3 ՀՄ Հիմնական").First().H.ToString();

                //Պահեստային Դիտակետեր
                P_HDN_x.Text = mk.Ditaketer.Where(d => d.Name == "ՀԴՆ Պահեստային").First().X.ToString();
                P_HDN_y.Text = mk.Ditaketer.Where(d => d.Name == "ՀԴՆ Պահեստային").First().Y.ToString();
                P_HDN_h.Text = mk.Ditaketer.Where(d => d.Name == "ՀԴՆ Պահեստային").First().H.ToString();

                P_HRM1_x.Text = mk.Ditaketer.Where(d => d.Name == "1 ՀՄ Պահեստային").First().X.ToString();
                P_HRM1_y.Text = mk.Ditaketer.Where(d => d.Name == "1 ՀՄ Պահեստային").First().Y.ToString();
                P_HRM1_h.Text = mk.Ditaketer.Where(d => d.Name == "1 ՀՄ Պահեստային").First().H.ToString();

                P_HRM2_x.Text = mk.Ditaketer.Where(d => d.Name == "2 ՀՄ Պահեստային").First().X.ToString();
                P_HRM2_y.Text = mk.Ditaketer.Where(d => d.Name == "2 ՀՄ Պահեստային").First().Y.ToString();
                P_HRM2_h.Text = mk.Ditaketer.Where(d => d.Name == "2 ՀՄ Պահեստային").First().H.ToString();

                P_HRM3_x.Text = mk.Ditaketer.Where(d => d.Name == "3 ՀՄ Պահեստային").First().X.ToString();
                P_HRM3_y.Text = mk.Ditaketer.Where(d => d.Name == "3 ՀՄ Պահեստային").First().Y.ToString();
                P_HRM3_h.Text = mk.Ditaketer.Where(d => d.Name == "3 ՀՄ Պահեստային").First().H.ToString();
            }
            catch(Exception)
            {
                
            }

            //Կրակային Դիրքեր ըստ 
            try
            {
                HM1HD1_x.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄ1ՀԴ").First().X.ToString();
                HM1HD1_y.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄ1ՀԴ").First().Y.ToString();
                HM1HD1_h.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄ1ՀԴ").First().H.ToString();
                HM1HD1_hu.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄ1ՀԴ").First().HU.ToString();
                HM1_dis.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄ1ՀԴ").First().Dis.ToString();

                HM1HD2_x.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄ2ՀԴ").First().X.ToString();
                HM1HD2_y.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄ2ՀԴ").First().Y.ToString();
                HM1HD2_h.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄ2ՀԴ").First().H.ToString();
                HM1HD2_hu.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄ2ՀԴ").First().HU.ToString();

                HM2HD1_x.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄ1ՀԴ").First().X.ToString();
                HM2HD1_y.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄ1ՀԴ").First().Y.ToString();
                HM2HD1_h.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄ1ՀԴ").First().H.ToString();
                HM2HD1_hu.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄ1ՀԴ").First().HU.ToString();
                HM2_dis.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄ1ՀԴ").First().Dis.ToString();

                HM2HD2_x.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄ2ՀԴ").First().X.ToString();
                HM2HD2_y.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄ2ՀԴ").First().Y.ToString();
                HM2HD2_h.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄ2ՀԴ").First().H.ToString();
                HM2HD2_hu.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄ2ՀԴ").First().HU.ToString();

                HM3HD1_x.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄ1ՀԴ").First().X.ToString();
                HM3HD1_y.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄ1ՀԴ").First().Y.ToString();
                HM3HD1_h.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄ1ՀԴ").First().H.ToString();
                HM3HD1_hu.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄ1ՀԴ").First().HU.ToString();
                HM3_dis.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄ1ՀԴ").First().Dis.ToString();

                HM3HD2_x.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄ2ՀԴ").First().X.ToString();
                HM3HD2_y.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄ2ՀԴ").First().Y.ToString();
                HM3HD2_h.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄ2ՀԴ").First().H.ToString();
                HM3HD2_hu.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄ2ՀԴ").First().HU.ToString();

                //Կրակային դիրքեր մարտկոցի կազմոց
                PHM1_x.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄՊահեստային").First().X.ToString();
                PHM1_y.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄՊահեստային").First().Y.ToString();
                PHM1_h.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄՊահեստային").First().H.ToString();
                PHM1_hu.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄՊահեստային").First().HU.ToString();
                PHM1_dis.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "1ՀրՄՊահեստային").First().Dis.ToString();

                PHM2_x.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄՊահեստային").First().X.ToString();
                PHM2_y.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄՊահեստային").First().Y.ToString();
                PHM2_h.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄՊահեստային").First().H.ToString();
                PHM2_hu.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄՊահեստային").First().HU.ToString();
                PHM2_dis.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "2ՀրՄՊահեստային").First().Dis.ToString();

                PHM3_x.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄՊահեստային").First().X.ToString();
                PHM3_y.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄՊահեստային").First().Y.ToString();
                PHM3_h.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄՊահեստային").First().H.ToString();
                PHM3_hu.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄՊահեստային").First().HU.ToString();
                PHM3_dis.Text = mk.KrakayinDirqer.Where(kd => kd.Name == "3ՀրՄՊահեստային").First().Dis.ToString();
            }
            catch(Exception)
            {

            }
        }
    }
}