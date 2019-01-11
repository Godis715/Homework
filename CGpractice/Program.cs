using System;


namespace CGpractice
{
    class Program
    {
        static void Main(string[] args)
        {
            // size of canvas is 255 x 255
            MyCanvas canvas = new MyCanvas(255, 255);

            for (int i = 0; i < 256; ++i)
            {
                for (int j = 0; j < 256; ++j)
                {
                    double dist = Math.Sqrt((127 - i) * (127 - i) + (127 - j) * (127 - j));
                    // color of pixel (i, j)
                    double r = (dist * Math.Sqrt(i * j)) % 255;
                    double g = 255 - r;
                    double b = 0;

                    canvas.SetPixel(i, j, new byte[] { (byte) b, (byte) r, (byte) g });
                }
            }

            canvas.Save("myImage.bmp");
        }
    }
}
