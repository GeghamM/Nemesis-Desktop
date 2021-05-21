using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Management;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BaseEntities;

namespace Nemesis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Window martakarg = new Martakarg();
        Window task = new Task();
        Window targets = new Targets();
        Window tools = new Tools();

        public static string SourcePath
        {
            get
            {
                return Directory.GetCurrentDirectory();
            }
        }
        public MainWindow()
        {
            byte[] certificate;
            //if (File.Exists("Certificate"))
            //{
            //    using (Stream fStream = File.OpenRead("Certificate"))
            //    {
            //        certificate = (byte[])new BinaryFormatter().Deserialize(fStream);
            //    }

            //    if (!(new SertificateValidation().CheckSertificate(certificate)))
            //    {
            //        new SertificateValidation().ShowDialog();
            //    }
            //}
            //else
            //{
            //    new SertificateValidation().ShowDialog();
            //}

            Initializers.InitializeCoreRanges();
            Initializers.InitiazieCores();
            Initializers.InitiazieLevels();
            InitializeComponent();
            InitializeSettings();
            InatializeImageSources();

        }

        private void InatializeImageSources()
        {
            /////////////////////////////////////////////////////////////////////////////////////
            BitmapImage clear = new BitmapImage();
            clear.BeginInit();
            clear.UriSource = new Uri(SourcePath + "\\Images\\BrandingBlack.png");
            clear.EndInit();
            MainBackground.Source = clear;
        }
        private void OpenMartakarg(object sender, RoutedEventArgs e)
        {
            if (!Martakarg.HasReference)
                martakarg = new Martakarg();
            martakarg.Show();
            martakarg.Activate();
            if (martakarg.WindowState == WindowState.Minimized) martakarg.WindowState = WindowState.Normal;

        }

        private void OpenFireTask(object sender, RoutedEventArgs e)
        {
            if (!Task.HasReference)
                task = new Task();
            task.Show();
            task.Activate();
            if (task.WindowState == WindowState.Minimized) task.WindowState = WindowState.Normal;
        }

        private void OpenTargets(object sender, RoutedEventArgs e)
        {
            if (!Targets.HasReference)
                targets = new Targets();
            targets.Show();
            targets.Activate();
            if (targets.WindowState == WindowState.Minimized) targets.WindowState = WindowState.Normal;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            martakarg.Close();
            task.Close();
            targets.Close();
            Application.Current.Shutdown();
        }

        private void OpenTools(object sender, RoutedEventArgs e)
        {
            if (!Tools.HasReference)
                tools = new Tools();
            tools.Show();
            tools.Activate();
            if (tools.WindowState == WindowState.Minimized) tools.WindowState = WindowState.Normal;
        }

        private void SaveSettings(object sender, RoutedEventArgs e)
        {
            Skin OldSkin = App.Skin;
            new Settings()
            {
                IsDarkThemeSelected = (bool)Setting_IsDark.IsChecked,
                TargetsPageSize = int.Parse(Setting_TargetPageSize.Text)
            }.Save();

            if (OldSkin != App.Skin)
            {
                if (MessageBox.Show("վերագործարկել Ծրագիրը փոփոխությունները ուժի մեջ մտնելու համար", "Վերագործարկե՞լ", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                }
            }
        }

        private void InitializeSettings()
        {
            try
            {
                var settings = new Settings();

                settings.Initialize();
                if (settings.IsDarkThemeSelected)
                {
                    Setting_IsDark.IsChecked = true;
                }
                else
                {
                    Setting_IsLight.IsChecked = true;
                }
                Setting_TargetPageSize.Text = settings.TargetsPageSize.ToString();

            }
            catch
            {

            }
        }

        private void ExitExpanders(object sender, MouseButtonEventArgs e)
        {
            Settings.IsExpanded = false;
        }
    }
}
