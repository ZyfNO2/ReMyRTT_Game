using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    public event EventHandler OnStartMoving;
    public event EventHandler OnStopMoving;
    
    
    private List<Vector3> positionList;
    private int currentPositionIndex;
    
    private float moveSpeed = 4f;
    private float rotateSpeed = 16f;
    private readonly float stoppingDistance = .1f;
    
    [SerializeField] private int maxMoveDistance = 2;

    private Vector3 targetPosition;
    
    private void Start()
    {
        GetValidActionGridPositionList();
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        // List<GridPosition> pathGridPositionList = new List<GridPosition>();
        //
        // positionList = new List<Vector3>();
        //
        // pathGridPositionList.Add(gridPosition);//need reverse
        //
        // foreach (GridPosition pathGridPosition in pathGridPositionList)
        // {
        //     positionList.Add(LevelGrid.Instance.GetWorldPosition(pathGridPosition));
        // }

        //targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        
        List<GridPosition> pathGridPositionList = 
            PathFindingManager.Instance.FindPath(unit.GetGridPosition(), 
                gridPosition,out int pathLength);
        
        currentPositionIndex = 0;
        positionList = new List<Vector3>();

        foreach (GridPosition pathGridPosition in pathGridPositionList)
        {
            positionList.
                Add(LevelGrid.Instance.GetWorldPosition(pathGridPosition));
        }
        
        OnStartMoving?.Invoke(this,EventArgs.Empty);
       
        ActionStart(onActionComplete);
    }
    
    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        Vector3 targetPosition = positionList[currentPositionIndex];
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        
        transform.forward = Vector3.Lerp
            (transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
        
        
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            transform.position += moveDirection * (moveSpeed * Time.deltaTime);
            
        }
        else
        {
            currentPositionIndex++;
            if (currentPositionIndex >= positionList.Count)
            {
                OnStopMoving?.Invoke(this,EventArgs.Empty);
                
                ActionComplete();
            }
            
        }
        
        
        
    }


    public override string GetActionName()
    {
        return "Move";
    }

    

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition =  new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition).Count != 0)
                {
                    continue;
                }

                if (testGridPosition == unit.GetGridPosition())
                {
                    //self
                    continue;
                }
                
                
                if ((Mathf.Abs(x) + Mathf.Abs(z)) > maxMoveDistance)
                {
                    continue;
                }
                
                
                
                validGridPositionList.Add(testGridPosition);
                //Debug.Log(testGridPosition);
            }
        }


        return validGridPositionList;
        
        
    }
}
