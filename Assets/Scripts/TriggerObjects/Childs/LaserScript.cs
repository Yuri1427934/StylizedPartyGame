using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserScript : TriggerObj
{
    public string TargetTag;

    public MeshRenderer meshRenderer;
    public Collider LaserCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(TargetTag))
        {
            if (other.GetComponent<PlayerScript>()) other.GetComponent<PlayerScript>().StunFunc();
        }
    }

    public override void TriggerAction()
    {
        if (meshRenderer) meshRenderer.enabled = !meshRenderer.enabled;
        if(LaserCollider) LaserCollider.enabled = !LaserCollider.enabled;
    }
}
