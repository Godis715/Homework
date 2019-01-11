using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace CGpractice
{
    class MyCanvas
    {

        private Bitmap image;
        private BitmapData bd;

        private int width;
        private int height;
        private int bpp;

        public MyCanvas(int w, int h)
        {
            width = w;
            height = h;

            image = new Bitmap(w, h);
            bd = image.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            bpp = Bitmap.GetPixelFormatSize(bd.PixelFormat) / 8;
        }

        public byte[] GetPixel(int x, int y, int bytes)
        {
            byte[] color = new byte[bytes];

            unsafe
            {
                var firstByte = (byte*)bd.Scan0 + (y * bd.Stride + x * bpp);
                for (int i = 0; i < bytes; ++i)
                {
                    color[i] = firstByte[i];
                }
            }

            return color;
        }

        public void SetPixel(int x, int y, byte[] color)
        {
            unsafe
            {
                var firstByte = (byte*)bd.Scan0 + (y * bd.Stride + x * bpp);
                for (int i = 0; i < color.Length; ++i)
                {
                    firstByte[i] = color[i];
                }
            }
        }

        public void Save(String fileName)
        {
            image.UnlockBits(bd);

            image.Save(fileName, ImageFormat.Bmp);
        }
    }

}
