using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }
    
    [SerializeField] private Transform gridDebugObjectPrefab;
    
    private GridSystem gridSystem; 
    
    //public event EventHandler OnAnyUnitMovedGridPosition;
    
    [SerializeField]private int width;
    [SerializeField]private int height;
    [SerializeField]private float cellSize;
    
    public event EventHandler OnAnyUnitMovedGridPosition;
    
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one LevelGrid" + transform + "-" + Instance );
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        gridSystem = new GridSystem(width, height,cellSize,
            (GridSystem g,GridPosition gridPosition)=> new GridObject(g,gridPosition));
        List<GridDebugObject> gridDebugObjects = gridSystem.CreateDebugObjects(gridDebugObjectPrefab);

        foreach (GridDebugObject gridDebugObject in gridDebugObjects)
        {
            gridDebugObject.transform.parent = this.transform;
        }
        
    }

    private void Start()
    {
        PathFindingManager.Instance.SetUp(width,height,cellSize);
    }


    public void UnitMovePosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtPosition(fromGridPosition,unit);
        
        AddUnitAtGridPosition(toGridPosition,unit);
        
        OnAnyUnitMovedGridPosition?.Invoke(this,EventArgs.Empty);
    }

    public List<Unit> GetUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        List<Unit> unitList = gridObject.GetUnitList();
        return unitList;
    } 
    
    
    public void AddUnitAtGridPosition(GridPosition gridPosition,Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
        //Debug.Log(gridObject.GetUnitList());
    }
    
    public void RemoveUnitAtPosition(GridPosition gridPosition , Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        //Debug.Log(unit);
        gridObject.RemoveUnit(unit);
    }
    
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return gridSystem.GetGridPosition(worldPosition);
    }
    
    
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return gridSystem.GetWorldPosition(gridPosition);
    }
          
    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);
    
    public int GetWidth() => gridSystem.GetWidth();
    public int GetHeight() => gridSystem.GetHeight();

    public GridSystem GetGridSystem()
    {
        return gridSystem;
    }
    
}
