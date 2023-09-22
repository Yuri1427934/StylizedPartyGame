using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 RotateSpd;

    private void FixedUpdate()
    {
        Vector3 RotateAmount= RotateSpd*Time.deltaTime;
        this.transform.Rotate(RotateAmount.x, RotateAmount.y, RotateAmount.z,Space.World);
    }
}
