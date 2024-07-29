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
        
        (int, int)? result = null;
        int minDistance = int.MaxValue;

        for (int i = 0; i <= 97; i++)
        {
            for (int j = 0; j <= 97; j++)
            {
                if (Is3x3MatrixAllOnes(matrix, i, j, onesSet))
                {
                    int distance = i + j;
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        result = (i, j);
                    }
                }
            }
        }

        if (result.HasValue)
        {
            Console.WriteLine($"The nearest 3x3 matrix with all ones is located at position ({result.Value.Item1}, {result.Value.Item2}).");
        }
        else
        {
            Console.WriteLine("No 3x3 matrix with all ones was found.");
        }
    }
    
    static bool Is3x3MatrixAllOnes(int[,] matrix, int x, int y, HashSet<(int, int)> onesSet)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!onesSet.Contains((x + i, y + j)))
                {
                    return false;
                }
            }
        }
        return true;
    }
}
