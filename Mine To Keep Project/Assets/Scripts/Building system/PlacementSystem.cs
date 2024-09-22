using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabaseSO database;

    [SerializeField]
    private GameObject gridVisualization;
    [SerializeField]
    private GameObject placementUI;

    private GridData structureData;

    [SerializeField]
    private PreviewSystem preview;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    [SerializeField]
    private ObjectPlacer objectPlacer;

    IBuildingState buildingState;

    private void Awake()
    {
        inputManager.OnActivate += ToggleUI;
    }

    private void Start()
    {
        StopPlacement();
        structureData = new();
        structureData.Initialize();
    }

    private void ToggleUI()
    {
        if (placementUI.activeSelf)
        {
            placementUI.SetActive(false);
        }
        else
        {
            placementUI.SetActive(true);
        }
    }

    private bool HasEnoughResources(int objectID)
    {
        for (int i = 0; i < database.GetObjectByID(objectID).resourceCostAmount.Length; i++)
        {
            if (database.GetObjectByID(objectID).resourceCostAmount[i] >
                ResourceManager.instance.GetResourceAmount(database.GetObjectByID(objectID).resourceCost[i]))
            {
                Debug.Log("Not enough resources!");
                return false;
            }
        }
        return true;
    }

    public void StartPlacement(int objectID)
    {
        //check if resources available
        if (!HasEnoughResources(objectID))
        {
            return;
        }

        StopPlacement();
        gridVisualization.SetActive(true);

        buildingState = new PlacementState(objectID, grid, preview, database, structureData, objectPlacer);

        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    public void StartRemoving()
    {
        StopPlacement();
        gridVisualization.SetActive(true);

        buildingState = new RemovingState(grid, preview, database, structureData, objectPlacer);

        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }

        //check if resources available
        //if (buildingState is PlacementState)
        //{
        //    for (int i = 0; i < database.GetObjectByID(buildingState.GetID()).resourceCostAmount.Length; i++)
        //    {
        //        if (database.GetObjectByID(buildingState.GetID()).resourceCostAmount[i] >
        //            ResourceManager.instance.GetResourceAmount(database.GetObjectByID(buildingState.GetID()).resourceCost[i]))
        //        {
        //            Debug.Log("Not enough resources!");
        //            StopPlacement();
        //            return;
        //        }
        //    }
        //}

        //calculate placement position
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        buildingState.OnAction(gridPosition);

        //check if resources available
        if (buildingState is PlacementState)
        {
            if (!HasEnoughResources(buildingState.GetID()))
            {
                StopPlacement();
            }
        }
    }

    private void StopPlacement()
    {
        if(buildingState == null)
        {
            return;
        }
        gridVisualization.SetActive(false);
        buildingState.EndState();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;

        lastDetectedPosition = Vector3Int.zero;

        buildingState = null;
    }

    public void PlacementExitButtonHelper()
    {
        //if still building -> exit building; else -> exit UI
        if (gridVisualization.activeSelf)
        {
            StopPlacement();
            return;
        }
        placementUI.SetActive(false);
    }

    private void Update()
    {
        if(buildingState == null)
        {
            return;
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        if(lastDetectedPosition != gridPosition)
        {
            buildingState.UpdateState(gridPosition);
            lastDetectedPosition = gridPosition;
        }
    }
}
