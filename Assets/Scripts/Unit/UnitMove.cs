using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    private Vector3 targetPosition;
    private Vector3 nextNodePosition;
    private List<Vector3> positionList;
    

    private Unit unit;
    private bool isMoving;
    
    private float moveSpeed = 4f;
    private float rotateSpeed = 16f;
    private readonly float stoppingDistance = .1f;

    [SerializeField]private LineRenderer pathRender;
    
    

    private void Awake()
    {
        unit = GetComponent<Unit>();
        //pathRender = GetComponent<LineRenderer>();
    }

    private void Start()
    {
     
    }

    private void Update()
    {
        

        HandleMoveSpeed();
        
        if (!isMoving)
        {
            return;
        }
        
        HandleMove();

        RenderTrail();
        

    }

    private void HandleMove()
    {
        if (positionList.Count > 1)
        {
            Vector3 nextNodePosition = positionList[1];
            Vector3 moveDirection = (nextNodePosition - transform.position).normalized;
        
            transform.forward = Vector3.Lerp
                (transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

            if (Vector3.Distance(transform.position, nextNodePosition) > stoppingDistance)
            {
                transform.position += moveDirection * (moveSpeed * Time.deltaTime);
            }
            else
            {
                if (LevelGrid.Instance.GetGridPosition(this.transform.position) ==
                    LevelGrid.Instance.GetGridPosition(targetPosition))
                {
                    //end
                    isMoving = false;
                }
                else
                {
                    FindPath(LevelGrid.Instance.GetGridPosition(targetPosition));
                }
            }
        }
        
        
    }

    private void HandleMoveSpeed()
    {
        GridPosition unitGridPosition = LevelGrid.Instance.GetGridPosition(this.transform.position);
        if (LevelGrid.Instance.GetUnitAtGridPosition(unitGridPosition).Count > 1)
        {
            moveSpeed = 2f;
        }
        else
        {
            moveSpeed = 4f;
        }
    }

    public void StartMove(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        isMoving = true;
        FindPath(LevelGrid.Instance.GetGridPosition(targetPosition));
    }
    
    public void FindPath(GridPosition gridPosition)
    {
        List<GridPosition> pathGridPositionList = 
            PathFindingManager.Instance.FindPath(unit.GetGridPosition(), 
                gridPosition,out int pathLength);
        
        positionList = new List<Vector3>();

        foreach (GridPosition pathGridPosition in pathGridPositionList)
        {
            positionList.
                Add(LevelGrid.Instance.GetWorldPosition(pathGridPosition));
            
            //Debug.Log(pathGridPosition.ToString());
            
        }

        //RenderTrail();
    }

    private List<Vector3> ListToRender()
    {
        return positionList;
    }

    private void RenderTrail()
    {

        if (pathRender == null)
        {
            return;
        }
        
        pathRender.positionCount = ListToRender().Count;

        for (int i = 0; i < pathRender.positionCount; i++)
        {
            pathRender.SetPosition(i,ListToRender()[i]);
        }

        ListToRender()[0] = this.transform.position;


    }
    
    
    
    

}
