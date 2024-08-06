using System;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    // [SerializeField] private GameObject unitSelectedVisual;
    // [SerializeField] private Unit unit;
    // private void Awake()
    // {
    //     
    // }
    //
    // private void Start()
    // {
    //     UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
    // }
    //
    // private void Update()
    // {
    //     UpdateUnitSelectedVisual();
    // }
    //
    // private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
    // {
    //     if (UnitActionSystem.Instance.GetSelectedUnit() == null)
    //     {
    //         return;
    //     }
    //     UpdateUnitSelectedVisual();
    // }
    //
    // private void UpdateUnitSelectedVisual()
    // {
    //     if (UnitActionSystem.Instance.GetSelectedUnit() == unit)
    //     {
    //         unitSelectedVisual.SetActive(true);
    //     }
    //     else
    //     {
    //         unitSelectedVisual.SetActive(false);
    //     }
    // }
    //
    //
    // private void OnDestroy()
    // {
    //     UnitActionSystem.Instance.OnSelectedUnitChanged -= UnitActionSystem_OnSelectedUnitChanged;
    // }
}
