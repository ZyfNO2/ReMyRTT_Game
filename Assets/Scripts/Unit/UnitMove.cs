using System;
using System.Collections.Generic;
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


    private void Awake()
    {
        unit = GetComponent<Unit>();
    }

    private void Start()
    {
        targetPosition = new Vector3(6, 0, 6);
        
         //
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isMoving = true;
            FindPath(LevelGrid.Instance.GetGridPosition(targetPosition));
        }

        if (!isMoving)
        {
            return;
        }
        
        
        Vector3 nextNodePosition = positionList[1];
        Vector3 moveDirection = (nextNodePosition - transform.position).normalized;
        
        transform.forward = Vector3.Lerp
            (transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

        if (Vector3.Distance(transform.position, nextNodePosition) > stoppingDistance)
        {
            Debug.Log("111");
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
        
        
    }
    

}
