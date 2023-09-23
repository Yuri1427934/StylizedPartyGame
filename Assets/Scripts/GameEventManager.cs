using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This is the main game event we can use in game
/// Objects can use trigger each other by invoke event here
/// </summary>
public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// The event which can trigger interactible objects
    /// (Ex. Laser button trigger the laser show/hide)
    /// </summary>
    public UnityEvent<string> ObjectTrigger;

    /// <summary>
    /// Event which can respawn player(temp)
    /// </summary>
    public UnityEvent<string, GameObject> PlayerRespawn;
}
