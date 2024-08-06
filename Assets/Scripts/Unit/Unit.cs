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
    [SerializeField] private bool isEnemy = false;
    [SerializeField]private int actionPoints = 9;
    
    
    private GridPosition gridPosition;
    private UnitHealth unitHealth;
    
    
    private void Awake()
    {
        unitHealth = GetComponent<UnitHealth>();
        //Debug.Log(baseActionArray);
     
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
    
    
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
    
    public int GetActionPoints()
    {
        return actionPoints;
    }
    

    

    public bool IsSelected()
    {
        return isSelected;
    }

    public void SetIsSelected(bool isSelected)
    {
        this.isSelected = isSelected;
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

    public bool IsEnemy()
    {
        return isEnemy;
    }
    
    
    

    private void OnDestroy()
    {
        unitHealth.OnDead -= HealthSystem_OnDead;
    }
    
    
    
    
    
    
    
}
