using System;
using System.Collections.Generic;
using System.Text;

namespace TravisTestProj_Factorial
{
    public static class Mathematics
    {
        public static int Factorial(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("Factorial of negative number is not defined");
            }

            int answer = 1;

            for (int i = 1; i <= n; ++i)
            {
                answer *= i;
            }

            return answer
        }
    }
}
