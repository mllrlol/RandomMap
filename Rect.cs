using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rect
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public (int, int) Center { get { return (X + Width / 2, Y + Height / 2); } }

    public Rect(Vector2 startPoint, Vector2 size)
    {
        X = (int)startPoint.x;
        Y = (int)startPoint.y;
        Width = (int)(startPoint.x + size.x);
        Height = (int)(startPoint.y + size.y);
    }

    public Rect(int x, int y, int roomWidth, int roomHeight)
    {
        X = x;
        Y = y;
        Width = x + roomWidth;
        Height = y + roomHeight;
    }

    public bool Intersect(Rect other)
    {
        return !(other.X > Width || other.Width < X || other.Y > Height || other.Height < Y);
    }

    public Vector2 CenterPos()
    {
        return new Vector2((X + Width) / 2, (Y + Height) / 2);
    }
}