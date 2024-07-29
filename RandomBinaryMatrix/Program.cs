using System;
using System.Collections.Generic;

class SparseMatrix
{
    static void Main()
    {
        int[,] matrix = new int[100, 100];
        Random rand = new Random();
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                matrix[i, j] = rand.Next(2);
            }
        }

        HashSet<(int, int)> onesSet = new HashSet<(int, int)>();
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                if (matrix[i, j] == 1)
                {
                    onesSet.Add((i, j));
                }
            }
        }
    }
}