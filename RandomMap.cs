using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Wall,
    Passageway,
    Void
}

public class RandomMap : MonoBehaviour
{
    [SerializeField] GameObject wallPrefab;
    [SerializeField] GameObject passagewayPrefab;
    [SerializeField] private bool manualSeed;
    [SerializeField] private int seed;
    [SerializeField] private int width = 20;
    [SerializeField] private int height = 20;
    [SerializeField] private float cellSize = 1;
    [SerializeField] private int maxRoomCount = 10;
    [SerializeField] private int roomMinSize = 3;
    [SerializeField] private int roomMaxSize = 10;
    [SerializeField] private bool canCut;

    private Grid<TileType> grid;
    private List<Rect> rooms;
    private GameObject[,] mapItems;

    public bool CanCut { get { return canCut; } set { canCut = value; } }
    public GameObject WallPrefab { get => wallPrefab; set => wallPrefab = value; }
    public GameObject PassagewayPrefab { get => passagewayPrefab; set => passagewayPrefab = value; }
    public GameObject[,] MapItems { get => mapItems; set => mapItems = value; }
    public bool ManualSeed { get { return manualSeed; } set { manualSeed = value; } }
    public int Seed { get { return seed; } set { seed = value; } }
    public int Width { get { return width; } set { width = value; } }
    public int Height { get { return height; } set { height = value; } }
    public float CellSize { get { return cellSize; } set { cellSize = value; } }
    public int MaxRoomCount { get { return maxRoomCount; } set { maxRoomCount = value; } }
    public int RoomMinSize { get { return roomMinSize; } set { roomMinSize = value; } }
    public int RoomMaxSize { get { return roomMaxSize; } set { roomMaxSize = value; } }

    private void Start()
    {
        if (manualSeed)
        {
            Random.InitState(seed);
        }
        else
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
        }
        NewMapWithRoomsAndPassageways();
    }

    public void NewMapWithRoomsAndPassageways()
    {
        DestoryMapItem();
        GenerateRooms();
        CutOutWallOutline();
        InstantiateTiles();
    }
    public void NewMapWithRoomsAndPassagewaysEditor()
    {
        if (manualSeed)
        {
            Random.InitState(seed);
        }
        else
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
        }
        DestoryMapItemEditor();
        GenerateRooms();
        if (canCut)
        {
            CutOutWallOutline();
        }
        InstantiateTiles();
    }

    private void GenerateRooms()
    {
        grid = new Grid<TileType>(Width, Height, CellSize, transform.position, TileType.Wall);
        rooms = new List<Rect>();

        for (int i = 0; i < MaxRoomCount; i++)
        {
            int size = Random.Range(RoomMinSize, RoomMaxSize);
            int x = Random.Range(1, Width - size);
            int y = Random.Range(1, Height - size);
            Rect newRoom = new Rect(x, y, size, size);

            foreach (Rect otherRoom in rooms)
            {
                if (newRoom.Intersect(otherRoom))
                {
                    continue;
                }
            }

            ApplyRoomToGrid(newRoom);
            rooms.Add(newRoom);

            if (i > 0)
            {
                ApplyTunnelsToGrid(newRoom, rooms[i - 1]);
            }
        }
    }

    private void CutOutWallOutline()
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                if (grid.GetData(x, y) == TileType.Wall)
                {
                    if (!(grid.GetData(x + 1, y) == TileType.Passageway ||
                        grid.GetData(x, y + 1) == TileType.Passageway ||
                        grid.GetData(x - 1, y) == TileType.Passageway ||
                        grid.GetData(x, y - 1) == TileType.Passageway))
                    {
                        grid.SetData(x, y, TileType.Void);
                    }
                }
            }
        }
    }

    private void ApplyTunnelsToGrid(Rect previousRoom, Rect nextRoom)
    {
        if (Random.Range(0, 2) == 1)
        {
            ApplyHorizontalTunnel(previousRoom.X, nextRoom.X, previousRoom.Y);
            ApplyVerticalTunnel(previousRoom.Y, nextRoom.Y, nextRoom.X);
        }
        else
        {
            ApplyVerticalTunnel(previousRoom.Y, nextRoom.Y, previousRoom.X);
            ApplyHorizontalTunnel(previousRoom.X, nextRoom.X, nextRoom.Y);
        }
    }

    private void DestoryMapItem()
    {
        if (MapItems != null)
        {
            foreach (GameObject go in MapItems)
            {
                if (go != null)
                {
                    Destroy(go);
                }
            }
        }
        MapItems = new GameObject[Width, Height];
    }

    private void DestoryMapItemEditor()
    {
        if (MapItems != null)
        {
            foreach (GameObject go in MapItems)
            {
                if (go != null)
                {
                    DestroyImmediate(go);
                }
            }
        }
        MapItems = new GameObject[Width, Height];
    }

    public void InstantiateTiles()
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                TileType tileType = grid.GetData(x, y);
                switch (tileType)
                {
                    case TileType.Passageway:
                        try
                        {
                            MapItems[x, y] = Instantiate(passagewayPrefab, grid.GetWorldPosition(x, y), Quaternion.identity);
                        }
                        catch { }
                        break;
                    case TileType.Wall:
                        MapItems[x, y] = Instantiate(wallPrefab, grid.GetWorldPosition(x, y), Quaternion.identity);
                        break;
                }
            }
        }
    }

    private void ApplyRoomToGrid(Rect room, TileType tileType = TileType.Passageway)
    {
        for (int x = (int)room.X; x < room.Width; x++)
        {
            for (int y = (int)room.Y; y < room.Height; y++)
            {
                grid.SetData(x, y, tileType);
            }
        }
    }

    private void ApplyHorizontalTunnel(int xStart, int xEnd, int y)
    {
        for (int x = Mathf.Min(xStart, xEnd); x <= Mathf.Max(xStart, xEnd); x++)
        {
            grid.SetData(x, y, TileType.Passageway);
        }
    }

    private void ApplyVerticalTunnel(int yStart, int yEnd, int x)
    {
        for (int y = Mathf.Min(yStart, yEnd); y <= Mathf.Max(yStart, yEnd); y++)
        {
            grid.SetData(x, y, TileType.Passageway);
        }
    }
}