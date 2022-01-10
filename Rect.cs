using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rect
{
    public int StartX { get; private set; }
    public int StartY { get; private set; }
    public int EndX { get; private set; }
    public int EndY { get; private set; }
    public (int, int) Center { get { return ((StartX + EndX) / 2, (StartY + EndY) / 2); } }

    /// <summary>
    /// Creates a new Rect with the given start and end points.
    /// </summary>
    /// <param name="startX">The x coordinate of the start point.</param>
    /// <param name="startY">The y coordinate of the start point.</param>
    /// <param name="endX">The x coordinate of the end point.</param>
    /// <param name="endY">The y coordinate of the end point.</param>
    public Rect(int x, int y, int endX, int endY)
    {
        StartX = x;
        StartY = y;
        EndX = x + endX;
        EndY = y + endY;
    }

    /// <summary>
    /// Creates a new Rect with the given start and end coordinates.
    /// </summary>
    /// <param name="startX">The x coordinate of the start point.</param>
    /// <param name="startY">The y coordinate of the start point.</param>
    public Rect(Vector2 startPoint, Vector2 size)
    {
        StartX = (int)startPoint.x;
        StartY = (int)startPoint.y;
        EndX = (int)(startPoint.x + size.x);
        EndY = (int)(startPoint.y + size.y);
    }

    /// <summary>
    /// Checks whether two rectangles intersect.
    /// </summary>
    /// <param name="other">The other rectangle to check against.</param>
    public bool Intersect(Rect other)
    {
        return !(EndX < other.StartX || StartX > other.EndX || EndY < other.StartY || StartY > other.EndY);
    }
}