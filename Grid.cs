using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grid<T>
{
    private int Width { get; set; }
    private int Height { get; set; }
    private float CellSize { get; set; }
    public Vector3 OriginPosition { get; private set; }
    private Vector3[,] Positions { get; set; }
    private T[,] Values { get; set; }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="width">Width of the grid.</param>
    /// <param name="height">Height of the grid.</param>
    /// <param name="cellSize">Size of each cell.</param>
    /// <param name="originPosition">Position of the origin of the grid.</param>
    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        Width = width;
        Height = height;
        CellSize = cellSize;
        OriginPosition = originPosition;
        Positions = new Vector3[width, height];
        Values = new T[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Positions[x, y] = new Vector3(x, 0, y) * cellSize + originPosition;
            }
        }
    }

    /// <summary>
    /// Constructor, fills the grid with a default value.
    /// </summary>
    /// <param name="width">Width of the grid.</param>
    /// <param name="height">Height of the grid.</param>
    /// <param name="cellSize">Size of each cell.</param>
    /// <param name="originPosition">Position of the origin of the grid.</param>
    /// <param name="defaultValue">Default value of each cell.</param>
    public Grid(int width, int height, float cellSize, Vector3 originPosition, T defaultValue)
    {
        Width = width;
        Height = height;
        CellSize = cellSize;
        OriginPosition = originPosition;
        Positions = new Vector3[width, height];
        Values = new T[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Positions[x, y] = new Vector3(x, 0, y) * cellSize + originPosition;
                Values[x, y] = defaultValue;
            }
        }
    }

    /// <summary>
    /// Gets the value of a cell, by index.
    /// </summary>
    /// <param name="x">X coordinate of the cell.</param>
    /// <param name="y">Y coordinate of the cell.</param>
    /// <returns>Value of the cell.</returns>
    public T GetData(int x, int y)
    {
        return Values[x, y];
    }

    /// <summary>
    /// Gets the value of a cell, by world position.
    /// </summary>
    /// <param name="x">X coordinate of the cell.</param>
    /// <param name="y">Y coordinate of the cell.</param>
    /// <returns>Value of the cell.</returns>
    public T GetData(Vector3 position)
    {
        return Values[(int)((position.x - OriginPosition.x) / CellSize), (int)((position.z - OriginPosition.z) / CellSize)];
    }

    /// <summary>
    /// Gets the value of a cell. If the cell is out of bounds, returns the closest value cell.
    /// </summary>
    /// <param name="x">X coordinate of the cell.</param>
    /// <param name="y">Y coordinate of the cell.</param>
    /// <returns>Value of the cell.</returns>
    public T GetDataSecure(int x, int y)
    {
        if (x < 0)
        {
            x = 0;
        }
        else if (x >= Width)
        {
            x = Width - 1;
        }
        if (y < 0)
        {
            y = 0;
        }
        else if (y >= Height)
        {
            y = Height - 1;
        }

        return Values[x, y];
    }

    /// <summary>
    /// Gets the value of a cell. If the cell is out of bounds, returns the closest value cell.
    /// </summary>
    /// <param name="position">World postion of the cell.</param>
    /// <returns>Value of the cell.</returns>
    public T GetDataSecure(Vector3 position)
    {
        int x = (int)((position.x - OriginPosition.x) / CellSize),
            y = (int)((position.z - OriginPosition.z) / CellSize);

        if (x < 0)
        {
            x = 0;
        }
        else if (x >= Width)
        {
            x = Width - 1;
        }
        if (y < 0)
        {
            y = 0;
        }
        else if (y >= Height)
        {
            y = Height - 1;
        }

        return Values[x, y];
    }

    /// <summary>
    /// Gets the value of a cell. If the cell is out of bounds, returns the value given.
    /// </summary>
    /// <param name="x">X coordinate of the cell.</param>
    /// <param name="y">Y coordinate of the cell.</param>
    /// <param name="data">Default value of the cell.</param>
    /// <returns>Value of the cell.</returns>
    public T GetDataSecure(T data, int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return data;
        }
        return Values[x, y];
    }

    /// <summary>
    /// Gets the value of a cell. If the cell is out of bounds, returns the value given.
    /// </summary>
    /// <param name="position">World postion of the cell.</param>
    /// <param name="data">Default value of the cell.</param>
    /// <returns>Value of the cell.</returns>
    public T GetDataSecure(T data, Vector3 position)
    {
        int x = (int)((position.x - OriginPosition.x) / CellSize),
            y = (int)((position.z - OriginPosition.z) / CellSize);

        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return data;
        }
        return Values[x, y];
    }

    /// <summary>
    /// Sets the value of a cell, by index.
    /// </summary>
    /// <param name="x">X coordinate of the cell.</param>
    /// <param name="y">Y coordinate of the cell.</param>
    /// <param name="data">Value to set the cell.</param>
    public void SetData(int x, int y, T data)
    {
        Values[x, y] = data;
    }

    /// <summary>
    /// Sets the value of a cell, by position.
    /// </summary>
    /// <param name="position">World postion of the cell.</param>
    /// <param name="data">Value to set the cell.</param>
    public void SetData(Vector3 position, T data)
    {
        Values[(int)((position.x - OriginPosition.x) / CellSize), (int)((position.z - OriginPosition.z) / CellSize)] = data;
    }

    /// <summary>
    /// Sets the value of a cell, by index. If the cell is out of bounds, set the value of the closet cell to it.
    /// </summary>
    /// <param name="data">Value of the cell.</param>
    /// <param name="x">X coordinate of the cell.</param>
    /// <param name="y">Y coordinate of the cell.</param>
    public void SetDataSecure(int x, int y, T data)
    {
        if (x < 0)
        {
            x = 0;
        }
        else if (x >= Width)
        {
            x = Width - 1;
        }
        if (y < 0)
        {
            y = 0;
        }
        else if (y >= Height)
        {
            y = Height - 1;
        }

        Values[x, y] = data;
    }

    /// <summary>
    /// Sets the value of a cell, by position. If the cell is out of bounds, set the value of the closet cell to it.
    /// </summary>
    /// <param name="data">Value of the cell.</param>
    /// <param name="position">World postion of the cell.</param>
    public void SetDataSecure(Vector3 position, T data)
    {
        int x = (int)((position.x - OriginPosition.x) / CellSize),
            y = (int)((position.z - OriginPosition.z) / CellSize);

        if (x < 0)
        {
            x = 0;
        }
        else if (x >= Width)
        {
            x = Width - 1;
        }
        if (y < 0)
        {
            y = 0;
        }
        else if (y >= Height)
        {
            y = Height - 1;
        }

        Values[x, y] = data;
    }

    /// <summary>
    /// Sets the value of a cell, by index. If the cell is out of bounds, does nothing.
    /// </summary>
    /// <param name="x">X coordinate of the cell.</param>
    /// <param name="y">Y coordinate of the cell.</param>
    /// <param name="data">Value of the cell.</param>
    public void SetDataSecure(T data, int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return;
        }
        Values[x, y] = data;
    }

    /// <summary>
    /// Sets the value of a cell, by posistion. If the cell is out of bounds, does nothing.
    /// </summary>
    /// <param name="position">World postion of the cell.</param>
    /// <param name="data">Value of the cell.</param>
    public void SetDataSecure(T data, Vector3 position)
    {
        int x = (int)((position.x - OriginPosition.x) / CellSize),
            y = (int)((position.z - OriginPosition.z) / CellSize);

        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return;
        }
        Values[x, y] = data;
    }

    /// <summary>
    /// Fills the grid with the given data
    /// </summary>
    /// <param name="newData">The data to fill the grid with</param>
    /// <param name="startX">The x coordinate of the start of the fill</param>
    /// <param name="startY">The y coordinate of the start of the fill</param>
    /// <param name="endX">The x coordinate of the end of the fill</param>
    /// <param name="endY">The y coordinate of the end of the fill</param>
    public void Fill(T newData)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Values[x, y] = newData;
            }
        }
    }

    /// <summary>
    ///  Fills the girid with the given data, but only in the given area, by using the given start and end indexes
    /// </summary>
    /// <param name="newData">The data to fill the grid with</param>
    /// <param name="startX">The x coordinate of the start of the fill</param>
    /// <param name="startY">The y coordinate of the start of the fill</param>
    /// <param name="endX">The x coordinate of the end of the fill</param>
    /// <param name="endY">The y coordinate of the end of the fill</param>
    public void FillArea(T newData, int startX, int startY, int endX, int endY)
    {
        for (int i = startX; i < startX + endX; i++)
        {
            for (int j = startY; j < startY + endY; j++)
            {
                Values[i, j] = newData;
            }
        }
    }

    /// <summary>
    ///  Fills the girid with the given data, but only in the given area, by using the given start and end postion.
    /// </summary>
    /// <param name="newData">The data to fill the grid with</param>
    /// <param name="start">The start of the fill</param>
    /// <param name="end">The end of the fill</param>
    public void FillArea(T newData, Vector3 start, Vector3 end)
    {
        int startX = (int)((start.x - OriginPosition.x) / CellSize),
            startY = (int)((start.z - OriginPosition.z) / CellSize),
            endX = (int)((end.x - OriginPosition.x) / CellSize),
            endY = (int)((end.z - OriginPosition.z) / CellSize);

        for (int i = startX; i < endX; i++)
        {
            for (int j = startY; j < endY; j++)
            {
                Values[i, j] = newData;
            }
        }
    }

    /// <summary>
    ///  Fills the girid with the given data, but only in the given area, by using the given start and end indexes. If the start or end indexes are outside the grid, it won't fill anything.
    /// </summary>
    /// <param name="newData">The data to fill the grid with.</param>
    /// <param name="start">The start of the fill.</param>
    /// <param name="end">The end of the fill.</param>
    public void FillAreaSecure(T newData, int startX, int startY, int endX, int endY)
    {
        if (startX < 0 || startX >= Width || startY < 0 || startY >= Height || endX < 0 || endX >= Width || endY < 0 || endY >= Height)
        {
            return;
        }

        for (int i = startX; i < startX + endX; i++)
        {
            for (int j = startY; j < startY + endY; j++)
            {
                Values[i, j] = newData;
            }
        }
    }

    /// <summary>
    ///  Fills the girid with the given data, but only in the given area, by using the given start and end postion. If the start or end postion are outside the grid, it won't fill anything.
    /// </summary>
    /// <param name="newData">The data to fill the grid with.</param>
    /// <param name="start">The start of the fill.</param>
    /// <param name="end">The end of the fill.</param>
    public void FillAreaSecure(T newData, Vector3 start, Vector3 end)
    {
        int startX = (int)((start.x - OriginPosition.x) / CellSize),
            startY = (int)((start.z - OriginPosition.z) / CellSize),
            endX = (int)((end.x - OriginPosition.x) / CellSize),
            endY = (int)((end.z - OriginPosition.z) / CellSize);

        if (startX < 0 || startX >= Width || startY < 0 || startY >= Height || endX < 0 || endX >= Width || endY < 0 || endY >= Height)
        {
            return;
        }

        for (int i = startX; i < endX; i++)
        {
            for (int j = startY; j < endY; j++)
            {
                Values[i, j] = newData;
            }
        }
    }

    /// <summary>
    /// Repleaces the grid with the given data.
    /// </summary>
    /// <param name="newData">The data to fill the grid with.</param>
    /// <param name="oldData">The data to replace.</param>
    public void Replace(T newData, T oldData)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (Values[x, y].Equals(oldData))
                {
                    Values[x, y] = newData;
                }
            }
        }
    }

    /// <summary>
    /// Repleaces the area with the given data, but only in the given area, by using the start and end indexes.
    /// </summary>
    /// <param name="newData">The data to fill the grid with.</param>
    /// <param name="oldData">The data to replace.</param>
    /// <param name="startX">The x coordinate of the start of the fill.</param>
    /// <param name="startY">The y coordinate of the start of the fill.</param>
    /// <param name="endX">The x coordinate of the end of the fill.</param>
    /// <param name="endY">The y coordinate of the end of the fill.</param>
    public void ReplaceArea(T newData, T oldData, int startX, int startY, int endX, int endY)
    {
        for (int i = startX; i < startX + endX; i++)
        {
            for (int j = startY; j < startY + endY; j++)
            {
                if (Values[i, j].Equals(oldData))
                {
                    Values[i, j] = newData;
                }
            }
        }
    }

    /// <summary>
    /// Repleaces the area with the given data, but only in the given area, by using the start and end positons.
    /// </summary>
    /// <param name="newData">The data to fill the grid with.</param>
    /// <param name="oldData">The data to replace.</param>
    /// <param name="start">The start of the fill.</param>
    /// <param name="end">The end of the fill.</param>
    public void ReplaceArea(T newData, T oldData, Vector3 start, Vector3 end)
    {
        int startX = (int)((start.x - OriginPosition.x) / CellSize),
            startY = (int)((start.z - OriginPosition.z) / CellSize),
            endX = (int)((end.x - OriginPosition.x) / CellSize),
            endY = (int)((end.z - OriginPosition.z) / CellSize);

        for (int i = startX; i < endX; i++)
        {
            for (int j = startY; j < endY; j++)
            {
                if (Values[i, j].Equals(oldData))
                {
                    Values[i, j] = newData;
                }
            }
        }
    }

    /// <summary>
    /// Repleaces the area with the given data, but only in the given area, by using the start and end indexes. If the start or end indexes are outside the grid, it won't repleace anything.
    /// </summary>
    /// <param name="newData">The data to fill the grid with.</param>
    /// <param name="dataToreplace">The data to replace.</param>
    /// <param name="startX">The x coordinate of the start of the fill.</param>
    /// <param name="startY">The y coordinate of the start of the fill.</param>
    /// <param name="endX">The x coordinate of the end of the fill.</param>
    /// <param name="endY">The y coordinate of the end of the fill.</param>
    public void ReplaceAreaSecure(T newData, T dataToreplace, int startX, int startY, int endX, int endY)
    {
        if (startX < 0 || startX >= Width || startY < 0 || startY >= Height)
        {
            return;
        }

        for (int i = startX; i < startX + endX; i++)
        {
            for (int j = startY; j < startY + endY; j++)
            {
                if (Values[i, j].Equals(dataToreplace))
                {
                    Values[i, j] = newData;
                }
            }
        }
    }

    /// <summary>
    /// Repleaces the area with the given data, but only in the given area, by using the start and end positons. If the start or end position is outside the grid, it won't repleace anything.
    /// </summary>
    /// <param name="newData">The data to fill the grid with.</param>
    /// <param name="dataToreplace">The data to replace.</param>
    /// <param name="start">The start of the fill.</param>
    /// <param name="end">The end of the fill.</param>
    public void ReplaceAreaSecure(T newData, T dataToreplace, Vector3 start, Vector3 end)
    {
        int startX = (int)((start.x - OriginPosition.x) / CellSize),
            startY = (int)((start.z - OriginPosition.z) / CellSize),
            endX = (int)((end.x - OriginPosition.x) / CellSize),
            endY = (int)((end.z - OriginPosition.z) / CellSize);

        if (startX < 0 || startX >= Width || startY < 0 || startY >= Height)
        {
            return;
        }

        for (int i = startX; i < endX; i++)
        {
            for (int j = startY; j < endY; j++)
            {
                if (Values[i, j].Equals(dataToreplace))
                {
                    Values[i, j] = newData;
                }
            }
        }
    }

    /// <summary>
    /// Returns the world position of the data at the given index
    /// </summary>
    /// <param name="x"> X index of the datas position in the world.</param>
    /// <param name="y"> Y indey of the dates position in the world.</param>
    /// <returns>The data at the given position.</returns>
    public Vector3 GetWorldPosition(int x, int y)
    {
        return Positions[x, y];
    }

    /// <summary>
    /// Returns the lenght of the grid in the given dimension axis.
    /// </summary>
    /// <param name="dimension">Dimension of the grid</param>
    /// <returns>The lenght of the grid at the given dimension.</returns>
    public int GetLength(int dimension)
    {
        return Values.GetLength(dimension);
    }
}