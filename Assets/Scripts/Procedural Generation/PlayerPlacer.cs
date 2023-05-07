using UnityEngine;

public class PlayerPlacer : MonoBehaviour
{
    [SerializeField] private DungeonData dungeonData;
    [SerializeField] private Transform player;

    public void PlacePlayerInStartRoom() {
        if (dungeonData != null && player != null) {
            RoomData room = dungeonData.GetStartRoom();
            if (room != null)
                player.position = (Vector3Int)room.center;
        }
    }
}
