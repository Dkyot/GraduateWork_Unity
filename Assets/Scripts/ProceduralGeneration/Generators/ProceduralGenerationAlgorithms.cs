using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ProceduralGenerationAlgorithms
{
    #region Methods of generation
    public static List<BoundsInt> AlgorithmBSP(BoundsInt space, int minWidth, int minHeight) {
        Queue<BoundsInt> spaceQueue = new Queue<BoundsInt>();
        List<BoundsInt> spacesList = new List<BoundsInt>();
        spaceQueue.Enqueue(space);

        while (spaceQueue.Count > 0) {
            BoundsInt room = spaceQueue.Dequeue();

            if (room.size.y >= minHeight && room.size.x >= minWidth) {
                if (Random.value < 0.5f) {
                    if (room.size.y >= minHeight * 2) HorizontalSplit(spaceQueue, room);
                    else if (room.size.x >= minWidth * 2) VerticalSplit(spaceQueue, room);
                    else if (room.size.x >= minWidth && room.size.y >= minHeight) spacesList.Add(room);
                }
                else {
                    if (room.size.x >= minWidth * 2) VerticalSplit(spaceQueue, room);
                    else if (room.size.y >= minHeight * 2) HorizontalSplit(spaceQueue, room);
                    else if (room.size.x >= minWidth && room.size.y >= minHeight) spacesList.Add(room);
                }
            }
        }
        return spacesList;
    }
    #endregion

    #region Split mehtods
    private static void VerticalSplit(Queue<BoundsInt> roomQueue, BoundsInt room) {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(
            new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomQueue.Enqueue(room1);
        roomQueue.Enqueue(room2);
    }

    private static void HorizontalSplit(Queue<BoundsInt> roomQueue, BoundsInt room) {
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