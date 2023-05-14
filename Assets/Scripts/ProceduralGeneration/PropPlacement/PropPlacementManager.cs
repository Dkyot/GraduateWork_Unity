using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PropPlacementManager : MonoBehaviour
{
    [SerializeField] private DungeonData dungeonData;

    [SerializeField] private RoomTypeSO EmptyRoom;
    [SerializeField] private RoomTypeSO StartRoom;
    [SerializeField] private RoomTypeSO ExitRoom;
    [SerializeField] private RoomTypeSO TreasureRoom;
    [SerializeField] private RoomTypeSO EnemyRoom;
    [SerializeField] private RoomTypeSO BossRoom;

    private RoomTypeSO propsToPlace;

    [SerializeField] private Transform parentObject;

    [SerializeField, Range(0, 1)] private float cornerPropPlacementChance = 0.7f;

    [SerializeField] private UnityEvent OnEndOfPropPlacement;

    public void ProcessRooms() {
        if (dungeonData == null)
            return;
        foreach (RoomData room in dungeonData.rooms) {
            switch (room.roomType) {
                case TypesOfRooms.EmptyRoom: propsToPlace = EmptyRoom; break;
                case TypesOfRooms.StartRoom: propsToPlace = StartRoom; break;
                case TypesOfRooms.ExitRoom: propsToPlace = ExitRoom; break;
                case TypesOfRooms.TreasureRoom: propsToPlace = TreasureRoom; break;
                case TypesOfRooms.EnemyRoom: propsToPlace = EnemyRoom; break;
                case TypesOfRooms.BossRoom: propsToPlace = BossRoom; break;
                default: continue;
            }
            if (propsToPlace.roomProps.Count == 0)
                continue;

            List<PropSO> cornerProps = propsToPlace.roomProps.Where(x => x.Corner).ToList();
            if (cornerProps.Count != 0) PlaceCornerProps(room, cornerProps);

            List<PropSO> leftWallProps = propsToPlace.roomProps.Where(x => x.NearWallLeft).ToList();
            PlaceProps(room, leftWallProps, room.NearWallTilesLeft, PlacementOriginCorner.BottomLeft);

            List<PropSO> rightWallProps = propsToPlace.roomProps.Where(x => x.NearWallRight).ToList();
            PlaceProps(room, rightWallProps, room.NearWallTilesRight, PlacementOriginCorner.TopRight);

            List<PropSO> topWallProps = propsToPlace.roomProps.Where(x => x.NearWallUP).ToList();
            PlaceProps(room, topWallProps, room.NearWallTilesUp, PlacementOriginCorner.TopLeft);

            List<PropSO> downWallProps = propsToPlace.roomProps.Where(x => x.NearWallDown).ToList();
            PlaceProps(room, downWallProps, room.NearWallTilesDown, PlacementOriginCorner.BottomLeft);

            List<PropSO> innerProps = propsToPlace.roomProps.Where(x => x.Inner).ToList();
            PlaceProps(room, innerProps, room.InnerTiles, PlacementOriginCorner.BottomLeft);
        }
        OnEndOfPropPlacement?.Invoke();
    }

    #region Methods of placing props
    private void PlaceProps(RoomData room, List<PropSO> wallProps, HashSet<Vector2Int> availableTiles, PlacementOriginCorner placement) {
        HashSet<Vector2Int> tempPositons = new HashSet<Vector2Int>(availableTiles);
        tempPositons.ExceptWith(dungeonData.corridors);

        foreach (PropSO propToPlace in wallProps) {
            int quantity  = UnityEngine.Random.Range(propToPlace.PlacementQuantityMin, propToPlace.PlacementQuantityMax +1);

            for (int i = 0; i < quantity; i++) {
                tempPositons.ExceptWith(room.PropPositions);
                List<Vector2Int> availablePositions = tempPositons.OrderBy(x => Guid.NewGuid()).ToList();
                if (TryPlacingPropBruteForce(room, propToPlace, availablePositions, placement) == false)
                    break;
            }

        }
    }

    private bool TryPlacingPropBruteForce(RoomData room, PropSO propToPlace, List<Vector2Int> availablePositions, PlacementOriginCorner placement) {
        for (int i = 0; i < availablePositions.Count; i++) {
            Vector2Int position = availablePositions[i];

            if (room.PropPositions.Contains(position))
                continue;

            List<Vector2Int> freePositionsAround  = TryToFitProp(propToPlace, availablePositions, position, placement);

            if (freePositionsAround.Count == 1) {
                PlacePropGameObjectAt(room, position, propToPlace);
                foreach (Vector2Int pos in freePositionsAround) {
                    room.PropPositions.Add(pos);
                }
                return true;
            }
        }
        return false;
    }

    private List<Vector2Int> TryToFitProp(PropSO prop, List<Vector2Int> availablePositions, Vector2Int originPosition, PlacementOriginCorner placement) {
        List<Vector2Int> freePositions = new List<Vector2Int>();

        if (placement == PlacementOriginCorner.BottomLeft) {
            Vector2Int tempPos = originPosition;
            if (availablePositions.Contains(tempPos))
                freePositions.Add(tempPos);
        }
        else if (placement == PlacementOriginCorner.BottomRight) {
            Vector2Int tempPos = originPosition;
            if (availablePositions.Contains(tempPos))
                freePositions.Add(tempPos);
        }
        else if (placement == PlacementOriginCorner.TopLeft) {
            Vector2Int tempPos = originPosition;
            if (availablePositions.Contains(tempPos))
                freePositions.Add(tempPos);
        }
        else {
            Vector2Int tempPos = originPosition;
            if (availablePositions.Contains(tempPos))
                freePositions.Add(tempPos);
        }
        return freePositions;
    }

    private void PlaceCornerProps(RoomData room, List<PropSO> cornerProps) {
        float tempChance = cornerPropPlacementChance;

        foreach (Vector2Int cornerTile in room.CornerTiles) {
            if (UnityEngine.Random.value < tempChance) {
                PropSO propToPlace = cornerProps[UnityEngine.Random.Range(0, cornerProps.Count)];
                PlacePropGameObjectAt(room, cornerTile, propToPlace);
            }
            else {
                tempChance = Mathf.Clamp01(tempChance + 0.1f);
            }
        }
    }

    private GameObject PlacePropGameObjectAt(RoomData room, Vector2Int placementPostion, PropSO propToPlace) {
        GameObject prop = Instantiate(propToPlace.propPrefab);
        prop.transform.localPosition = new Vector3((placementPostion.x + 0.5f) * 2.5f, (placementPostion.y + 0.5f) * 2.5f, -1f);
        prop.transform.SetParent(parentObject);
        prop.transform.localPosition = new Vector3(prop.transform.localPosition.x, prop.transform.localPosition.y, -1f);
        
        room.PropPositions.Add(placementPostion);
        room.PropObjectReferences.Add(prop);
        return prop;
    }
    #endregion
}

public enum PlacementOriginCorner {
    BottomLeft,
    BottomRight,
    TopLeft,
    TopRight
}