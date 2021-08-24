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

        for (int row = 0; row < 3; row++)
        {
            for (int column = 0; column < 3; column++)
            {
                matrix[column, row] = n;
                n++;
            }
        }

        int width;
        int height;
        System.Numerics.Vector2 center;

        width = matrix.GetUpperBound(0) + 1;
        height = matrix.GetUpperBound(1) + 1;
        float x = (width - 1) / 2.0f;
        float y = (height - 1) / 2.0f;
        center = new System.Numerics.Vector2(x, y);

        int[,] result = new int[width, height];
        for (int row = 0; row < width; row++)
        {
            for (int col = 0; col < height; col++)
            {
                System.Numerics.Vector2 vector = new System.Numerics.Vector2(row, col);
                System.Numerics.Matrix3x2 rotationMatrix =
                    System.Numerics.Matrix3x2.CreateRotation(0.7853982f, center); //0.7853982 is 45deg in radians
                System.Numerics.Vector2 rotatedVector = System.Numerics.Vector2.Transform(vector, rotationMatrix);
                result[Mathf.RoundToInt(rotatedVector.Y), Mathf.RoundToInt(rotatedVector.X)] = matrix[col, row];
                Debug.Log("Initial vector = " + "[" + col + ", " + row + "] ");
                Debug.Log("Rotated vector = " + "[" + Mathf.RoundToInt(rotatedVector.Y) + ", " + Mathf.RoundToInt(rotatedVector.X) + "]");
            }
        }

        Assert.That(result[0, 1] == matrix[0, 0], "Top-Left Misalignment, Matrix improperly rotated."); 
        Assert.That(result[height-1, width-2] == matrix[height-1, width-1], "Bottom-Right Misalignment, Matrix improperly rotated.");
    }
}
