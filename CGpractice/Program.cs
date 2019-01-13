using System;


namespace CGpractice
{
    class Program
    {
        static void Main(string[] args)
        {
            // size of canvas is 255 x 255
            MyCanvas canvas = new MyCanvas(255, 255);
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

			//int x1 = 75;
			//int y1 = 75;
			//int x2 = 125;
			//int y2 = 125;

			int d = 100;
			int c = 100;
			//var cir = new MyCircle(c, c, 25, canvas);
			//cir.Scale(c, c, 2f);
			//cir.NewScale(c, c, 2f);
			//cir.Transform(50, 50);
			//cir.Rotate(c - 25, c - 25, 120);
			//var rec = new MyRectangle(c, c, 50, 50, canvas);
			//rec.Scale(c + 25, c + 25, 2);
			//rec.NewScale(c + 25, c + 25, 3);
			//rec.Transform(50, 50);
			//rec.Rotate(0, 0, 30);
			//rec.NewRatate(c, c, 120);
			double x1 = 11.33;
			double y1 = 109.5;

			double x2 = 104.44;
			double y2 = 137.6;

			double step = 10;
			//canvas.DrawLineParam(x1, y1, x2, y2);
			//canvas.DrawLineDDA(x1, y1 + step, x2, y2  + step);
			//canvas.DrawLineBrez(x1, y1 + 2 * step, x2, y2 + 2 * step);

			//canvas.DrawCircleParam(50, 100, 30);
			//canvas.DrawCircleNeyavnoe(100, 100, 30);
			//canvas.DrawCircleBrez(150, 100, 30);

			//Console.Write("Input x2: ");
			//x2 = Double.Parse(Console.ReadLine());
			//Console.Write("Input y2: ");
			//y2 = Double.Parse(Console.ReadLine());
			//canvas.DrawLineParam(x1, y1, x2, y2);
			//canvas.DrawLineDDA(x1, y1 + step, x2, y2 + step);
			//canvas.DrawLineBrez(x1, y1 + 2 * step, x2, y2 + 2 * step);

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

			int xClilk = 190;
			int yClick = 21;
			//canvas.FillOnClickRec(xClilk, yClick, 100, 150, 100);
			canvas.FillOnClickIt(xClilk, yClick, 100, 150, 100);
			//Vector2[] points1 = new Vector2[4];
			//points1[0] = new Vector2(50, 200);
			//points1[1] = new Vector2(100, 200);
			//points1[2] = new Vector2(100, 250);
			//points1[3] = new Vector2(50, 250);
			//Nangle nangle1 = new Nangle(points1, canvas);
			//nangle1.FillForAngle();
			canvas.Save("myImage.bmp");
		}
    }
}
