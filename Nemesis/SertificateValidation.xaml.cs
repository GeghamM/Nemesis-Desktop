using BaseService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Windows;

namespace Nemesis
{
    /// <summary>
    /// Логика взаимодействия для SertificateValidation.xaml
    /// </summary>
    public partial class SertificateValidation : Window
    {
        private bool IsValidated = false;

        public SertificateValidation()
        {
            InitializeComponent();
            var query = new ManagementObjectSearcher("SELECT * FROM Win32_baseboard");
            var BaseBoard = query.Get();
            string serial = "";
            foreach (var Board in BaseBoard)
            {
                serial = Board["SerialNumber"].ToString();
            }
            UserCode.Text = serial;
        }

        public bool CheckSertificate(byte[] certificate)
        {
            
            var query = new ManagementObjectSearcher("SELECT * FROM Win32_baseboard");
            var BaseBoard = query.Get();
            String serial = "";
            String output = "";
            string Storedoutput = "";
            foreach (var Board in BaseBoard)
            {
                serial = Board["SerialNumber"].ToString();
            }
            using (var managed = new AesManaged())
            {
                var encrypt = AES.Encrypt(serial);
                //var decrypt = AES.Decrypt(encrypt, System.Text.Encoding.UTF8.GetBytes(Key), managed.IV);
                Storedoutput = AES.Decrypt(certificate);
                output = encrypt.ToBase36String();
            }
            return Storedoutput == output;
        }

        private void TryMakeSertificate(object sender, RoutedEventArgs e)
        {
            var query = new ManagementObjectSearcher("SELECT * FROM Win32_baseboard");
            var BaseBoard = query.Get();
            String serial = "";
            String output = "";
            foreach (var Board in BaseBoard)
            {
                serial = Board["SerialNumber"].ToString();
            }
            using (var managed = new AesManaged())
            {
                var encrypt = AES.Encrypt(serial);
                var decrypt = AES.Decrypt(encrypt);
                output = encrypt.ToBase36String();
            }
            if (UserHash.Text == output)
            {
                try
                {
                    using (Stream fStream = new FileStream("Certificate", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                    {
                        using (var managed = new AesManaged())
                        {
                            new BinaryFormatter().Serialize(fStream, AES.Encrypt(output));
                        }
                    }
                }
                catch { }
                IsValidated = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Սխալ Սերիա","Սխալ");
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if(IsValidated != true)
            Application.Current.Shutdown();

        }
    }
}
