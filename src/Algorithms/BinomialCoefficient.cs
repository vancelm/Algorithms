using System;

namespace Algorithms
{
    public static class BinomialCoefficient
    {
        public static int BinomialCoefficient_Recursive(int n, int k)
        {
            if (k > n)
            {
                return 0;
            }

            if (k == 0 || k == n)
            {
                return 1;
            }

            return BinomialCoefficient_Recursive(n - 1, k - 1) + BinomialCoefficient_Recursive(n - 1, k);
        }

        public static int BinomialCoefficient_BottomUp(int n, int k)
        {
            int[,] C = new int[n + 1, k + 1];

            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; k <= Math.Min(i, k); j++)
                {
                    if (j == 0 || j == i)
                    {
                        C[i, j] = 1;
                    }
                    else
                    {
                        C[i, j] = C[i - 1, j - 1] + C[i - 1, j];
                    }
                }
            }

            return C[n, k];
        }

        public static int BinomialCoefficient_BottomUp_Optimized(int n, int k)
        {
            int[] C = new int[k + 1];
            C[0] = 1;

            for (int i = 1; i <= n; i++)
            {
                for (int j = Math.Min(i, k); j > 0; j--)
                {
                    C[j] = C[j] + C[j - 1];
                }
            }

            return C[k];
        }
    }
}
