using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI unitName;
    [SerializeField] private Unit unit;
    [SerializeField] private UnitHealth unitHealth;
    [SerializeField]private Image healthBar;
    [SerializeField] private MeshRenderer selectedMeshRenderer;
    private void Start()
    {
        unit.OnThisUnitSelectionChanged += Unit_OnThisUnitSelectionChanged;
        unitHealth.OnDamaged += UnitHealth_OnDamaged;
        unitName.text = unit.name;
        UpdateHealthBar();
        UpdateSelectedVisual();
    }

    private void Unit_OnThisUnitSelectionChanged(object sender, EventArgs e)
    {
        UpdateSelectedVisual();
    }

    private void UnitHealth_OnDamaged(object sender, EventArgs e)
    {
        UpdateHealthBar();
    }


    private void UpdateHealthBar()
    {
        healthBar.fillAmount = unitHealth.GetHealthNormalized();
    }

    private void UpdateSelectedVisual()
    {
        selectedMeshRenderer.enabled = unit.IsSelected();
    }
    
    
}
