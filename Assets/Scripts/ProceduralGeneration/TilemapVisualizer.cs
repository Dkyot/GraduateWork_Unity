using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private TileBase fTile, wTop, wSideRight, wSideLeft, wBottom, wFull,
                                      wInnerCornerDownLeft, wInnerCornerDownRight,
                                      wDiagonalCornerDownRight, wDiagonalCornerDownLeft, wDiagonalCornerUpRight, wDiagonalCornerUpLeft;

    public void Clear() {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
    
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPos) {
        foreach (Vector2Int position in floorPos)
            PaintSingleTile(floorTilemap, fTile, position);
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position) {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void PaintSingleBasicWall(Vector2Int position, string binaryType) {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if (WallByteTypes.wallTop.Contains(typeAsInt))            tile = wTop;
        else if (WallByteTypes.wallSideRight.Contains(typeAsInt)) tile = wSideRight;
        else if (WallByteTypes.wallSideLeft.Contains(typeAsInt))  tile = wSideLeft;
        else if (WallByteTypes.wallBottm.Contains(typeAsInt))     tile = wBottom;
        else if (WallByteTypes.wallFull.Contains(typeAsInt))      tile = wFull;

        if (tile != null) PaintSingleTile(wallTilemap, tile, position);
    }

    public void AddWallColider() {
        var walls = wallTilemap.gameObject;

        if (walls.GetComponent<TilemapCollider2D>() == null) 
            walls.AddComponent(typeof(TilemapCollider2D));
        if (walls.GetComponent<CompositeCollider2D>() == null)
            walls.AddComponent(typeof(CompositeCollider2D));
        
        walls.GetComponent<CompositeCollider2D>().geometryType =  CompositeCollider2D.GeometryType.Polygons;
        walls.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        walls.GetComponent<TilemapCollider2D>().usedByComposite = true;

    }

    public void PaintSingleCornerWall(Vector2Int position, string binaryType) {
        int type = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if (WallByteTypes.wallInnerCornerDownLeft.Contains(type))          tile = wInnerCornerDownLeft;
        else if (WallByteTypes.wallInnerCornerDownRight.Contains(type))    tile = wInnerCornerDownRight;
        else if (WallByteTypes.wallDiagonalCornerDownLeft.Contains(type))  tile = wDiagonalCornerDownLeft;
        else if (WallByteTypes.wallDiagonalCornerDownRight.Contains(type)) tile = wDiagonalCornerDownRight;
        else if (WallByteTypes.wallDiagonalCornerUpRight.Contains(type))   tile = wDiagonalCornerUpRight;
        else if (WallByteTypes.wallDiagonalCornerUpLeft.Contains(type))    tile = wDiagonalCornerUpLeft;
        else if (WallByteTypes.wallFullEightDirections.Contains(type))     tile = wFull;
        else if (WallByteTypes.wallBottmEightDirections.Contains(type))    tile = wBottom;

        if (tile != null) PaintSingleTile(wallTilemap, tile, position);
    }
}
