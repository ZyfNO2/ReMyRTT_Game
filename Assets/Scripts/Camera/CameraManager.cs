using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //[SerializeField] private GameObject actionCameraGameObject;
    private Unit targetUnit;
    //private Vector3 actionCameraPosition;


    private void Start()
    {
        BaseAction.OnAnyActionStarted += BaseAction_OnAnyActionStarted;
        BaseAction.OnAnyActionCompleted += BaseAction_OnAnyActionCompleted;

        HideActionCamera();

        //actionCameraPosition = new Vector3();
        //actionCameraGameObject = new GameObject();
        

    }

    private void Update()
    {
        // if (targetUnit != null && actionCameraGameObject != null)
        // {
        //     actionCameraGameObject.transform.LookAt(targetUnit.transform);
        //     
        //     actionCameraGameObject.transform.position = targetUnit.transform.position + Vector3.back * 8 + Vector3.up * 8;
        //     
        // }
    }


    private void BaseAction_OnAnyActionCompleted(object sender, EventArgs e)
    {
        //if (sender is ShootAction)
        switch (sender)
        {
            case MoveAction moveAction:
                //actionCameraGameObject.transform.parent = null;
                HideActionCamera();
                
                break;
        }
    }

    private void BaseAction_OnAnyActionStarted(object sender, EventArgs e)
    {
        //if (sender is ShootAction)
        switch (sender)
        {
            case MoveAction moveAction:
                // actionCameraGameObject.transform.position =
                //     UnitActionSystem.Instance.GetSelectedUnit().transform.position;
                //
                // actionCameraGameObject.transform.position =
                //     actionCameraGameObject.transform.position + Vector3.back * 2 + Vector3.up * 3;
                targetUnit = UnitActionSystem.Instance.GetSelectedUnit();
                //actionCameraPosition = targetUnit.transform.position + Vector3.back * 2 + Vector3.up * 3;

                //actionCameraGameObject.transform.parent = targetUnit.transform;
                //actionCameraGameObject.transform.position = actionCameraPosition;
                //actionCameraGameObject.transform.LookAt(targetUnit.transform);
                
                ShowActionCamera();
                break;
        }
        
        
        
    }


    private void ShowActionCamera()
    {
        //actionCameraGameObject.SetActive(true);
    }

    private void HideActionCamera()
    {
        //actionCameraGameObject.SetActive(false);
        
    }
    
    
}
