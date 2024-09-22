using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHelper : MonoBehaviour
{
    public void AddTimber(int amount)
    {
        GameResource resource = new GameResource(ResourceType.Timber, amount);
        ResourceManager.instance.AddResource(resource);
    }

    public void AddStone(int amount)
    {
        GameResource resource = new GameResource(ResourceType.Stone, amount);
        ResourceManager.instance.AddResource(resource);
    }

    public void AddIron(int amount)
    {
        GameResource resource = new GameResource(ResourceType.Iron, amount);
        ResourceManager.instance.AddResource(resource);
    }

    public void RemoveTimber(int amount)
    {
        GameResource resource = new GameResource(ResourceType.Timber, amount);
        ResourceManager.instance.RemoveResource(resource);
    }

    public void RemoveStone(int amount)
    {
        GameResource resource = new GameResource(ResourceType.Stone, amount);
        ResourceManager.instance.RemoveResource(resource);
    }
    public void RemoveIron(int amount)
    {
        GameResource resource = new GameResource(ResourceType.Iron, amount);
        ResourceManager.instance.RemoveResource(resource);
    }
}
