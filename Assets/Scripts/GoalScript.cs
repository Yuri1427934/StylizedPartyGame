using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public string PlayerTag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(PlayerTag))
        {
            if (MainGameManager.instance)
                MainGameManager.instance.GameWin(other.name);
        }
    }
}
