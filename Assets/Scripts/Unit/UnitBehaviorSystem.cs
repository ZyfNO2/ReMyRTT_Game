using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviorSystem : MonoBehaviour
{
    public static UnitBehaviorSystem Instance { get; private set; }
    [SerializeField] private LayerMask unitLayerMask;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one UnityActionSystem" + transform + "-" + Instance );
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        
    }


    private void Update()
    {
        HandleUnitSelection();
        HandleMove();
    }

    private void HandleMove()
    {
        if (UnitManager.Instance.GetSelectedList().Count == 0)
        {
            return;
        }

        if (InputManager.Instance.IsMouseButton1DownThisFrame())
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());


            foreach (Unit unit in UnitManager.Instance.GetSelectedList())
            {
                Vector3 mousePosition = LevelGrid.Instance.GetWorldPosition(mouseGridPosition);
                Debug.Log(unit.GetUnitMove().name);
                unit.GetUnitMove().StartMove(mousePosition);
            }
            

        }
        
        
    }

    private void HandleUnitSelection()
    {
        if (InputManager.Instance.IsMouseButtonDownThisFrame())
        {
            Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
            if (Physics.Raycast(ray, out RaycastHit raycastHit,
                    float.MaxValue, unitLayerMask))
            {
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    unit.SetIsSelected(!unit.IsSelected());
                }
            }
        }
    }
}
