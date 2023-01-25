using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonData
{
    public HashSet<Vector2Int> corridors;
    public List<RoomData> rooms;

    public DungeonData() {
        rooms = new List<RoomData>();
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

    public void Debuger() {
        //Debug.ClearDeveloperConsole();
        //Debug.Log(rooms.Count);
        //Debug.Log("=======");
        foreach (RoomData room in rooms) {
            if (room.edges.Count > 2) {
            Debug.Log(room.center + " " + room.edges.Count + "||| ");
            // Debug.Log("tl:"+room.tr+" br:"+room.bl);
            for (int i = 0; i < room.edges.Count; i++) {
                Debug.Log(room.edges[i].connectedRoom.center + " _" + room.edges[i].edgeWeight);
            }
            // Debug.Log("_______________");
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
}

public class RoomData 
{
    public HashSet<Vector2Int> roomFloor;
    public Vector2Int center;
    public List<GraphEdge> edges;
    public Vector2Int tr;
    public Vector2Int bl;


    public RoomData(HashSet<Vector2Int> floor, Vector2Int center, Vector2Int tr, Vector2Int bl) {
        edges = new List<GraphEdge>();
        roomFloor = floor;
        this.center = center;

        this.tr = tr;
        this.bl = bl;
    }

    public void AddEdge(RoomData room, int weight) {
        edges.Add(new GraphEdge(room, weight));
    }
}

public class GraphEdge
{
    public RoomData connectedRoom;
    public int edgeWeight;

    public GraphEdge(RoomData connectedRoom, int weight) {
        this.connectedRoom = connectedRoom;
        edgeWeight = weight;
    }
}
