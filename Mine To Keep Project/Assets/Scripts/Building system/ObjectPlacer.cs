using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> placedStructures = new List<GameObject>();

    public int PlaceObject(GameObject prefab, Vector3 position)
    {
        int index = 0;
        GameObject newObject = Instantiate(prefab);
        newObject.transform.position = position;
        for (index = 0; index < placedStructures.Count; index++)
        {
            if (placedStructures[index] == null)
            {
                placedStructures[index] = newObject;
                break;
            }
        }
        if (index == placedStructures.Count)
            placedStructures.Add(newObject);
        return index;
    }

    internal void RemoveObjectAt(int gameObjectIndex)
    {
        if(placedStructures.Count <= gameObjectIndex || placedStructures[gameObjectIndex] == null)
        {
            return;
        }
        Destroy(placedStructures[gameObjectIndex]);
        placedStructures[gameObjectIndex] = null;
    }
}
