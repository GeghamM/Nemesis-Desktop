using BaseEntities;
using DataAccessLayer;
using ActionsLayer;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Linq;
using System.IO;
using Nemesis.Windows.PrintWindows;
using BaseService;
using Nemesis.Windows.Partials;

namespace Nemesis
{
    public partial class Task : Window
    {
        public static string SourcePath
        {
            get
            {
                return Directory.GetCurrentDirectory();
            }
        }
        public static bool HasReference { set; get; }
        public Task()
        {
            HasReference = true;
            InitializeComponent();
            InitializeComboBoxes();
            HandlePaytucichTypes(Paytucich.Text);
            InatializeImageSources();
            AXKName.Items.Clear();
            AXKName.Items.Add(new ComboBoxItem() { Content = "Ոչ Պլանային" });
            AXKName.SelectedItem = AXKName.Items[0];
            var targets = DataReader.GetAXKTargets();
            foreach (var item in targets)
            {
                AXKName.Items.Add(new ComboBoxItem() { Content = item.ID });
            }
            HandleTaskTypes(TaskType.Text);
            KayficentDropdownClosed(this, new EventArgs());
        }

        private void InatializeImageSources()
        {
            /////////////////////////////////////////////////////////////////////////////////////
            BitmapImage clear = new BitmapImage();
            clear.BeginInit();
            clear.UriSource = new Uri(SourcePath + "\\Images\\Clear.ico");
            clear.EndInit();
            Clear.Source = clear;
            /////////////////////////////////////////////////////////////////////////////////////
            BitmapImage plus = new BitmapImage();
            plus.BeginInit();
            plus.UriSource = new Uri(SourcePath + "\\Images\\Plus.ico");
            plus.EndInit();
            Plus.Source = plus;
            /////////////////////////////////////////////////////////////////////////////////////
            BitmapImage screen = new BitmapImage();
            screen.BeginInit();
            screen.UriSource = new Uri(SourcePath + "\\Images\\Screenshot.ico");
            screen.EndInit();
            ScreenShot.Source = screen;
            /////////////////////////////////////////////////////////////////////////////////////
            BitmapImage sob = new BitmapImage();
            sob.BeginInit();
            sob.UriSource = new Uri(SourcePath + "\\Images\\Sob.ico");
            sob.EndInit();
            SobNote.Source = sob;
            /////////////////////////////////////////////////////////////////////////////////////
            BitmapImage Hranot = new BitmapImage();
            Hranot.BeginInit();
            Hranot.UriSource = new Uri(SourcePath + "\\Images\\Hranot.ico");
            Hranot.EndInit();
            HranotNote.Source = Hranot;
            /////////////////////////////////////////////////////////////////////////////////////
            BitmapImage Ditarkum = new BitmapImage();
            Ditarkum.BeginInit();
            Ditarkum.UriSource = new Uri(SourcePath + "\\Images\\Ditarkum.jpg");
            Ditarkum.EndInit();
            DitarkumImage.Source = Ditarkum;
            /////////////////////////////////////////////////////////////////////////////////////
        }

        public void InitializeComboBoxes()
        {
            Initializer.InitializeSystems(ref Systems);
            Initializer.InitializeTaskTypes(ref TaskType);
            Initializer.InitializeShootingMethod(ref ShootingMethod, false);
            Initializer.InitializeShootingCore(ref ShootingCore, "");
            Initializer.InitializePositions(ref Positions);
            Initializer.InitializeDitakets(ref Ditaket1);
            Initializer.InitializeDitakets(ref Ditaket2);
            Initializer.InitializePaytucich(ref Paytucich, Systems.Text);
            Initializer.InitializeAXKType(ref AXKType);
            Initializer.InitializeTexamas(ref Texamas);
        }

        protected override void OnClosed(EventArgs e)
        {
            HasReference = false;
            base.OnClosed(e);
        }

        protected void AddAXKToolsToToolBar()
        {
            MainToolbarStackPanel.Children.Add(new ComboBox() { Name = "AXKMethod", Margin = new Thickness(5), FontSize = 15, FontWeight = FontWeights.ExtraBold, Width = 200 });
            MainToolbarStackPanel.Children.Add(new ComboBox() { Name = "Texamas", Margin = new Thickness(5), FontSize = 15, FontWeight = FontWeights.ExtraBold, Width = 200 });
        }


        private void TargetLostFocus(object sender, RoutedEventArgs e)
        {
            if (((sender is ComboBox) && (sender as ComboBox).Name == Ditaket2.Name) || (sender is Task)) { } else DKFokusLost(this, new RoutedEventArgs());
            if (!string.IsNullOrWhiteSpace(Target_X.Text) &&
               !string.IsNullOrWhiteSpace(Target_Y.Text) &&
               !string.IsNullOrWhiteSpace(Target_H.Text) &&
               !string.IsNullOrWhiteSpace(Target_FrontMeter.Text) &&
               !string.IsNullOrWhiteSpace(Target_Deepness.Text))
            {

                if (TaskType.Text != "Բևեռային եղանակով" && TaskType.Text != "Զուգորդված դիտարկում")
                {
                    DKTexagrakanDzax();
                }
                Texagrakan Hramanatarakan = null;
                if (TaskType.Text == "Բևեռային եղանակով")
                {
                    Hramanatarakan = new Texagrakan()
                    {
                        Distance = double.Parse(D_Hram2.Text),
                        Alpha = double.Parse(Helper.RepresentStrings(Alpha_Hram2.Text)),
                        M = double.Parse(M_NSH2.Text)
                    };
                }

                if (Positions.Text == "Դասակի Կազմով")
                {
                    /////////////////////////////////////////////////////////////////////////////////////
                    SetBattarey1Data(Service.GetBattareyData(
                        Owin.Martakarg.GetKD(1, 1),
                        Owin.Martakarg.GetKD(1, 2),
                        Owin.Martakarg.GetDitaket(Ditaket2.Text),
                        GetTarget(),
                        GetTaskInfo(),
                        IsWithKayficent() || IsWithHavelyal() ? GetKayficentsFromView() : null,
                        Hramanatarakan
                        )); ;
                    /////////////////////////////////////////////////////////////////////////////////////
                    SetBattarey2Data(Service.GetBattareyData(
                        Owin.Martakarg.GetKD(2, 1),
                        Owin.Martakarg.GetKD(2, 2),
                        Owin.Martakarg.GetDitaket(Ditaket2.Text),
                        GetTarget(),
                        GetTaskInfo(),
                        IsWithKayficent() || IsWithHavelyal() ? GetKayficentsFromView() : null,
                        Hramanatarakan
                        )); ;
                    /////////////////////////////////////////////////////////////////////////////////////
                    SetBattarey3Data(Service.GetBattareyData(
                        Owin.Martakarg.GetKD(3, 1),
                        Owin.Martakarg.GetKD(3, 2),
                        Owin.Martakarg.GetDitaket(Ditaket2.Text),
                        GetTarget(),
                        GetTaskInfo(),
                        IsWithKayficent() || IsWithHavelyal() ? GetKayficentsFromView() : null,
                        Hramanatarakan
                        )); ;
                    /////////////////////////////////////////////////////////////////////////////////////
                }
                else
                {
                    /////////////////////////////////////////////////////////////////////////////////////
                    SetBattarey1Data(Service.GetBattareyData(
                        Owin.Martakarg.GetKD(1, 0),
                        Owin.Martakarg.GetKD(1, 0),
                        Owin.Martakarg.GetDitaket(Ditaket2.Text),
                        GetTarget(),
                        GetTaskInfo(),
                        IsWithKayficent() || IsWithHavelyal() ? GetKayficentsFromView() : null,
                        Hramanatarakan
                        )); ;
                    /////////////////////////////////////////////////////////////////////////////////////
                    SetBattarey2Data(Service.GetBattareyData(
                        Owin.Martakarg.GetKD(2, 0),
                        Owin.Martakarg.GetKD(2, 0),
                        Owin.Martakarg.GetDitaket(Ditaket2.Text),
                        GetTarget(),
                        GetTaskInfo(),
                        IsWithKayficent() || IsWithHavelyal() ? GetKayficentsFromView() : null,
                        Hramanatarakan
                        )); ;
                    /////////////////////////////////////////////////////////////////////////////////////
                    SetBattarey3Data(Service.GetBattareyData(
                        Owin.Martakarg.GetKD(3, 0),
                        Owin.Martakarg.GetKD(3, 0),
                        Owin.Martakarg.GetDitaket(Ditaket2.Text),
                        GetTarget(),
                        GetTaskInfo(),
                        IsWithKayficent() || IsWithHavelyal() ? GetKayficentsFromView() : null,
                        Hramanatarakan
                        )); ;
                    /////////////////////////////////////////////////////////////////////////////////////
                }
            }
        }

        private void FrontConvertKeyup(object sender, KeyEventArgs e)
        {
            HandleFront((sender as TextBox).Name, D_Hram2.Text, TargetFront_AB.Text, Target_FrontMeter.Text);
        }

        private void OnTargetNumberChanged(object sender, TextChangedEventArgs e)
        {
            if (TaskType.Text == "Պլանային (Նշ. ցանկից)")
            {
                var target = TargetManager.GetTargetByNumber((sender as TextBox).Text, out _);
                if (target != null)
                {
                    SetTarget(target);
                    HandleFront("Target_FrontMeter", D_Hram2.Text, TargetFront_AB.Text, Target_FrontMeter.Text);
                    TargetLostFocus(this, new RoutedEventArgs());
                }
                else
                {
                    ReSetTarget();
                    ResetBattares();
                }

            }
        }

        private void TaskTypechanged(object sender, EventArgs e)
        {
            HandleTaskTypes(TaskType.Text);
            HandleCameraSelections();

            if (TaskType.Text == "ԱԱԿ" || TaskType.Text == "ՇԱԿ")
            {
                if (double.TryParse(AXK_aj_X.Text, out _) &&
                    double.TryParse(AXK_aj_Y.Text, out _) &&
                    double.TryParse(AXK_aj_H.Text, out _) &&
                    double.TryParse(AXK_dzax_X.Text, out _) &&
                    double.TryParse(AXK_dzax_Y.Text, out _) &&
                    double.TryParse(AXK_dzax_H.Text, out _))
                {
                    var Retval = Service.HandleAXKData(Ditaket1.Text, Ditaket2.Text, GetTaskInfo(), GetAXKTarget(), IsWithKayficent() || IsWithHavelyal() ? GetKayficentsFromView() : null);
                    if (Retval.P2 != null) SetAXKpointLeft(Retval.P2);
                    if (Retval.P1 != null) SetAXKpointRight(Retval.P1);
                    if (Retval.Tex1 != null) SetTexagrakanaj(Retval.Tex1);
                    if (Retval.Tex2 != null) SetTexagrakandzax(Retval.Tex2);
                    if (Retval.Bat1 != null) SetBattarey1Data(Retval.Bat1);
                    if (Retval.Bat2 != null) SetBattarey2Data(Retval.Bat2);
                    if (Retval.Bat3 != null) SetBattarey3Data(Retval.Bat3);
                    SetAxkFront(Retval.Front);

                }
            }
        }

        private void HandleCameraSelections()
        {
            if (Ditaket2.Text.Contains("Տեսախցիկ"))
            {
                AzimutPanel2.Visibility = Visibility.Visible;
                Alpha_Hram2.IsEnabled = false;
            }
            else
            {
                AzimutPanel2.Visibility = Visibility.Collapsed;
                if (TaskType.Text != "Պլանային (Նշ. ցանկից)" && TaskType.Text != "ՈՒղղանկյուն կոորդինատներով")
                {
                    if (!((TaskType.Text == "ԱԱԿ" || TaskType.Text == "ՇԱԿ") && AXKType.Text == "ՈՒղղանկյուն")) Alpha_Hram2.IsEnabled = true;
                }
            }
            if (Ditaket1.Text.Contains("Տեսախցիկ"))
            {
                AzimutPanel1.Visibility = Visibility.Visible;
                Alpha_Hram1.IsEnabled = false;
            }
            else
            {
                AzimutPanel1.Visibility = Visibility.Collapsed;
                if (TaskType.Text != "Պլանային (Նշ. ցանկից)" && TaskType.Text != "ՈՒղղանկյուն կոորդինատներով")
                {
                    if (!((TaskType.Text == "ԱԱԿ" || TaskType.Text == "ՇԱԿ") && AXKType.Text == "ՈՒղղանկյուն")) Alpha_Hram1.IsEnabled = true;
                }
            }
        }

        private void DKFokusLost(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TaskType.Text == "Զուգորդված դիտարկում" &&
                    double.TryParse(Helper.RepresentStrings(Alpha_Hram1.Text), out _) &&
                    double.TryParse(Helper.RepresentStrings(Alpha_Hram2.Text), out _) &&
                    double.TryParse(M_NSH1.Text, out _) &&
                    double.TryParse(M_NSH2.Text, out _))
                {
                    ParallelView();
                    return;
                }
                if (TaskType.Text == "Բևեռային եղանակով")
                {
                    var point = Service.GetPoint(Owin.Martakarg.GetDitaket(Ditaket2.Text), new Texagrakan()
                    {
                        Distance = double.Parse(D_Hram2.Text),
                        Alpha = double.Parse(Helper.RepresentStrings(Alpha_Hram2.Text)),
                        M = double.Parse(M_NSH2.Text)
                    });

                    SetPointToTarget(point);

                    TargetLostFocus(this, e);

                }
                if ((TaskType.Text == "ԱԱԿ" || TaskType.Text == "ՇԱԿ"))
                {
                    if (double.TryParse(Helper.RepresentStrings(Alpha_Hram2.Text), out _) &&
                        double.TryParse(D_Hram2.Text, out _) && double.TryParse(M_NSH2.Text, out _))
                    {
                        SetAXKpointLeft(Service.GetPoint(Owin.Martakarg.GetDitaket(Ditaket2.Text),
                                        new Texagrakan(double.Parse(Helper.RepresentStrings(Alpha_Hram2.Text)), double.Parse(D_Hram2.Text), double.Parse(M_NSH2.Text), 1)));
                    }

                    if (double.TryParse(Helper.RepresentStrings(Alpha_Hram1.Text), out _) &&
                        double.TryParse(D_Hram1.Text, out _) && double.TryParse(M_NSH1.Text, out _))
                    {
                        SetAXKpointRight(Service.GetPoint(Owin.Martakarg.GetDitaket(Ditaket1.Text),
                                        new Texagrakan(double.Parse(Helper.RepresentStrings(Alpha_Hram1.Text)), double.Parse(D_Hram1.Text), double.Parse(M_NSH2.Text), 1)));
                    }
                    if (double.TryParse(Helper.RepresentStrings(Alpha_Hram2.Text), out _) &&
                        double.TryParse(D_Hram2.Text, out _) && double.TryParse(M_NSH2.Text, out _) &&
                        double.TryParse(Helper.RepresentStrings(Alpha_Hram1.Text), out _) &&
                        double.TryParse(D_Hram1.Text, out _) && double.TryParse(M_NSH1.Text, out _))
                    {
                        var Retval = Service.HandleAXKData(Ditaket1.Text, Ditaket2.Text, GetTaskInfo(), GetAXKTarget(), IsWithKayficent() || IsWithHavelyal() ? GetKayficentsFromView() : null);
                        if (Retval.P2 != null) SetAXKpointLeft(Retval.P2);
                        if (Retval.P1 != null) SetAXKpointRight(Retval.P1);
                        if (Retval.Tex1 != null) SetTexagrakanaj(Retval.Tex1);
                        if (Retval.Tex2 != null) SetTexagrakandzax(Retval.Tex2);
                        if (Retval.Bat1 != null) SetBattarey1Data(Retval.Bat1);
                        if (Retval.Bat2 != null) SetBattarey2Data(Retval.Bat2);
                        if (Retval.Bat3 != null) SetBattarey3Data(Retval.Bat3);
                        SetAxkFront(Retval.Front);
                    }
                }
            }
            catch { }
        }

        private void AXKFokusLost(object sender, RoutedEventArgs e)
        {
            var box = sender as TextBox;
            if (box.Name == "AXK_dzax_X" || box.Name == "AXK_dzax_Y" || box.Name == "AXK_dzax_H" ||
                box.Name == "AXK_aj_X" || box.Name == "AXK_aj_Y" || box.Name == "AXK_aj_H")
                try
                {
                    if (TaskType.Text == "ԱԱԿ" || TaskType.Text == "ՇԱԿ")
                    {

                        if (double.TryParse(AXK_dzax_X.Text, out _) && double.TryParse(AXK_dzax_Y.Text, out _) && double.TryParse(AXK_dzax_H.Text, out _))
                        {
                            SetTexagrakandzax(Service.GetTexagrakan(Owin.Martakarg.GetDitaket(Ditaket2.Text),
                                new Target(new BaseEntities.Point(int.Parse(AXK_dzax_X.Text), int.Parse(AXK_dzax_Y.Text), int.Parse(AXK_dzax_H.Text)))));
                        }

                        if (double.TryParse(AXK_aj_X.Text, out _) && double.TryParse(AXK_aj_Y.Text, out _) && double.TryParse(AXK_aj_H.Text, out _))
                        {
                            SetTexagrakanaj(Service.GetTexagrakan(Owin.Martakarg.GetDitaket(Ditaket2.Text),
                                new Target(new BaseEntities.Point(int.Parse(AXK_aj_X.Text), int.Parse(AXK_aj_Y.Text), int.Parse(AXK_aj_H.Text)))));
                        }

                        if (double.TryParse(AXK_aj_X.Text, out _) &&
                            double.TryParse(AXK_aj_Y.Text, out _) &&
                            double.TryParse(AXK_aj_H.Text, out _) &&
                            double.TryParse(AXK_dzax_X.Text, out _) &&
                            double.TryParse(AXK_dzax_Y.Text, out _) &&
                            double.TryParse(AXK_dzax_H.Text, out _))
                        {
                            var Retval = Service.HandleAXKData(Ditaket1.Text, Ditaket2.Text, GetTaskInfo(), GetAXKTarget(), IsWithKayficent() || IsWithHavelyal() ? GetKayficentsFromView() : null);
                            if (Retval.P2 != null) SetAXKpointLeft(Retval.P2);
                            if (Retval.P1 != null) SetAXKpointRight(Retval.P1);
                            if (Retval.Tex1 != null) SetTexagrakanaj(Retval.Tex1);
                            if (Retval.Tex2 != null) SetTexagrakandzax(Retval.Tex2);
                            if (Retval.Bat1 != null) SetBattarey1Data(Retval.Bat1);
                            if (Retval.Bat2 != null) SetBattarey2Data(Retval.Bat2);
                            if (Retval.Bat3 != null) SetBattarey3Data(Retval.Bat3);
                            SetAxkFront(Retval.Front);
                        }
                    }
                }
                catch (Exception)
                {

                }
        }
        private void HandleCores()
        {
            var selected = ShootingCore.SelectedIndex;
            Initializer.InitializeShootingCore(ref ShootingCore, Paytucich.Text);
            if (Paytucich.Text == "Т-7 (С-463)" && selected == 6)
                ShootingCore.SelectedItem = ShootingCore.Items[5];
            else
                ShootingCore.SelectedItem = ShootingCore.Items[selected];
        }
        private void DitaketClosed(object sender, EventArgs e)
        {
            DKFokusLost(sender, new RoutedEventArgs());
            TargetLostFocus(sender, new RoutedEventArgs());
        }
        private void Count(object sender, EventArgs e)
        {
            HandleCores();
            HandleKMH();
            if (sender is ComboBox)
            {
                KayficentDropdownClosed(this, new EventArgs());
            }
            if (TaskType.Text == "ԱԱԿ" || TaskType.Text == "ՇԱԿ")
            {
                if (double.TryParse(AXK_aj_X.Text, out _) &&
                    double.TryParse(AXK_aj_Y.Text, out _) &&
                    double.TryParse(AXK_aj_H.Text, out _) &&
                    double.TryParse(AXK_dzax_X.Text, out _) &&
                    double.TryParse(AXK_dzax_Y.Text, out _) &&
                    double.TryParse(AXK_dzax_H.Text, out _))
                {
                    var Retval = Service.HandleAXKData(Ditaket1.Text, Ditaket2.Text, GetTaskInfo(), GetAXKTarget(), IsWithKayficent() || IsWithHavelyal() ? GetKayficentsFromView() : null);
                    if (Retval.P2 != null) SetAXKpointLeft(Retval.P2);
                    if (Retval.P1 != null) SetAXKpointRight(Retval.P1);
                    if (Retval.Tex1 != null) SetTexagrakanaj(Retval.Tex1);
                    if (Retval.Tex2 != null) SetTexagrakandzax(Retval.Tex2);
                    if (Retval.Bat1 != null) SetBattarey1Data(Retval.Bat1);
                    if (Retval.Bat2 != null) SetBattarey2Data(Retval.Bat2);
                    if (Retval.Bat3 != null) SetBattarey3Data(Retval.Bat3);
                    SetAxkFront(Retval.Front);
                }
            }
            else
                TargetLostFocus(sender, new RoutedEventArgs());
        }

        private void HandleKMH()
        {
            if (Positions.Text == "Մարտկոցի Կազմով")
            {
                KMH_Label1.Content = "ԿՄՀ(ԿԲՀ)";
                KMH_Label2.Content = "ԿՄՀ(ԿԲՀ)";
                KMH_Label3.Content = "ԿՄՀ(ԿԲՀ)";
            }
            else
            {
                KMH_Label1.Content = "ԿՄՀ";
                KMH_Label2.Content = "ԿՄՀ";
                KMH_Label3.Content = "ԿՄՀ";
            }
        }

        private void ResetClick(object sender, MouseButtonEventArgs e)
        {
            ReSetTarget();
            ResetBattares();
            ToolBoxExpander.IsExpanded = false;
        }

        private void Texamas_DropDownClosed(object sender, EventArgs e)
        {
            if (double.TryParse(AXK_aj_X.Text, out _) &&
                double.TryParse(AXK_aj_Y.Text, out _) &&
                double.TryParse(AXK_aj_H.Text, out _) &&
                double.TryParse(AXK_dzax_X.Text, out _) &&
                double.TryParse(AXK_dzax_Y.Text, out _) &&
                double.TryParse(AXK_dzax_H.Text, out _))
            {
                var Retval = Service.HandleAXKData(Ditaket1.Text, Ditaket2.Text, GetTaskInfo(), GetAXKTarget(), IsWithKayficent() || IsWithHavelyal() ? GetKayficentsFromView() : null);
                if (Retval.P2 != null) SetAXKpointLeft(Retval.P2);
                if (Retval.P1 != null) SetAXKpointRight(Retval.P1);
                if (Retval.Tex1 != null) SetTexagrakanaj(Retval.Tex1);
                if (Retval.Tex2 != null) SetTexagrakandzax(Retval.Tex2);
                if (Retval.Bat1 != null) SetBattarey1Data(Retval.Bat1);
                if (Retval.Bat2 != null) SetBattarey2Data(Retval.Bat2);
                if (Retval.Bat3 != null) SetBattarey3Data(Retval.Bat3);
                SetAxkFront(Retval.Front);
            }
        }

        private void PaytucichClosed(object sender, EventArgs e)
        {
            if (Paytucich.Text == "В-90 (ОФ-462)")
            {
                ShootingCore.SelectedItem = ShootingCore.Items[0];
            }
            HandlePaytucichTypes(Paytucich.Text);
            HandleShootingType();
            Count(sender, e);
        }

        private void SaveScreen(object sender, MouseButtonEventArgs e)
        {
            ToolBoxExpander.IsExpanded = false;
            MessageBox.Show("Պահպանելու համար սեղմեք ստեղնաշարի «P » կոճակը", "Օգնություն", new MessageBoxButton());
        }

        private void SystemChanged(object sender, EventArgs e)
        {
            ResetBattares();
            Initializer.InitializePaytucich(ref Paytucich, Systems.Text);
            Initializer.InitializeShootingCore(ref ShootingCore, Paytucich.Text);
            HandleShootingType();
            Count(sender, new EventArgs());
        }

        private void HandleShootingType()
        {
            if (Paytucich.Text == "РГМ-2 (ОФ-462)") Initializer.InitializeShootingMethod(ref ShootingMethod, false);
            else Initializer.InitializeShootingMethod(ref ShootingMethod, true);
        }

        private void ExitExpander(object sender, RoutedEventArgs e)
        {
            (sender as Expander).IsExpanded = false;
        }

        private void ExitExpanders(object sender, MouseButtonEventArgs e)
        {
            ToolBoxExpander.IsExpanded = false;
            KayficentPanel.IsExpanded = false;
        }

        private void SaveTarget(object sender, MouseButtonEventArgs e)
        {
            var target = new Target
            {
                ID = TargetNumber.Text,
                Description = TargetName.Text,
                X = int.Parse(Target_X.Text),
                Y = int.Parse(Target_Y.Text),
                H = int.Parse(Target_H.Text),
                Front = int.Parse(Target_FrontMeter.Text),
                Deepness = int.Parse(Target_Deepness.Text)
            };
            var oldTarget = TargetManager.GetTargetByNumber(TargetNumber.Text, out _);
            if (oldTarget != null)
            {
                target.Num = oldTarget.Num;
                DataWriter.AddTarget(target, true);
            }
            else
            {
                DataWriter.AddTarget(target);
            }
            ToolBoxExpander.IsExpanded = false;
        }

        private void TaskKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    SetTarget(TargetManager.GetNextTarget(TargetNumber.Text));
                    Count(sender, new EventArgs());
                    break;

                case Key.Left:
                    SetTarget(TargetManager.GetPreviousTarget(TargetNumber.Text));
                    Count(sender, new EventArgs());
                    break;

                case Key.P:
                    ScreenCapture(4, TargetNumber.Text + " " + TargetName.Text);
                    break;
                case Key.W:
                    MessageBox.Show("Congrats");
                    break;
            }

            if (e.Key == Key.P)
            {
                if (TaskType.Text == "ՇԱԿ" || TaskType.Text == "ԱԱԿ")
                {
                    ScreenCapture(4, AXKName.Text);
                }
                else ScreenCapture(4, TargetNumber.Text + " " + TargetName.Text);
            }
        }
        private void ShowSob(object sender, MouseButtonEventArgs e)
        {
            Window Note = new Sob(new TaskInfo()
            {
                TaskType = TaskType.Text,
                SelectedCore = ShootingCore.Text,
                Paytucich = Paytucich.Text,
                Positions = Positions.Text,
                ShootingType = ShootingMethod.Text,
                AXKType = AXKType.Text,
            });
            Note.ShowDialog();
        }
        private void AXKNameSelected(object sender, EventArgs e)
        {
            if (AXKName.SelectedItem != null)
            {
                var targets = DataReader.GetAXKTargets();
                var t = targets.Where(i => (AXKName.SelectedItem as ComboBoxItem).Content.ToString() == i.ID).FirstOrDefault();
                if (t != null)
                {
                    /////////////////////////////////////////////////////////////////////////////////////
                    AXK_aj_X.Text = t.Right.X.ToString();
                    AXK_aj_Y.Text = t.Right.Y.ToString();
                    AXK_aj_H.Text = t.Right.H.ToString();
                    /////////////////////////////////////////////////////////////////////////////////////
                    AXK_dzax_X.Text = t.Left.X.ToString();
                    AXK_dzax_Y.Text = t.Left.Y.ToString();
                    AXK_dzax_H.Text = t.Left.H.ToString();
                    /////////////////////////////////////////////////////////////////////////////////////
                    AXKFokusLost(AXK_aj_X, new RoutedEventArgs());
                }
            }
        }
        private void AXKName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((AXKName.SelectedItem as ComboBoxItem).Content.ToString() != "Ոչ Պլանային")
            {
                AXKType.SelectedItem = AXKType.Items[1];
                HandleTaskTypes(TaskType.Text);
                AXKNameSelected(this, new EventArgs());
            }
            else
            {
                ReSetTarget();
                ResetBattares();
            }
        }
        private void ShowHranot(object sender, MouseButtonEventArgs e)
        {
            Window Note = new Hranot(new TaskInfo()
            {
                TaskType = TaskType.Text,
                SelectedCore = ShootingCore.Text,
                Paytucich = Paytucich.Text,
                Positions = Positions.Text,
                ShootingType = ShootingMethod.Text,
                AXKType = AXKType.Text,
            });
            Note.ShowDialog();
        }
        private void KayficentDropdownClosed(object sender, EventArgs e)
        {
            switch (IsKayficent.Text)
            {
                case "Տեղագրական":
                    KayficentPanel.Visibility = Visibility.Collapsed;
                    /////////////////////////////////////////////////////////////////////////////////////
                    Hashvarkayin_Panel_M1_1.Visibility = Visibility.Collapsed;
                    Hashvarkayin_Panel_M1_2.Visibility = Visibility.Collapsed;
                    /////////////////////////////////////////////////////////////////////////////////////
                    Hashvarkayin_Panel_M2_1.Visibility = Visibility.Collapsed;
                    Hashvarkayin_Panel_M2_2.Visibility = Visibility.Collapsed;
                    /////////////////////////////////////////////////////////////////////////////////////
                    Hashvarkayin_Panel_M3_1.Visibility = Visibility.Collapsed;
                    Hashvarkayin_Panel_M3_2.Visibility = Visibility.Collapsed;
                    /////////////////////////////////////////////////////////////////////////////////////
                    break;
                case "Հաշվարկային":
                    KayficentPanel.Visibility = Visibility.Visible;
                    if (ShootingCore.Text != "Ավտոմատ լիցք")
                    {
                        /////////////////////////////////////////////////////////////////////////////////////
                        Hashvarkayin_Panel_M1_1.Visibility = Visibility.Visible;
                        Hashvarkayin_Panel_M1_2.Visibility = Visibility.Visible;
                        /////////////////////////////////////////////////////////////////////////////////////
                        Hashvarkayin_Panel_M2_1.Visibility = Visibility.Visible;
                        Hashvarkayin_Panel_M2_2.Visibility = Visibility.Visible;
                        /////////////////////////////////////////////////////////////////////////////////////
                        Hashvarkayin_Panel_M3_1.Visibility = Visibility.Visible;
                        Hashvarkayin_Panel_M3_2.Visibility = Visibility.Visible;
                        /////////////////////////////////////////////////////////////////////////////////////
                    }
                    else
                    {
                        /////////////////////////////////////////////////////////////////////////////////////
                        Hashvarkayin_Panel_M1_1.Visibility = Visibility.Collapsed;
                        Hashvarkayin_Panel_M1_2.Visibility = Visibility.Collapsed;
                        /////////////////////////////////////////////////////////////////////////////////////
                        Hashvarkayin_Panel_M2_1.Visibility = Visibility.Collapsed;
                        Hashvarkayin_Panel_M2_2.Visibility = Visibility.Collapsed;
                        /////////////////////////////////////////////////////////////////////////////////////
                        Hashvarkayin_Panel_M3_1.Visibility = Visibility.Collapsed;
                        Hashvarkayin_Panel_M3_2.Visibility = Visibility.Collapsed;
                    }
                    break;
            }
            Count(this, new EventArgs());
        }
        private void KayficentChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var Sender = sender as TextBox;

                if (Sender.Name == "K_Distance" || Sender.Name == "K_Davarot" || Sender.Name == "Z_Reper")
                {
                    /////////////////////////////////////////////////////////////////////////////////////
                    K_Dist1.Text = "";
                    K_DeltaDistance1.Text = "";
                    K_DeltaDavarot1.Text = "";
                    /////////////////////////////////////////////////////////////////////////////////////
                    K_Dist2.Text = "";
                    K_DeltaDistance2.Text = "";
                    K_DeltaDavarot2.Text = "";
                    /////////////////////////////////////////////////////////////////////////////////////
                    K_Dist3.Text = "";
                    K_DeltaDistance3.Text = "";
                    K_DeltaDavarot3.Text = "";
                    /////////////////////////////////////////////////////////////////////////////////////
                }
                else
                {
                    K_Distance.Text = "";
                    K_Davarot.Text = "";
                    Z_Reper.Text = "";
                }
            }
            catch { }
        }
        private bool IsWithKayficent()
        {
            if (!(string.IsNullOrWhiteSpace(K_Distance.Text) || string.IsNullOrWhiteSpace(K_Davarot.Text)))
            {
                return true;
            }
            return false;
        }
        private bool IsWithHavelyal()
        {
            if (!(string.IsNullOrWhiteSpace(K_Dist1.Text) || string.IsNullOrWhiteSpace(K_DeltaDistance1.Text) || string.IsNullOrWhiteSpace(K_DeltaDavarot1.Text)
                || string.IsNullOrWhiteSpace(K_Dist2.Text) || string.IsNullOrWhiteSpace(K_DeltaDistance2.Text) || string.IsNullOrWhiteSpace(K_DeltaDavarot2.Text)
                || string.IsNullOrWhiteSpace(K_Dist3.Text) || string.IsNullOrWhiteSpace(K_DeltaDistance3.Text) || string.IsNullOrWhiteSpace(K_DeltaDavarot3.Text)))
            {
                return true;
            }
            return false;
        }
        private void WindwStateChanged(object sender, EventArgs e)
        {
            Scroller.Height = this.ActualHeight - 260;
        }
        private void WindowSozeChanged(object sender, SizeChangedEventArgs e)
        {
            Scroller.Height = this.ActualHeight - 260;
        }
        private void HandleCameraSelection(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).Name == "Ditaket2")
            {
                if (((e.AddedItems[0] as ComboBoxItem).Content as string).Contains("Տեսախցիկ"))
                {
                    AzimutPanel2.Visibility = Visibility.Visible;
                    Alpha_Hram2.IsEnabled = false;
                }
                else
                {
                    AzimutPanel2.Visibility = Visibility.Collapsed;
                    if (TaskType.Text != "Պլանային (Նշ. ցանկից)" && TaskType.Text != "ՈՒղղանկյուն կոորդինատներով")
                    {
                        if (!((TaskType.Text == "ԱԱԿ" || TaskType.Text == "ՇԱԿ") && AXKType.Text == "ՈՒղղանկյուն")) Alpha_Hram2.IsEnabled = true;
                    }
                }
            }
            else if ((sender as ComboBox).Name == "Ditaket1")
            {
                if (((e.AddedItems[0] as ComboBoxItem).Content as string).Contains("Տեսախցիկ"))
                {
                    AzimutPanel1.Visibility = Visibility.Visible;
                    Alpha_Hram1.IsEnabled = false;
                }
                else
                {
                    AzimutPanel1.Visibility = Visibility.Collapsed;
                    if (TaskType.Text != "Պլանային (Նշ. ցանկից)" && TaskType.Text != "ՈՒղղանկյուն կոորդինատներով")
                    {
                        if (!((TaskType.Text == "ԱԱԿ" || TaskType.Text == "ՇԱԿ") && AXKType.Text == "ՈՒղղանկյուն")) Alpha_Hram1.IsEnabled = true;
                    }
                }
            }
        }
        private void AzimutToAlpha(object sender, KeyEventArgs e)
        {
            if ((sender as TextBox).Name == "Alpha_Azimut1" && double.TryParse(Alpha_Azimut1.Text, out _))
            {
                Alpha_Hram1.Text = Helper.FormatAngle(Helper.GetAngleFromCamera(
                    DataReader.GetCameras().Where(cam => cam.Name == Ditaket1.Text).Single(),
                    double.TryParse(Alpha_Azimut1.Text, out _) ? double.Parse(Alpha_Azimut1.Text) : 0));
            }
            else if ((sender as TextBox).Name == "Alpha_Azimut2" && double.TryParse(Alpha_Azimut2.Text, out _))
            {
                Alpha_Hram2.Text = Helper.FormatAngle(Helper.GetAngleFromCamera(
                    DataReader.GetCameras().Where(cam => cam.Name == Ditaket2.Text).Single(),
                    double.TryParse(Alpha_Azimut2.Text, out _) ? double.Parse(Alpha_Azimut2.Text) : 0));
            }
        }
        private void ShowDitarkum(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool IsBat1 = M1_Core.Text == "###" ? false : true,
                    IsBat2 = M2_Core.Text == "###" ? false : true,
                    Isbat3 = M3_Core.Text == "###" ? false : true;
                if (!IsBat1 && !IsBat2 && !Isbat3)
                {
                    throw new InvalidOperationException();
                }
                var TaskInformation = GetTaskInfo();
                DitarkumDataModel DDM = new DitarkumDataModel()
                {
                    Ditaket = Owin.Martakarg.GetDitaket(Ditaket2.Text),
                    ParallelDitaket = TaskInformation.TaskType == "Զուգորդված դիտարկում" ? Owin.Martakarg.GetDitaket(Ditaket1.Text) : null,
                    Hramanatarakan = new Texagrakan()
                    {
                        Alpha = Helper.GetAngleFromString(Alpha_Hram2.Text),
                        Distance = double.Parse(D_Hram2.Text),
                        M = double.Parse(M_NSH2.Text)
                    },

                    ParallelHramanatarakan = TaskInformation.TaskType == "Զուգորդված դիտարկում" ? new Texagrakan()
                    {
                        Alpha = Helper.GetAngleFromString(Alpha_Hram1.Text),
                        Distance = double.Parse(D_Hram1.Text),
                        M = double.Parse(M_NSH1.Text)
                    } : null,

                    Target = GetTarget(),

                    TaskInfo = TaskInformation,

                    Battary1Texakayum = IsBat1 ? new PaytyunTexakayum
                    {
                        Core = M1_Core.Text,
                        Davarot1 = Helper.GetAngleFromString(M1_D1_DHU.Text),
                        Pricel1 = double.Parse(M1_D1_Pricel.Text),
                        Poxrak1 = TaskInformation.Paytucich != "РГМ-2 (ОФ-462)" ? double.Parse(M1_D1_Poxrak.Text) : -1,
                        Davarot2 = TaskInformation.Positions == "Դասակի Կազմով" ? Helper.GetAngleFromString(M1_D2_DHU.Text) : 0,
                        Pricel2 = TaskInformation.Positions == "Դասակի Կազմով" ? double.Parse(M1_D2_Pricel.Text) : 0,
                        Poxrak2 = TaskInformation.Paytucich != "РГМ-2 (ОФ-462)" && TaskInformation.Positions == "Դասակի Կազմով" ? double.Parse(M1_D2_Poxrak.Text) : -1,
                        Ku = double.TryParse(M1_KU.Text, out _) ? double.Parse(M1_KU.Text) : -1
                    } : null,

                    Battary2Texakayum = IsBat2 ? new PaytyunTexakayum
                    {
                        Core = M2_Core.Text,
                        Davarot1 = Helper.GetAngleFromString(M2_D1_DHU.Text),
                        Pricel1 = double.Parse(M2_D1_Pricel.Text),
                        Poxrak1 = TaskInformation.Paytucich != "РГМ-2 (ОФ-462)" ? double.Parse(M2_D1_Poxrak.Text) : -1,
                        Davarot2 = TaskInformation.Positions == "Դասակի Կազմով" ? Helper.GetAngleFromString(M2_D2_DHU.Text) : 0,
                        Pricel2 = TaskInformation.Positions == "Դասակի Կազմով" ? double.Parse(M2_D2_Pricel.Text) : 0,
                        Poxrak2 = TaskInformation.Paytucich != "РГМ-2 (ОФ-462)" && TaskInformation.Positions == "Դասակի Կազմով" ? double.Parse(M2_D2_Poxrak.Text) : -1,
                        Ku = double.TryParse(M2_KU.Text, out _) ? double.Parse(M2_KU.Text) : -1
                    } : null,

                    Battary3Texakayum = Isbat3 ? new PaytyunTexakayum
                    {
                        Core = M3_Core.Text,
                        Davarot1 = Helper.GetAngleFromString(M3_D1_DHU.Text),
                        Pricel1 = double.Parse(M3_D1_Pricel.Text),
                        Poxrak1 = TaskInformation.Paytucich != "РГМ-2 (ОФ-462)" ? double.Parse(M3_D1_Poxrak.Text) : -1,
                        Davarot2 = TaskInformation.Positions == "Դասակի Կազմով" ? Helper.GetAngleFromString(M3_D2_DHU.Text) : 0,
                        Pricel2 = TaskInformation.Positions == "Դասակի Կազմով" ? double.Parse(M3_D2_Pricel.Text) : 0,
                        Poxrak2 = TaskInformation.Paytucich != "РГМ-2 (ОФ-462)" && TaskInformation.Positions == "Դասակի Կազմով" ? double.Parse(M3_D2_Poxrak.Text) : -1,
                        Ku = double.TryParse(M3_KU.Text, out _) ? double.Parse(M3_KU.Text) : -1
                    } : null
                };
                new Ditarkum().SetTaskData(DDM).ShowDialog();
                Ditarkum.RefCount--;
            }
            catch (Exception o)
            {
                MessageBox.Show("Հնարավոր չէ դիտարկում կատարել", "Կիսատ Խնդիր");
            }
        }
        private void HandleView(object sender, SelectionChangedEventArgs e)
        {
            if ((e.AddedItems[0] as ComboBoxItem).Content as string == "Դասակի Կազմով")
            {
                M1_D1_DHU.Width = 63;
                M1_D1_Dtex.Width = 63;
                M1_D1_D_Hashv.Width = 63;
                M1_D1_LVL.Width = 63;
                M1_D1_Poxrak.Width = 63;
                M1_D1_Pricel.Width = 63;
                M1_D1_shrjaberTex.Width = 63;
                M1_D1_shrjaber_Hashv.Width = 63;
                /////////////////////////////////////////////////////////////////////////////////////
                M1_D2_DHU.Visibility = Visibility.Visible;
                M1_D2_Dtex.Visibility = Visibility.Visible;
                M1_D2_D_Hashv.Visibility = Visibility.Visible;
                M1_D2_LVL.Visibility = Visibility.Visible;
                M1_D2_Poxrak.Visibility = Visibility.Visible;
                M1_D2_Pricel.Visibility = Visibility.Visible;
                M1_D2_shrjaberTex.Visibility = Visibility.Visible;
                M1_D2_shrjaber_Hashv.Visibility = Visibility.Visible;
                /////////////////////////////////////////////////////////////////////////////////////
                M2_D1_DHU.Width = 63;
                M2_D1_Dtex.Width = 63;
                M2_D1_D_Hashv.Width = 63;
                M2_D1_LVL.Width = 63;
                M2_D1_Poxrak.Width = 63;
                M2_D1_Pricel.Width = 63;
                M2_D1_shrjaberTex.Width = 63;
                M2_D1_shrjaber_Hashv.Width = 63;
                /////////////////////////////////////////////////////////////////////////////////////
                M2_D2_DHU.Visibility = Visibility.Visible;
                M2_D2_Dtex.Visibility = Visibility.Visible;
                M2_D2_D_Hashv.Visibility = Visibility.Visible;
                M2_D2_LVL.Visibility = Visibility.Visible;
                M2_D2_Poxrak.Visibility = Visibility.Visible;
                M2_D2_Pricel.Visibility = Visibility.Visible;
                M2_D2_shrjaberTex.Visibility = Visibility.Visible;
                M2_D2_shrjaber_Hashv.Visibility = Visibility.Visible;
                /////////////////////////////////////////////////////////////////////////////////////
                M3_D1_DHU.Width = 63;
                M3_D1_Dtex.Width = 63;
                M3_D1_D_Hashv.Width = 63;
                M3_D1_LVL.Width = 63;
                M3_D1_Poxrak.Width = 63;
                M3_D1_Pricel.Width = 63;
                M3_D1_shrjaberTex.Width = 63;
                M3_D1_shrjaber_Hashv.Width = 63;
                /////////////////////////////////////////////////////////////////////////////////////
                M3_D2_DHU.Visibility = Visibility.Visible;
                M3_D2_Dtex.Visibility = Visibility.Visible;
                M3_D2_D_Hashv.Visibility = Visibility.Visible;
                M3_D2_LVL.Visibility = Visibility.Visible;
                M3_D2_Poxrak.Visibility = Visibility.Visible;
                M3_D2_Pricel.Visibility = Visibility.Visible;
                M3_D2_shrjaberTex.Visibility = Visibility.Visible;
                M3_D2_shrjaber_Hashv.Visibility = Visibility.Visible;
            }
            else
            {
                M1_D1_DHU.Width = 130;
                M1_D1_Dtex.Width = 130;
                M1_D1_D_Hashv.Width = 130;
                M1_D1_LVL.Width = 130;
                M1_D1_Poxrak.Width = 130;
                M1_D1_Pricel.Width = 130;
                M1_D1_shrjaberTex.Width = 130;
                M1_D1_shrjaber_Hashv.Width = 130;
                M1_D2_DHU.Visibility = Visibility.Collapsed;
                M1_D2_Dtex.Visibility = Visibility.Collapsed;
                M1_D2_D_Hashv.Visibility = Visibility.Collapsed;
                M1_D2_LVL.Visibility = Visibility.Collapsed;
                M1_D2_Poxrak.Visibility = Visibility.Collapsed;
                M1_D2_Pricel.Visibility = Visibility.Collapsed;
                M1_D2_shrjaberTex.Visibility = Visibility.Collapsed;
                M1_D2_shrjaber_Hashv.Visibility = Visibility.Collapsed;
                /////////////////////////////////////////////////////////////////////////////////////
                M2_D1_DHU.Width = 130;
                M2_D1_Dtex.Width = 130;
                M2_D1_D_Hashv.Width = 130;
                M2_D1_LVL.Width = 130;
                M2_D1_Poxrak.Width = 130;
                M2_D1_Pricel.Width = 130;
                M2_D1_shrjaberTex.Width = 130;
                M2_D1_shrjaber_Hashv.Width = 130;
                M2_D2_DHU.Visibility = Visibility.Collapsed;
                M2_D2_Dtex.Visibility = Visibility.Collapsed;
                M2_D2_D_Hashv.Visibility = Visibility.Collapsed;
                M2_D2_LVL.Visibility = Visibility.Collapsed;
                M2_D2_Poxrak.Visibility = Visibility.Collapsed;
                M2_D2_Pricel.Visibility = Visibility.Collapsed;
                M2_D2_shrjaberTex.Visibility = Visibility.Collapsed;
                M2_D2_shrjaber_Hashv.Visibility = Visibility.Collapsed;
                /////////////////////////////////////////////////////////////////////////////////////
                M3_D1_DHU.Width = 130;
                M3_D1_Dtex.Width = 130;
                M3_D1_D_Hashv.Width = 130;
                M3_D1_LVL.Width = 130;
                M3_D1_Poxrak.Width = 130;
                M3_D1_Pricel.Width = 130;
                M3_D1_shrjaberTex.Width = 130;
                M3_D1_shrjaber_Hashv.Width = 130;
                M3_D2_DHU.Visibility = Visibility.Collapsed;
                M3_D2_Dtex.Visibility = Visibility.Collapsed;
                M3_D2_D_Hashv.Visibility = Visibility.Collapsed;
                M3_D2_LVL.Visibility = Visibility.Collapsed;
                M3_D2_Poxrak.Visibility = Visibility.Collapsed;
                M3_D2_Pricel.Visibility = Visibility.Collapsed;
                M3_D2_shrjaberTex.Visibility = Visibility.Collapsed;
                M3_D2_shrjaber_Hashv.Visibility = Visibility.Collapsed;
            }
        }
    }
}