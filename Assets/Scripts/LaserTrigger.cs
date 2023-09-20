using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrigger : MonoBehaviour
{
    public string TriggerId;
    public string PlayerTag;
    private bool IsOn;
    private int PlayerCount;
    private void OnTriggerEnter(Collider other)
    {
        if (IsOn) return;
        if (other.tag.Equals(PlayerTag))
        {
            PlayerCountChange(1);
            if (GameEventManager.instance) GameEventManager.instance.ObjectTrigger.Invoke(TriggerId);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsOn) return;
        if (other.tag.Equals(PlayerTag))
        {
            PlayerCountChange(-1);
            if (GameEventManager.instance) GameEventManager.instance.ObjectTrigger.Invoke(TriggerId);
        }
    }

    void PlayerCountChange(int amount)
    {
        PlayerCount = Mathf.Clamp(PlayerCount+amount,0,4);
        IsOn = PlayerCount > 0;
    }
}
