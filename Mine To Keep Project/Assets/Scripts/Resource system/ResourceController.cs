using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceController))]
public class ResourceController : MonoBehaviour
{
    private Dictionary<ResourceType, int> _resourceDictionary = new Dictionary<ResourceType, int>(); // Change this to save file later
    private ResourcePresenter _presenter;

    public static ResourceController Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        if (_presenter == null)
        {
            _presenter = GetComponent<ResourcePresenter>();
        }

        foreach (ResourceType resourceType in Enum.GetValues(typeof(ResourceType)))
        {
            _resourceDictionary.Add(resourceType, 0);
            GameResource resource = new GameResource(resourceType, 1);
            AddResource(resource);
            RemoveResource(resource);
        }
    }


    public void AddResource(GameResource resource)
    {
        ResourceType type = resource.Type;

        _resourceDictionary[type] += resource.Amount;

        Debug.Log($"GameResource {resource.Amount} {resource.Type} added");
        Debug.Log($"Total amount: {_resourceDictionary[type]}");
        _presenter.UpdateResourceDisplay(type);
    }

    public void RemoveResource(GameResource resource)
    {
        ResourceType type = resource.Type;

        _resourceDictionary[type] -= resource.Amount;

        if(_resourceDictionary[type] < 0)
        {
            _resourceDictionary[type] = 0;
        }

        Debug.Log($"GameResource {resource.Amount} {resource.Type} removed");
        Debug.Log($"Total amount: {_resourceDictionary[type]}");
        _presenter.UpdateResourceDisplay(type);
    }

    public int GetResourceAmount(ResourceType type)
    {
        return _resourceDictionary[type];
    }
}
