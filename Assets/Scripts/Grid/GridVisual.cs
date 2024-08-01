using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisual : MonoBehaviour
{
    public static GridVisual Instance { get; private set; }
    [SerializeField] private Transform gridSystemVisualSinglePrefab;
    [SerializeField] private Transform gridVisualSingleParent;
    //private GridVisualSingle[,] gridVisualArray;
    private GridVisualSingle[,] gridVisualSingleArray;
    
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one GridSystemVisual" + transform + "-" + Instance );
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        gridVisualSingleArray = new GridVisualSingle[LevelGrid.Instance.GetWidth(),LevelGrid.Instance.GetHeight()];
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridVisualSingle = Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition),Quaternion.identity);
                gridVisualSingle.parent = gridVisualSingleParent;
                gridVisualSingleArray[x, z] = gridVisualSingle.GetComponent<GridVisualSingle>();

            }
        }

        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
        LevelGrid.Instance.OnAnyUnitMovedGridPosition += LevelGrid_OnAnyUnitMovedGridPosition;
        Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
        HideAllGridPosition();

    }

    


    private void UpdateGirdVisual()
    {
        HideAllGridPosition();
        
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        if (selectedUnit == null)
        {
            return;
        }
        
        BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();

        List<GridPosition>validActionGridPositionList = selectedAction.GetValidActionGridPositionList();

        ShowGridPositionRange(validActionGridPositionList);


    }

    private void ShowGridPositionRange(List<GridPosition> gridPositionList)
    {
        foreach (GridPosition gridPosition in gridPositionList)
        {
            ShowGridPosition(gridPosition);
        }
    }
    
    
    public void HideAllGridPosition()
    {
        
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                gridVisualSingleArray[x,z].Hide();
                //gridVisualArray[x, z].Hide();
                //Debug.Log(gridSystemVisualArray[x, z]);


            }
        }
    }

    public void ShowGridPosition(GridPosition gridPosition)
    {
        gridVisualSingleArray[gridPosition.x,gridPosition.z].Show(null);
    }

    
    private void Unit_OnAnyUnitDead(object sender, EventArgs e)
    {
        UpdateGirdVisual();
    }

    private void LevelGrid_OnAnyUnitMovedGridPosition(object sender, EventArgs e)
    {
        UpdateGirdVisual();
    }

   
    private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e)
    {
        UpdateGirdVisual();
    }
    
    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
    {
        HideAllGridPosition();
    }
    
    
    
}
