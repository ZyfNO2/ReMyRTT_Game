using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAction : BaseAction
{
    [SerializeField] private  int maxDamageDistance = 1;
 
    public static event EventHandler<OnDamageEventArgs> OnAnyDamage;
    public event EventHandler<OnDamageEventArgs> OnDamage;
    
    public class OnDamageEventArgs : EventArgs
    {
        public List<Unit> targetUnitList;
        public Unit doDamageUnit;

    }
    
    private float stateTimer;
    private List<Unit> targetUnit;
    private bool canDamage;//mark the state of damage
    
    

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
        
        float aimingStateTime = 1f;
        stateTimer = aimingStateTime;

        canDamage = true;
        
        ActionStart(onActionComplete);
        
        
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }
        
        stateTimer -= Time.deltaTime;
        
        Damage();
        
        ActionComplete();
        
    }

    private void Damage()
    {
        OnAnyDamage?.Invoke(this,new OnDamageEventArgs
        {
            targetUnitList = this.targetUnit,
            doDamageUnit = unit,
        });
        
        OnDamage?.Invoke(this,new OnDamageEventArgs
        {
            targetUnitList = targetUnit,
            doDamageUnit = unit,
        });
        
        
        Debug.Log(targetUnit[0]);
        targetUnit[0].Damage(90);
        
    }
    

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxDamageDistance; x <= maxDamageDistance; x++)
        {
            for (int z = -maxDamageDistance; z <= maxDamageDistance; z++)
            {
                GridPosition offsetGridPosition =  new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition).Count == 0)
                {
                    continue;
                }
                
                if (testGridPosition == unit.GetGridPosition())
                {
                    continue;
                }
                
                
                // if ((Mathf.Abs(x) + Mathf.Abs(z)) > maxDamageDistance)
                // {
                //     continue;
                // }
                
                
                
                validGridPositionList.Add(testGridPosition);
                //Debug.Log(testGridPosition);
            }
        }


        return validGridPositionList;

    }
    
    
    
    public override string GetActionName()
    {
        return "Damage";
    }
    
    public List<Unit> GetTargetUnit()
    {
        return targetUnit;
    }
    
    public int GetMaxDamageDistance()
    {
        return maxDamageDistance;
    }
    
    
    
    
    
    
    
}
