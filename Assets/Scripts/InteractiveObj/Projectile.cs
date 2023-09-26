using System;
using UnityEngine;

public class Projectile : MonoBehaviour,IInteractive
{
    private GameObject Rope;
    [SerializeField]
    private Material Purple;

    [SerializeField]
    private Material defaultMat;

    [SerializeField]
    private Transform releasePos;

    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private float RangeOfTrapDetectionFromPlayer = 25;

    [SerializeField]
    private float minimumDistanceFromTrapRequiredToShoot = 15;

    bool canShoot;
    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, RangeOfTrapDetectionFromPlayer);

        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.tag == "Trap" && Vector3.Distance(hitCollider.transform.position, transform.position) < minimumDistanceFromTrapRequiredToShoot)
            {
                Rope = hitCollider.gameObject;
                Rope.GetComponent<MeshRenderer>().material = Purple;
                canShoot = true;
            }
            else if(Rope && Vector3.Distance(Rope.transform.position, transform.position) > minimumDistanceFromTrapRequiredToShoot)
            {
                Rope.GetComponent<MeshRenderer>().material = defaultMat;
                canShoot = false;
            }
        }

    }
    public void Shoot()
    {
        if(canShoot)
        {
            var step = Time.deltaTime * 10;
            var instance = Instantiate(projectile, releasePos.position, transform.rotation);
            instance.GetComponent<Rigidbody>().AddForce(10 * (Rope.transform.position - releasePos.position));
        }

    }

    public void UseItem()
    {
        Debug.Log("Item used");
    }
}
