using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2N8
{
    static class CombinatorialAlgorithms
    {
        static int W(char x, char y)
        {
            if (x == y) return 0;
            return 1;
        }

        public static int LevenshteinDistance(string word1, string word2)
        {
            int M = word1.Length,
                N = word2.Length;
            int[,] D = new int[M+1, N+1];
            for (int i = 1; i <= M; ++i)
                D[i, 0] = D[i - 1, 0] + 1;
            for(int j = 1; j <= N; ++j)
                D[0, j] = D[0, j-1] + 1;
            for (int i = 1; i <= M; ++i)
                for (int j = 1; j <= N; ++j)
                {
                    D[i, j] = Math.Min(Math.Min(D[i - 1, j] + 1, D[i, j - 1] + 1), D[i - 1, j - 1] + W(word1[i - 1], word2[j - 1]));
                }

            int result = D[M, N];
            return result;
        }
    }
}
