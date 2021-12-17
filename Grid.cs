using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grid<T>
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public float CellSize { get; private set; }
    public Vector3 OriginPosition { get; private set; }
    public Vector3[,] Positions { get; private set; }
    public T[,] GridData { get; private set; }

    public Grid(int width, int height, float cellSize, Vector3 origin)
    {
        Width = width;
        Height = height;
        CellSize = cellSize;
        OriginPosition = origin;
        Positions = new Vector3[width, height];
        GridData = new T[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Positions[x, y] = new Vector3(x, 0, y) * cellSize + origin;
            }
        }
    }

    public Grid(int width, int height, float cellSize, Vector3 origin, T data)
    {
        Width = width;
        Height = height;
        CellSize = cellSize;
        OriginPosition = origin;
        Positions = new Vector3[width, height];
        GridData = new T[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Positions[x, y] = new Vector3(x, 0, y) * cellSize + origin;
                GridData[x, y] = data;
            }
        }
    }

    public T GetData(int x, int y, T type = default(T))
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return type;
        }
        return GridData[x, y];
    }

    public T GetData(Vector3 position)
    {
        return GridData[(int)((position.x - OriginPosition.x) / CellSize), (int)((position.y - OriginPosition.y) / CellSize)];
    }

    public void SetData(int x, int y, T data)
    {
        GridData[x, y] = data;
    }

    public void SetData(Vector3 position, T data)
    {
        GridData[(int)((position.x - OriginPosition.x) / CellSize), (int)((position.y - OriginPosition.y) / CellSize)] = data;
    }

    public int GetLength(int dimension)
    {
        return GridData.GetLength(dimension);
    }

    public void Fill(T data)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                GridData[x, y] = data;
            }
        }
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return Positions[x, y];
    }
}