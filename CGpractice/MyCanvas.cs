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

        public int Width { get; private set; }
        public int Height { get; private set; }

        private int bpp;

        public MyCanvas(int w, int h)
        {
            Width = w;
            Height = h;

            image = new Bitmap(w, h);
            bd = image.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            bpp = Bitmap.GetPixelFormatSize(bd.PixelFormat) / 8;
        }

        public MyCanvas(String path)
        {
            image = new Bitmap(path);

            Width = image.Width;
            Height = image.Height;

            bd = image.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

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
            if (x < 0 || y < 0 || x >= Width || y >= Height) return;
            unsafe
            {
                var firstByte = (byte*)bd.Scan0 + (y * bd.Stride + x * bpp);
                for (int i = 0; i < color.Length; ++i)
                {
                    firstByte[i] = color[i];
                }
            }
        }

        public byte[] GetPixel(int x, int y)
        {
            return GetPixel(x, y, 3);
        }

        public bool EqualColor(int x, int y, byte[] color)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height) return false;
            var pxColor = GetPixel(x, y);
            for (int i = 0; i < color.Length; ++i)
                if (pxColor[i] != color[i]) return false;
            return true;
        }

        public void Save(String fileName)
        {
            image.UnlockBits(bd);
            image.Save(fileName, ImageFormat.Bmp);
            bd = image.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
        }

        public void Close()
        {
            image.UnlockBits(bd);
        }
    }

}
