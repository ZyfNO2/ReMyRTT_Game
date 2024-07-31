using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    
    private List<ActionButtonUI> actionButtonUIList;
    
    private void Awake()
    {
        actionButtonUIList = new List<ActionButtonUI>(); 
    }
    
    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
        UnitActionSystem.Instance.OnActionStarted += UnitActionSystem_OnActionStarted;
        //TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        //Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;
        
        //UpdateActionPoints();
        CreateUnitActionButton();
        UpdateSelectedVisual(); 
        
    }

    private void CreateUnitActionButton()
    {
        
        
        foreach (Transform buttonTransform in actionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }
        
        actionButtonUIList.Clear();

        if (UnitActionSystem.Instance.GetSelectedUnit() == null)
        {
            return;
        }
        
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        
        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);
            
            actionButtonUIList.Add(actionButtonUI);
            
        }
        
    }
    
    private void UpdateSelectedVisual()
    {
        foreach (ActionButtonUI actionButtonUI in actionButtonUIList)
        {
            actionButtonUI.UpdateSelectedVisual();
        }
    }
    
    
    private void UnitActionSystem_OnSelectedUnitChanged(object sender,EventArgs e )
    {
        
        CreateUnitActionButton();
        UpdateSelectedVisual();
        //UpdateActionPoints();
    }

    

    public void UnitActionSystem_OnActionStarted(object sender, EventArgs e)
    {
        //UpdateActionPoints();
    }
    
    private void UnitActionSystem_OnSelectedActionChanged(object sender,EventArgs e)
    {
        UpdateSelectedVisual();
    }
    
    
    
    
}
