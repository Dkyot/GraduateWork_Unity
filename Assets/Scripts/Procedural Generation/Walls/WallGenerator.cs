using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPos, TilemapVisualizer visualizer) {
        HashSet<Vector2Int> commonWPos = FindWallsDirections(floorPos, Direction2D.cardinalDirectionsList);
        HashSet<Vector2Int> cornerWPos = FindWallsDirections(floorPos, Direction2D.diagonalDirectionsList);

        CreateBasicWall(visualizer, commonWPos, floorPos);
        CreateCornerWalls(visualizer, cornerWPos, floorPos);
    }

    private static void CreateCornerWalls(TilemapVisualizer visualizer, HashSet<Vector2Int> cornerWPos, HashSet<Vector2Int> floorPos) {
        foreach (Vector2Int position in cornerWPos){
            string  neighboursBinaryType = "";
            foreach (Vector2Int direction in Direction2D.eightDirectionsList) {
                Vector2Int neighbourPos = position + direction;
                if (floorPos.Contains(neighbourPos)) 
                    neighboursBinaryType += "1";
                else
                    neighboursBinaryType += "0";
            }
            visualizer.PaintSingleCornerWall(position, neighboursBinaryType);
        }
    }

    private static void CreateBasicWall(TilemapVisualizer visualizer, HashSet<Vector2Int> commonWPos, HashSet<Vector2Int> floorPos) {
        foreach (Vector2Int position in commonWPos) {
            string  neighboursBinaryType = "";
            foreach (Vector2Int direction in Direction2D.cardinalDirectionsList) {
                Vector2Int neighbourPos = position + direction;
                if (floorPos.Contains(neighbourPos)) 
                    neighboursBinaryType += "1";
                else
                    neighboursBinaryType += "0";
            }
            visualizer.PaintSingleBasicWall(position, neighboursBinaryType);
        }
    }

    private static HashSet<Vector2Int> FindWallsDirections(HashSet<Vector2Int> floorPos, List<Vector2Int> directionsList) {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();
        foreach (Vector2Int position in floorPos) {
            foreach (Vector2Int direction in directionsList) {
                Vector2Int neighbourPos = position + direction;
                if (floorPos.Contains(neighbourPos) == false)
                    wallPos.Add(neighbourPos);
            }
        }
        return wallPos;
    }
}