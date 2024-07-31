using System;
using UnityEngine;

public class ActionShortCutManager : MonoBehaviour
{
    [SerializeField] private GameObject selectedGameobject;

    private BaseAction baseAction;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            UnitActionSystem.Instance.SetSelectedAction(UnitActionSystem.Instance.GetSelectedUnit().GetAction<MoveAction>());
        }
        
        if (Input.GetKeyDown(KeyCode.N))
        {
            UnitActionSystem.Instance.SetSelectedAction(UnitActionSystem.Instance.GetSelectedUnit().GetAction<SpinAction>());
        }
    }
    
}