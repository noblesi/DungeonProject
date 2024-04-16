using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(BoxCollider2D))]
public class InstantiatedRoom : MonoBehaviour
{
    [HideInInspector] public Room room;
    [HideInInspector] public Grid grid;
    [HideInInspector] public Tilemap groundTilemap;
    [HideInInspector] public Tilemap decoration1Tilemap;
    [HideInInspector] public Tilemap decoration2Tilemap;
    [HideInInspector] public Tilemap frontTilemap;
    [HideInInspector] public Tilemap collisionTilemap;
    [HideInInspector] public Tilemap minimapTilemap;
    [HideInInspector] public Bounds roomColliderBounds;

    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();

        roomColliderBounds = boxCollider2D.bounds;
    }

    public void Initialize(GameObject roomGameobj)
    {
        PopulateTilemapMemberVariables(roomGameobj);

        BlockOffUnusedDoorways();

        DisableCollisionTilemapRenderer();
    }

    private void PopulateTilemapMemberVariables(GameObject roomGameobj)
    {
        grid = roomGameobj.GetComponentInChildren<Grid>();

        Tilemap[] tilemaps = roomGameobj.GetComponentsInChildren<Tilemap>();    

        foreach(Tilemap tilemap in tilemaps)
        {
            if(tilemap.gameObject.tag == "groundTilemap")
            {
                groundTilemap = tilemap;
            }
            else if(tilemap.gameObject.tag == "decoration1Tilemap")
            {
                decoration1Tilemap = tilemap;
            }
            else if(tilemap.gameObject.tag == "decoration2Tilemap")
            {
                decoration2Tilemap = tilemap;
            }
            else if(tilemap.gameObject.tag == "frontTilemap")
            {
                frontTilemap = tilemap;
            }
            else if(tilemap.gameObject.tag == "collisionTilemap")
            {
                collisionTilemap = tilemap;
            }
            else if(tilemap.gameObject.tag == "minimapTilemap")
            {
                minimapTilemap = tilemap;
            }
        }
    }

    private void BlockOffUnusedDoorways()
    {
        foreach(DoorWay doorway in room.doorWayList)
        {
            if (doorway.isConnected)
                continue;

            if(collisionTilemap != null)
            {
                BlockDoorwayOnTilemapLayer(collisionTilemap, doorway);
            }

            if(minimapTilemap != null)
            {
                BlockDoorwayOnTilemapLayer(minimapTilemap, doorway);
            }

            if(groundTilemap != null)
            {
                BlockDoorwayOnTilemapLayer(groundTilemap, doorway);
            }

            if(decoration1Tilemap != null)
            {
                BlockDoorwayOnTilemapLayer(decoration1Tilemap, doorway);
            }

            if(decoration2Tilemap != null)
            {
                BlockDoorwayOnTilemapLayer(decoration2Tilemap, doorway);
            }

            if(frontTilemap != null)
            {
                BlockDoorwayOnTilemapLayer(frontTilemap, doorway);
            }
        }
    }

    private void BlockDoorwayOnTilemapLayer(Tilemap tilemap, DoorWay doorway)
    {
        switch (doorway.orientation)
        {
            case Orientation.north:
            case Orientation.south:
                BlockDoorwayHorizontally(tilemap, doorway);
                break;

            case Orientation.east:
            case Orientation.west:
                BlockDoorwayVertically(tilemap, doorway);
                break;

            case Orientation.none:
                break;
        }
    }

    private void BlockDoorwayHorizontally(Tilemap tilemap, DoorWay doorway)
    {
        Vector2Int startPosition = doorway.doorwayStartCopyPosition;

        for(int xPos = 0; xPos < doorway.doorwayCopyTileWidth; xPos++)
        {
            for(int yPos = 0; yPos < doorway.doorwayCopyTileHeight; yPos++)
            {
                Matrix4x4 transformMatrix = tilemap.GetTransformMatrix(new Vector3Int(startPosition.x + xPos, startPosition.y - yPos, 0));

                tilemap.SetTile(new Vector3Int(startPosition.x + 1 + xPos, startPosition.y - yPos, 0), tilemap.GetTile(new Vector3Int(startPosition.x +
                    xPos, startPosition.y - yPos, 0)));

                tilemap.SetTransformMatrix(new Vector3Int(startPosition.x + 1 + xPos, startPosition.y - yPos, 0), transformMatrix);
            }
        }
    }

    private void BlockDoorwayVertically(Tilemap tilemap, DoorWay doorway)
    {
        Vector2Int startPosition = doorway.doorwayStartCopyPosition;

        for (int yPos = 0; yPos < doorway.doorwayCopyTileHeight; yPos++)
        {
            for (int xPos = 0; xPos < doorway.doorwayCopyTileWidth; xPos++)
            {
                Matrix4x4 transformMatrix = tilemap.GetTransformMatrix(new Vector3Int(startPosition.x + xPos, startPosition.y - yPos, 0));

                tilemap.SetTile(new Vector3Int(startPosition.x + xPos, startPosition.y - 1 - yPos, 0), tilemap.GetTile(new Vector3Int(startPosition.x +
                    xPos, startPosition.y - yPos, 0)));

                tilemap.SetTransformMatrix(new Vector3Int(startPosition.x + xPos, startPosition.y - 1 - yPos, 0), transformMatrix);
            }
        }
    }

    private void DisableCollisionTilemapRenderer()
    {
        collisionTilemap.gameObject.GetComponent<TilemapRenderer>().enabled = false;
    }
}
