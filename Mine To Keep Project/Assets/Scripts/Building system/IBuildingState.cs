using UnityEngine;

public interface IBuildingState
{
    void EndState();

    int GetID();
    void OnAction(Vector3Int gridPosition);
    void UpdateState(Vector3Int gridPosition);
}