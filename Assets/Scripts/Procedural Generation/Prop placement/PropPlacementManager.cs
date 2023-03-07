using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PropPlacementManager : MonoBehaviour
{
    [SerializeField]
    private DungeonData dungeonData;

    [SerializeField]
    private RoomTypeSO EmptyRoom;
    [SerializeField]
    private RoomTypeSO StartRoom;
    [SerializeField]
    private RoomTypeSO ExitRoom;
    [SerializeField]
    private RoomTypeSO TreasureRoom;
    [SerializeField]
    private RoomTypeSO EnemyRoom;
    [SerializeField]
    private RoomTypeSO BossRoom;

    private RoomTypeSO propsToPlace;

    [SerializeField, Range(0, 1)]
    private float cornerPropPlacementChance = 0.7f;

    [SerializeField]
    private GameObject propPrefab;

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

            //Place props place props in the corners
            List<PropSO> cornerProps = propsToPlace.roomProps.Where(x => x.Corner).ToList();
            PlaceCornerProps(room, cornerProps);

            //Place props near LEFT wall
            List<PropSO> leftWallProps = propsToPlace.roomProps
            .Where(x => x.NearWallLeft)
            .OrderByDescending(x => x.PropSize.x * x.PropSize.y)
            .ToList();

            PlaceProps(room, leftWallProps, room.NearWallTilesLeft, PlacementOriginCorner.BottomLeft);

            //Place props near RIGHT wall
            List<PropSO> rightWallProps = propsToPlace.roomProps
            .Where(x => x.NearWallRight)
            .OrderByDescending(x => x.PropSize.x * x.PropSize.y)
            .ToList();

            PlaceProps(room, rightWallProps, room.NearWallTilesRight, PlacementOriginCorner.TopRight);

            //Place props near UP wall
            List<PropSO> topWallProps = propsToPlace.roomProps
            .Where(x => x.NearWallUP)
            .OrderByDescending(x => x.PropSize.x * x.PropSize.y)
            .ToList();

            PlaceProps(room, topWallProps, room.NearWallTilesUp, PlacementOriginCorner.TopLeft);

            //Place props near DOWN wall
            List<PropSO> downWallProps = propsToPlace.roomProps
            .Where(x => x.NearWallDown)
            .OrderByDescending(x => x.PropSize.x * x.PropSize.y)
            .ToList();

            PlaceProps(room, downWallProps, room.NearWallTilesDown, PlacementOriginCorner.BottomLeft);

            //Place inner props
            List<PropSO> innerProps = propsToPlace.roomProps
                .Where(x => x.Inner)
                .OrderByDescending(x => x.PropSize.x * x.PropSize.y)
                .ToList();
            PlaceProps(room, innerProps, room.InnerTiles, PlacementOriginCorner.BottomLeft);
        }
    }

    private void PlaceProps(RoomData room, List<PropSO> wallProps, HashSet<Vector2Int> availableTiles, PlacementOriginCorner placement) {
        //Remove path positions from the initial nearWallTiles to ensure the clear path to traverse dungeon
        HashSet<Vector2Int> tempPositons = new HashSet<Vector2Int>(availableTiles);
        tempPositons.ExceptWith(dungeonData.corridors);

        //We will try to place all the props
        foreach (PropSO propToPlace in wallProps) {
            //We want to place only certain quantity of each prop
            int quantity  = UnityEngine.Random.Range(propToPlace.PlacementQuantityMin, propToPlace.PlacementQuantityMax +1);

            for (int i = 0; i < quantity; i++) {
                //remove taken positions
                tempPositons.ExceptWith(room.PropPositions);
                //shuffel the positions
                List<Vector2Int> availablePositions = tempPositons.OrderBy(x => Guid.NewGuid()).ToList();
                //If placement has failed there is no point in trying to place the same prop again
                if (TryPlacingPropBruteForce(room, propToPlace, availablePositions, placement) == false)
                    break;
            }

        }
    }

    private bool TryPlacingPropBruteForce(RoomData room, PropSO propToPlace, List<Vector2Int> availablePositions, PlacementOriginCorner placement) {
        //try placing the objects starting from the corner specified by the placement parameter
        for (int i = 0; i < availablePositions.Count; i++) {
            //select the specified position (but it can be already taken after placing the corner props as a group)
            Vector2Int position = availablePositions[i];
            if (room.PropPositions.Contains(position))
                continue;

            //check if there is enough space around to fit the prop
            List<Vector2Int> freePositionsAround  = TryToFitProp(propToPlace, availablePositions, position, placement);

            //If we have enough spaces place the prop
            if (freePositionsAround.Count == propToPlace.PropSize.x * propToPlace.PropSize.y) {
                //Place the gameobject
                PlacePropGameObjectAt(room, position, propToPlace);
                //Lock all the positions recquired by the prop (based on its size)
                foreach (Vector2Int pos in freePositionsAround) {
                    //Hashest will ignore duplicate positions
                    room.PropPositions.Add(pos);
                }

                //Deal with groups
                if (propToPlace.PlaceAsGroup) {
                    PlaceGroupObject(room, position, propToPlace, 1);
                }
                return true;
            }
        }
        return false;
    }

    private List<Vector2Int> TryToFitProp(PropSO prop, List<Vector2Int> availablePositions, Vector2Int originPosition, PlacementOriginCorner placement) {
        List<Vector2Int> freePositions = new List<Vector2Int>();

        //Perform the specific loop depending on the PlacementOriginCorner
        if (placement == PlacementOriginCorner.BottomLeft) {
            for (int xOffset = 0; xOffset < prop.PropSize.x; xOffset++) {
                for (int yOffset = 0; yOffset < prop.PropSize.y; yOffset++) {
                    Vector2Int tempPos = originPosition + new Vector2Int(xOffset, yOffset);
                    if (availablePositions.Contains(tempPos))
                        freePositions.Add(tempPos);
                }
            }
        }
        else if (placement == PlacementOriginCorner.BottomRight) {
            for (int xOffset = -prop.PropSize.x + 1; xOffset <= 0; xOffset++) {
                for (int yOffset = 0; yOffset < prop.PropSize.y; yOffset++) {
                    Vector2Int tempPos = originPosition + new Vector2Int(xOffset, yOffset);
                    if (availablePositions.Contains(tempPos))
                        freePositions.Add(tempPos);
                }
            }
        }
        else if (placement == PlacementOriginCorner.TopLeft) {
            for (int xOffset = 0; xOffset < prop.PropSize.x; xOffset++) {
                for (int yOffset = -prop.PropSize.y + 1; yOffset <= 0; yOffset++) {
                    Vector2Int tempPos = originPosition + new Vector2Int(xOffset, yOffset);
                    if (availablePositions.Contains(tempPos))
                        freePositions.Add(tempPos);
                }
            }
        }
        else {
            for (int xOffset = -prop.PropSize.x + 1; xOffset <= 0; xOffset++) {
                for (int yOffset = -prop.PropSize.y + 1; yOffset <= 0; yOffset++) {
                    Vector2Int tempPos = originPosition + new Vector2Int(xOffset, yOffset);
                    if (availablePositions.Contains(tempPos))
                        freePositions.Add(tempPos);
                }
            }
        }
        return freePositions;
    }

    private void PlaceCornerProps(RoomData room, List<PropSO> cornerProps) {
        float tempChance = cornerPropPlacementChance;

        foreach (Vector2Int cornerTile in room.CornerTiles) {
            if (UnityEngine.Random.value < tempChance) {
                PropSO propToPlace = cornerProps[UnityEngine.Random.Range(0, cornerProps.Count)];

                PlacePropGameObjectAt(room, cornerTile, propToPlace);
                if (propToPlace.PlaceAsGroup) {
                    PlaceGroupObject(room, cornerTile, propToPlace, 2);
                }
            }
            else {
                tempChance = Mathf.Clamp01(tempChance + 0.1f);
            }
        }
    }

    private void PlaceGroupObject(RoomData room, Vector2Int groupOriginPosition, PropSO propToPlace, int searchOffset) {
        //*Can work poorely when placing bigger props as groups

        //calculate how many elements are in the group -1 that we have placed in the center
        int count = UnityEngine.Random.Range(propToPlace.GroupMinCount, propToPlace.GroupMaxCount) - 1;
        count = Mathf.Clamp(count, 0, 8);

        //find the available spaces around the center point.
        //we use searchOffset to limit the distance between those points and the center point
        List<Vector2Int> availableSpaces = new List<Vector2Int>();
        for (int xOffset = -searchOffset; xOffset <= searchOffset; xOffset++) {
            for (int yOffset = -searchOffset; yOffset <= searchOffset; yOffset++) {
                Vector2Int tempPos = groupOriginPosition + new Vector2Int(xOffset, yOffset);
                if (room.roomFloor.Contains(tempPos) && !dungeonData.corridors.Contains(tempPos) && !room.PropPositions.Contains(tempPos)) {
                    availableSpaces.Add(tempPos);
                }
            }
        }

        //shuffle the list
        availableSpaces.OrderBy(x => Guid.NewGuid());

        //place the props (as many as we want or if there is less space fill all the available spaces)
        int tempCount = count < availableSpaces.Count ? count : availableSpaces.Count;
        for (int i = 0; i < tempCount; i++) {
            PlacePropGameObjectAt(room, availableSpaces[i], propToPlace);
        }

    }

    private GameObject PlacePropGameObjectAt(RoomData room, Vector2Int placementPostion, PropSO propToPlace) {
        //Instantiat the prop at this positon
        GameObject prop = Instantiate(propPrefab);
        SpriteRenderer propSpriteRenderer = prop.GetComponentInChildren<SpriteRenderer>();
        
        //set the sprite
        propSpriteRenderer.sprite = propToPlace.PropSprite;

        //Add a collider
        CapsuleCollider2D collider 
            = propSpriteRenderer.gameObject.AddComponent<CapsuleCollider2D>();
        collider.offset = Vector2.zero;
        if(propToPlace.PropSize.x > propToPlace.PropSize.y)
        {
            collider.direction = CapsuleDirection2D.Horizontal;
        }
        Vector2 size 
            = new Vector2(propToPlace.PropSize.x*0.8f, propToPlace.PropSize.y*0.8f);
        collider.size = size;

        prop.transform.localPosition = (Vector2)placementPostion;
        //adjust the position to the sprite
        propSpriteRenderer.transform.localPosition 
            = (Vector2)propToPlace.PropSize * 0.5f;

        //Save the prop in the room data (so in the dunbgeon data)
        room.PropPositions.Add(placementPostion);
        room.PropObjectReferences.Add(prop);
        return prop;
    }
}

public enum PlacementOriginCorner {
    BottomLeft,
    BottomRight,
    TopLeft,
    TopRight
}