using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;

public class PlayerControlManager : MonoBehaviour
{
    [SerializeField]
    private Vector3 Movement;
    /// <summary>
    /// The movement event for object to subscribe
    /// </summary>
    public UnityEvent<Vector3> MovementEvent;

    [SerializeField]
    private bool Jump;
    /// <summary>
    /// The jump event for object to subscribe
    /// </summary>
    public UnityEvent<bool> JumpEvent;
    /// <summary>
    /// Get movement input
    /// </summary>
    /// <param name="i_input"></param>
    public void GetMovement(CallbackContext i_input)
    {
        if (i_input.started) return;
        Movement = i_input.ReadValue<Vector3>();
        MovementEvent.Invoke(Movement);
    }

    /// <summary>
    /// Get jump input
    /// </summary>
    /// <param name="i_input"></param>
    public void GetJump(CallbackContext i_input)
    {
        if (i_input.started) return;
        Jump= i_input.performed;
        JumpEvent.Invoke(Jump);
    }
}
