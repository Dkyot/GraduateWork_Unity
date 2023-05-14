using UnityEngine;

public class PlayerPlacer : MonoBehaviour
{
    [SerializeField] private DungeonData dungeonData;
    [SerializeField] private Transform player;

    public void PlacePlayerInStartRoom() {
        if (dungeonData != null && player != null) {
            RoomData room = dungeonData.GetStartRoom();
            if (room != null) {
                Vector3 spawnPos = (Vector3Int)room.center;
                player.position = new Vector3(spawnPos.x * 2.5f, spawnPos.y * 2.5f, player.position.z);
            }
        }
    }
}
