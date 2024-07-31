using UnityEngine;
using System;
using UnityEngine.Serialization;

public class Unit : MonoBehaviour
{
    public static event EventHandler OnAnyUnitDead;
    public static event EventHandler OnAnyUnitSpawned;
    public static event EventHandler OnAnyActionPointsChanged;
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField]private bool isSelected;
    [SerializeField]private int actionPoints = 9;
    
    private BaseAction[] baseActionArray;
    private GridPosition gridPosition;
    private UnitHealth unitHealth;
    
    private void Awake()
    {
        unitHealth = GetComponent<UnitHealth>();
        //Debug.Log(baseActionArray);
        baseActionArray = GetComponents<BaseAction>();
    }

    private void Start()
    {
        unitHealth.OnDead += HealthSystem_OnDead;
        
        
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        
        LevelGrid.Instance.AddUnitAtGridPosition(this.GetGridPosition(),this);
        
        OnAnyUnitSpawned?.Invoke(this,EventArgs.Empty);
        
    }
    
    private void Update()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        if (newGridPosition != gridPosition)
        {
            GridPosition oldGridPosition = gridPosition;
            gridPosition = newGridPosition;
            LevelGrid.Instance.UnitMovePosition(this,oldGridPosition,newGridPosition);

        }
    }
    
    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
    {
        
        if (CanSpendActionPointToTakeAction(baseAction))
        {
            SpendActionPoint(baseAction.GetActionPointsCost());
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private void SpendActionPoint(int amount)
    {
        actionPoints -= amount;
        
        OnAnyActionPointsChanged?.Invoke(this,EventArgs.Empty);
    }
    
    public bool CanSpendActionPointToTakeAction(BaseAction baseAction)
    {
        if (actionPoints >= baseAction.GetActionPointsCost())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
    
    public int GetActionPoints()
    {
        return actionPoints;
    }
    
    public T GetAction<T>() where T: BaseAction
    {
        foreach (BaseAction baseAction in baseActionArray)
        {
            if (baseAction is T)
            {
                return (T)baseAction;
            }
        }

        return null;
    }
    

    public bool IsSelected()
    {
        return isSelected;
    }

    public void SetIsSelected(bool isSelected)
    {
        this.isSelected = isSelected;
    }
    
    public BaseAction[] GetBaseActionArray()
    {
        return baseActionArray;
    }
    
    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }
    
    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        LevelGrid.Instance.RemoveUnitAtPosition(gridPosition,this);
        
        Destroy(gameObject);
        
        OnAnyUnitDead?.Invoke(this,EventArgs.Empty);
    }


    public void Damage(int damageAmount)
    {
        unitHealth.Damage(damageAmount);
    }


    private void OnDestroy()
    {
        unitHealth.OnDead -= HealthSystem_OnDead;
    }
}
