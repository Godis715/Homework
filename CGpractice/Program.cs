using System;
using System.Collections.Generic;

namespace CGpractice
{
    class Program
    {
        static void Main(string[] args)
        {
			// size of canvas is 255 x 255
			MyCanvas canvas = new MyCanvas(256, 265);
			//MyCanvas canvas = new MyCanvas("gaus.png");
			var segments = new List<MyCanvas.Segment>();
			//segments.Add(new MyCanvas.Segment { point1 = new Vector2(70, 200), point2 = new Vector2(120, 50) });
			//segments.Add(new MyCanvas.Segment { point1 = new Vector2(120, 50), point2 = new Vector2(170, 200) });
			//segments.Add(new MyCanvas.Segment { point1 = new Vector2(170, 200), point2 = new Vector2(70, 200) });
			//canvas.DrawFractalCurveKoha(segments, 1);
			segments.Add(new MyCanvas.Segment { point1 = new Vector2(50, 100), point2 = new Vector2(100, 140) });
			canvas.DrawFractalDragon_HarterHathaway(segments, 10);

			#region Коррекция изображения
			//Коррекция изображения
			//canvas.AutoContrast();
			//canvas.GammaCorrection(2);
			//canvas.LogCorrection(10);
			//canvas.ExpCorrection(1);
			//canvas.Balance_referenceСolor(123, 49, 255, 255, 255);
			//canvas.Balance_GrayWorld();
			//canvas.Balance_PerfectReflector();
			//canvas.DiffusePseudomonotone();
			#endregion

			double[][] H;
			double K;
			#region Свёртка
			/* 
			H = new double[3][];
			H[0] = new double[] { 0, 1, 0 };
			H[1] = new double[] { 1, 2, 1 };
			H[2] = new double[] { 0, 1, 0 };
			K = 6;
			canvas.BilateralConvolution(H, K);
			H = new double[3][];
			H[0] = new double[] { -1, -1, -1 };
			H[1] = new double[] { -1, 9, -1 };
			H[2] = new double[] { -1, -1, -1 };
			K = 1;
			canvas.BilateralConvolution(H, K);
			H = new double[3][];
			H[0] = new double[] { 0, 1, 0 };
			H[1] = new double[] { -1, 0, 1 };
			H[2] = new double[] { 0, -1, 0 };
			K = 1;
			canvas.BilateralConvolution(H, K);
			*/
			#endregion

			#region Выделение границ
			/* 
			H = new double[3][];
			H[0] = new double[] { -1, -1, -1 };
			H[1] = new double[] { 0, 0, 0 };
			H[2] = new double[] { 1, 1, 1 };
			K = 1;
			canvas.BilateralConvolution(H, K);
			MyCanvas tempCanvas = new MyCanvas("borderTest2.png");
			tempCanvas.AutoLevels();
			H = new double[3][];
			H[0] = new double[] { -1, 0, 1 };
			H[1] = new double[] { -1, 0, 1 };
			H[2] = new double[] { -1, 0, 1 };
			K = 1;
			tempCanvas.BilateralConvolution(H, K);
			canvas.Merger(tempCanvas);
			*/
			#endregion

			#region Устранение шумов
			//int size = 7;
			//H = new double[size][];
			//for (int i = 0; i < size; i++)
			//{
			//	H[i] = new double[size];
			//	for (int j = 0; j < size; j++)
			//	{
			//		H[i][j] = 1;
			//	}
			//}
			//K = size * size;
			//canvas.BilateralConvolution(H, K);

			//double d = 4;
			//H = canvas.CreateGaussianH(7, d);
			//K = 1;
			//canvas.BilateralConvolution(H, K);

			//canvas.MediumFilter(1);
			#endregion

			//canvas.PrintHistogram();
			#region Отсечение видимой частью
			/* 
			canvas.DrawLineDDA(111, 179, 172, 138);
			canvas.DrawLineDDA(17, 30, 160, 40);
			canvas.DrawLineDDA(50, 190, 230, 190);
			canvas.DrawLineDDA(80, 150, 150, 80);
			canvas.DrawLineDDA(190, 10, 230, 110);
			canvas.DrawLineDDA(50, 10, 50, 110);
			canvas.DrawLineDDA(50, 170, 170, 190);

			canvas.CreateArea_CSalg(100, 100, 200, 200);
			canvas.CreateArea__LBalg(100, 100, 200, 200);
			var areaPoints = new Vector2[] { new Vector2(30, 70), new Vector2(150, 100),
				new Vector2(160, 150), new Vector2(130, 190), new Vector2(70, 140)};
			canvas.CreateArea__CBalg(areaPoints);
			*/
			#endregion

			#region Рисование
			//for (int i = 0; i < 256; ++i)
			//{
			//	for (int j = 0; j < 256; ++j)
			//	{
			//		double dist = Math.Sqrt((127 - i) * (127 - i) + (127 - j) * (127 - j));
			//		double r = (dist * Math.Sqrt(i * j)) % 255;
			//		double g = 255 - r;
			//		double b = 0;
			//		canvas.SetPixel(i, j, new byte[] { (byte)r, (byte)g, (byte)b });
			//	}
			//}
			#endregion

			#region Геометрические преобразования
			/*
			int d = 100;
			int c = 100;
			var cir = new MyCircle(c, c, 25, canvas);
			cir.Scale(c, c, 2f);
			cir.NewScale(c, c, 2f);
			cir.Transform(15, 15);
			cir.Rotate(c - 25, c - 25, 120);
			var rec = new MyRectangle(c, c, 50, 50, canvas);
			rec.Scale(c + 25, c + 25, 2);
			rec.NewScale(c + 25, c + 25, 3);
			rec.Transform(50, 50);
			rec.Rotate(0, 0, 30);
			rec.NewRatate(c, c, 120);
			*/
			#endregion

			#region Отрезки
			/*
			double x1 = 11.33;
			double y1 = 109.5;
			double x2 = 104.44;
			double y2 = 137.6;

			double step = 10;
			canvas.DrawLineParam(x1, y1, x2, y2);
			canvas.DrawLineDDA(x1, y1 + step, x2, y2  + step);
			canvas.DrawLineBrez(x1, y1 + 2 * step, x2, y2 + 2 * step);

			Console.Write("Input x2: ");
			x2 = Double.Parse(Console.ReadLine());
			Console.Write("Input y2: ");
			y2 = Double.Parse(Console.ReadLine());
			canvas.DrawLineParam(x1, y1, x2, y2);
			canvas.DrawLineDDA(x1, y1 + step, x2, y2 + step);
			canvas.DrawLineBrez(x1, y1 + 2 * step, x2, y2 + 2 * step);
			*/
			#endregion

			#region Окружности
			/* 
			canvas.DrawCircleParam(50, 100, 30);
			canvas.DrawCircleNeyavnoe(100, 100, 30);
			canvas.DrawCircleBrez(150, 100, 30);
			*/
			#endregion

			#region N угольник
			/*
			Vector2[] points = new Vector2[6];
			points[0] = new Vector2(50, 100);
			points[1] = new Vector2(50, 50);
			points[2] = new Vector2(120, 90);
			points[3] = new Vector2(180, 60);
			points[4] = new Vector2(200, 150);
			points[5] = new Vector2(100, 150);
			Nangle nangle = new Nangle(points, canvas);
			//nangle.FillForAngle();
			//nangle.FillForRow();
			*/
			#endregion

			#region Затравка
			/* 
			int xClilk = 190;
			int yClick = 21;
			canvas.FillOnClickRec(xClilk, yClick, 100, 150, 100);
			canvas.FillOnClickIt(xClilk, yClick, 100, 150, 100);
			*/
			#endregion

			#region Устранение ступенчатости по Брезенхему
			/* 
			double st = 50;
			canvas.DrawLineBrez(50, 100 + st, 180, 100 + st);
			canvas.DrawLineBrez(50, 100 + st, 200, 120 + st);
			canvas.DrawLineBrez(180, 100 + st, 200, 120 + st);
			canvas.FillOnClickIt(167, (int)(106 + st), 100, 200, 20);

			canvas.DrawLineBrezMod(50, 100, 180, 100);
			canvas.DrawLineBrezMod(50, 100, 200, 120);
			canvas.DrawLineBrezMod(180, 100, 200, 120);
			canvas.FillOnClickIt(167, 106, 100, 200, 20);
			*/
			#endregion

			canvas.Save("myImage.bmp");
		}
    }
}
