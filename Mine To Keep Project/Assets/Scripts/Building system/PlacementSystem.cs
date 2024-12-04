using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private InputManager _inputManager;

    [SerializeField]
    private Grid _grid;

    [SerializeField]
    private ObjectsDatabaseSO _database;

    [SerializeField]
    private GameObject _gridVisualization;
    [SerializeField]
    private GameObject _placementUI;

    private GridData _structureData;

    [SerializeField]
    private PreviewSystem _preview;

    private Vector3Int _lastDetectedPosition = Vector3Int.zero;

    [SerializeField]
    private ObjectPlacer _objectPlacer;

    IBuildingState _buildingState;

    private void Awake()
    {
        _inputManager.OnActivate += ToggleUI;
    }

    private void Start()
    {
        StopPlacement();
        _structureData = new();
        _structureData.Initialize();

        Initialize();
    }

    private void Initialize()
    {
        foreach (var item in _database.ObjectsDataList)
        {

        }
    }

    private void ToggleUI()
    {
        if (_placementUI.activeSelf)
        {
            _placementUI.SetActive(false);
        }
        else
        {
            _placementUI.SetActive(true);
        }
    }

    private bool HasEnoughResources(int objectID)
    {
        for (int i = 0; i < _database.GetObjectByID(objectID).ResourceCostAmount.Length; i++)
        {
            if (_database.GetObjectByID(objectID).ResourceCostAmount[i] >
                ResourceController.Instance.GetResourceAmount(_database.GetObjectByID(objectID).ResourceCost[i]))
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
        _gridVisualization.SetActive(true);

        _buildingState = new PlacementState(objectID, _grid, _preview, _database, _structureData, _objectPlacer);

        _inputManager.OnClicked += PlaceStructure;
        _inputManager.OnExit += StopPlacement;
    }

    public void StartRemoving()
    {
        StopPlacement();
        _gridVisualization.SetActive(true);

        _buildingState = new RemovingState(_grid, _preview, _database, _structureData, _objectPlacer);

        _inputManager.OnClicked += PlaceStructure;
        _inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (_inputManager.IsPointerOverUI())
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
        Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);

        _buildingState.OnAction(gridPosition);

        //check if resources available
        if (_buildingState is PlacementState)
        {
            if (!HasEnoughResources(_buildingState.GetID()))
            {
                StopPlacement();
            }
        }
    }

    private void StopPlacement()
    {
        if(_buildingState == null)
        {
            return;
        }
        _gridVisualization.SetActive(false);
        _buildingState.EndState();
        _inputManager.OnClicked -= PlaceStructure;
        _inputManager.OnExit -= StopPlacement;

        _lastDetectedPosition = Vector3Int.zero;

        _buildingState = null;
    }

    public void PlacementExitButtonHelper()
    {
        //if still building -> exit building; else -> exit UI
        if (_gridVisualization.activeSelf)
        {
            StopPlacement();
            return;
        }
        _placementUI.SetActive(false);
    }

    private void Update()
    {
        if(_buildingState == null)
        {
            return;
        }
        Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);

        if(_lastDetectedPosition != gridPosition)
        {
            _buildingState.UpdateState(gridPosition);
            _lastDetectedPosition = gridPosition;
        }
    }
}
