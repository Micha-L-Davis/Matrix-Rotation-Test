using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateMatrix : MonoBehaviour
{
    // Initial data assigned in inspector
    [SerializeField]
    private int[] _initialArray9 = new int[9];

    // Declare variables for base matrices to evaluate
    private int[,] _baseMatrix3x3 = new int[3, 3];

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
            int[,] rotatedMatrix = new int[3, 3];
            rotatedMatrix = RotateClockwise(_baseMatrix3x3);
            _baseMatrix3x3 = rotatedMatrix;
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
            int[,] rotatedMatrix = new int[3, 3];
            rotatedMatrix = RotateCounterClockwise(_baseMatrix3x3);
            _baseMatrix3x3 = rotatedMatrix;
            UpdateVisualizer();
            r--;
        }
    }

    private void Start()
    {
        LoadBaseMatrix();
        _inputField.text = "" + 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void LoadBaseMatrix()
    {
        int width;
        int height;

        width = _baseMatrix3x3.GetUpperBound(0) + 1;
        height = _baseMatrix3x3.GetUpperBound(1) + 1;

        // Iterate through 1D array
        int i = 0;

        for (int column = 0; column < width; column++)
        {
            for (int row = 0; row < height; row++)
            {
                _baseMatrix3x3[column, row] = _initialArray9[i];
                _cellText[i].text = _initialArray9[i].ToString();
                i++;
            }
        }
    }
   
    private void UpdateVisualizer()
    {
        int width;
        int height;

        width = _baseMatrix3x3.GetUpperBound(0) + 1;
        height = _baseMatrix3x3.GetUpperBound(1) + 1;

        // Iterate through 1D array
        int i = 0;

        for (int column = 0; column < width; column++)
        {
            for (int row = 0; row < height; row++)
            {
                _cellText[i].text = _baseMatrix3x3[column, row].ToString();
                i++;
            }
        }
    }

    private int[,] RotateClockwise(int[,] matrix)
    {
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
            }
        }
        return result;
    }

    private int[,] RotateCounterClockwise(int[,] matrix)
    {
        int width;
        int height;
        System.Numerics.Vector2 center;

        width = matrix.GetUpperBound(0) + 1;
        height = matrix.GetUpperBound(1) + 1;
        float x = (width - 1) / 2.0f;
        float y = (height - 1) / 2.0f;
        center = new System.Numerics.Vector2(x, y);

        int[,] result = new int[height, width];
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                System.Numerics.Vector2 vector = new System.Numerics.Vector2(row, col);
                System.Numerics.Matrix3x2 rotationMatrix = 
                    System.Numerics.Matrix3x2.CreateRotation(-0.7853982f, center); //-0.7853982 is -45deg in radians
                System.Numerics.Vector2 rotatedVector = System.Numerics.Vector2.Transform(vector, rotationMatrix);
                result[Mathf.RoundToInt(rotatedVector.Y), Mathf.RoundToInt(rotatedVector.X)] = matrix[col, row];
            }
        }
        return result;
    }
}

