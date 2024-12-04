using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHelper : MonoBehaviour
{
    public void AddTimber(int amount)
    {
        GameResource resource = new GameResource(ResourceType.TIMBER, amount);
        ResourceController.Instance.AddResource(resource);
    }

    public void AddStone(int amount)
    {
        GameResource resource = new GameResource(ResourceType.STONE, amount);
        ResourceController.Instance.AddResource(resource);
    }

    public void AddIron(int amount)
    {
        GameResource resource = new GameResource(ResourceType.IRON, amount);
        ResourceController.Instance.AddResource(resource);
    }

    public void RemoveTimber(int amount)
    {
        GameResource resource = new GameResource(ResourceType.TIMBER, amount);
        ResourceController.Instance.RemoveResource(resource);
    }

    public void RemoveStone(int amount)
    {
        GameResource resource = new GameResource(ResourceType.STONE, amount);
        ResourceController.Instance.RemoveResource(resource);
    }
    public void RemoveIron(int amount)
    {
        GameResource resource = new GameResource(ResourceType.IRON, amount);
        ResourceController.Instance.RemoveResource(resource);
    }
}
