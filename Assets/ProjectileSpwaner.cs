using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpwaner : MonoBehaviour
{
    [SerializeField]
    private GameObject SpawnObj;
    [SerializeField]
    private float SpawnFreq=1.5f;
    [SerializeField]
    private Vector3 SpawnForce=Vector3.zero;

    private void Start()
    {
        StartCoroutine(SpawnFunc());
    }
    IEnumerator SpawnFunc()
    {
        while (true) {
            yield return new WaitForSeconds(SpawnFreq);
            SpawnAction();
        }
       
    }

    void SpawnAction()
    {
        if (SpawnObj)
        {
            GameObject tmp = Instantiate(SpawnObj, this.transform.position, Quaternion.identity);
            if(tmp.GetComponent<Rigidbody>() != null)
            {
                tmp.GetComponent<Rigidbody>().AddForce(SpawnForce,ForceMode.Impulse);
            }
            Destroy(tmp,2f);
        }
    }
}
