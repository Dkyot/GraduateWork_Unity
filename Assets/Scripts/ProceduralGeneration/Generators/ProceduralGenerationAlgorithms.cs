using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ProceduralGenerationAlgorithms
{
    #region Methods of generation
    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt space, int minWidth, int minHeight) {
        Queue<BoundsInt> roomQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomQueue.Enqueue(space);

        while (roomQueue.Count > 0) {
            BoundsInt room = roomQueue.Dequeue();

            if (room.size.y >= minHeight && room.size.x >= minWidth) {
                if (Random.value < 0.5f) {
                    if (room.size.y >= minHeight * 2) SplitHorizontally(minHeight, roomQueue, room);
                    else if (room.size.x >= minWidth * 2) SplitVertically(minWidth, roomQueue, room);
                    else if (room.size.x >= minWidth && room.size.y >= minHeight) roomsList.Add(room);
                }
                else {
                    if (room.size.x >= minWidth * 2) SplitVertically(minWidth, roomQueue, room);
                    else if (room.size.y >= minHeight * 2) SplitHorizontally(minHeight, roomQueue, room);
                    else if (room.size.x >= minWidth && room.size.y >= minHeight) roomsList.Add(room);
                }
            }
        }
        return roomsList;
    }
    #endregion

    #region Split mehtods
    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomQueue, BoundsInt room) {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(
            new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomQueue.Enqueue(room1);
        roomQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomQueue, BoundsInt room) {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(
            new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomQueue.Enqueue(room1);
        roomQueue.Enqueue(room2);
    }
    #endregion
}