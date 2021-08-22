using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateMatrix : MonoBehaviour
{
    // Initial data assigned in inspector
    [SerializeField]
    private int[] _initialArray16 = new int[16];

    // Declare variables for base matrices to evaluate
    private int[,] _baseMatrix4x4 = new int[4, 4];

    // Serialized lists for the matrix visualizer text
    [SerializeField]
    private List<Text> _cellText;

    // Serialized reference to rotations input field
    [SerializeField]
    private InputField _inputField;
    private int _rotations = 1;

    public void SetRotations() // Called when _inputField.text is updated
    {
        _rotations = Int32.Parse(_inputField.text);
    }

    public void ClockwiseButton()
    {
        // Iterate rotations
        int r = _rotations;

        while (r > 0)
        {
            int[,] rotatedMatrix = new int[4, 4];
            rotatedMatrix = RotateClockwise(_baseMatrix4x4);
            _baseMatrix4x4 = rotatedMatrix;
            UpdateVisualizer();
            r--;
        }
    }

    public void CounterClockwiseButton()
    {
        // Iterate rotations
        int r = _rotations;

        while (r > 0)
        {
            int[,] rotatedMatrix = new int[4, 4];
            rotatedMatrix = RotateCounterClockwise(_baseMatrix4x4);
            _baseMatrix4x4 = rotatedMatrix;
            UpdateVisualizer();
            r--;
        }
    }

    private void Start()
    {
        LoadBaseMatrix();
        _inputField.text = "" + 1;
    }

    private void LoadBaseMatrix()
    {
        int width;
        int height;

        width = _baseMatrix4x4.GetUpperBound(0) + 1;
        height = _baseMatrix4x4.GetUpperBound(1) + 1;

        // Iterate through 1D array
        int i = 0;

        for (int column = 0; column < width; column++)
        {
            for (int row = 0; row < height; row++)
            {
                _baseMatrix4x4[column, row] = _initialArray16[i];
                _cellText[i].text = _initialArray16[i].ToString();
                i++;
            }
        }
    }
   
    private void UpdateVisualizer()
    {
        int width;
        int height;

        width = _baseMatrix4x4.GetUpperBound(0) + 1;
        height = _baseMatrix4x4.GetUpperBound(1) + 1;

        // Iterate through 1D array
        int i = 0;

        for (int column = 0; column < width; column++)
        {
            for (int row = 0; row < height; row++)
            {
                _cellText[i].text = _baseMatrix4x4[column, row].ToString();
                i++;
            }
        }
    }

    
    //private int[,] RotateClockwise(int[,] matrix)
    //{
    //    int width;
    //    int height;
    //    System.Numerics.Vector2 center;

    //    width = matrix.GetUpperBound(0) + 1;
    //    height = matrix.GetUpperBound(1) + 1;
    //    float x = (width - 1) / 2.0f;
    //    float y = (height - 1) / 2.0f;
    //    Debug.Log(x + ", " + y);
    //    center = new System.Numerics.Vector2(x, y);

    //    int[,] result = new int[height, width];
    //    for (int row = 0; row < height; row++)
    //    {
    //        for (int col = 0; col < width; col++)
    //        {
    //            System.Numerics.Vector2 vector = new System.Numerics.Vector2(row, col);
    //            System.Numerics.Matrix3x2 rotationMatrix = 
    //                System.Numerics.Matrix3x2.CreateRotation(0.7853982f, center); //0.7853982 is 45deg in radians
    //            System.Numerics.Vector2 rotatedVector = System.Numerics.Vector2.Transform(vector, rotationMatrix);
    //            Debug.Log("Original vector = " + vector.Y + ", " + vector.X + 
    //                ", Rotated vector = " + rotatedVector.Y + ", " + rotatedVector.X);
    //            // Expected [0,0] to become [1, 0] but here we're getting [-1, 2]
    //            result[Mathf.RoundToInt(rotatedVector.Y), Mathf.RoundToInt(rotatedVector.X)] = matrix[col, row];
    //        }
    //    }
    //    return result;
    //}

    private int[,] RotateClockwise(int[,] matrix)
    {
        int width;
        int middle;

        width = matrix.GetUpperBound(0) + 1;

        middle = width / 2;
        int[,] result = new int[width, width];

        for (int row = 0; row < width; ++row)
        {
            int corner = width - row - 1;
            bool firsthalf = row < middle;
            for (int column = 0; column < width; ++column)
            {
                int value = matrix[row, column];
                Debug.Log("Moving " + value);
                Debug.Log("Checking " + column + ", " + row);
                if (firsthalf)
                {
                    if (column < row)
                    {
                        result[row - 1, column] = value;
                    }
                    else if (column == row)
                    {
                        result[row, column + 1] = value;
                    }
                    else if (column < corner)
                    {
                        result[row, column + 1] = value;
                    }
                    else
                    {
                        result[row + 1, column] = value;
                    }
                }
                else
                {
                    if (column <= corner)
                    {
                        result[row - 1, column] = value;
                    }
                    //else if (column < row)
                    //{
                    //    result[row - 1, column] = value;
                    //}
                    else if (column > row)
                    {
                        result[row + 1, column] = value;
                    }
                    else
                    {
                        result[row, column -1] = value;
                    }
                }
            }
        }

        return result;
    }


    //private int[,] RotateCounterClockwise(int[,] matrix)
    //{
    //    int width;
    //    int height;
    //    System.Numerics.Vector2 center;

    //    width = matrix.GetUpperBound(0) + 1;
    //    height = matrix.GetUpperBound(1) + 1;
    //    float x = (width - 1) / 2.0f;
    //    float y = (height - 1) / 2.0f;
    //    center = new System.Numerics.Vector2(x, y);

    //    int[,] result = new int[height, width];
    //    for (int row = 0; row < height; row++)
    //    {
    //        for (int col = 0; col < width; col++)
    //        {
    //            System.Numerics.Vector2 vector = new System.Numerics.Vector2(row, col);
    //            System.Numerics.Matrix3x2 rotationMatrix = 
    //                System.Numerics.Matrix3x2.CreateRotation(-0.7853982f, center); // -0.7853982 is -45deg in radians
    //            System.Numerics.Vector2 rotatedVector = System.Numerics.Vector2.Transform(vector, rotationMatrix);
    //            Debug.Log("Original vector = " + vector.Y + ", " + vector.X +
    //                ", Rotated vector = " + rotatedVector.Y + ", " + rotatedVector.X);
    //            // Expected [0,0] -> [0, 1] but here we're getting [2, -1]
    //            result[Mathf.RoundToInt(rotatedVector.Y), Mathf.RoundToInt(rotatedVector.X)] = matrix[col, row];
    //        }
    //    }
    //    return result;
    //}

    private int[,] RotateCounterClockwise(int[,] matrix)
    {
        int width;
        int middle;

        width = matrix.GetUpperBound(0) + 1;

        middle = width / 2;
        int[,] result = new int[width, width];

        for (int row = 0; row < width; ++row)
        {
            int corner = width - row - 1;
            bool firsthalf = row < middle;
            for (int column = 0; column < width; ++column)
            {
                int value = matrix[row, column];

                if (firsthalf)
                {

                    if (column < row)
                    {
                        result[row + 1, column] = value;
                    }
                    else if (column == row)
                    {
                        result[row + 1, column] = value;
                    }
                    else if (column <= corner)
                    {
                        result[row, column - 1] = value;
                    }
                    else
                    {
                        result[row - 1, column] = value;
                    }

                }
                else
                {

                    if (column < corner)
                    {
                        result[row + 1, column] = value;
                    }
                    else if (column < row)
                    {
                        result[row, column + 1] = value;
                    }
                    else
                    {
                        result[row - 1, column] = value;
                    }

                }
            }
        }

        return result;
    }

}

