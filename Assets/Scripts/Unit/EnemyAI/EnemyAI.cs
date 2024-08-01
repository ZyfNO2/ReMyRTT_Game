using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Idle,
        Attack,
        Move,
    }

    private State state;
    private Unit unit;

    private DamageAction damageAction;
    private SpinAction spinAction;

    private float timeFlow = 1f;
    


    private List<GridPosition> validGridPositionList;
    private bool isCoolDown;
    
    private void Awake()
    {
        unit = GetComponent<Unit>();
        //Debug.Log(unit.name);
        
    }


    private void Start()
    {
        damageAction = unit.GetAction<DamageAction>();
        spinAction = unit.GetAction<SpinAction>();
    }


    private void Update()
    {
        state = AlterState();
        switch (state)
        {
            case State.Attack:
                TryTakeDamageAction();
                break;
            
            case State.Idle:
                TryTakeSpinAction();
                break;
        }

        timeFlow -= Time.deltaTime;

        if (timeFlow <= 0)
        {
            isCoolDown = false;
            timeFlow = 1f;
        }
        

        //记得改回private
        //Debug.Log(unit.GetAction<DamageAction>().GetState());
        // Debug.Log(unit.GetAction<DamageAction>().stateTimer);
    }


    private State AlterState()
    {
        
        validGridPositionList = damageAction.GetValidActionGridPositionList();

        if (validGridPositionList.Count != 0)
        {
            return State.Attack;
        }
        else
        {
            validGridPositionList = spinAction.GetValidActionGridPositionList();
            return State.Idle;
        }
        
        
    }
    
    
    private bool TryTakeDamageAction()
    {
        if (isCoolDown)
        {
            return false;
        }

        isCoolDown = true;
        damageAction.TakeAction(validGridPositionList[0],ClearBusy);
        return true;
    }

    private bool TryTakeSpinAction()
    {
        if (isCoolDown)
        {
            return false;
        }
        isCoolDown = true;
        //spin action
        return true;
    }
    
    
    
    private void ClearBusy()
    {
        //isBusy = false;
        
        //OnBusyChanged?.Invoke(this,isBusy);
    }
    
    
    
    
    

    
    
}
