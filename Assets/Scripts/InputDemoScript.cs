using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class InputDemoScript : MonoBehaviour
{
    public void GetInput(CallbackContext _input)
    {
        if (_input.performed)
        {
            Debug.Log(_input.ReadValue<Vector2>());
        }
    }
}
