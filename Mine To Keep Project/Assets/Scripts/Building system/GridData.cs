using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridData
{
    Dictionary<Vector3Int, PlacementData> placedObjects = new();
    Tilemap activeEnvironmentTilemap;

    public void Initialize()
    {
        Debug.Log("Initializing active environment tilemap");
        activeEnvironmentTilemap = GameObject.FindGameObjectWithTag("activeEnvironmentTilemap").GetComponent<Tilemap>();
    }

    public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectSize, int ID, int placedObjectIndex)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        PlacementData data = new PlacementData(positionToOccupy, ID, placedObjectIndex);
        foreach (var pos in positionToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
            {
                throw new Exception($"Dictionary already contains this cell position {pos}");
            }
            //also check if activeEnvironment is blocking placement
            if (activeEnvironmentTilemap.HasTile(gridPosition))
            {
                throw new Exception($"Environment already contains this cell position {pos}");
            }

            placedObjects[pos] = data;
        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new();
        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x, y, 0));
            }
        }
        return returnVal;
    }

    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        foreach (var pos in positionToOccupy)
        {
            if (placedObjects.ContainsKey(pos) || activeEnvironmentTilemap.HasTile(pos))
            {
                return false;
            }
        }
        return true;
    }

    internal int GetRepresentationIndex(Vector3Int gridPosition)
    {
        if(placedObjects.ContainsKey(gridPosition) == false)
        {
            return -1;
        }
        return placedObjects[gridPosition].placedObjectIndex;
    }

    internal void RemoveObjectAt(Vector3Int gridPosition)
    {
        foreach (var pos in placedObjects[gridPosition].occupiedPositions)
        {
            placedObjects.Remove(pos);
        }
    }

    internal int GetObjectIDAt(Vector3Int gridPosition)
    {
        Debug.Log($"Grabbing Object ID At: {gridPosition}");
        if (placedObjects.ContainsKey(gridPosition))
        {
            return placedObjects[gridPosition].ID;
        }
        Debug.Log($"No Object at position {gridPosition}");
        return -1;
    }
}

public class PlacementData
{
    public List<Vector3Int> occupiedPositions;

    public int ID { get; private set; }

    public int placedObjectIndex { get; private set; }


    public PlacementData(List<Vector3Int> occupiedPositions, int iD, int placedObjectIndex)
    {
        this.occupiedPositions = occupiedPositions;
        ID = iD;
        this.placedObjectIndex = placedObjectIndex;
    }
}
