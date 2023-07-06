using Fusion;
using LibSM64;
using NSMB.Entities.Player;
using UnityEngine;

public class SM64Controls : SM64InputProvider {

    public PlayerController controller;

    private NetworkButtons Input => controller.PreviousInputs.buttons;

    public override bool GetButtonHeld(Button button) {
        return button switch {
            Button.Jump => Input.IsSet(PlayerControls.Jump),
            Button.Kick => Input.IsSet(PlayerControls.PowerupAction),
            Button.Stomp => Input.IsSet(PlayerControls.Sprint),
            _ => false,
        };
    }

    public override Vector3 GetCameraLookDirection() {
        return Camera.main.transform.forward;
    }

    public override Vector2 GetJoystickAxes() {
        bool right = Input.IsSet(PlayerControls.Right);
        bool left =  Input.IsSet(PlayerControls.Left);
        bool up =    Input.IsSet(PlayerControls.Up);
        bool down =  Input.IsSet(PlayerControls.Down);

        Vector2 result = Vector2.zero;
        if (right) result += Vector2.right;
        if (left) result += Vector2.left;
        if (up) result += Vector2.up;
        if (down) result += Vector2.down;

        return result.normalized;
    }
}
