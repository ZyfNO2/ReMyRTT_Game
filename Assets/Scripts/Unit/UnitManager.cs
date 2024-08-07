using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }
    
    [SerializeField]private List<Unit> unitList;
    [SerializeField]private List<Unit> friendlyUnitList;
    [SerializeField]private List<Unit> enemyUnitList;
    [SerializeField]private List<Unit> selectedUnitList;
   


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one UnitManager" + transform + "-" + Instance );
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        
        unitList = new List<Unit>();
        friendlyUnitList = new List<Unit>();
        enemyUnitList = new List<Unit>();
        selectedUnitList = new List<Unit>();

    }

    private void Start()
    {
        Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
        Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
        Unit.OnAnyUnitSelectedChanged += Unit_OnAnyUnitSelectedChanged;

    }

    private void Unit_OnAnyUnitSelectedChanged(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;

        if (unit.IsSelected())
        {
            selectedUnitList.Add(unit);
        }
        else
        {
            selectedUnitList.Remove(unit);
        }
        
        
        
    }


    private void Unit_OnAnyUnitSpawned(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;
        unitList.Add(unit);
        if (unit != null && unit.IsEnemy())
        {
            enemyUnitList.Add(unit);
        }
        else
        {
            friendlyUnitList.Add(unit);
        }
    }
    
    
    
    private void Unit_OnAnyUnitDead(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;
        unitList.Remove(unit);
        if (unit.IsEnemy())
        {
            enemyUnitList.Remove(unit);
        }
        else
        {
            friendlyUnitList.Remove(unit);
        }
        
    }


    public List<Unit> GetUnitList()
    {
        return unitList;
    }
    public List<Unit> GetFriendlyUnitList()
    {
        return friendlyUnitList;
    }
    public List<Unit> GetEnemyUnitList()
    {
        return enemyUnitList;
    }
    
    public List<Unit> GetSelectedList()
    {
        return selectedUnitList;
    }

    
}