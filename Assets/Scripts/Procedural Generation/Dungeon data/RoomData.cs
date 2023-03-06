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

    public TypesOfRooms roomType = TypesOfRooms.EmptyRoom;

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