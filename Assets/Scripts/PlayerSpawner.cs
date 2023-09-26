using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    public Transform spawnPos;
    public RespawnManager respawnManager;
    public GameObject playerObj;
    public List<Material> PlayerColors;
    public int maxPlayerNumber = 4;
    private int playerNumber = 0;
    public void OnPlayerJoined(PlayerInput i_player)
    {
        if (playerNumber >= maxPlayerNumber) return;
        GameObject newPlayer = Instantiate(playerObj, spawnPos ? spawnPos.position : this.transform.position, Quaternion.identity);
        newPlayer.name = "P" + (playerNumber + 1);
        if (respawnManager) respawnManager.SetPlayerRespawn(newPlayer);
        if (newPlayer.GetComponent<PlayerScript>() && i_player.GetComponent<PlayerControlManager>())
        {
            if (PlayerColors.Count > playerNumber) newPlayer.GetComponent<PlayerScript>().SetCharacter(PlayerColors[playerNumber]);
            newPlayer.GetComponent<PlayerScript>().SetController(i_player.GetComponent<PlayerControlManager>());
        }

        playerNumber++;
        if (playerNumber >= maxPlayerNumber) this.gameObject.SetActive(false);
    }

    void OnGUI()
    {
        if (playerNumber < maxPlayerNumber)
        {
            GUIStyle myStyle = new GUIStyle();
            myStyle.fontSize = 20;
            GUI.Label(new Rect(10, 10, 250, 20), "Press Start or Enter to join", myStyle);
        }
    }
}
