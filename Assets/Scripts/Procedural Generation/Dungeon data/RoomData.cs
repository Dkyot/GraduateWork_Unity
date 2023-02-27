using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomData 
{
    public HashSet<Vector2Int> roomFloor;
    public Vector2Int center;
    public List<GraphEdge> edges;
    public Vector2Int tr;
    public Vector2Int bl;

    public HashSet<Vector2Int> NearWallTilesUp { get; set; } = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> NearWallTilesDown { get; set; } = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> NearWallTilesLeft { get; set; } = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> NearWallTilesRight { get; set; } = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> CornerTiles { get; set; } = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> InnerTiles { get; set; } = new HashSet<Vector2Int>();

    public HashSet<Vector2Int> PropPositions { get; set; } = new HashSet<Vector2Int>();
    public List<GameObject> PropObjectReferences { get; set; } = new List<GameObject>();
    public List<Vector2Int> PositionsAccessibleFromPath { get; set; } = new List<Vector2Int>();
    public List<GameObject> EnemiesInTheRoom { get; set; } = new List<GameObject>();

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

public class DijkstraAlgorithm 
{
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

public class RoomDataExtractor 
{
    //private bool showGizmo = true;

    private DungeonData dungeonData;

    public void ProcessRooms(DungeonData data) {
        dungeonData = data;
        if (dungeonData == null)
            return;

        foreach (RoomData room in dungeonData.rooms) {
            foreach (Vector2Int tilePosition in room.roomFloor) {
                int neighboursCount = 4;

                if(room.roomFloor.Contains(tilePosition+Vector2Int.up) == false) {
                    room.NearWallTilesUp.Add(tilePosition);
                    neighboursCount--;
                }
                if (room.roomFloor.Contains(tilePosition + Vector2Int.down) == false) {
                    room.NearWallTilesDown.Add(tilePosition);
                    neighboursCount--;
                }
                if (room.roomFloor.Contains(tilePosition + Vector2Int.right) == false) {
                    room.NearWallTilesRight.Add(tilePosition);
                    neighboursCount--;
                }
                if (room.roomFloor.Contains(tilePosition + Vector2Int.left) == false) {
                    room.NearWallTilesLeft.Add(tilePosition);
                    neighboursCount--;
                }

                if (neighboursCount <= 2)
                    room.CornerTiles.Add(tilePosition);

                if (neighboursCount == 4)
                    room.InnerTiles.Add(tilePosition);
            }

            room.NearWallTilesUp.ExceptWith(room.CornerTiles);
            room.NearWallTilesDown.ExceptWith(room.CornerTiles);
            room.NearWallTilesLeft.ExceptWith(room.CornerTiles);
            room.NearWallTilesRight.ExceptWith(room.CornerTiles);          
        }
    }
}
