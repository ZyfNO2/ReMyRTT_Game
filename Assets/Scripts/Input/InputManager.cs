#define USE_NEW_INPUT_STSTEM
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    public static InputManager  Instance { get; private set; }
    private PlayerInputActions playerInputActions;
    
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one InputManager" + transform + "-" + Instance );
            Destroy(gameObject);
            return;
        }
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

    }
    
    public Vector2 GetMouseScreenPosition()
    {
#if USE_NEW_INPUT_STSTEM
        return (Mouse.current == null) ? Vector2.zero : Mouse.current.position.ReadValue();
#else

        return Input.mousePosition;
#endif
    }

    public bool IsMouseButtonDownThisFrame()
    {
#if USE_NEW_INPUT_STSTEM
        return playerInputActions.Player.Click.WasPressedThisFrame();
#else

        return Input.GetMouseButton(0);
#endif

        
    }
    
    public bool IsMouseButton1DownThisFrame()
    {
#if USE_NEW_INPUT_STSTEM
        return playerInputActions.Player.Click1.WasPressedThisFrame();
#else

        return Input.GetMouseButton(1);
#endif

        
    }
    
    
    

    public Vector2 GetCameraMoveVector()
    {
#if USE_NEW_INPUT_STSTEM
        return playerInputActions.Player.CameraMovement.ReadValue<Vector2>();
#else
        Vector2 inputMoveDir = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.y = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.y = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = +1f;
        }


        return inputMoveDir;
#endif
    }

    public float GetCameraRotateAmount()
    {
#if USE_NEW_INPUT_STSTEM
        
        return playerInputActions.Player.CameraRotate.ReadValue<float>();
#else
        float rotateAmount = 0f;

        if (Input.GetKey(KeyCode.Q))
        {
            rotateAmount = +1f;
        }
        
        if (Input.GetKey(KeyCode.E))
        {
            rotateAmount = -1f;
        }
        
        return rotateAmount;
    }

    public float GetCameraZoomAmount()
    {
        float zoomAmount = 0f;
        
        if (Input.mouseScrollDelta.y > 0)
        {
            zoomAmount = -1f;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            zoomAmount = +1f;
        }

        return zoomAmount;
#endif
    }

    public float GetCameraZoomAmount()
    {
#if USE_NEW_INPUT_STSTEM
        return playerInputActions.Player.CameraZoom.ReadValue<float>();
#else
        float zoomAmount = 0f;
        if (Input.mouseScrollDelta.y >0)
        {
            zoomAmount = -1f;
        }
        if (Input.mouseScrollDelta.y <0)
        {
            zoomAmount = +1f;
        }

        return zoomAmount;
#endif

    }
    
    
    
}
