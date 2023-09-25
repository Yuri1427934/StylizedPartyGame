using System;
using UnityEngine;

public class BananaScript : MonoBehaviour,IInteractive
{
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private GameObject releasePos;
    private Rigidbody rb_projectile;
    Vector3 startPos;
    Vector3 startVelocity;
    float initialForce = 75f;
    float initialAngle = -45f;
    Quaternion rotation;
    int i = 0; //index of renderer
    int numberOfPoints = 10;
    float timer = 0.1f; //time difference between each point
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rb_projectile = projectile.GetComponent<Rigidbody>();
        rotation = Quaternion.Euler(initialAngle, 0, 0);
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            drawPath();
            Debug.Log("Drew path");
        }
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            var instance = Instantiate(projectile, releasePos.transform.position, releasePos.transform.rotation);
            instance.GetComponent<Rigidbody>().AddForce(rotation * (initialForce * transform.forward));
            lineRenderer.enabled = false;
        }
    }

    private void drawPath()
    {
        i = 0;
        lineRenderer.positionCount = numberOfPoints;
        lineRenderer.enabled = true;
        startPos = releasePos.transform.position;
        startVelocity = rotation * (initialForce * releasePos.transform.forward) / 5 ;
        lineRenderer.SetPosition(i, startPos);

        for(float j = 0; i < lineRenderer.positionCount-1; j += timer)
        {
            i++;
            Vector3 linePos = startPos + j * startVelocity;
            linePos.y = startPos.y + startVelocity.y * j + 0.5f * Physics.gravity.y * j * j;
            lineRenderer.SetPosition(i, linePos);
        }
    }

    public void UseItem()
    {
        Debug.Log("Item used");
    }
}
