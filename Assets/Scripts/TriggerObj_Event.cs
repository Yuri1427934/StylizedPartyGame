using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerObj_Event : TriggerObj
{
    [SerializeField]
    private bool IsOn;
    public UnityEvent OnAction;
    public UnityEvent OffAction;

    protected override void StartAction()
    {
        InvokeAction();
    }

    void InvokeAction()
    {
        if (IsOn) OnAction.Invoke();
        else OffAction.Invoke();
    }

    public override void TriggerAction()
    {
        IsOn = !IsOn;
        InvokeAction();
    }
}
