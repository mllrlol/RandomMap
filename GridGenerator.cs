using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridGenerator
{
    public static void CutOutWallOutline<T>(in Grid<T> grid, T wall, T passageway, T voidTile)
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                if (grid.GetData(x, y).Equals(wall))
                {
                    if (!(grid.GetDataSecure(x + 1, y).Equals(passageway) ||
                        grid.GetDataSecure(x, y + 1).Equals(passageway) ||
                        grid.GetDataSecure(x - 1, y).Equals(passageway) ||
                        grid.GetDataSecure(x, y - 1).Equals(passageway)))
                    {
                        grid.SetData(x, y, voidTile);
                    }
                }
            }
        }
    }
    public static void ApplyRoomToGrid<T>(in Grid<T> grid, in T passageway, in Rect room)
    {
        for (int x = (int)room.StartX; x < room.EndX; x++)
        {
            for (int y = (int)room.StartY; y < room.EndY; y++)
            {
                grid.SetData(x, y, passageway);
            }
        }
    }
    public static void ApplyTunnelsToGrid<T>(in Grid<T> grid, T passageway, in Rect previousRoom, in Rect nextRoom)
    {
        if (Random.Range(0, 2) == 1)
        {
            ApplyHorizontalTunnel<T>(in grid, in passageway, previousRoom.Center.Item1, nextRoom.Center.Item1, previousRoom.Center.Item2);
            ApplyVerticalTunnel<T>(in grid, in passageway, previousRoom.Center.Item2, nextRoom.Center.Item2, nextRoom.Center.Item1);
        }
        else
        {

            ApplyHorizontalTunnel<T>(in grid, in passageway, previousRoom.Center.Item1, nextRoom.Center.Item1, previousRoom.Center.Item2);
            ApplyVerticalTunnel<T>(in grid, in passageway, previousRoom.Center.Item2, nextRoom.Center.Item2, nextRoom.Center.Item1);
        }
    }
    public static void ApplyHorizontalTunnel<T>(in Grid<T> grid, in T tile, int xStart, int xEnd, int y)
    {
        for (int x = Mathf.Min(xStart, xEnd); x <= Mathf.Max(xStart, xEnd); x++)
        {
            grid.SetData(x, y, tile);
        }
    }
    public static void ApplyVerticalTunnel<T>(in Grid<T> grid, in T tile, int yStart, int yEnd, int x)
    {
        for (int y = Mathf.Min(yStart, yEnd); y <= Mathf.Max(yStart, yEnd); y++)
        {
            grid.SetData(x, y, tile);
        }
    }
}
