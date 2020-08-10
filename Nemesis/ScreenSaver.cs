using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Nemesis
{
    public partial class Task
    {
        public void ScreenCapture(int res, string name)
        {
            Window win = TaskScreen;
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)(res * win.Width), (int)(res * win.Height), res * 96, res * 96, PixelFormats.Pbgra32);
            bmp.Render(win);

            string PicPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screens");
            if (!Directory.Exists(PicPath))
                Directory.CreateDirectory(PicPath);

            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));

            string filePath = System.IO.Path.Combine(PicPath, string.Format("{0:MMddyyyyhhmmss}.png", name.Replace("/", "-")));
            using (Stream stream = File.Create(filePath))
            {
                encoder.Save(stream);
            }
            //DrawingVisual vis = new DrawingVisual();
            //using (var dc = vis.RenderOpen())
            //{
            //    dc.DrawImage(bmp, new Rect { Width = bmp.Width, Height = bmp.Height });
            //}
            //var pdialog = new PrintDialog();
            //if (pdialog.ShowDialog() == true)
            //{
            //    pdialog.PrintVisual(vis, "My Image");
            //}

        }
    }
}
