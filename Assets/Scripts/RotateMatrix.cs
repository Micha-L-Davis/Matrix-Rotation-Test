using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateMatrix : MonoBehaviour
{
    // Initial data assigned in inspector
    [SerializeField]
    private int[] _initialArray = new int[16];

    // Declare variables for base matrices to evaluate
    private int[,] _baseMatrix = new int[4, 4];

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
            rotatedMatrix = RotateClockwise(_baseMatrix);
            _baseMatrix = rotatedMatrix;
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
            rotatedMatrix = RotateCounterClockwise(_baseMatrix);
            _baseMatrix = rotatedMatrix;
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

        width = _baseMatrix.GetUpperBound(0) + 1;
        height = _baseMatrix.GetUpperBound(1) + 1;

        // Iterate through 1D array
        int i = 0;

        for (int column = 0; column < width; column++)
        {
            for (int row = 0; row < height; row++)
            {
                _baseMatrix[column, row] = _initialArray[i];

                // Update matrix visualizer
                _cellText[i].text = _initialArray[i].ToString();
                i++;
            }
        }
    }
   
    private void UpdateVisualizer()
    {
        int width;
        int height;

        width = _baseMatrix.GetUpperBound(0) + 1;
        height = _baseMatrix.GetUpperBound(1) + 1;

        // Iterate through 1D array
        int i = 0;

        for (int column = 0; column < width; column++)
        {
            for (int row = 0; row < height; row++)
            {
                _cellText[i].text = _baseMatrix[column, row].ToString();
                i++;
            }
        }
    }

    // This should work for any even-sized matrix
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
                // Brute force logic here--I couldn't get the rotation matrix function to work with the even-numbered array.
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
                // Brute force logic here--I couldn't get the rotation matrix function to work properly with the even-numbered array.
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

