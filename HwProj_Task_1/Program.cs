using System;

namespace MyProgram
{
	class Program {

		static long Fac(int num) {

			if(num < 0)
			{
				throw new Exception("Number was negative.");
			}

			long result = 1;

			for (int i = 2; i <= num; ++i) {
				result *= i;
			}

			if (result < 0) {
				throw new Exception("Too big result.");
			}

			return result;
		}

		static void Main(string[] args) {
			try {
				Console.Write("Enter the number of Fibonacci numbers \n");
				int num = Int32.Parse(Console.ReadLine());
				Console.Write(num.ToString() + "! = " + Fac(num).ToString() + "\n");
			}
			catch (Exception e) {
				Console.Write(e.Message);
			}
			Console.ReadKey();
		}
	}
}