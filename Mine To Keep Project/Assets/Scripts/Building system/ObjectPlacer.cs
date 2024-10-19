using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _placedStructures = new List<GameObject>();

    public int PlaceObject(GameObject prefab, Vector3 position)
    {
        int index = 0;
        GameObject newObject = Instantiate(prefab);
        newObject.transform.position = position;
        for (index = 0; index < _placedStructures.Count; index++)
        {
            if (_placedStructures[index] == null)
            {
                _placedStructures[index] = newObject;
                break;
            }
        }
        if (index == _placedStructures.Count)
            _placedStructures.Add(newObject);
        return index;
    }

    internal void RemoveObjectAt(int gameObjectIndex)
    {
        if(_placedStructures.Count <= gameObjectIndex || _placedStructures[gameObjectIndex] == null)
        {
            return;
        }
        Destroy(_placedStructures[gameObjectIndex]);
        _placedStructures[gameObjectIndex] = null;
    }
}
