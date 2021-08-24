using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RotateMatrixTests
{
    [Test]
    public void RotateClockwiseTest()
    {
        int n = 1;
        int[,] matrix = new int[3, 3];
        int width = 3;
        int height = 3;


        for (int row = 0; row < 3; row++)
        {
            for (int column = 0; column < 3; column++)
            {
                matrix[column, row] = n;
                n++;
            }
        }

        RotateMatrix rotateMatrix = GameObject.FindObjectOfType<RotateMatrix>();
        if (rotateMatrix == null)
        {
            Debug.LogError("Cannot find rotate matrix component.");
        }

        int [,] result = rotateMatrix.RotateClockwise(matrix);

        Assert.That(result[0, 1] == matrix[0, 0], 
            "Top-Left Misalignment, Matrix improperly rotated."); 
        Assert.That(result[height-1, width-2] == matrix[height-1, width-1], 
            "Bottom-Right Misalignment, Matrix improperly rotated.");
    }

    [Test]
    public void RotateCounterClockwiseTest()
    {
        int n = 1;
        int[,] matrix = new int[3, 3];
        int width = 3;
        int height = 3;


        for (int row = 0; row < 3; row++)
        {
            for (int column = 0; column < 3; column++)
            {
                matrix[column, row] = n;
                n++;
            }
        }

        RotateMatrix rotateMatrix = GameObject.FindObjectOfType<RotateMatrix>();
        if (rotateMatrix == null)
        {
            Debug.LogError("Cannot find rotate matrix component.");
        }

        int[,] result = rotateMatrix.RotateCounterClockwise(matrix);

        Assert.That(result[1, 0] == matrix[0, 0], "Top-Left Misalignment, Matrix improperly rotated.");
        Assert.That(result[height - 2, width - 1] == matrix[height - 1, width - 1], "Bottom-Right Misalignment, Matrix improperly rotated.");
    }
}
