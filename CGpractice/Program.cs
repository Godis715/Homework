using System;
using System.Collections.Generic;

namespace CGpractice
{
    class Program
    {

        static void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }

        static void CirclePixel(int cx, int cy, int x, int y, MyCanvas canvas, byte[] color)
        {
            int dx = x - cx;
            int dy = y - cy;

            canvas.SetPixel(cx + dx, cy + dy, color);
            canvas.SetPixel(cx + dy, cy + dx, color);
            canvas.SetPixel(cx + dx, cy - dy, color);
            canvas.SetPixel(cx + dy, cy - dx, color);
            canvas.SetPixel(cx - dx, cy + dy, color);
            canvas.SetPixel(cx - dy, cy + dx, color);
            canvas.SetPixel(cx - dx, cy - dy, color);
            canvas.SetPixel(cx - dy, cy - dx, color);

        }

        static byte[] MulColor(byte[] color, double koef)
        {
            var newColor = new byte[color.Length];
            for (int i = 0; i < color.Length; ++i)
            {
                newColor[i] = (byte)((double)color[i] * koef);
            }
            return newColor;
        }

        static void DrawHardLine(int x1, int y1, int x2, int y2, MyCanvas canvas, byte[] color)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;

            bool reversed = Math.Abs(dy) > Math.Abs(dx);
            if (reversed)
            {
                Swap(ref dx, ref dy);
                Swap(ref x1, ref y1);
                Swap(ref x2, ref y2);
            }
            if (dx < 0)
            {
                dx = -dx;
                dy = -dy;
                Swap(ref x1, ref x2);
                Swap(ref y1, ref y2);
            }

            int d = Math.Abs(2 * dy) - dx;

            int x = x1, y = y1;

            while (x != x2)
            {
                if (reversed)
                {
                    canvas.SetPixel(y, x, color);
                }
                else
                {
                    canvas.SetPixel(x, y, color);
                }

                ++x;

                if (d < 0)
                {
                    d += Math.Abs(2 * dy);
                }
                else
                {
                    d += 2 * (Math.Abs(dy) - dx);
                    y += Math.Sign(dy);
                }
            }
        }

        static void DrawCircle(int cx, int cy, int r, MyCanvas canvas, byte[] color)
        {
            int x = 0, y = r;
            int d = 0;

            while (x <= y)
            {
                CirclePixel(cx, cy, cx + x, cy + y, canvas, color);
                if (d < 0)
                {
                    int right = d + 2 * y - 1;
                    if (Math.Abs(d) > Math.Abs(right))
                    {
                        d += 2 * x + 1;
                        x++;
                    }
                    else
                    {
                        d += 2 * (x - y + 1);
                        x++; y--;
                    }
                }
                else
                {
                    int down = d - 2 * x - 1;
                    if (Math.Abs(d) > Math.Abs(down))
                    {
                        d -= 2 * y + 1;
                        y--;
                    }
                    else
                    {
                        d += 2 * (x - y + 1);
                        x++; y--;
                    }
                }
            }
        }
        
        private class IntComp : IComparer<int>
        {
            public int Compare(int a, int b)
            {
                return (a < b) ? 1 : 0;
            }
        }

        static bool NotToShowSegment(int[] edge, int xl, int xr, int yb, int yu)
        {
            return (edge[0] <= xl) & (edge[2] <= xl) |
                    (edge[0] >= xr) & (edge[2] >= xr) |
                    (edge[1] <= yb) & (edge[3] <= yb) |
                    (edge[1] >= yu) & (edge[3] >= yu);
        }

        static bool ClipSegment(int[] edge, int xl, int xr, int yb, int yu)
        {
            double tg = (double)(edge[3] - edge[1]) / (edge[2] - edge[0]);

            if (edge[0] < xl && edge[2] > xl)
            {
                int deltaX2 = xl - edge[2];
                edge[0] = xl;
                edge[1] = edge[3] + (int)(deltaX2 * tg);
                if(NotToShowSegment(edge, xl, xr, yb, yu)) return false;
            }
            else if (edge[0] > xl && edge[2] < xl)
            {
                int deltaX1 = xl - edge[0];
                edge[2] = xl;
                edge[3] = edge[1] + (int)(deltaX1 * tg);
                if(NotToShowSegment(edge, xl, xr, yb, yu)) return false;
            }

            if (edge[0] < xr && edge[2] > xr)
            {
                int deltaX1 = xr - edge[0];
                edge[2] = xr;
                edge[3] = edge[1] + (int)(deltaX1 * tg);
                if(NotToShowSegment(edge, xl, xr, yb, yu)) return false;
            }
            else if (edge[0] > xr && edge[2] < xr)
            {
                int deltaX2 = xr - edge[2];
                edge[0] = xr;
                edge[1] = edge[3] + (int)(deltaX2 * tg);
                if(NotToShowSegment(edge, xl, xr, yb, yu)) return false;
            }

            if (edge[1] < yb && edge[3] > yb)
            {
                int deltaY3 = yb - edge[3];
                edge[1] = yb;
                edge[0] = edge[2] + (int)(deltaY3 * (1 / tg));
                if(NotToShowSegment(edge, xl, xr, yb, yu)) return false;
            }
            else if (edge[1] > yb && edge[3] < yb)
            {
                int deltaY1 = yb - edge[1];
                edge[3] = yb;
                edge[2] = edge[0] + (int)(deltaY1 * (1 / tg));
                if(NotToShowSegment(edge, xl, xr, yb, yu)) return false;
            }

            if (edge[1] < yu && edge[3] > yu)
            {
                int deltaY1 = yu - edge[1];
                edge[3] = yu;
                edge[2] = edge[0] + (int)(deltaY1 * (1 / tg));
                if(NotToShowSegment(edge, xl, xr, yb, yu)) return false;
            }
            else if (edge[1] > yu && edge[3] < yu)
            {
                int deltaY2 = yu - edge[3];
                edge[1] = yu;
                edge[0] = edge[2] + (int)(deltaY2 * (1 / tg));
                if(NotToShowSegment(edge, xl, xr, yb, yu)) return false;
            }
            return true;
        }
        

        static MyCanvas ImageProcessing(MyCanvas canvas, double[][] core, int channel)
        {
            int shift = core.Length / 2;
            int center = (core.Length % 2 == 0) ? core.Length / 2 - 1 : core.Length / 2;

            MyCanvas newCanvas = new MyCanvas(canvas.Width, canvas.Height);

            for (int i = 0; i < canvas.Width; ++i)
            {
                for (int j = 0; j < canvas.Height; ++j)
                {
                    if (i < center || i >= canvas.Width - shift || j < center || j >= canvas.Height - shift)
                    {
                        continue;
                    }

                    double value = 0.0;
                    for (int x = 0; x < core.Length; ++x)
                    {
                        for (int y = 0; y < core.Length; ++y)
                        {
                            byte[] color = canvas.GetPixel(i - center + x, j - center + y);
                            value += (double)color[channel] * core[y][x];
                        }
                    }

                    byte normalValue;
                    value = Math.Abs(value);
                    if (value > 255.0) normalValue = 255;
                    else normalValue = (byte)value;

                    newCanvas.SetPixel(i, j, new byte[] { normalValue, normalValue, normalValue });
                }
            }
            return newCanvas;
        }

        static void Treshold(MyCanvas canvas, double down, double up, int channel)
        {
            for (int i = 0; i < canvas.Width; ++i) {
                for (int j = 0; j < canvas.Height; ++j) {
                    double value = (double)(canvas.GetPixel(i, j)[channel]) / 255;
                    if (value >= up) canvas.SetPixel(i, j, new byte[] { 255, 255, 255 });
                    else if (value <= down) canvas.SetPixel(i, j, new byte[] { 0, 0, 0 });
                    else canvas.SetPixel(i, j, new byte[] { 127, 127, 127});
                }
            }
        }

        static void Gamma(MyCanvas canvas, double gamma)
        {
            for (int i = 0; i < canvas.Width; ++i)
            {
                for (int j = 0; j < canvas.Height; ++j)
                {
                    byte[] resColor = new byte[3];
                    var color = canvas.GetPixel(i, j);
                    for (int k = 0; k < 3; ++k) {
                        double value = Math.Pow((double)color[k] / 255, gamma) * 255;
                        resColor[k] = (byte)value;
                    }
                    canvas.SetPixel(i, j, resColor);
                }
            }
        }

        static double[][] GetDerivGaussianCore(int size, double d, bool isDirX)
        {
            int hs = size / 2;
            double elemSum = 0.0;
            if (size % 2 == 0) size++;
            var core = new double[size][];
            for (int i = -hs; i <= hs; ++i)
            {
                core[i + hs] = new double[size];
                for (int j = -hs; j <= hs; ++j)
                {
                    core[i + hs][j + hs] = (double)((!isDirX) ? i : j) / (2 * Math.PI * Math.Pow(d, 4)) * Math.Pow(Math.E, -(double)(i * i + j * j) / (2.0 * d * d));
                    elemSum += Math.Abs(core[i + hs][j + hs]);
                }
            }

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    core[i][j] /= elemSum;
                }
            }

            return core;
        }

        static double[][] GetGaussianCore(int size, double d)
        {
            int hs = size / 2;
            double elemSum = 0.0;
            if (size % 2 == 0) size++;
            var core = new double[size][];
            for (int i = -hs; i <= hs; ++i) {
                core[i + hs] = new double[size];
                for (int j = -hs; j <= hs; ++j) {
                    core[i + hs][j + hs] = 1.0 / (2 * Math.PI * d * d) * Math.Pow(Math.E, -(double)(i * i + j * j) / (2.0 * d * d));
                    elemSum += core[i + hs][j + hs];
                }
            }

            for (int i = 0; i < size; ++i) {
                for (int j = 0; j < size; ++j) {
                    core[i][j] /= elemSum;
                }
            }

            return core;
        }

        static void Monochrome(MyCanvas canvas)
        {
            //0.299r + 0.587g + 0.114b
            for (int i = 0; i < canvas.Width; ++i)
            {
                for (int j = 0; j < canvas.Height; ++j)
                {
                    var col = canvas.GetPixel(i, j);
                    byte resColor = (byte)(col[0] * 0.114 + col[1] * 0.587 + col[2] * 0.299);
                    canvas.SetPixel(i, j, new byte[] { resColor, resColor, resColor });
                }
            }
        }

        static void NonMaximumDeleting(ref MyCanvas tmp, MyCanvas gradX, MyCanvas gradY)
        {
            MyCanvas newCanvas = new MyCanvas(tmp.Width, tmp.Height);
            for (int i = 1; i < tmp.Width - 1; ++i)
            {
                for (int j = 1; j < tmp.Height - 1; ++j)
                {
                    double angle = 0.0;
                    if (gradX.GetPixel(i, j)[0] == 0)
                        if (gradY.GetPixel(i, j)[0] == 0) continue;
                        else angle = 90;

                    if (gradX.GetPixel(i, j)[0] < 8)
                        if (gradY.GetPixel(i, j)[0] < 8) continue;

                    else angle = (Math.Atan((double)gradY.GetPixel(i, j)[0] / gradX.GetPixel(i, j)[0])) / Math.PI * 180;
                    int ang = (int)Math.Round(angle / 45);

                    if (ang == 0 && (tmp.GetPixel(i - 1, j)[0] < tmp.GetPixel(i, j)[0] && tmp.GetPixel(i, j)[0] < tmp.GetPixel(i + 1, j)[0] ||
                        tmp.GetPixel(i - 1, j)[0] > tmp.GetPixel(i, j)[0] && tmp.GetPixel(i, j)[0] > tmp.GetPixel(i + 1, j)[0]))
                    {
                        newCanvas.SetPixel(i, j, new byte[] { 0, 0, 0});
                    }
                    else if (ang == 1 && (tmp.GetPixel(i - 1, j - 1)[0] < tmp.GetPixel(i, j)[0] && tmp.GetPixel(i, j)[0] < tmp.GetPixel(i + 1, j + 1)[0] ||
                      tmp.GetPixel(i - 1, j - 1)[0] > tmp.GetPixel(i, j)[0] && tmp.GetPixel(i, j)[0] > tmp.GetPixel(i + 1, j + 1)[0] ||

                      tmp.GetPixel(i - 1, j + 1)[0] < tmp.GetPixel(i, j)[0] && tmp.GetPixel(i, j)[0] < tmp.GetPixel(i + 1, j - 1)[0] ||
                      tmp.GetPixel(i - 1, j + 1)[0] > tmp.GetPixel(i, j)[0] && tmp.GetPixel(i, j)[0] > tmp.GetPixel(i + 1, j - 1)[0]))
                    {
                        newCanvas.SetPixel(i, j, new byte[] { 0, 0, 0 });
                    }
                    else if (ang == 2 && (tmp.GetPixel(i, j - 1)[0] < tmp.GetPixel(i, j)[0] && tmp.GetPixel(i, j)[0] < tmp.GetPixel(i, j + 1)[0] ||
                      tmp.GetPixel(i, j - 1)[0] > tmp.GetPixel(i, j)[0] && tmp.GetPixel(i, j)[0] > tmp.GetPixel(i, j + 1)[0]))
                    {
                        newCanvas.SetPixel(i, j, new byte[] { 0, 0, 0 });
                    }
                    else
                    {
                        newCanvas.SetPixel(i, j, tmp.GetPixel(i, j));
                    }
                }
            }
            tmp = newCanvas;
        }

        static void FillGroup(MyCanvas img, int[] pointX, int[] pointY, byte[] color)
        {
            for (int i = 0; i < pointX.Length; ++i)
            {
                img.SetPixel(pointX[i], pointY[i], color);
            }
        }

        private static int[] stepX = { -1, 1, 0, -1, 1, 0, -1, 1 };

        private static int[] stepY = { -1, 1, -1, 0, -1, 1, 1, 0 };

        static void DeleteNonBorders(MyCanvas img)
        {
            for (int i = 0; i < img.Width; ++i)
            {
                for (int j = 0; j < img.Height; ++j)
                {
                    var col = img.GetPixel(i, j);
                    if (col[0] == 127)
                    {
                        List<int> pointX = new List<int>();
                        List<int> pointY = new List<int>();

                        Queue<int> queueX = new Queue<int>();
                        Queue<int> queueY = new Queue<int>();

                        bool delete = true;

                        pointX.Add(i); pointY.Add(j); queueX.Enqueue(i); queueY.Enqueue(j);

                        while (queueX.Count != 0)
                        {
                            int x = queueX.Dequeue();
                            int y = queueY.Dequeue();

                            for(int k = 0; k < 8; ++k)
                            {
                                byte currCol = img.GetPixel(x + stepX[k], y + stepY[k])[0];
                                if (currCol == 255) delete = false;
                                else if (currCol == 127)
                                {
                                    queueX.Enqueue(x + stepX[k]); pointX.Add(x + stepX[k]);
                                    queueY.Enqueue(y + stepY[k]); pointY.Add(y + stepY[k]);

                                    img.SetPixel(x + stepX[k], y + stepY[k], new byte[] { 126, 126, 126 });
                                }
                            }
                        }

                        if (delete) FillGroup(img, pointX.ToArray(), pointY.ToArray(), new byte[] { 0, 0, 0 });
                        else FillGroup(img, pointX.ToArray(), pointY.ToArray(), new byte[] { 255, 255, 255 });
                    }
                }
            }
        }

        static void AutoCorrection(MyCanvas img)
        {
            int maxValue = 0;
            int minValue = 255;
            for (int i = 0; i < img.Width - 10; ++i)
            {
                for (int j = 0; j < img.Height - 10; ++j)
                {
                    var col = img.GetPixel(i, j)[0];
                    if (col > maxValue) maxValue = col;
                    if (col < minValue) minValue = col;
                }
            }

            if (maxValue == 0) return;

            for (int i = 0; i < img.Width - 10; ++i)
            {
                for (int j = 0; j < img.Height - 10; ++j)
                {
                    var col = img.GetPixel(i, j)[0];
                    byte value = (byte)((double)(col - minValue) / maxValue * 255);
                    img.SetPixel(i, j, new byte[] { value, value, value });
                }
            }
        }

        static void Main(string[] args)
        {
            string name = "liza";
            string format = "jpg";
            MyCanvas canvas = new MyCanvas("img\\" + name + "." + format);


            double[][] coreDown = GetDerivGaussianCore(7, 0.7, false);

            double[][] coreRight = GetDerivGaussianCore(7, 0.7, true);

            double[][] blurCore = GetGaussianCore(9, 1.4);

            int channel = 0;

            Monochrome(canvas);

            var tmp = ImageProcessing(canvas, blurCore, channel);

            AutoCorrection(tmp);

            var gradY = ImageProcessing(tmp, coreDown, channel);

            var gradX = ImageProcessing(tmp, coreRight, channel);

            for (int i = 0; i < tmp.Width; ++i) {
                for (int j = 0; j < tmp.Height; ++j) {
                    double value = Math.Sqrt(gradX.GetPixel(i, j)[channel] * gradX.GetPixel(i, j)[channel] +
                                   gradY.GetPixel(i, j)[channel] * gradY.GetPixel(i, j)[channel]);
                    value = value / Math.Sqrt(2);
                    tmp.SetPixel(i, j, new byte[] { (byte)value, (byte)value, (byte)value });
                }
            }

            AutoCorrection(tmp);

            tmp.Save("gradYLena.bmp");

            NonMaximumDeleting(ref tmp, gradX, gradY);

            Treshold(tmp, 0.01, 0.3, channel);

            tmp.Save("gradYLena1.bmp");

            DeleteNonBorders(tmp);

            tmp.Save("img//result" + name + ".bmp");


            /*
            int xl = 30;
            int yu = 130;
            int yb = 70;
            int xr = 180;

            DrawHardLine(xl, yb, xl, yu, canvas, color);
            DrawHardLine(xl, yu, xr, yu, canvas, color);
            DrawHardLine(xr, yu, xr, yb, canvas, color);
            DrawHardLine(xr, yb, xl, yb, canvas, color);
            */
            /*int[][] edges = new int[][] {
                new int[] { 50, 100, 150, 30 },
                new int[] { 150, 30, 250, 200 },
                new int[] { 250, 200, 130, 60},
                new int[] { 130, 60, 50, 100},
                new int[] { 20, 140, 250, 150 },
                new int[] { 50, 50, 170, 69 },
                new int[] { 50, 50, 250, 69 },
                }; */

            /*
            int[][] edges = new int[height / 5][];

            for (int i = 0; i < edges.Length; i++)
            {
                edges[i] = new int[] { 0, i * 10, width - 1, i * 10 + 15 };
            }

            for (int i = 0; i < edges.Length; ++i)
            {
                if (NotToShowSegment(edges[i], xl, xr, yb, yu)) continue;
                if (ClipSegment(edges[i], xl, xr, yb, yu))
                    DrawHardLine(edges[i][0], edges[i][1], edges[i][2], edges[i][3], canvas, color);
            }

            canvas.Save("result.bmp"); */

            /*
            for (int i = 0; i < edges.Length; ++i)
            {
                DrawHardLine(edges[i][0], edges[i][1], edges[i][2], edges[i][3], canvas, color);
            }

            int ymax = 0;
            int ymin = height;

            for (int i = 0; i < edges.Length; ++i)
            {
                if (edges[i][1] > edges[i][3])
                {
                    Swap(ref edges[i][1], ref edges[i][3]);
                    Swap(ref edges[i][0], ref edges[i][2]);
                }
                if (edges[i][3] > ymax) ymax = edges[i][3];
                if (edges[i][1] < ymin) ymin = edges[i][1];
            }

            for (int i = ymin; i <= ymax; ++i)
            {
                var xlist = new List<int>();
                for (int j = 0; j < edges.Length; ++j)
                {
                    if (edges[j][3] - edges[j][1] != 0 && i >= edges[j][1] && i <= edges[j][3])
                    {
                        int x = (int)(edges[j][0] + (i - edges[j][1]) * (double)(edges[j][2] - edges[j][0]) / (edges[j][3] - edges[j][1]));
                        xlist.Add(x);
                    }
                }
                xlist.Sort(new IntComp());

                var enumer = xlist.GetEnumerator();
                enumer.MoveNext();
                while (true)
                {
                    int x1 = enumer.Current;
                    enumer.MoveNext();
                    int x2 = enumer.Current;
                    if (x1 > x2) Swap(ref x1, ref x2);
                    for (int j = x1; j <= x2; ++j)
                    {
                        canvas.SetPixel(j, i, new byte[] { 200, 30, 255});
                    }
                    if (!enumer.MoveNext()) break;
                }
            }

            canvas.Save("result.bmp");
            */
            /*
            int px = 70;
            int py = 60;

            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    double tmp = (double)(i + j) / (width + height);
                    int r = (int)(tmp * tmp * 255);
                    int b = (int)((1 - tmp) * (1 - tmp) * 255);
                    int g = 0;

                    int cx = width / 2;
                    int cy = height / 2;
                    int rad = 150;
                    double dist = (i - cx) * (i - cx) + (j - cy) * (j - cy);
                    if (dist < rad * rad)
                    {
                        double koef = dist / rad / rad;
                        r = (int)(r * koef);
                        b = (int)(b * koef);
                        g = (int)(g * koef);
                    }
                    canvas.SetPixel(i, j, new byte[] { (byte)b, (byte)g, (byte)r });
                }
            }

            DrawHardLine(px, py, px, 150 + py, canvas, color);
            DrawHardLine(px, 150 + py, 100 + px, py, canvas, color);
            DrawHardLine(100 + px, py, 100 + px, 150 + py, canvas, color);
            DrawHardLine(20 + px, -20 + py, 80 + px, -20 + py, canvas, color);
            DrawCircle(px + 50, py + 72, 110, canvas, color);
            DrawCircle(px + 50, py + 72, 115, canvas, color);

            canvas.Save("resultSoft.bmp"); */
            canvas.Close();

        }
    }
}
