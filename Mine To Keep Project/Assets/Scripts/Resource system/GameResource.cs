using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResource
{
    public ResourceType type { get; private set; }

    public int amount { get; private set; }

    public GameResource(ResourceType type, int amount)
    {
        this.type = type;
        this.amount = amount;
    }
}


public enum ResourceType
{
    Wood,
    Timber,
    Rock,
    Stone,
    Coal,
    IronOre,
    Iron
}