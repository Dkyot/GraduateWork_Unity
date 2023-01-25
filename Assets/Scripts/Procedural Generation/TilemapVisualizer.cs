using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap;
    [SerializeField]
    private Tilemap wallTilemap;
    [SerializeField]
    private TileBase floorTile, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull,
    wallInnerCornerDownLeft, wallInnerCornerDownRight,
    wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions) {
        PaintFloorTiles(floorPositions, floorTilemap, floorTile);
    }

    private void PaintFloorTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile) {
        foreach (Vector2Int position in positions) {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position) {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void PaintSingleBasicWall(Vector2Int position, string binaryType) {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallByteTypes.wallTop.Contains(typeAsInt)) {
            tile = wallTop;
        } else if (WallByteTypes.wallSideRight.Contains(typeAsInt)) {
            tile = wallSideRight;
        } else if (WallByteTypes.wallSideLeft.Contains(typeAsInt)) {
            tile = wallSideLeft;
        } else if (WallByteTypes.wallBottm.Contains(typeAsInt)) {
            tile = wallBottom;
        } else if (WallByteTypes.wallFull.Contains(typeAsInt)) {
            tile = wallFull;
        }

        if (tile != null) {
            PaintSingleTile(wallTilemap, tile, position);
        }
    }

    public void Clear() {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    public void AddFloorColider() {
        var floor = floorTilemap.gameObject;

        if (floor.GetComponent<TilemapCollider2D>() == null) 
            floor.AddComponent(typeof(TilemapCollider2D));
        if (floor.GetComponent<CompositeCollider2D>() == null)
            floor.AddComponent(typeof(CompositeCollider2D));
        
        floor.GetComponent<CompositeCollider2D>().geometryType =  CompositeCollider2D.GeometryType.Polygons;
        floor.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        floor.GetComponent<TilemapCollider2D>().usedByComposite = true;

    }

    public void PaintSingleCornerWall(Vector2Int position, string binaryType) {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if (WallByteTypes.wallInnerCornerDownLeft.Contains(typeAsInt)) {
            tile = wallInnerCornerDownLeft;
        } else if (WallByteTypes.wallInnerCornerDownRight.Contains(typeAsInt)) {
            tile = wallInnerCornerDownRight;
        } else if (WallByteTypes.wallDiagonalCornerDownLeft.Contains(typeAsInt)) {
            tile = wallDiagonalCornerDownLeft;
        } else if (WallByteTypes.wallDiagonalCornerDownRight.Contains(typeAsInt)) {
            tile = wallDiagonalCornerDownRight;
        } else if (WallByteTypes.wallDiagonalCornerUpRight.Contains(typeAsInt)) {
            tile = wallDiagonalCornerUpRight;
        } else if (WallByteTypes.wallDiagonalCornerUpLeft.Contains(typeAsInt)) {
            tile = wallDiagonalCornerUpLeft;
        } else if (WallByteTypes.wallFullEightDirections.Contains(typeAsInt)) {
            tile = wallFull;
        } else if (WallByteTypes.wallBottmEightDirections.Contains(typeAsInt)) {
            tile = wallBottom;
        }

        if (tile != null) {
            PaintSingleTile(wallTilemap, tile, position);
        }
    }
}
