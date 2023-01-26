using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

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

public class DijkstraAlgorithm {
    private int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount) {
        int min = int.MaxValue;
        int minIndex = 0;
        for (int v = 0; v < verticesCount; ++v) {
            if (shortestPathTreeSet[v] == false && distance[v] <= min) {
                min = distance[v];
                minIndex = v;
            }
        }
        return minIndex;
    }
 
    private void Print(int[] distance, int verticesCount) {
        Debug.Log("Вершина    Расстояние от источника");
        for (int i = 0; i < verticesCount; ++i)
            Debug.Log(i+"--"+distance[i]);
    }

    public void RunAlgorithm(int[,] graph, int source, int verticesCount) {
        int[] distance = new int[verticesCount];
        bool[] shortestPathTreeSet = new bool[verticesCount];
        for (int i = 0; i < verticesCount; ++i) {
            distance[i] = int.MaxValue;
            shortestPathTreeSet[i] = false;
        }
        distance[source] = 0;
        for (int count = 0; count < verticesCount - 1; ++count) {
            int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);
            shortestPathTreeSet[u] = true;
            for (int v = 0; v < verticesCount; ++v)
                if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v]) && distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
                    distance[v] = distance[u] + graph[u, v];
        }
        Print(distance, verticesCount);
    }
}

