using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerObj : MonoBehaviour
{
    public string ObjectId;
    private void Start()
    {
        if (GameEventManager.instance) GameEventManager.instance.ObjectTrigger.AddListener(TriggerFunc);
    }

    void TriggerFunc(string i_ObjId)
    {
        if (ObjectId.Equals(i_ObjId))
        {
            TriggerAction();
        }
    }

    public virtual void TriggerAction()
    {
        Debug.Log("Do something");
    }

}
