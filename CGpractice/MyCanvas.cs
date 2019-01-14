using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace CGpractice
{
    class MyCanvas
    {

        private Bitmap image;
        private BitmapData bd;

        public readonly int width;
		public readonly int height;
        private int bpp;

        public MyCanvas(int w, int h)
        {
            width = w;
            height = h;

            image = new Bitmap(w, h);
            bd = image.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            bpp = Bitmap.GetPixelFormatSize(bd.PixelFormat) / 8;
        }

		public MyCanvas(string fileName)
		{
			image = new Bitmap(fileName);
			width = image.Width - 1;
			height = image.Height - 1;
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

		public void DiffusePseudomonotone()
		{
			int d = 0;
			for (int y = 0; y <= height; y++)
			{
				d = 0;
				for (int x = 0; x <= width; x++)
				{
					var color = GetPixel(x, y, 1)[0];
					if (color + d < 128)
					{
						SetPixel(x, y, new byte[] { 0, 0, 0});
						d = (color + d) - 0;
					}
					else
					{
						SetPixel(x, y, new byte[] { 255, 255, 255 });
						d = (color + d) - 255;
					}
				}
			}
		}

		public void DrawLineParam(double x1, double y1, double x2, double y2)
		{
			int N = Math.Max(Math.Abs((int)(x2 - x1)), Math.Abs((int)(y2 - y1))) + 1;

			double dt = 1 / (double)(N - 1);

			double vec1 = 1;
			double vec2 = 6;

			double r = 5;
			double g = 40;
			double b = 40;

			double tx = x1;
			double ty = y1;

			double tv = vec1;


			for (int i = 0; i < N; i++)
			{
				SetPixel((int)tx, (int)ty, new byte[] { (byte)(r * tv), (byte)(g * tv), (byte)(b * tv) });
				tx += dt * (x2 - x1);
				ty += dt * (y2 - y1);
				tv += dt * (vec2 - vec1);
			}
		}

		public void DrawLineDDA(double x1, double y1, double x2, double y2, double r = 200, double g = 200, double b = 20)
		{
			double k = (Math.Abs(y2 - y1)) / (double)(Math.Abs(x2 - x1));

			double tx = x1;
			double ty = y1;

			double dx;
			double dy;
			if (k <= 1)
			{
				dx = 1;
				if (x2 - x1 < 0) dx *= -1;

				dy = k;
				if (y2 - y1 < 0) dy *= -1;

				while ((int)tx != (int)x2)
				{
					SetPixel((int)tx, (int)ty, new byte[] { (byte)r, (byte)g, (byte)b });
					tx += dx;
					ty += dy;
				}
			}
			else
			{
				dx = 1 / k;
				if (x2 - x1 < 0) dx *= -1;


				dy = 1;
					if (y2 - y1 < 0) dy *= -1;

				while ((int)ty != (int)y2)
				{
					SetPixel((int)tx, (int)ty, new byte[] { (byte)r, (byte)g, (byte)b });
					tx += dx;
					ty += dy;
				}
			}
		}

		public void DrawLineBrez(double x1, double y1, double x2, double y2, double r = 100, double g = 200, double b = 20)
		{
			double dy = y2 - y1;
			double dx = x2 - x1;
			double d = 2 * dy - dx;
			double x = x1;
			double y = y1;
			do
			{
				SetPixel((int)x, (int)y, new byte[] { (byte)r, (byte)g, (byte)b });
				if (d < 0) d += 2 * dy;
				else { d += 2 * (dy - dx); ++y; }
				++x;
			} while (x <= x2);
		}

		public void DrawLineBrezMod(double x1, double y1, double x2, double y2, double r = 100, double g = 200, double b = 20)
		{
			double I = 1;

			double x = x1;
			double y = y1;
			double k = I * (y2 - y1) / (x2 - x1);
			double d = I / 2;
			double dmax = I - k;
			do
			{
				SetPixel((int)x, (int)y, new byte[] { (byte)(r * d), (byte)(g * d), (byte)(b * d) });
				if (d >= dmax) { ++y; d -= dmax; }
				else d += k;
				++x;
			} while (x <= x2);
		}

		private void PutCircPixels(int x0, int y0, int x1, int y1, int x2, int y2)
		{
			DrawLineDDA(x1, y1, x2, y2); //1 сектор
			DrawLineDDA(2 * x0 - x1, 2 * y0 - y1, 2 * x0 - x2, 2 * y0 - y2); //Противоположно 1 сектору

			DrawLineDDA(x0 + (y1 - y0), y0 + (x1 - x0), x0 + (y2 - y0), y0 + (x2 - x0)); //2 сектор
			DrawLineDDA(x0 - (y1 - y0), y0 - (x1 - x0), x0 - (y2 - y0), y0 - (x2 - x0)); //Противоположно 2 сектору

			DrawLineDDA(x0 - (y1 - y0), y0 + (x1 - x0), x0 - (y2 - y0), y0 + (x2 - x0)); //3 сектор
			DrawLineDDA(x0 + (y1 - y0), y0 - (x1 - x0), x0 + (y2 - y0), y0 - (x2 - x0)); //Противоположно 3 сектору

			DrawLineDDA(2 * x0 - x1, y1, 2 * x0 - x2, y2); //4 сектор
			DrawLineDDA(x1, 2 * y0 - y1, x2, 2 * y0 - y2); //Противоположно 4 сектору
		}
		public void DrawCircleParam(int x0, int y0, int rad)
		{
			int phi = 0;
			int x1, x2 = x0 + rad;
			int y1, y2 = y0;
			for (; phi <= 45; ++phi) 
			{
				x1 = x2; y1 = y2;
				x2 = (int)(rad * Math.Cos((phi * Math.PI) / 180)) + x0;
				y2 = (int)(rad * Math.Sin((phi * Math.PI) / 180)) + y0;
				PutCircPixels(x0, y0, x1, y1, x2, y2);
			}
		}

		private void PutCircPixels(int x0, int y0, int x, int y, double r, double g, double b)
		{
			SetPixel(x, y, new byte[] { (byte)r, (byte)g, (byte)b }); //1 сектор
			SetPixel(2 * x0 - x, 2 * y0 - y, new byte[] { (byte)r, (byte)g, (byte)b }); //Противоположно 1 сектору

			SetPixel(x0 + (y - y0), y0 + (x - x0), new byte[] { (byte)r, (byte)g, (byte)b }); //2 сектор
			SetPixel(x0 - (y - y0), y0 - (x - x0), new byte[] { (byte)r, (byte)g, (byte)b }); //Противоположно 2 сектору

			SetPixel(x0 - (y - y0), y0 + (x - x0), new byte[] { (byte)r, (byte)g, (byte)b }); //3 сектор
			SetPixel(x0 + (y - y0), y0 - (x - x0), new byte[] { (byte)r, (byte)g, (byte)b }); //Противоположно 3 сектору

			SetPixel(2 * x0 - x, y, new byte[] { (byte)r, (byte)g, (byte)b }); //4 сектор
			SetPixel(x, 2 * y0 - y, new byte[] { (byte)r, (byte)g, (byte)b }); //Противоположно 4 сектору
		}

		public void DrawCircleNeyavnoe(int x0, int y0, int rad)
		{
			for (int x = x0; x <= x0 + rad / Math.Sqrt(2); ++x)
			{
				int y = (int)(Math.Sqrt(rad * rad - (x - x0) * (x - x0))) + y0;
				PutCircPixels(x0, y0, x, y, 10, 150, 20);
			}
		}

		public void DrawCircleBrez(int x0, int y0, int rad)
		{
			int d = 3 - 2 * rad;
			int x = x0;
			int y = y0 + rad;
			do
			{
				PutCircPixels(x0, y0, x, y, 50, 60, 100);
				if (d < 0)
				{
					d += 4 * (x - x0) + 6;
				}
				else
				{
					d += 4 * (x - x0 + y0 - y) + 10; --y;
				}
				++x;
			} while (x < x0 + rad / Math.Sqrt(2));
		}

		private void FillPixel(int tx, int ty, byte[] color, byte[] baseColor)
		{
			var tempColor = GetPixel(tx, ty, 3);
			if (tempColor[0] == baseColor[0] && tempColor[1] == baseColor[1] && tempColor[2] == baseColor[2])
			{
				SetPixel(tx, ty, color);
				if (tx - 1 >= 0) FillPixel(tx - 1, ty, color, baseColor);
				if (tx + 1 <= width) FillPixel(tx + 1, ty, color, baseColor);
				if (ty - 1 >= 0) FillPixel(tx, ty - 1, color, baseColor);
				if (ty + 1 <= height) FillPixel(tx, ty + 1, color, baseColor);
			}
		}
		public void FillOnClickRec(int x0, int y0, double r, double g, double b)
		{
			var baseColor = GetPixel(x0, y0, 3);
			FillPixel(x0, y0, new byte[] { (byte)r, (byte)g, (byte)b }, baseColor);
		}

		public void FillOnClickIt(int x0, int y0, double r, double g, double b)
		{
			var baseColor = GetPixel(x0, y0, 3);
			var color = new byte[] { (byte)r, (byte)g, (byte)b };	
			var stack = new Stack<int[]>();
			stack.Push(new int[] { x0, y0 });
			do
			{
				var tempPixel = stack.Pop();
				int tx = tempPixel[0];
				int ty = tempPixel[1];
				var tempColor = GetPixel(tx, ty, 3);
				if (tempColor[0] == baseColor[0] && tempColor[1] == baseColor[1] && tempColor[2] == baseColor[2])
				{
					SetPixel(tx, ty, color);
					if (tx - 1 >= 0) stack.Push(new int[] { tx - 1, ty });
					if (tx + 1 <= width) stack.Push(new int[] { tx + 1, ty });
					if (ty - 1 >= 0) stack.Push(new int[] { tx, ty - 1 });
					if (ty + 1 <= height) stack.Push(new int[] { tx, ty + 1 });
				}
			} while (stack.Count != 0);

		}
	}

	class Vector2
	{
		public double x;
		public double y;
		public Vector2(double _x, double _y)
		{
			x = _x;
			y = _y;
		}

		public static Vector2 operator +(Vector2 vec1, Vector2 vec2)
		{
			return new Vector2(vec1.x + vec2.x, vec1.y + vec2.y);
		}

		public static Vector2 operator -(Vector2 vec1, Vector2 vec2)
		{
			return new Vector2(vec1.x - vec2.x, vec1.y - vec2.y);
		}

		public static Vector2 operator *(Vector2 vec1, double m)
		{
			return new Vector2(vec1.x * m, vec1.y * m);
		}

		public static double operator *(Vector2 vec1, Vector2 vec2)
		{
			return vec1.x * vec2.x + vec1.y * vec2.y;
		}

		public static Vector2 operator /(Vector2 vec1, double m)
		{
			return new Vector2(vec1.x / m, vec1.y / m);
		}

		public static double Distance(Vector2 vec1, Vector2 vec2)
		{
			return (double)Math.Sqrt(Math.Pow(vec1.x - vec2.x, 2) + Math.Pow(vec1.y - vec2.y, 2));
		}

		public double Length()
		{
			return (double)Math.Sqrt(x * x + y * y);
		}

		public static double Dot(Vector2 vec1, Vector2 vec2) {
			return (vec1.x * vec2.x + vec1.y * vec2.y);
		}

		public static double Cross(Vector2 vec1, Vector2 vec2)
		{
			return -(vec1.x * vec2.y - vec1.y * vec2.x);
		}

		public static double Angle(Vector2 vec1, Vector2 vec2)
		{

			double angle = Math.Acos(Vector2.Dot(vec1, vec2) / (vec1.Length() * vec2.Length()));
			//if (Vector2.Cross(vec1, vec2) >= 0)
			//{
			//	return angle;
			//}
			//else
			//{
			//	return Math.PI * 2 - angle;
			//}
			return angle;
		}
	}

	class Nangle
	{
		private MyCanvas canvas;
		private Vector2[] points;
		private Vector2 pArea1;
		private Vector2 pArea2;
		private void CountArea()
		{
			pArea1 = new Vector2(canvas.width + 1, canvas.height + 1);
			pArea2 = new Vector2(0, 0);
			for (int i = 0; i < points.Length; i++)
			{
				if (points[i].x < pArea1.x) pArea1.x = points[i].x;
				if (points[i].y < pArea1.y) pArea1.y = points[i].y;
				if (points[i].x > pArea2.x) pArea2.x = points[i].x;
				if (points[i].y > pArea2.y) pArea2.y = points[i].y;
			}
			//canvas.DrawLineDDA(pArea1.x, pArea1.y, pArea1.x, pArea2.y, 150, 10, 100);
			//canvas.DrawLineDDA(pArea1.x, pArea1.y, pArea2.x, pArea1.y, 150, 10, 100);
			//canvas.DrawLineDDA(pArea2.x, pArea2.y, pArea2.x, pArea1.y, 150, 10, 100);
			//canvas.DrawLineDDA(pArea2.x, pArea2.y, pArea1.x, pArea2.y, 150, 10, 100);
		}

		public Nangle(Vector2[] _points, MyCanvas _canvas)
		{
			points = _points;
			canvas = _canvas;
			for (int i = 1; i < points.Length; i++)
			{
				canvas.DrawLineBrez(points[i - 1].x, points[i - 1].y, points[i].x, points[i].y);
			}
			canvas.DrawLineBrez(points[points.Length - 1].x, points[points.Length - 1].y, points[0].x, points[0].y);
			CountArea();
		}

		public void FillForAngle()
		{
			double r = 30;
			double g = 100;
			double b = 70;

			for (double y = pArea1.y; y < pArea2.y; y++)
			{
				for (double x = pArea1.x; x < pArea2.x; x++)
				{
					double angle = 0.0;
					Vector2 vec1;
					Vector2 vec2;
					for (int i = 0; i < points.Length - 1; i++)
					{
						vec1 = new Vector2(points[i].x - x, points[i].y - y);
						vec2 = new Vector2(points[i + 1].x - x, points[i + 1].y - y);
						angle += (Vector2.Angle(vec2, vec1) * 180) / Math.PI;
					}
					vec1 = new Vector2(points[points.Length - 1].x - x, points[points.Length - 1].y - y);
					vec2 = new Vector2(points[0].x - x, points[0].y - y);
					angle += (Vector2.Angle(vec2, vec1) * 180) / Math.PI;
					if (Math.Abs(angle - 360) <= 0.05)
					{
						canvas.SetPixel((int)x, (int)y, new byte[] { (byte)r, (byte)g, (byte)b });
					}
				}
			}
		}

		public void FillForRow()
		{
			/*Улучшение: 
			 * 1.Составить список ребёр, чтобы y1 < y2.
			 * 2.Спускаться от yMin до yMax. Если есть рёбра, у которых начало совпадает с текущим y, добавить их в CAP.
			 * 3.Рёбра в САР должны быть упорядочены по x.
			 * 4.С рёбрами из САР найти иксы по текущему y и заполнить получившиеся отрезки
			 * 5.Если какое-то ребро вышло из границ рассмотрения, то удалить его из САР
			 */
			double r = 100;
			double g = 50;
			double b = 50;

			
			var ords = new List<double>[(int)(pArea2.y - pArea1.y) + 1];
			for (int i = 0; i < ords.Length; i++)
			{
				ords[i] = new List<double>();
			}

			double x;
			double y;
			double dx;
			for (int i = 0; i < points.Length; i++)
			{
				double x1 = points[i].x;
				double y1 = points[i].y;
				double x2, y2;
				if (i != points.Length - 1)
				{
					x2 = points[i + 1].x;
					y2 = points[i + 1].y;
				}
				else
				{
					x2 = points[0].x;
					y2 = points[0].y;
				}
				if (y2 < y1)
				{
					double t = y2;
					y2 = y1;
					y1 = t;

					t = x2;
					x2 = x1;
					x1 = t;
				}
				x = x1;
				y = y1;
				dx = (x2 - x1) / (y2 - y1);
				while ((int)y != (int)y2)
				{
					ords[(int)(y - pArea1.y)].Add(x);
					++y; ; x += dx;
				}
			}

			for (int ty = 0; ty < ords.Length; ty++)
			{
				if (ords[ty].Count >= 2)
				{
					for (int i = 0; i <= ords[ty].Count - 2; i += 2)
					{
						canvas.DrawLineDDA(ords[ty][i], ty + pArea1.y, ords[ty][i + 1], ty + pArea1.y, r, g, b);
					}
				}
			}
		}
	}

	class MyCircle
	{
		private MyCanvas canvas;

		private double r = 30;
		private double g = 128;
		private double b = 255;

		private int x;
		private int y;
		private int rad;

		public MyCircle(int _x, int _y, int _rad, MyCanvas _canvas)
		{
			x = _x;
			y = _y;
			rad = _rad;
			canvas = _canvas;
			for (int i = y - rad; i < y + rad; ++i)
			{
				for (int j = x - rad; j < x + rad; ++j)
				{
					if ((i - x) * (i - x) + (j - y) * (j - y) <= rad * rad)
					{
						canvas.SetPixel(i, j, new byte[] { (byte)r, (byte)g, (byte)b });
					}
				}
			}
		}

		public void Scale(int cx, int cy, double a)
		{
			for (int i = y - rad; i < y + rad; ++i)
			{
				for (int j = x - rad; j < x + rad; ++j)
				{
					if ((i - x) * (i - x) + (j - y) * (j - y) <= rad * rad)
					{
						int tx = (int)(cx + (i - cx) * a);
						int ty = (int)(cy + (j - cy) * a);
						if (tx >= 0 && tx <= canvas.width && ty >= 0 && ty <= canvas.height)
						{
							var color = canvas.GetPixel(i, j, 3);
							canvas.SetPixel(tx, ty, color);
						}
					}
				}
			}

		}

		public void NewScale(int cx, int cy, double a)
		{
			double errorx = 0.0;
			double errory = 0.0;
			for (int i = 0; i <= canvas.width; ++i)
			{
				for (int j = 0; j <= canvas.height; ++j)
				{
					int tx = (int)((i + cx * (a - 1)) / a);
					int ty = (int)((j + cy * (a - 1)) / a);

					errorx += (((i + cx * (a - 1)) / a) - tx);
					errory += (((j + cy * (a - 1)) / a) - ty);

					int dx = 0;
					int dy = 0;
					if (errorx >= 1.0) { errorx = 0.0; dx = 1; }
					if (errory >= 1.0) { errory = 0.0; dy = 1; }
					if ((tx - x) * (tx - x) + (ty - y) * (ty - y) <= rad * rad)
					{
						var color = canvas.GetPixel(tx, ty, 3);
						canvas.SetPixel(i, j, color);
						canvas.SetPixel(i + dx, j + dy, color);
					}
				}
			}
		}

		public void Transform(int dx, int dy)
		{
			for (int i = y - rad; i < y + rad; ++i)
			{
				for (int j = x - rad; j < x + rad; ++j)
				{
					if ((i - x) * (i - x) + (j - y) * (j - y) <= rad * rad)
					{
						int tx = i + dx;
						int ty = j + dy;
						if (tx >= 0 && tx <= canvas.width && ty >= 0 && ty <= canvas.height)
						{
							var color = canvas.GetPixel(i, j, 3);
							canvas.SetPixel(tx, ty, color);
						}
					}
				}
			}
		}

		public void Rotate(int cx, int cy, double _angle)
		{
			double angle = (_angle * Math.PI) / 180;
			for (int i = y - rad; i < y + rad; ++i)
			{
				for (int j = x - rad; j < x + rad; ++j)
				{
					if ((i - x) * (i - x) + (j - y) * (j - y) <= rad * rad)
					{
						int tx = (int)((i - cx) * Math.Cos(angle) - (j - cy) * Math.Sin(angle)) + cx;
						int ty = (int)((i - cx) * Math.Sin(angle) + (j - cy) * Math.Cos(angle)) + cy;
						if (tx >= 0 && tx <= canvas.width && ty >= 0 && ty <= canvas.height)
						{
							var color = canvas.GetPixel(i, j, 3);
							canvas.SetPixel(tx, ty, color);
						}
					}
				}
			}

		}

	}

	class MyRectangle
	{
		private MyCanvas canvas;

		private double r = 30;
		private double g = 128;
		private double b = 255;

		private int x;
		private int y;
		private int w;
		private int h;

		public MyRectangle(int _x, int _y, int _w, int _h, MyCanvas _canvas)
		{
			x = _x;
			y = _y;
			w = _w;
			h = _h;
			canvas = _canvas;
			for (int i = y; i < y + h; ++i)
			{
				for (int j = x; j < x + w; ++j)
				{
					canvas.SetPixel(i, j, new byte[] { (byte)r, (byte)g, (byte)b });
				}
			}
		}

		public void Scale(int cx, int cy, double a)
		{
			for (int i = y; i < y + h; ++i)
			{
				for (int j = x; j < x + w; ++j)
				{
					int tx = (int)(cx + (i - cx) * a);
					int ty = (int)(cy + (j - cy) * a);
					if (tx >= 0 && tx <= canvas.width && ty >= 0 && ty <= canvas.height)
					{
						var color = canvas.GetPixel(i, j, 3);
						canvas.SetPixel(tx, ty, color);
					}
				}
			}
		}

		public void NewScale(int cx, int cy, double a)
		{
			double errorx = 0.0;
			double errory = 0.0;
			for (int i = 0; i <= canvas.width; ++i)
			{
				for (int j = 0; j <= canvas.height; ++j)
				{
					int tx = (int)((i + cx * (a - 1)) / a);
					int ty = (int)((j + cy * (a - 1)) / a);
					

					errorx += (((i + cx * (a - 1)) / a) - tx);
					errory += (((j + cy * (a - 1)) / a) - ty);

					int dx = 0;
					int dy = 0;
					if (errorx >= 1.0) { errorx = 0.0; dx = 1; }
					if (errory >= 1.0) { errory = 0.0; dy = 1; }

					if (tx >= x && tx <= x + w && ty >= y && ty <= y + h)
					{
						var color = canvas.GetPixel(tx, ty, 3);
						canvas.SetPixel(i, j, color);
						canvas.SetPixel(i + dx, j + dy, color);
					}
				}
			}
		}

		public void Transform(int dx, int dy)
		{
			for (int i = y; i < y + h; ++i)
			{
				for (int j = x; j < x + w; ++j)
				{
					int tx = i + dx;
					int ty = j + dy;
					if (tx >= 0 && tx <= canvas.width && ty >= 0 && ty <= canvas.height)
					{
						var color = canvas.GetPixel(i, j, 3);
						canvas.SetPixel(tx, ty, color);
					}
				}
			}
		}

		public void Rotate(int cx, int cy, double _angle)
		{
			double angle = (_angle * Math.PI) / 180;	
			for (int i = y; i < y + h; ++i)
			{
				for (int j = x; j < x + w; ++j)
				{
					int tx = (int)((i - cx) * Math.Cos(angle) - (j - cy) * Math.Sin(angle)) + cx;
					int ty = (int)((i - cx) * Math.Sin(angle) + (j - cy) * Math.Cos(angle)) + cy;
					if (tx >= 0 && tx <= canvas.width && ty >= 0 && ty <= canvas.height)
					{
						var color = canvas.GetPixel(i, j, 3);
						canvas.SetPixel(tx, ty, color);
					}
				}
			}
		}

		public void NewRatate(int cx, int cy, double _angle)
		{
			double angle = (_angle * Math.PI) / 180;
			for (int i = 0; i <= canvas.width; ++i)
			{	
				for (int j = 0; j <= canvas.height; ++j)
				{
					int tx = (int)((i - (cx - (cx) * Math.Cos(angle) + (cy) * Math.Sin(angle))) * Math.Cos(angle)
						+ (j - (cy - (cx) * Math.Sin(angle) - (cy) * Math.Cos(angle))) * Math.Sin(angle));
					int ty = (int)( -1	* (i - (cx - (cx) * Math.Cos(angle) + (cy) * Math.Sin(angle))) * Math.Sin(angle) 
						+ (j - (cy - (cx) * Math.Sin(angle) - (cy) * Math.Cos(angle))) * Math.Cos(angle));
					if (tx >= x && tx <= x + w && ty >= y && ty <= y + h)
					{
						var color = canvas.GetPixel(tx, ty, 3);
						canvas.SetPixel(i, j, color);
					}
				}
			}
		}
	}
}
