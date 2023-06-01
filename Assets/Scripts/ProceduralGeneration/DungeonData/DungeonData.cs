using System.Collections.Generic;
using UnityEngine;

public class DungeonData : MonoBehaviour
{
    public HashSet<Vector2Int> corridors;
    public List<RoomData> rooms;

    public DungeonData() {
        rooms = new List<RoomData>();
        corridors = new HashSet<Vector2Int>();
    }
    
    public void AddRoomTypes() {
        if(rooms.Count >= 6) {
            List<int> roomIndexes = new List<int>();
            for (int i = 0; i < rooms.Count; i++)
                roomIndexes.Add(i);

            //Debug.Log(roomIndexes.Count);

            rooms[0].roomType = TypesOfRooms.StartRoom;
            roomIndexes.Remove(0);

            DijkstraAlgorithm dijkstra = new DijkstraAlgorithm();
            int exit = dijkstra.RunAlgorithm(CorvertToMatrix(), 0, rooms.Count);
            rooms[exit].roomType = TypesOfRooms.ExitRoom;
            roomIndexes.Remove(exit);

            int randomRoom = Random.Range(0, roomIndexes.Count);
            rooms[roomIndexes[randomRoom]].roomType = TypesOfRooms.TreasureRoom;
            roomIndexes.RemoveAt(randomRoom);

            randomRoom = Random.Range(0, roomIndexes.Count);
            rooms[roomIndexes[randomRoom]].roomType = TypesOfRooms.BossRoom;
            roomIndexes.RemoveAt(randomRoom);

            rooms[roomIndexes[0]].roomType = TypesOfRooms.EnemyRoom;
            roomIndexes.RemoveAt(0);

            rooms[roomIndexes[0]].roomType = TypesOfRooms.EnemyRoom;
            roomIndexes.RemoveAt(0);

            if (roomIndexes.Count > 0)
                rooms[roomIndexes[0]].roomType = TypesOfRooms.BossRoom;

        }
    }

    public RoomData GetStartRoom() {
        foreach (RoomData room in rooms) {
            if (room.roomType == TypesOfRooms.StartRoom) 
                return room;
        }
        return null;
    }

    #region Graph methods
    public void ResetData() {
        foreach(RoomData room in rooms) {
            foreach(GameObject prop in room.PropObjectReferences) {
                DestroyImmediate(prop, false);
            }
        }

        rooms = new List<RoomData>();
        corridors = new HashSet<Vector2Int>();
    }

    public void AddRoom(RoomData room) {
        rooms.Add(room);
    }

    public RoomData FindRoom(Vector2Int center) {
        foreach (RoomData pos in rooms) {
           if (pos.center.Equals(center))
            return pos;
        }
        return null;
    }

    public void AddEdge(Vector2Int room1, Vector2Int room2, int weight) {
        RoomData first = FindRoom(room1);
        RoomData second = FindRoom(room2);
                
        if (first != null && second != null) {
            if (!EdgeExists(first, second)) {
                first.AddEdge(second, weight);
                second.AddEdge(first, weight);
            }
        }
    }

    public bool EdgeExists(RoomData room1, RoomData room2) {
        foreach (GraphEdge edge in room1.edges) {
            if (room2.Equals(edge.connectedRoom))
                return true;
        }
        return false;
    }
    #endregion

    #region Auxiliary methods
    private int FindEdgeWeight(RoomData room1, RoomData room2) {
        foreach (GraphEdge edge in room1.edges) {
            if (room2.Equals(edge.connectedRoom))
                return edge.edgeWeight;
        }
        return 0;
    }

    private int[,] CorvertToMatrix() {
        int[,] graph = new int[rooms.Count, rooms.Count];
        for (int i = 0; i < rooms.Count; i++) {
            for (int j = 0; j < rooms.Count; j++) {
                if (EdgeExists(rooms[i], rooms[j]))
                    graph[i, j] = FindEdgeWeight(rooms[i], rooms[j]);
            }
        }
        return graph;
    }
    
    public void Debuger() {
        // ClearConsolLog();

        // int i = 0;
        // foreach (RoomData room in rooms) {
        //     Debug.Log(room.center + " _" + i);
        //     i++;
        // }

        // DijkstraAlgorithm dijkstra = new DijkstraAlgorithm();
        // dijkstra.RunAlgorithm(CorvertToMatrix(), 0, rooms.Count);
    }

    private void ClearConsolLog() {
        System.Type log = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
        System.Reflection.MethodInfo clearMethod = log.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        clearMethod.Invoke(null, null);
    }
    #endregion
}

public enum TypesOfRooms {
    EmptyRoom,
    StartRoom,
    ExitRoom,
    TreasureRoom,
    EnemyRoom,
    BossRoom
}