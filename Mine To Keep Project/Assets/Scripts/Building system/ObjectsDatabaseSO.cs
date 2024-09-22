using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ObjectsDatabaseSO : ScriptableObject
{
    public List<ObjectData> objectsData;

    public ObjectData GetObjectByID(int id)
    {
        foreach (ObjectData obj in objectsData)
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
    public ResourceType[] resourceCost{ get; private set;}

    [field: SerializeField]
    public int[] resourceCostAmount { get; private set; }

    [field: SerializeField]
    public GameObject Prefab { get; private set; }
}
