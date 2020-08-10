using BaseEntities;
using DataAccessLayer;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Linq;
using System.IO;
using System.ComponentModel;

namespace Nemesis
{
    public partial class Targets : Window
    {
        public static string SourcePath
        {
            get
            {
                return Directory.GetCurrentDirectory();
            }
        }
        private readonly int PageSize = 24;
        private int CurrentPage { get; set; }
        Window AddTargets;
        public static bool HasReference { set; get; }
        public Targets()
        {
            PageSize = new Settings().Initialize().TargetsPageSize;
            CurrentPage = 1;
            HasReference = true;
            InitializeComponent();
            InatializeImageSources();
            IsNormal.IsChecked = true;

        }
        private void InatializeImageSources()
        {
            /////////////////////////////////////////////////////////////////////////////////////
            BitmapImage plus = new BitmapImage();
            plus.BeginInit();
            plus.UriSource = new Uri(SourcePath + "\\Images\\Plus.ico");
            plus.EndInit();
            Plus.Source = plus;
            AXKPlus.Source = plus;
            /////////////////////////////////////////////////////////////////////////////////////
            BitmapImage searchIcon = new BitmapImage();
            searchIcon.BeginInit();
            searchIcon.UriSource = new Uri(SourcePath + "\\Images\\Scope.ico");
            searchIcon.EndInit();
            SearchIcon.Source = searchIcon;
            /////////////////////////////////////////////////////////////////////////////////////
            BitmapImage clear = new BitmapImage();
            clear.BeginInit();
            clear.UriSource = new Uri(SourcePath + "\\Images\\Clear.ico");
            clear.EndInit();
            ClearIcon.Source = clear;
            /////////////////////////////////////////////////////////////////////////////////////
            BitmapImage insert = new BitmapImage();
            insert.BeginInit();
            insert.UriSource = new Uri(SourcePath + "\\Images\\Insert.ico");
            insert.EndInit();
            InsertIcon.Source = insert;
            /////////////////////////////////////////////////////////////////////////////////////
            BitmapImage minus = new BitmapImage();
            minus.BeginInit();
            minus.UriSource = new Uri(SourcePath + "\\Images\\Minus.ico");
            minus.EndInit();
            Minus.Source = minus;
            /////////////////////////////////////////////////////////////////////////////////////
            BitmapImage refresh = new BitmapImage();
            refresh.BeginInit();
            refresh.UriSource = new Uri(SourcePath + "\\Images\\Refresh.ico");
            refresh.EndInit();
            RefreshView.Source = refresh;
            /////////////////////////////////////////////////////////////////////////////////////
        }
        protected override void OnClosed(EventArgs e)
        {
            HasReference = false;
            base.OnClosed(e);
        }
        private void RefreshTargetList(bool isDalate = false, string filter = null)
        {
            var targets = DataReader.GetTargets();
            TargetCount.Content = "Ընդհամենը " + targets.Count + " Նշանակետ";
            if (filter != null)
            {
                targets = targets.Where(item => item.ID.ToUpper().Contains(filter.ToUpper()) || item.Description.ToUpper().Contains(filter.ToUpper())).ToList();
            }
            else SearchBox.Text = "";
            if ((CurrentPage) * PageSize >= targets.Count)
            {
                Next.IsEnabled = false;
            }
            else
            {
                Next.IsEnabled = true;
            }
            if (isDalate is true && (CurrentPage - 1) * PageSize >= targets.Count && targets.Count!=0)
            {
                CurrentPage--;
            }
            if (targets.Count != 0)
            {
                targets = targets.GetRange((PageSize * (CurrentPage - 1)), (targets.Count - (CurrentPage - 1) * PageSize) > PageSize ? PageSize : (targets.Count - (CurrentPage - 1) * PageSize));
            }
            TargetList.Children.Clear();
            foreach (var t in targets)
            {
                TargetList.Children.Add(InitializeTargetsPanel(t));
            }
            CurrentPageLabel.Content = CurrentPage;
            if (CurrentPage == 1)
            {
                Previous.IsEnabled = false;
            }
            else
            {
                Previous.IsEnabled = true;
            }
        }
        private void RefreshAXKTargetList(bool isDalate = false, string filter = null)
        {
            var targets = DataReader.GetAXKTargets();
            TargetCount.Content = "Ընդհամենը " + targets.Count + " Նշանակետ";
            if (filter != null)
            {
                targets = targets.Where(item => item.ID.ToUpper().Contains(filter.ToUpper()) || item.Description.ToUpper().Contains(filter.ToUpper())).ToList();
            }
            else SearchBox.Text = "";
            if ((CurrentPage) * PageSize >= targets.Count)
            {
                Next.IsEnabled = false;
            }
            else
            {
                Next.IsEnabled = true;
            }
            if (isDalate is true && (CurrentPage - 1) * PageSize >= targets.Count && targets.Count != 0)
            {
                CurrentPage--;
            }
            if (targets.Count != 0)
            {
                targets = targets.GetRange((PageSize * (CurrentPage - 1)), (targets.Count - (CurrentPage - 1) * PageSize) > PageSize ? PageSize : (targets.Count - (CurrentPage - 1) * PageSize));
            }
            AAKTargetList.Children.Clear();
            foreach (var t in targets)
            {
                AAKTargetList.Children.Add(InitializeAXKTargetsPanel(t));
            }
            CurrentPageLabel.Content = CurrentPage;
            if (CurrentPage == 1)
            {
                Previous.IsEnabled = false;
            }
            else
            {
                Previous.IsEnabled = true;
            }
        }
        private UIElement InitializeAXKTargetsPanel(AXK t)
        {
            StackPanel panel = new StackPanel() { Margin = new Thickness(0, 5, 0, 0) };
            panel.Orientation = Orientation.Horizontal;
            panel.HorizontalAlignment = HorizontalAlignment.Center;
            panel.VerticalAlignment = VerticalAlignment.Center;
            panel.Height = 20;
            /////////////////////////////////////////////////////////////////////////////////////
            var textbox1 = new TextBox()
            {
                Style = TargetNumber.Style,
                Margin = new Thickness(0, 0, 2, 0),
                FontSize = 14,
                FontFamily = new FontFamily("Adver Unicode"),
                Width = 130,
                FontWeight = FontWeights.ExtraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Text = t.ID
            };
            textbox1.LostKeyboardFocus += AXKBoxLostFocus;
            panel.Children.Add(textbox1);
            /////////////////////////////////////////////////////////////////////////////////////
            var textbox2 = new TextBox()
            {
                Style = TargetNumber.Style,
                Margin = new Thickness(2, 0, 2, 0),
                FontSize = 14,
                FontFamily = new FontFamily("Adver Unicode"),
                Width = 302,
                FontWeight = FontWeights.ExtraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Text = t.Description
            };
            textbox2.LostKeyboardFocus += AXKBoxLostFocus;
            panel.Children.Add(textbox2);
            /////////////////////////////////////////////////////////////////////////////////////
            var textbox3 = new TextBox()
            {
                Style = TargetNumber.Style,
                Margin = new Thickness(2, 0, 2, 0),
                FontSize = 14,
                FontFamily = new FontFamily("Adver Unicode"),
                Width = 75,
                FontWeight = FontWeights.ExtraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Text = t.Right.X.ToString()
            };
            textbox3.LostKeyboardFocus += AXKBoxLostFocus;
            panel.Children.Add(textbox3);
            /////////////////////////////////////////////////////////////////////////////////////
            var textbox4 = new TextBox()
            {
                Style = TargetNumber.Style,
                Margin = new Thickness(2, 0, 2, 0),
                FontSize = 14,
                FontFamily = new FontFamily("Adver Unicode"),
                Width = 75,
                FontWeight = FontWeights.ExtraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Text = t.Right.Y.ToString()
            };
            textbox4.LostKeyboardFocus += AXKBoxLostFocus;
            panel.Children.Add(textbox4);
            /////////////////////////////////////////////////////////////////////////////////////
            var textbox5 = new TextBox()
            {
                Style = TargetNumber.Style,
                Margin = new Thickness(2, 0, 2, 0),
                FontSize = 14,
                FontFamily = new FontFamily("Adver Unicode"),
                Width = 55,
                FontWeight = FontWeights.ExtraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Text = t.Right.H.ToString()
            };
            textbox5.LostKeyboardFocus += AXKBoxLostFocus;
            panel.Children.Add(textbox5);
            /////////////////////////////////////////////////////////////////////////////////////
            var textbox6 = new TextBox()
            {
                Style = TargetNumber.Style,
                Margin = new Thickness(2, 0, 2, 0),
                FontSize = 14,
                FontFamily = new FontFamily("Adver Unicode"),
                Width = 75,
                FontWeight = FontWeights.ExtraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Text = t.Left.X.ToString()
            };
            textbox6.LostKeyboardFocus += AXKBoxLostFocus;
            panel.Children.Add(textbox6);
            /////////////////////////////////////////////////////////////////////////////////////
            var textbox7 = new TextBox()
            {
                Style = TargetNumber.Style,
                Margin = new Thickness(2, 0, 2, 0),
                FontSize = 14,
                FontFamily = new FontFamily("Adver Unicode"),
                Width = 75,
                FontWeight = FontWeights.ExtraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Text = t.Left.Y.ToString()
            };
            textbox7.LostKeyboardFocus += AXKBoxLostFocus;
            panel.Children.Add(textbox7);
            /////////////////////////////////////////////////////////////////////////////////////
            var textbox8 = new TextBox()
            {
                Style = TargetNumber.Style,
                Margin = new Thickness(2, 0, 2, 0),
                FontSize = 14,
                FontFamily = new FontFamily("Adver Unicode"),
                Width = 55,
                FontWeight = FontWeights.ExtraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Text = t.Left.H.ToString()
            };
            textbox8.LostKeyboardFocus += AXKBoxLostFocus;
            panel.Children.Add(textbox8);
            /////////////////////////////////////////////////////////////////////////////////////
            var Minus = new Image()
            {
                Margin = new Thickness(2, 0, 0, 0),
                Width = 20,
                Source = this.Minus.Source,
                Cursor = Cursors.Hand,
                Style = this.Minus.Style,
                RenderTransform = this.Minus.RenderTransform,
            };
            Minus.MouseUp += MinusAXK_MouseUp;
            panel.Children.Add(Minus);
            /////////////////////////////////////////////////////////////////////////////////////
            return panel;
        }

        private StackPanel InitializeTargetsPanel(Target t)
        {
            StackPanel panel = new StackPanel() { Margin = new Thickness(0, 5, 0, 0) };
            panel.Orientation = Orientation.Horizontal;
            panel.HorizontalAlignment = HorizontalAlignment.Left;
            panel.VerticalAlignment = VerticalAlignment.Center;
            panel.Height = 20;
            /////////////////////////////////////////////////////////////////////////////////////
            var textbox1 = new TextBox()
            {
                Style = TargetNumber.Style,
                Margin = new Thickness(0, 0, 0, 0),
                FontSize = 14,
                FontFamily = new FontFamily("Adver Unicode"),
                Width = 90,
                FontWeight = FontWeights.ExtraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Text = t.ID
            };
            textbox1.LostKeyboardFocus += BoxLostFocus;
            panel.Children.Add(textbox1);
            /////////////////////////////////////////////////////////////////////////////////////
            var textbox2 = new TextBox()
            {
                Style = TargetNumber.Style,
                Margin = new Thickness(2, 0, 2, 0),
                FontSize = 14,
                FontFamily = new FontFamily("Adver Unicode"),
                Width = 302,
                FontWeight = FontWeights.ExtraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Text = t.Description
            };
            textbox2.LostKeyboardFocus += BoxLostFocus;
            panel.Children.Add(textbox2);
            /////////////////////////////////////////////////////////////////////////////////////
            var textbox3 = new TextBox()
            {
                Style = TargetNumber.Style,
                Margin = new Thickness(2, 0, 2, 0),
                FontSize = 14,
                FontFamily = new FontFamily("Adver Unicode"),
                Width = 75,
                FontWeight = FontWeights.ExtraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Text = t.X.ToString()
            };
            textbox3.LostKeyboardFocus += BoxLostFocus;
            panel.Children.Add(textbox3);
            /////////////////////////////////////////////////////////////////////////////////////
            var textbox4 = new TextBox()
            {
                Style = TargetNumber.Style,
                Margin = new Thickness(2, 0, 2, 0),
                FontSize = 14,
                FontFamily = new FontFamily("Adver Unicode"),
                Width = 75,
                FontWeight = FontWeights.ExtraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Text = t.Y.ToString()
            };
            textbox4.LostKeyboardFocus += BoxLostFocus;
            panel.Children.Add(textbox4);
            /////////////////////////////////////////////////////////////////////////////////////
            var textbox5 = new TextBox()
            {
                Style = TargetNumber.Style,
                Margin = new Thickness(2, 0, 2, 0),
                FontSize = 14,
                FontFamily = new FontFamily("Adver Unicode"),
                Width = 55,
                FontWeight = FontWeights.ExtraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Text = t.H.ToString()
            };
            textbox5.LostKeyboardFocus += BoxLostFocus;
            panel.Children.Add(textbox5);
            /////////////////////////////////////////////////////////////////////////////////////
            var textbox6 = new TextBox()
            {
                Style = TargetNumber.Style,
                Margin = new Thickness(2, 0, 2, 0),
                FontSize = 14,
                FontFamily = new FontFamily("Adver Unicode"),
                Width = 50,
                FontWeight = FontWeights.ExtraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Text = t.Front.ToString()
            };
            textbox6.LostKeyboardFocus += BoxLostFocus;
            panel.Children.Add(textbox6);
            /////////////////////////////////////////////////////////////////////////////////////
            var textbox7 = new TextBox()
            {
                Style = TargetNumber.Style,
                Margin = new Thickness(2, 0, 2, 0),
                FontSize = 14,
                FontFamily = new FontFamily("Adver Unicode"),
                Width = 50,
                FontWeight = FontWeights.ExtraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Text = t.Deepness.ToString()
            };
            textbox7.LostKeyboardFocus += BoxLostFocus;
            panel.Children.Add(textbox7);
            /////////////////////////////////////////////////////////////////////////////////////
            var Minus = new Image()
            {
                Margin = new Thickness(2, 0, 2, 0),
                Width = 20,
                Source = this.Minus.Source,
                Cursor = Cursors.Hand,
                Style = this.Minus.Style,
                RenderTransform = this.Minus.RenderTransform,
            };
            Minus.MouseUp += Minus_MouseUp;
            panel.Children.Add(Minus);
            /////////////////////////////////////////////////////////////////////////////////////
            return panel;
        }
        private void BoxLostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var Childs = ((sender as TextBox).Parent as StackPanel).Children;
            DataWriter.AddTarget(new Target((Childs[0] as TextBox).Text,
                (Childs[1] as TextBox).Text,
                int.Parse((Childs[2] as TextBox).Text),
                int.Parse((Childs[3] as TextBox).Text),
                int.Parse((Childs[4] as TextBox).Text),
                int.Parse((Childs[5] as TextBox).Text),
                int.Parse((Childs[6] as TextBox).Text)),
                true);
            RefreshTargetList();
        }
        private void AXKBoxLostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var Childs = ((sender as TextBox).Parent as StackPanel).Children;
            DataWriter.AddAXKTarget(new AXK((Childs[0] as TextBox).Text,
                (Childs[1] as TextBox).Text,
                int.Parse((Childs[2] as TextBox).Text),
                int.Parse((Childs[3] as TextBox).Text),
                int.Parse((Childs[4] as TextBox).Text),
                int.Parse((Childs[5] as TextBox).Text),
                int.Parse((Childs[6] as TextBox).Text),
                int.Parse((Childs[7] as TextBox).Text)),
                true);
            RefreshAXKTargetList();
        }
        private void Minus_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DataWriter.DeleteTarget((((sender as Image).Parent as StackPanel).Children[0] as TextBox).Text);
            RefreshTargetList(true);
        }
        private void MinusAXK_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DataWriter.DeleteAXKTarget((((sender as Image).Parent as StackPanel).Children[0] as TextBox).Text);
            RefreshAXKTargetList(true);
        }
        private void Add_List(object sender, MouseButtonEventArgs e)
        {
            if (!InitializeTargets.HasReference)
                AddTargets = new InitializeTargets(IsAAk.IsChecked);
            AddTargets.ShowDialog();
            if (IsNormal.IsChecked == true)
            {
                RefreshTargetList();
            }
            else
            {
                RefreshAXKTargetList();
            }
        }
        public void Refresh(object sender, CancelEventArgs e)
        {
            RefreshTargetList();
            RefreshAXKTargetList();
        }
        private void Add_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var Target = new Target(TargetNumber.Text,
                        TargetName.Text,
                        int.Parse(Target_X.Text),
                        int.Parse(Target_Y.Text),
                        int.Parse(Target_H.Text),
                        int.Parse(Target_Chakat.Text),
                        int.Parse(Target_Xorutyun.Text));

                AddTarget(Target);
                ClearInputs();
                RefreshTargetList();

            }
            catch { }
        }
        private void AddTarget(object t)
        {
            var target = t as Target;
            try
            {
                DataWriter.AddTarget(target);

            }
            catch (Exception)
            {

            }
        }
        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage--;
            if ((bool)IsNormal.IsChecked)
            {
                RefreshTargetList(false, SearchBox.Text);
            }
            else
            {
                RefreshAXKTargetList(false, SearchBox.Text);
            }
        }
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage++;
            if ((bool)IsNormal.IsChecked)
            {
                RefreshTargetList(false, SearchBox.Text);
            }
            else
            {
                RefreshAXKTargetList(false, SearchBox.Text);
            }
        }
        private void Clear_List(object sender, MouseButtonEventArgs e)
        {
            CurrentPage = 1;
            if (IsNormal.IsChecked == true)
            {
                DataWriter.ClearTargets();
                RefreshTargetList();
            }
            else
            {
                DataWriter.ClearAXKTargets();
                RefreshAXKTargetList();
            }
        }
        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            while (e.Key != Key.Enter)
            {
                return;
            }
            CurrentPage = 1;
            RefreshTargetList(false, SearchBox.Text);
        }
        private void IsAAk_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPage = 1;
            TargetPanel.Visibility = Visibility.Collapsed;
            AAKPanel.Visibility = Visibility.Visible;
            TargetList.Visibility = Visibility.Collapsed;
            AAKTargetList.Visibility = Visibility.Visible;
            RefreshAXKTargetList();
        }
        private void IsNormal_checked(object sender, RoutedEventArgs e)
        {
            CurrentPage = 1;
            TargetPanel.Visibility = Visibility.Visible;
            AAKPanel.Visibility = Visibility.Collapsed;
            TargetList.Visibility = Visibility.Visible;
            AAKTargetList.Visibility = Visibility.Collapsed;
            RefreshTargetList();
        }
        private void AddAXK_Click(object sender, MouseButtonEventArgs e)
        {
            AXK target = new AXK()
            {
                ID = AAKNumber.Text.Replace(' ', '-'),
                Description = AAKtName.Text,
                Right = new BaseEntities.Point(int.Parse(AAK_Aj_X.Text), int.Parse(AAK_Aj_Y.Text), int.Parse(AAK_Aj_H.Text)),
                Left = new BaseEntities.Point(int.Parse(AAK_DZAX_X.Text), int.Parse(AAK_DZAX_Y.Text), int.Parse(AAK_DZAX_H.Text)),
            };
            DataWriter.AddAXKTarget(target);
            ClearInputs();
            RefreshAXKTargetList();
        }
        private void ClearInputs()
        {
            /////////////////////////////////////////////////////////////////////////////////////
            TargetNumber.Text = "";
            TargetName.Text = "";
            Target_X.Text = "";
            Target_Y.Text = "";
            Target_H.Text = "";
            Target_Chakat.Text = "";
            Target_Xorutyun.Text = "";
            /////////////////////////////////////////////////////////////////////////////////////
            AAKNumber.Text = "";
            AAKtName.Text = "";
            AAK_Aj_X.Text = "";
            AAK_Aj_Y.Text = "";
            AAK_Aj_H.Text = "";
            AAK_DZAX_X.Text = "";
            AAK_DZAX_Y.Text = "";
            AAK_DZAX_H.Text = "";
            /////////////////////////////////////////////////////////////////////////////////////
        }

        private void SearchUnFocused(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text.Length == 0)
            {
                SearchHint.Visibility = Visibility.Visible;
            }
        }
        private void SearchFocused(object sender, RoutedEventArgs e)
        {
            SearchHint.Visibility = Visibility.Collapsed;
        }
        private void RefreshList(object sender, MouseButtonEventArgs e)
        {
            CurrentPage = 1;
            RefreshAXKTargetList();
            RefreshTargetList();
            SearchBox.Text = "";
            SearchHint.Visibility = Visibility.Visible;
        }
        private void WindowSozeChanged(object sender, SizeChangedEventArgs e)
        {
            Scroller.Height = this.ActualHeight - 180;
        }
        private void WindwStateChanged(object sender, EventArgs e)
        {
            Scroller.Height = this.ActualHeight - 180;
        }
    }
}