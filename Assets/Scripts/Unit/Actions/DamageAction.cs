using System;
using System.Collections.Generic;
using UnityEngine;

public class DamageAction : BaseAction
{
    public enum State
    {
        Prepare,
        Damage,
        CoolOff,
    
    }
    
    
    [SerializeField] private  int maxDamageDistance = 3;
    [SerializeField]private State state;
    public static event EventHandler<OnDamageEventArgs> OnAnyDamage;
    public event EventHandler<OnDamageEventArgs> OnDamage;
    
    public class OnDamageEventArgs : EventArgs
    {
        public List<Unit> targetUnitList;
        public Unit doDamageUnit;

    }
    
    [SerializeField]public float stateTimer;
    [SerializeField]private List<Unit> targetUnit;
    [SerializeField]private bool canDamage;//mark the state of damage
    
    

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
        state = State.Prepare;
        float coolDownStateTime = .5f;
        stateTimer = coolDownStateTime;

        canDamage = true;
        
        ActionStart(onActionComplete);
        
        
    }

    private void Update()
    {
        //Debug.Log(canDamage);
        
        if (!isActive)
        {
            return;
        }
        
        stateTimer -= Time.deltaTime;

        switch (state)
        {
            case State.Prepare:
                Vector3 aimDir = (targetUnit[0].GetWorldPosition() - unit.GetWorldPosition()).normalized;
                float rotateSpeed = 10f;
                
                transform.forward = Vector3.Lerp(transform.forward,aimDir, 
                    Time.deltaTime * rotateSpeed);
                break;
            
            case State.Damage:
                
                if (canDamage)
                {
                    Damage();
                    canDamage = false;
                }
                break;
            case State.CoolOff:
                break;
            
        }
        
        
        if (stateTimer <= 0f)
        {
            NextState();
        }
        
        
        
    }

    private void NextState()
    {
        switch (state)
        {
            case State.Prepare:
                state = State.Damage;
                float prepareStateTime = .1f;
                stateTimer = prepareStateTime;
                break;
            
            case State.Damage:
                state = State.CoolOff;
                float coolOffStateTime = .5f;
                stateTimer = coolOffStateTime;
                break;
            
            case State.CoolOff:
                ActionComplete();
                break;
        }
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
        
        
        //Debug.Log(targetUnit[0]);
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
                
                List<Unit> unitList = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);
                bool isGetTatger = false;
                foreach (Unit unitDamaged in unitList)
                {
                    if (unitDamaged.IsEnemy() != unit.IsEnemy())
                    {
                        isGetTatger = true;
                    }
                }

                if (!isGetTatger)
                {
                    continue;
                }
                
                
                
                if (testGridPosition == unit.GetGridPosition())
                {
                    continue;
                }
                
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

    public State GetState()
    {
        return state;
    }
    
    
    
    
    
}
