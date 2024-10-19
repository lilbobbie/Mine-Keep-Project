using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHelper : MonoBehaviour
{
    public void AddTimber(int amount)
    {
        GameResource resource = new GameResource(ResourceType.TIMBER, amount);
        ResourceManager.Instance.AddResource(resource);
    }

    public void AddStone(int amount)
    {
        GameResource resource = new GameResource(ResourceType.STONE, amount);
        ResourceManager.Instance.AddResource(resource);
    }

    public void AddIron(int amount)
    {
        GameResource resource = new GameResource(ResourceType.IRON, amount);
        ResourceManager.Instance.AddResource(resource);
    }

    public void RemoveTimber(int amount)
    {
        GameResource resource = new GameResource(ResourceType.TIMBER, amount);
        ResourceManager.Instance.RemoveResource(resource);
    }

    public void RemoveStone(int amount)
    {
        GameResource resource = new GameResource(ResourceType.STONE, amount);
        ResourceManager.Instance.RemoveResource(resource);
    }
    public void RemoveIron(int amount)
    {
        GameResource resource = new GameResource(ResourceType.IRON, amount);
        ResourceManager.Instance.RemoveResource(resource);
    }
}
