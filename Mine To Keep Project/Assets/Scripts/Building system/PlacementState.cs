using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlacementState : IBuildingState
{
    private int _selectedObjectIndex = -1;
    int ID;
    Grid grid;
    PreviewSystem previewSystem;
    ObjectsDatabaseSO database;
    GridData structureData;
    ObjectPlacer objectPlacer;

    public PlacementState(int iD,
                          Grid grid,
                          PreviewSystem previewSystem,
                          ObjectsDatabaseSO database,
                          GridData structureData,
                          ObjectPlacer objectPlacer)
    {
        ID = iD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.database = database;
        this.structureData = structureData;
        this.objectPlacer = objectPlacer;

        _selectedObjectIndex = database.ObjectsDataList.FindIndex(data => data.ID == ID);
        if (_selectedObjectIndex > -1)
        {
            previewSystem.StartShowingPlacementPreview(
                database.ObjectsDataList[_selectedObjectIndex].Prefab,
                database.ObjectsDataList[_selectedObjectIndex].Size);
        }
        else
        {
            throw new System.Exception($"No object with ID {iD}");
        }
    }

    public void EndState()
    {
        previewSystem.StopShowingPlacementPreview();
    }

    public int GetID()
    {
        return ID;
    }

    public void OnAction(Vector3Int gridPosition)
    {
        //check if placement valid
        bool isPlacementValid = CheckPlacementValidity(gridPosition, _selectedObjectIndex);
        if (isPlacementValid == false)
        {
            return;
        }

        int index = objectPlacer.PlaceObject(database.ObjectsDataList[_selectedObjectIndex].Prefab, grid.CellToWorld(gridPosition));

        //GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? floorData : structureData;
        GridData selectedData = structureData;
        selectedData.AddObjectAt(gridPosition,
            database.ObjectsDataList[_selectedObjectIndex].Size,
            database.ObjectsDataList[_selectedObjectIndex].ID,
            index);

        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);

        for (int i = 0; i < database.GetObjectByID(ID).ResourceCost.Length; i++)
        {
            GameResource resource = new GameResource(database.GetObjectByID(ID).ResourceCost[i], database.GetObjectByID(ID).ResourceCostAmount[i]);
            ResourceController.Instance.RemoveResource(resource);
        }
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        //GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ? floorData : structureData;
        GridData selectedData = structureData;

        return selectedData.CanPlaceObjectAt(gridPosition, database.ObjectsDataList[selectedObjectIndex].Size);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool isPlacementValid = CheckPlacementValidity(gridPosition, _selectedObjectIndex);

        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), isPlacementValid);
    }
}
