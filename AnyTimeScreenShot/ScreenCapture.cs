using System.Drawing.Imaging;
using System.Drawing;
using System;
using System.Windows.Forms;

namespace AnyTimeScreenShot
{
    class ScreenCapture
    {
        static public void Capture(Rectangle rect)
        {
            Bitmap captureBitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics captureGraphics = Graphics.FromImage(captureBitmap);

            captureGraphics.CopyFromScreen( rect.X, rect.Y, 0, 0, captureBitmap.Size );

            captureBitmap.Save(@"D:\Pictures\cap.jpg", ImageFormat.Jpeg);
        }
    }
}
