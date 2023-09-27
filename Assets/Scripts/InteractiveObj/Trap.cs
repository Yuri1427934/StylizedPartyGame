using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    bool isDestroyed = false;
    Transform NetPos;
    GameObject Net;
    HingeJoint joint;
    // Start is called before the first frame update
    void Start()
    {
        //Net.AddComponent<Rigidbody>();
        NetPos = gameObject.GetComponentInChildren<Transform>();
        Net = null;
        joint = gameObject.GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void LateUpdate()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            Debug.Log("Trap");
            Destroy(collision.gameObject);
            if(joint.connectedBody)
                Net = joint.connectedBody.gameObject;
            joint.connectedBody = null;
            isDestroyed = true;
        }
        if (isDestroyed)
        {
            Invoke("Reset", 5f);
            Debug.Log("Resetted trap");
        }

    }

    private void Reset()
    {
        if(Net)
        {
            Net.transform.position = new Vector3(NetPos.localPosition.x, NetPos.localPosition.y - 1.5f, NetPos.localPosition.z);
            joint.connectedBody = Net.GetComponent<Rigidbody>();
            isDestroyed = false;
            Net = null;
        }
    }

}
