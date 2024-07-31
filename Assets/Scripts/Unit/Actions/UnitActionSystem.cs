using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    //private bool isBusy;
    


    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    //public event EventHandler<bool> OnBusyChanged;
    public event EventHandler OnActionStarted;
    
    



    //[SerializeField] private List<Unit> selectedUnitList;
    [SerializeField] private Unit selectedUnit;
    [SerializeField]private BaseAction selectedAction;
    [SerializeField] private LayerMask unitLayerMask;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one UnityActionSystem" + transform + "-" + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        //selectedUnitList = new List<Unit>();
        selectedUnit = null;
    }

    private void Update()
    {
        // if (isBusy)
        // {
        //     return;
        // }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        
        if (TryHandleUnitSelection())
        {
            return;
        }
        
        HandleSelectedAction();
        
        
        
    }


    private bool TryHandleUnitSelection()
    {
        if (InputManager.Instance.IsMouseButtonDownThisFrame())
        {
            Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
            {
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    //Debug.Log("1");
                    // if (unit.IsSelected())
                    // {
                    //     unit.SetIsSelected(false);
                    //     selectedUnitList.Remove(unit);
                    //     OnSelectedUnitChanged?.Invoke(this,EventArgs.Empty);
                    // }else if (!unit.IsSelected())
                    // {
                    //     unit.SetIsSelected(true);
                    //     selectedUnitList.Add(unit);
                    //     OnSelectedUnitChanged?.Invoke(this,EventArgs.Empty);
                    // }

                    if (unit == selectedUnit)
                    {
                        return false;
                    }

                    if (selectedUnit != null)
                    {
                        selectedUnit.SetIsSelected(false);
                    }
                    
                    //selectedUnit = unit;
                    
                    SetSelectedUnit(unit);

                    selectedUnit.SetIsSelected(true);
                    return true;
                }
            }
        
            
            
        }
        return false;
    }


    private void HandleSelectedAction()
    {
        if (selectedAction == null)
        {
            return;
        }
        
        
        
        
        
        if (InputManager.Instance.IsMouse1ButtonDownThisFrame())
        {
            // if (!selectedUnit.TrySpendActionPointsToTakeAction(selectedAction))
            // {
            //     return;
            // }
            
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if (!selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                return;
            }
            
            SetBusy();
            
            selectedAction.TakeAction(mouseGridPosition,ClearBusy);

            OnActionStarted?.Invoke(this,EventArgs.Empty);
            
            
        }
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        
        SetSelectedAction(null);
        
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);

    }
    
    
    public void SetSelectedAction(BaseAction baseAction)
    {
        if (baseAction== null)
        {
            return;
        }
        
        selectedAction = baseAction;
        
        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
        
    }
    
    
    
    
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }
    
    
    
    
    private void SetBusy()
    {
        // isBusy = true;
        //
        // OnBusyChanged?.Invoke(this,isBusy);
        //
    }
    
    private void ClearBusy()
    {
        //isBusy = false;
        
        //OnBusyChanged?.Invoke(this,isBusy);
    }

    // public bool GetBusy()
    // {
    //     return isBusy;
    // }
    
    

}
