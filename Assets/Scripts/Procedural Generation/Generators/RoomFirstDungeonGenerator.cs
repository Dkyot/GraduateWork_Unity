using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : AbstractDungeonGenerator
{
    
    [SerializeField]
    private int minRoomWidth = 10;
    [SerializeField]
    private int minRoomHeight = 8;
    [SerializeField]
    private int dungeonWidth = 40;
    [SerializeField]
    private int dungeonHeight = 50;
    [SerializeField]
    [Range(0, 5)]
    private int offset = 2;

    private DungeonData dungeonData;
    private bool intersectionFlag = false;

    [SerializeField]
    private UnityEvent OnEndOfGeneration;
    [SerializeField]
    private UnityEvent OnEndOfDataExtraction;

    protected override void RunProceduralGenetation() {
        dungeonData = GetComponent<DungeonData>();
        dungeonData.ResetData();

        CreateRooms();

        OnEndOfGeneration?.Invoke();
        OnEndOfDataExtraction?.Invoke();
    }

    #region Methods of building a dungeon
    private void CreateRooms() {
        List<BoundsInt> roomsList;

        if ((dungeonWidth * dungeonHeight)/6 < minRoomWidth * minRoomHeight) {
            Debug.Log("incorrect proportions");
            minRoomHeight = 8;
            minRoomWidth = 10;
            dungeonHeight = 25;
            dungeonWidth = 25;
        }
        do {
            roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(
            new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);
        } while (roomsList.Count < 6 || roomsList.Count > 12);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        floor = CreareSimpleRoom(roomsList);
        
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (BoundsInt room in roomsList) {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        dungeonData.corridors = corridors;
        floor.UnionWith(corridors);

        tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreareSimpleRoom(List<BoundsInt> roomsList) {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (BoundsInt room in roomsList) {
                HashSet<Vector2Int> roomFloor = new HashSet<Vector2Int>();
                Vector2Int roomCenter = new Vector2Int(Mathf.RoundToInt(room.center.x), Mathf.RoundToInt(room.center.y));
            for (int col = offset; col < room.size.x - offset; col++) {
                for (int row = offset; row < room.size.y - offset; row++) {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                        roomFloor.Add(position);
                }
            }

            dungeonData.AddRoom(new RoomData(roomFloor, roomCenter,
            new Vector2Int(room.xMax - offset, room.yMax - offset), 
            new Vector2Int(room.xMin + offset - 1, room.yMin + offset - 1)));
        }
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters) {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        Vector2Int currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0) {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            if (intersectionFlag == false)
                dungeonData.AddEdge(currentRoomCenter, closest, (int)Vector2.Distance(currentRoomCenter, closest));
            else
                intersectionFlag = false;
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination) {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        Vector2Int position = currentRoomCenter;
        corridor.Add(position);
            while (position.y != destination.y) {
                if (destination.y > position.y)
                    position += Vector2Int.up;
                else if (destination.y < position.y)
                    position += Vector2Int.down;
                CheckRoomEnter(position, corridor, currentRoomCenter, destination);
                corridor.Add(position);
            }
            while (position.x != destination.x) {
                if (destination.x > position.x)
                    position += Vector2Int.right;
                else if (destination.x < position.x)
                    position += Vector2Int.left;
                CheckRoomEnter(position, corridor, currentRoomCenter, destination);
                corridor.Add(position);
            }
        return corridor;
    }

    private void CheckRoomEnter(Vector2Int position, HashSet<Vector2Int> corridor, Vector2Int currentRoomCenter, Vector2Int destination) {
        foreach (RoomData room in dungeonData.rooms) {
            if (!room.Equals(dungeonData.FindRoom(currentRoomCenter)) && !room.Equals(dungeonData.FindRoom(destination))) {
                if (position.x >= room.bl.x && position.x <= room.tr.x) {
                    if (position.y >= room.bl.y && position.y <= room.tr.y) {                       
                        if (!dungeonData.EdgeExists(dungeonData.FindRoom(currentRoomCenter), room)) {
                            dungeonData.AddEdge(currentRoomCenter, room.center, (int)Vector2.Distance(currentRoomCenter, room.center));
                            intersectionFlag = true;                            
                        }
                        if (!dungeonData.EdgeExists(dungeonData.FindRoom(destination), room)) {
                            dungeonData.AddEdge(destination, room.center, (int)Vector2.Distance(destination, room.center));
                            intersectionFlag = true;                           
                        }
                        if (dungeonData.EdgeExists(dungeonData.FindRoom(currentRoomCenter), room) && dungeonData.EdgeExists(dungeonData.FindRoom(destination), room)) {
                            intersectionFlag = true;
                        }
                        
                    }
                }
            }
        }
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters) {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (Vector2Int position in roomCenters) {
            float currentDistance = Vector2.Distance(position, currentRoomCenter);
            if (currentDistance < distance) {
                distance = currentDistance;
                closest = position;
            }
        }
        return closest;
    }
    #endregion
}