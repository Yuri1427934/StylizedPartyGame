using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    public Transform SpawnPos;
    public GameObject PlayerObj;
    public void OnPlayerJoined(PlayerInput i_player)
    {
        GameObject newPlayer = Instantiate(PlayerObj, SpawnPos.position,Quaternion.identity);
        if (newPlayer.GetComponent<PlayerScript>() && i_player.GetComponent<PlayerControlManager>())
        {
            newPlayer.GetComponent<PlayerScript>().SetController(i_player.GetComponent<PlayerControlManager>());
        }
    }
}
