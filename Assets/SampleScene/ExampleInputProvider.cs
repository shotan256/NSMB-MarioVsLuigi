using UnityEngine;
using LibSM64;
using UnityEngine.InputSystem;

public class ExampleInputProvider : SM64InputProvider
{
    public GameObject cameraObject = null;

    public InputActionReference movement, jump, kick, z;

    public void Start() {
        movement.action.actionMap.Enable();
    }

    public override Vector3 GetCameraLookDirection()
    {
        Debug.Log("A");
        return Camera.main.transform.forward;
    }

    public override Vector2 GetJoystickAxes()
    {
        Debug.Log("b" + movement.action.ReadValue<Vector2>());
        return movement.action.ReadValue<Vector2>();
    }

    public override bool GetButtonHeld( Button button )
    {
        Debug.Log("c");
        switch( button )
        {
            case SM64InputProvider.Button.Jump:  return jump.action.IsPressed();
            case SM64InputProvider.Button.Kick:  return kick.action.IsPressed();
            case SM64InputProvider.Button.Stomp: return z.action.IsPressed();
        }
        return false;
    }
}