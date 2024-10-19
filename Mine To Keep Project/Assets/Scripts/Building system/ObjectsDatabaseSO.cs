using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ObjectsDatabaseSO : ScriptableObject
{
    public List<ObjectData> ObjectsData;

    public ObjectData GetObjectByID(int id)
    {
        foreach (ObjectData obj in ObjectsData)
        {
            if (id == obj.ID)
            {
                return obj;
            }
        }
        Debug.Log($"No object with ID: {id}");
        return null;
    }
}


[Serializable]
public class ObjectData
{
    [field: SerializeField]
    public string Name {  get; private set; }

    [field: SerializeField]
    public int ID { get; private set; }

    [field: SerializeField]
    public Vector2Int Size { get; private set; } = Vector2Int.one;

    [field: SerializeField]
    public ResourceType[] ResourceCost{ get; private set;}

    [field: SerializeField]
    public int[] ResourceCostAmount { get; private set; }

    [field: SerializeField]
    public GameObject Prefab { get; private set; }
}
