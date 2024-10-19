using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResource
{
    public ResourceType Type { get; private set; }

    public int Amount { get; private set; }

    public GameResource(ResourceType type, int amount)
    {
        this.Type = type;
        this.Amount = amount;
    }
}


public enum ResourceType
{
    WOOD,
    TIMBER,
    ROCK,
    STONE,
    COAL,
    IRONORE,
    IRON
}