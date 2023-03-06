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

    public void Debuger() {
        // //Debug.ClearDeveloperConsole();
        // //Debug.Log(rooms.Count);
        // //Debug.Log("=======");
        // foreach (RoomData room in rooms) {
        //     if (room.edges.Count > 2) {
        //     Debug.Log(room.center + " " + room.edges.Count + "||| ");
        //     // Debug.Log("tl:"+room.tr+" br:"+room.bl);
        //     for (int i = 0; i < room.edges.Count; i++) {
        //         Debug.Log(room.edges[i].connectedRoom.center + " _" + room.edges[i].edgeWeight);
        //     }
        //     // Debug.Log("_______________");
        //     }
        // }

        int i = 0;
        foreach (RoomData room in rooms) {
            Debug.Log(room.center + " _" + i);
            i++;
        }

        DijkstraAlgorithm deb = new DijkstraAlgorithm();
        
        deb.RunAlgorithm(CorvertToMatrix(), 0, rooms.Count);

    }

    public bool EdgeExists(RoomData room1, RoomData room2) {
        foreach (GraphEdge edge in room1.edges) {
            if (room2.Equals(edge.connectedRoom))
                return true;
        }
        return false;
    }

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
}