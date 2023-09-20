using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Interact : MonoBehaviour
{
    public void buttonInteract(CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("Interact");
        }
    }

    public void buttonSabotage(CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Sabotage");
        }
    }
}
