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
        public static Battarey GetBattareyData(KrakayinDirq dirq1, KrakayinDirq dirq2, Ditaket ditaket, Target target, TaskInfo taskinfo, Kayficents kfs = null, Texagrakan Hramanatarakan = null)
        {
            try
            {
                Battarey Data = new Battarey();
                var texagrakan = GetTexagrakan(ditaket, target);
                bool dasaknerov = taskinfo.Positions == "Դասակի Կազմով";

                var T1 = GetTexagrakan(dirq1, target);

                Data.First.Davarot = T1.Alpha;
                Data.First.Distance = T1.Distance;


                if ((Math.Abs(Data.First.Davarot - dirq1.HU)) > 30 && Data.First.Davarot > dirq1.HU)
                {
                    Data.First.Davarot = Data.First.Davarot - dirq1.HU - 60;
                }
                else
                if ((Math.Abs(Data.First.Davarot - dirq1.HU)) > 30 && Data.First.Davarot < dirq1.HU)
                {
                    Data.First.Davarot = Data.First.Davarot - dirq1.HU + 60;
                }
                else
                {
                    Data.First.Davarot = Data.First.Davarot - dirq1.HU;
                }


                if (Hramanatarakan != null)
                {
                    Data.PS = T1.Alpha - Hramanatarakan.Alpha;
                    Data.KDDirection = Data.PS > 0 ? "ՁԱԽ" : "ԱՋ";
                    Data.PS = Math.Round(Math.Abs(Data.PS), 2);
                    if (Data.PS > 30)
                    {
                        Data.PS = Math.Abs(60 - Data.PS);
                    }
                }
                else
                {
                    Data.PS = Math.Abs(Math.Round(Math.Acos((target.X - dirq1.X) / Data.First.Distance) * 9.55414, 2)
                        - texagrakan.Alpha);
                    if (Data.PS > 30)
                    {
                        Data.PS = Math.Abs(60 - Data.PS);
                    }
                    Data.KDDirection = texagrakan.Alpha > Math.Round(Math.Acos((target.X - dirq1.X) / Data.First.Distance) * 9.55414, 2) ? "ԱՋ" : "ՁԱԽ";
                }


                if (Data.PS < 5)
                {
                    Data.SHU = Math.Round(Data.PS / (0.01 * Data.First.Distance), 2);
                    Data.KU = Math.Round(texagrakan.Distance / Data.First.Distance, 1);
                }

                Data.punj = target.Front / (0.004 * Data.First.Distance);

                Data.KMH = Math.Round(((target.Front - 90) / (0.004 * Data.First.Distance)) / 100, 2);

                if (dasaknerov)
                {
                    var T2 = GetTexagrakan(dirq2, target);
                    Data.Second.Davarot = T2.Alpha;
                    Data.Second.Distance = T2.Distance;
                    Data.KMH = Math.Round((dirq1.Dis / (0.001 * Data.First.Distance)) / 100, 2);
                    if (target.Y - dirq2.Y < 0) Data.Second.Davarot = 60 - Data.Second.Davarot;
                    Data.Second.Davarot = (Math.Abs(Data.Second.Davarot - dirq2.HU)) > 30 && Data.Second.Davarot > dirq2.HU ? Data.Second.Davarot - dirq2.HU - 60 : Data.Second.Davarot - dirq2.HU;
                }


                //double oo = Math.Asin(double.Parse(l5HD.Text) / d);
                List<CoreInfo> GNS = new List<CoreInfo>();
                string core = "";

                var OriginalDistances = new double[2] { Data.First.Distance, Data.Second.Distance };
                var OriginalDavarots = new double[2] { Data.First.Davarot, Data.Second.Davarot };

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (kfs != null && taskinfo.SelectedCore != "Ավտոմատ լիցք" && taskinfo.Kayficents == "Հաշվարկային")
                {
                    if (kfs.K_Dist != 0)
                    {

                        Data.First.DistanceHashvarkayin = Data.First.Distance + (Data.First.Distance * 0.01 * kfs.K_Dist);
                        Data.Second.DistanceHashvarkayin = Data.Second.Distance + (Data.Second.Distance * 0.01 * kfs.K_Dist);

                        Data.First.DavarotHashvarkayin = Data.First.Davarot + kfs.K_Davarot;
                        Data.Second.DavarotHashvarkayin = Data.Second.Davarot + kfs.K_Davarot;
                    }
                    else if (kfs.Distance1 != 0)
                    {
                        var kfList = new List<List<double>>
                        {
                            new List<double> { kfs.Distance1, kfs.DeltaDistance1, kfs.DeltaDavarot1 },
                            new List<double> { kfs.Distance2, kfs.DeltaDistance2, kfs.DeltaDavarot2 },
                            new List<double> { kfs.Distance3, kfs.DeltaDistance3, kfs.DeltaDavarot3 }
                        };

                        //1111111111111111111111111111111111/////////////////////////////////////////////////////////
                        var closest1 = kfList[0];
                        var dif1 = Math.Abs(kfList[0][0] - Data.First.Distance);
                        double Dx1 = (kfList[1][1] - kfList[0][1]) / (kfList[1][0] - kfList[0][0]);
                        double Alphax1 = (kfList[1][2] - kfList[0][2]) / (kfList[1][0] - kfList[0][0]);
                        if (Math.Abs(kfList[1][0] - Data.First.Distance) < dif1)
                        {
                            closest1 = kfList[1];
                            dif1 = Math.Abs(kfList[1][0] - Data.First.Distance);
                            Dx1 = (kfList[2][1] - kfList[1][1]) / (kfList[2][0] - kfList[1][0]);
                            Alphax1 = (kfList[2][2] - kfList[1][2]) / (kfList[2][0] - kfList[1][0]);
                        }
                        if (Math.Abs(kfList[2][0] - Data.First.Distance) < dif1)
                        {
                            closest1 = kfList[2];
                            dif1 = Math.Abs(kfList[2][0] - Data.First.Distance);
                            Dx1 = (kfList[2][1] - kfList[1][1]) / (kfList[2][0] - kfList[1][0]);
                            Alphax1 = (kfList[2][2] - kfList[1][2]) / (kfList[2][0] - kfList[1][0]);
                        }
                        Data.First.DistanceHashvarkayin = Data.First.Distance + closest1[1] + (Data.First.Distance > closest1[0] ? dif1 * Dx1 : dif1 * Dx1 * -1);
                        Data.First.DavarotHashvarkayin = Data.First.Davarot + closest1[2] + (Data.First.Distance > closest1[0] ? dif1 * Alphax1 : dif1 * Alphax1 * -1);
                        //1111111111111111111111111111111111/////////////////////////////////////////////////////////

                        //2222222222222222222222222222222222/////////////////////////////////////////////////////////
                        var closest2 = kfList[0];
                        var dif2 = Math.Abs(kfList[0][0] - Data.Second.Distance);
                        double Dx2 = (kfList[1][1] - kfList[0][1]) / (kfList[1][0] - kfList[0][0]);
                        double Alphax2 = (kfList[1][2] - kfList[0][2]) / (kfList[1][0] - kfList[0][0]);
                        if (Math.Abs(kfList[1][0] - Data.Second.Distance) < dif2)
                        {
                            closest2 = kfList[1];
                            dif2 = Math.Abs(kfList[1][0] - Data.Second.Distance);
                            Dx2 = (kfList[2][1] - kfList[1][1]) / (kfList[2][0] - kfList[1][0]);
                            Alphax2 = (kfList[2][2] - kfList[1][2]) / (kfList[2][0] - kfList[1][0]);
                        }
                        if (Math.Abs(kfList[2][0] - Data.Second.Distance) < dif2)
                        {
                            closest2 = kfList[2];
                            dif2 = Math.Abs(kfList[2][0] - Data.Second.Distance);
                            Dx2 = (kfList[2][1] - kfList[1][1]) / (kfList[2][0] - kfList[1][0]);
                            Alphax2 = (kfList[2][2] - kfList[1][2]) / (kfList[2][0] - kfList[1][0]);
                        }
                        Data.Second.DistanceHashvarkayin = Data.Second.Distance + closest2[1] + (Data.Second.Distance > closest2[0] ? dif2 * Dx2 : dif2 * Dx2 * -1);
                        Data.Second.DavarotHashvarkayin = Data.Second.Davarot + closest2[2] + (Data.Second.Distance > closest2[0] ? dif2 * Alphax2 : dif2 * Alphax2 * -1);
                        //2222222222222222222222222222222222/////////////////////////////////////////////////////////
                    }
                }
                else
                {
                    Data.First.DistanceHashvarkayin = Data.First.Distance;
                    Data.First.DavarotHashvarkayin = Data.First.Davarot;

                    Data.Second.DistanceHashvarkayin = Data.Second.Distance;
                    Data.Second.DavarotHashvarkayin = Data.Second.Davarot;
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                GNS = Core.CoreAutoSelection(taskinfo.SelectedCore, taskinfo.ShootingType, (int)Data.First.DistanceHashvarkayin, (int)Data.Second.DistanceHashvarkayin, dirq1.H, taskinfo.Paytucich, ref core);
                Data.Core = core;
                double NT1 = 0, NT2 = 0;
                if (GNS != null)
                {
                    NT1 = GNS[0] != null ? (Data.First.DistanceHashvarkayin - GNS[0].TDistance) / GNS[0].DX : -10000;
                    NT2 = GNS[1] != null ? (Data.Second.DistanceHashvarkayin - GNS[1].TDistance) / GNS[1].DX : -10000;
                    double deltax1 = NT1, deltax2 = NT2;
                    int[] ds = new int[7];
                    ds[0] = 0; ds[1] = 500; ds[2] = 1000; ds[3] = 1500; ds[4] = 2000; ds[5] = 2500; ds[6] = 3000;
                    int closest1 = ds.Where(i => Math.Abs(dirq1.H - i) <= 250).First();
                    int closest2 = ds.Where(i => Math.Abs(dirq2.H - i) <= 250).First();


                    switch (closest1)
                    {
                        case 0:
                            switch (taskinfo.ShootingType)
                            {
                                case "Գետնամերձ":
                                    NT1 = NT1 != -10000 ? GNS[0].pr0 + NT1 : -1;
                                    break;
                                case "Մարտիրային":
                                    NT1 = NT1 != -10000 ? GNS[0].pr0 - NT1 : -1;
                                    break;
                            }
                            break;
                        case 500:
                            switch (taskinfo.ShootingType)
                            {
                                case "Գետնամերձ":
                                    NT1 = NT1 != -10000 ? GNS[0].pr500 + NT1 : -1;
                                    break;
                                case "Մարտիրային":
                                    NT1 = NT1 != -10000 ? GNS[0].pr500 - NT1 : -1;
                                    break;
                            }
                            break;
                        case 1000:
                            switch (taskinfo.ShootingType)
                            {
                                case "Գետնամերձ":
                                    NT1 = NT1 != -10000 ? GNS[0].pr1000 + NT1 : -1;
                                    break;
                                case "Մարտիրային":
                                    NT1 = NT1 != -10000 ? GNS[0].pr1000 - NT1 : -1;
                                    break;
                            }
                            break;
                        case 1500:
                            switch (taskinfo.ShootingType)
                            {
                                case "Գետնամերձ":
                                    NT1 = NT1 != -10000 ? GNS[0].pr1500 + NT1 : -1;
                                    break;
                                case "Մարտիրային":
                                    NT1 = NT1 != -10000 ? GNS[0].pr1500 - NT1 : -1;
                                    break;
                            }
                            break;
                        case 2000:
                            switch (taskinfo.ShootingType)
                            {
                                case "Գետնամերձ":
                                    NT1 = NT1 != -10000 ? GNS[0].pr2000 + NT1 : -1;
                                    break;
                                case "Մարտիրային":
                                    NT1 = NT1 != -10000 ? GNS[0].pr2000 - NT1 : -1;
                                    break;
                            }
                            break;
                        case 2500:
                            switch (taskinfo.ShootingType)
                            {
                                case "Գետնամերձ":
                                    NT1 = NT1 != -10000 ? GNS[0].pr2500 + NT1 : -1;
                                    break;
                                case "Մարտիրային":
                                    NT1 = NT1 != -10000 ? GNS[0].pr2500 - NT1 : -1;
                                    break;
                            }
                            break;
                        case 3000:
                            switch (taskinfo.ShootingType)
                            {
                                case "Գետնամերձ":
                                    NT1 = NT1 != -10000 ? GNS[0].pr3000 + NT1 : -1;
                                    break;
                                case "Մարտիրային":
                                    NT1 = NT1 != -10000 ? GNS[0].pr2500 - NT1 : -1;
                                    break;
                            }
                            break;
                    }

                    switch (closest2)
                    {
                        case 0:
                            switch (taskinfo.ShootingType)
                            {
                                case "Գետնամերձ":
                                    NT2 = NT2 != -10000 && GNS[1].pr0 != -1 ? GNS[1].pr0 + NT2 : -1;
                                    break;
                                case "Մարտիրային":
                                    NT2 = NT2 != -10000 && GNS[1].pr0 != -1 ? GNS[1].pr0 - NT2 : -1;
                                    break;
                            }
                            break;
                        case 500:
                            switch (taskinfo.ShootingType)
                            {
                                case "Գետնամերձ":
                                    NT2 = NT2 != -10000 && GNS[1].pr500 != -1 ? GNS[1].pr500 + NT2 : -1;
                                    break;
                                case "Մարտիրային":
                                    NT2 = NT2 != -10000 && GNS[1].pr500 != -1 ? GNS[1].pr500 - NT2 : -1;
                                    break;
                            }
                            break;
                        case 1000:
                            switch (taskinfo.ShootingType)
                            {
                                case "Գետնամերձ":
                                    NT2 = NT2 != -10000 && GNS[1].pr1000 != -1 ? GNS[1].pr1000 + NT2 : -1;
                                    break;
                                case "Մարտիրային":
                                    NT2 = NT2 != -10000 && GNS[1].pr1000 != -1 ? GNS[1].pr1000 - NT2 : -1;
                                    break;
                            }
                            break;
                        case 1500:
                            switch (taskinfo.ShootingType)
                            {
                                case "Գետնամերձ":
                                    NT2 = NT2 != -10000 && GNS[1].pr1500 != -1 ? GNS[1].pr1500 + NT2 : -1;
                                    break;
                                case "Մարտիրային":
                                    NT2 = NT2 != -10000 && GNS[1].pr1500 != -1 ? GNS[1].pr1500 - NT2 : -1;
                                    break;
                            }
                            break;
                        case 2000:
                            switch (taskinfo.ShootingType)
                            {
                                case "Գետնամերձ":
                                    NT2 = NT2 != -10000 && GNS[1].pr2000 != -1 ? GNS[1].pr2000 + NT2 : -1;
                                    break;
                                case "Մարտիրային":
                                    NT2 = NT2 != -10000 && GNS[1].pr2000 != -1 ? GNS[1].pr2000 - NT2 : -1;
                                    break;
                            }
                            break;
                        case 2500:
                            switch (taskinfo.ShootingType)
                            {
                                case "Գետնամերձ":
                                    NT2 = NT2 != -10000 && GNS[1].pr2500 != -1 ? GNS[1].pr2500 + NT2 : -1;
                                    break;
                                case "Մարտիրային":
                                    NT2 = NT2 != -10000 && GNS[1].pr2500 != -1 ? GNS[1].pr2500 - NT2 : -1;
                                    break;
                            }
                            break;
                        case 3000:
                            switch (taskinfo.ShootingType)
                            {
                                case "Գետնամերձ":
                                    NT2 = NT2 != -10000 && GNS[1].pr3000 != -1 ? GNS[1].pr3000 + NT2 : -1;
                                    break;
                                case "Մարտիրային":
                                    NT2 = NT2 != -10000 && GNS[1].pr3000 != -1 ? GNS[1].pr2500 - NT2 : -1;
                                    break;
                            }
                            break;
                    }

                    Data.First.Pricel = NT1 != -1 ? Math.Round(NT1, 0) : 0;
                    Data.Second.Pricel = NT2 != -1 ? Math.Round(NT2, 0) : 0;
                    Data.DeltaX = NT1 != -1 ? GNS[0].DX : 0;
                    if (taskinfo.Paytucich != "РГМ-2 (ОФ-462)" && GNS[0] != null)
                    {
                        int n1 = 0, n2 = 0;
                        switch (closest1)
                        {
                            case 0:
                                n1 = GNS[0].N0;
                                n2 = GNS[1] != null ? GNS[1].N0 : 0;
                                break;
                            case 500:
                                n1 = GNS[0].N500;
                                n2 = GNS[1] != null ? GNS[1].N500 : 0;
                                break;
                            case 1000:
                                n1 = GNS[0].N1000;
                                n2 = GNS[1] != null ? GNS[1].N1000 : 0;
                                break;
                            case 1500:
                                n1 = GNS[0].N1500;
                                n2 = GNS[1] != null ? GNS[1].N1500 : 0;
                                break;
                            case 2000:
                                n1 = GNS[0].N2000;
                                n2 = GNS[1] != null ? GNS[1].N2000 : 0;
                                break;
                            case 2500:
                                n1 = GNS[0].N2500;
                                n2 = GNS[1] != null ? GNS[1].N2500 : 0;
                                break;
                            case 3000:
                                n1 = GNS[0].N3000;
                                n2 = GNS[1] != null ? GNS[1].N3000 : 0;
                                break;
                        }
                        Data.DeltaN = Math.Round(0.001 * GNS[0].TDistance / GNS[0].VRV, 1);
                        Data.First.Paytucich = n1 + (deltax1 * Data.DeltaN);
                        if (GNS[1] != null)
                            Data.Second.Paytucich = n2 + (deltax2 * Data.DeltaN);
                    }

                    Data.Ttrichqayin = NT1 != -1 ? (int)GNS[0].T : 0;
                    Data.Z = NT1 != -1 ? GNS[0].Z : 0;
                    var Z_Rap = Data.Z;
                    if (kfs != null && taskinfo.SelectedCore != "Ավտոմատ լիցք" && kfs.Z_Reper != 0)
                    {
                        Z_Rap += int.Parse((100 * kfs.Z_Reper).ToString());
                    }
                    else if (kfs != null && taskinfo.SelectedCore != "Ավտոմատ լիցք" && kfs.DeltaDavarot1 != 0)
                    {
                        Z_Rap = 0;
                    }
                    if (GNS[0] != null && GNS[0].DX != 0)
                    {
                        double jump = GNS[0].DX != 0 ? target.Deepness / (3 * GNS[0].DX) : -1;
                        jump = jump > (int)jump && jump != -1 ? (int)jump + 1 : jump;
                        Data.Catk = target.Deepness >= 100 && jump != -1 ? jump : 0;
                    }
                    Data.First.DavarotHU = Math.Round(Data.First.DavarotHashvarkayin - (0.01 * Z_Rap), 2);
                    Data.Second.DavarotHU = Math.Round(Data.Second.DavarotHashvarkayin - (0.01 * Z_Rap), 2);
                }
                else
                {
                    Data.First.Pricel = 0;
                    Data.Second.Pricel = 0;
                }
                if (taskinfo.System == "152մմ Դ - 1")
                {
                    Data.First.Level = 30 + (target.H - dirq1.H) / (0.1 * Data.First.Distance);
                    Data.Second.Level = 30 + (target.H - dirq2.H) / (0.1 * Data.Second.Distance);
                }
                else
                {
                    try
                    {
                        Data.First.Level = Core.GetLevel((int)Data.First.Distance, Data.Core, taskinfo.ShootingType, dirq1.H, target.H, taskinfo.Paytucich);
                    }
                    catch
                    {
                        Data.First.Level = 0;
                    }
                    try
                    {
                        Data.Second.Level = Core.GetLevel((int)Data.Second.Distance, Data.Core, taskinfo.ShootingType, dirq2.H, target.H, taskinfo.Paytucich);
                    }
                    catch
                    {
                        Data.Second.Level = 0;
                    }
                }
                if (taskinfo.ShootingType == "Գետնամերձ" && Data.First.Pricel + ((Data.First.Level - 30) * 100) > 750)
                {
                    Data.First.Pricel = 0;
                    Data.First.Level = 0;
                }

                if (!dasaknerov)
                {
                    Data.Second.Davarot = 0;
                    Data.Second.Distance = 0;
                    Data.Second.DavarotHU = 0;
                    Data.Second.Level = 0;
                    Data.Second.Pricel = 0;
                }
                else
                {
                    if (taskinfo.ShootingType == "Գետնամերձ" && Data.Second.Pricel + ((Data.Second.Level - 30) * 100) > 750)
                    {
                        Data.Second.Pricel = 0;
                        Data.Second.Level = 0;
                    }
                }

                if (taskinfo.Positions == "Մարտկոցի Կազմով")
                {
                    Data.Second.Paytucich = 0;
                }

                //Data.DeltaN = 
                return Data;
            }
            catch
            {

                //throw;
                return new Battarey();
            }
        }

    }
}
