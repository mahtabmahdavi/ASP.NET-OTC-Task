﻿using System;
using System.Collections.Generic;

class SparseMatrix
{
    static void Main()
    {
        int[,] matrix = GenerateRandomMatrix(100, 100);
        HashSet<(int, int)> onesSet = GetOnesPosition(matrix);

        var closestMatrix = FindClosest3x3MatrixWithAllOnes(matrix, onesSet);

        if (closestMatrix.HasValue)
        {
            Console.WriteLine($"The nearest 3x3 matrix with all ones is located at position ({closestMatrix.Value.x}, {closestMatrix.Value.y}).");
        }
        else
        {
            Console.WriteLine("No 3x3 matrix with all ones was found.");
        }
    }

    static int[,] GenerateRandomMatrix(int rows, int cols)
    {
        int[,] matrix = new int[rows, cols];
        Random rand = new Random();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = rand.Next(2);
            }
        }
        return matrix;
    }

    static HashSet<(int, int)> GetOnesPosition(int[,] matrix)
    {
        HashSet<(int, int)> onesSet = new HashSet<(int, int)>();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == 1)
                {
                    onesSet.Add((i, j));
                }
            }
        }
        return onesSet;
    }

    static (int x, int y)? FindClosest3x3MatrixWithAllOnes(int[,] matrix, HashSet<(int, int)> onesSet)
    {
        (int x, int y)? closest = null;
        int minDistance = int.MaxValue;

        for (int i = 0; i <= matrix.GetLength(0) - 3; i++)
        {
            for (int j = 0; j <= matrix.GetLength(1) - 3; j++)
            {
                if (Is3x3MatrixAllOnes(i, j, onesSet))
                {
                    int distance = i + j;
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closest = (i, j);
                    }
                }
            }
        }

        return closest;
    }

    static bool Is3x3MatrixAllOnes(int startX, int startY, HashSet<(int, int)> onesSet)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!onesSet.Contains((startX + i, startY + j)))
                {
                    return false;
                }
            }
        }
        return true;
    }
}
