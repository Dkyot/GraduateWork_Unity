using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DungeonRoomsDataExtractor : MonoBehaviour
{
    public DungeonData dungeonData;

    [SerializeField]
    private bool showGizmo = false;

    public void ProcessRooms() {
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

    private void OnDrawGizmosSelected() {
        if (dungeonData == null || showGizmo == false)
            return;
        foreach (RoomData room in dungeonData.rooms) {
            //Draw inner tiles
            Gizmos.color = Color.yellow;
            foreach (Vector2Int floorPosition in room.InnerTiles) {
                if (dungeonData.corridors.Contains(floorPosition)) {
                    continue;
                }
                Gizmos.DrawCube(floorPosition + Vector2.one * 0.5f, Vector2.one);
            }
            //Draw near wall tiles UP
            Gizmos.color = Color.blue;
            foreach (Vector2Int floorPosition in room.NearWallTilesUp) {
                if (dungeonData.corridors.Contains(floorPosition))
                    continue;
                Gizmos.DrawCube(floorPosition + Vector2.one * 0.5f, Vector2.one);
            }
            //Draw near wall tiles DOWN
            Gizmos.color = Color.green;
            foreach (Vector2Int floorPosition in room.NearWallTilesDown) {
                if (dungeonData.corridors.Contains(floorPosition))
                    continue;
                Gizmos.DrawCube(floorPosition + Vector2.one * 0.5f, Vector2.one);
            }
            //Draw near wall tiles RIGHT
            Gizmos.color = Color.white;
            foreach (Vector2Int floorPosition in room.NearWallTilesRight) {
                if (dungeonData.corridors.Contains(floorPosition))
                    continue;
                Gizmos.DrawCube(floorPosition + Vector2.one * 0.5f, Vector2.one);
            }
            //Draw near wall tiles LEFT
            Gizmos.color = Color.cyan;
            foreach (Vector2Int floorPosition in room.NearWallTilesLeft) {
                if (dungeonData.corridors.Contains(floorPosition))
                    continue;
                Gizmos.DrawCube(floorPosition + Vector2.one * 0.5f, Vector2.one);
            }
            //Draw near wall tiles CORNERS
            Gizmos.color = Color.magenta;
            foreach (Vector2Int floorPosition in room.CornerTiles) {
                if (dungeonData.corridors.Contains(floorPosition))
                    continue;
                Gizmos.DrawCube(floorPosition + Vector2.one * 0.5f, Vector2.one);
            }
        }
    }
}