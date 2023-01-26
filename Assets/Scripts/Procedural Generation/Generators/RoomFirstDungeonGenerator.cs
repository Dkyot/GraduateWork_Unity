using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomWalkMapGenerator
{
    
    [SerializeField]
    private int minRoomWidth = 4;
    [SerializeField]
    private int minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth = 20;
    [SerializeField]
    private int dungeonHeight = 20;
    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;
    [SerializeField]
    private bool randomWalkRooms = false;

    private DungeonData dungeonData;

    protected override void RunProceduralGenetation()
    {
        dungeonData = new DungeonData();
        CreateRooms();
        dungeonData.Debuger();
    }

    private void CreateRooms() {
        List<BoundsInt> roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int
        (dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        
        if (randomWalkRooms) 
            floor = CreateRoomsRandomly(roomsList);
        else 
            floor = CreareSimpleRoom(roomsList);
        
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (BoundsInt room in roomsList) {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for (int i = 0; i < roomsList.Count; i++) {
            BoundsInt roomBounds = roomsList[i];
            Vector2Int roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            HashSet<Vector2Int> roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);
            
            Vector2Int tr = new Vector2Int(int.MinValue, int.MinValue);
            Vector2Int bl = new Vector2Int(int.MaxValue, int.MaxValue);
            
            foreach (Vector2Int position in roomFloor) {
                if(position.x >= (roomBounds.xMin + offset) 
                && position.x <= (roomBounds.xMax - offset)
                && position.y >= (roomBounds.yMin - offset) 
                && position.y <= (roomBounds.yMax - offset)) {
                    floor.Add(position);
                    if (position.x > tr.x)                    
                        tr.x = position.x;
                    if (position.y > tr.y)                    
                        tr.y = position.y;
                    if (position.x < bl.x)                    
                        bl.x = position.x;
                    if (position.y < bl.y)                    
                        bl.y = position.y;
                }
            }
            tr.x++;tr.y++;
            bl.x--;bl.y--; 

            dungeonData.AddRoom(new RoomData(roomFloor, roomCenter, tr, bl));
        }
        return floor;
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

    private bool flag = false;
    
    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters) {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        Vector2Int currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0) {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            if (flag == false)
                dungeonData.AddEdge(currentRoomCenter, closest, (int)Vector2.Distance(currentRoomCenter, closest));
            else
                flag = false;
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
                            //Debug.Log("+");
                            flag = true;                            
                        }
                        if (!dungeonData.EdgeExists(dungeonData.FindRoom(destination), room)) {
                            dungeonData.AddEdge(destination, room.center, (int)Vector2.Distance(destination, room.center));
                            //Debug.Log("-");
                            flag = true;                           
                        }
                        if (dungeonData.EdgeExists(dungeonData.FindRoom(currentRoomCenter), room) && dungeonData.EdgeExists(dungeonData.FindRoom(destination), room)) {
                            flag = true;
                            //Debug.Log("*");
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
}
