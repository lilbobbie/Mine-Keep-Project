using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    private Dictionary<ResourceType, int> resourceDictionary = new Dictionary<ResourceType, int>();

    [SerializeField]
    private ResourcePresenter presenter;

    public static ResourceManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        foreach (ResourceType resourceType in Enum.GetValues(typeof(ResourceType)))
        {
            resourceDictionary.Add(resourceType, 0);
            GameResource resource = new GameResource(resourceType, 1);
            AddResource(resource);
            RemoveResource(resource);
        }
    }


    public void AddResource(GameResource resource)
    {
        ResourceType type = resource.type;

        resourceDictionary[type] += resource.amount;

        Debug.Log($"GameResource {resource.amount} {resource.type} added");
        Debug.Log($"Total amount: {resourceDictionary[type]}");
        presenter.UpdateResourceDisplay(type);
    }

    public void RemoveResource(GameResource resource)
    {
        ResourceType type = resource.type;

        resourceDictionary[type] -= resource.amount;

        if(resourceDictionary[type] < 0)
        {
            resourceDictionary[type] = 0;
        }

        Debug.Log($"GameResource {resource.amount} {resource.type} removed");
        Debug.Log($"Total amount: {resourceDictionary[type]}");
        presenter.UpdateResourceDisplay(type);
    }

    public int GetResourceAmount(ResourceType type)
    {
        return resourceDictionary[type];
    }
}
