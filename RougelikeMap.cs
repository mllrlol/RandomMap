using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Wall,
    Passageway,
    Void
}

public class RougelikeMap : MonoBehaviour
{
    [field: SerializeField] public GameObject WallPrefab { get; set; }
    [field: SerializeField] public GameObject PassagewayPrefab { get; set; }
    [field: SerializeField] public bool CanCut { get; set; }
    [field: SerializeField] public bool ManualSeed { get; set; }
    [field: SerializeField] public int Seed { get; set; }
    [field: SerializeField] public int Width { get; set; } = 20;
    [field: SerializeField] public int Height { get; set; } = 20;
    [field: SerializeField] public float CellSize { get; set; } = 1f;
    [field: SerializeField] public int MaxRoomCount { get; set; } = 10;
    [field: SerializeField] public int RoomMinSize { get; set; } = 1;
    [field: SerializeField] public int RoomMaxSize { get; set; } = 1;
    [field: SerializeField] public bool ShouldOverlap { get; set; }

    [field: SerializeField] private GameObject[,] MapItems { get; set; }
    [field: SerializeField] private List<Rect> rooms { get; set; }
    [field: SerializeField] private Grid<TileType> grid;

    public void NewMapWithRoomsAndPassageways()
    {
        if (ManualSeed)
        {
            Random.InitState(Seed);
        }
        else
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
        }
        DestoryMapItem();
        GenerateRooms();
        GridGenerator.CutOutWallOutline<TileType>(in grid, TileType.Wall, TileType.Passageway, TileType.Void);
        InstantiateTiles();
    }
    public void NewMapWithRoomsAndPassagewaysEditor()
    {
        Random.InitState(Seed);
        DestoryMapItemEditor();
        if (ShouldOverlap)
        {
            GenerateRooms();
        }
        else
        {
            GenerateRoomsOverlap();
        }
        if (CanCut)
        {
            GridGenerator.CutOutWallOutline<TileType>(in grid, TileType.Wall, TileType.Passageway, TileType.Void);
        }
        InstantiateTiles();
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
    public void DestoryMapItemEditor()
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

    private void GenerateRooms()
    {
        grid = new Grid<TileType>(Width, Height, CellSize, transform.position);
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
                    goto nextUpperLoop;
                }
            }

            GridGenerator.ApplyRoomToGrid<TileType>(in grid, TileType.Passageway, in newRoom);
            rooms.Add(newRoom);

            Debug.Log("Hmmmm");
            Debug.Log("Room " + i + ": " + newRoom.Center);
            if (rooms.Count > 1)
            {
                GridGenerator.ApplyTunnelsToGrid<TileType>(in grid, TileType.Passageway, rooms[rooms.Count - 1], rooms[rooms.Count - 2]);
            }

        nextUpperLoop:;
        }
    }

    private void GenerateRoomsOverlap()
    {
        grid = new Grid<TileType>(Width, Height, CellSize, transform.position);
        rooms = new List<Rect>();

        for (int i = 0; i < MaxRoomCount; i++)
        {
            int size = Random.Range(RoomMinSize, RoomMaxSize);
            int x = Random.Range(1, Width - size);
            int y = Random.Range(1, Height - size);
            Rect newRoom = new Rect(x, y, size, size);

            GridGenerator.ApplyRoomToGrid<TileType>(in grid, TileType.Passageway, in newRoom);
            rooms.Add(newRoom);

            Debug.Log("Hmmmm");
            Debug.Log("Room " + i + ": " + newRoom.Center);

            if (rooms.Count > 1)
            {
                GridGenerator.ApplyTunnelsToGrid<TileType>(in grid, TileType.Passageway, rooms[rooms.Count - 1], rooms[rooms.Count - 2]);
            }
        }
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
                        GameObject item = MapItems[x, y];
                        if (item != null)
                        {
                            MapItems[x, y] = Instantiate(PassagewayPrefab, grid.GetWorldPosition(x, y), Quaternion.identity, transform);
                        }
                        break;
                    case TileType.Wall:
                        MapItems[x, y] = Instantiate(WallPrefab, grid.GetWorldPosition(x, y), Quaternion.identity, transform);
                        break;
                }
            }
        }
    }
    // TODO: Draw gridoutlines

}