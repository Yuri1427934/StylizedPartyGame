using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;

public class PlayerControlManager : MonoBehaviour
{
    [SerializeField]
    private Vector3 Movement;
    public UnityEvent<Vector3> MovementEvent;

    [SerializeField]
    private bool Jump;
    public UnityEvent<bool> JumpEvent;
    public void GetMovement(CallbackContext i_input)
    {
        if (i_input.started) return;
        Movement = i_input.ReadValue<Vector3>();
        MovementEvent.Invoke(Movement);
    }

    public void GetJump(CallbackContext i_input)
    {
        if (i_input.started) return;
        Jump= i_input.performed;
        JumpEvent.Invoke(Jump);
    }
}
