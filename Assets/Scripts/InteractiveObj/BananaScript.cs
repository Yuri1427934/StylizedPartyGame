using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaScript : MonoBehaviour,IInteractive
{
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private Transform releasePosition;

    [SerializeField]
    [Range(10, 100)]
    private int linePoints = 25;

    [SerializeField]
    [Range(0.01f, 0.25f)]
    private float timeBetweenPoints = 0.1f;

    [SerializeField]
    private float throwStrenth = 10f;

    [SerializeField]
    private float bananaMass = 2f;
    private void DrawProjection()
    {
        lineRenderer.enabled = true;
        lineRenderer.positionCount = Mathf.CeilToInt(linePoints / timeBetweenPoints) + 1;
        Vector3 startPos = releasePosition.position;
        Vector3 initVelocity = throwStrenth * gameObject.transform.forward / bananaMass;
        int i = 0;

        lineRenderer.SetPosition(i, startPos);
        for(float time = 0; time < linePoints; time += timeBetweenPoints)
        {
            i++;
            Vector3 point = startPos + time * initVelocity;
            point.y = startPos.y + initVelocity.y * time + (Physics.gravity.y / 2f * time * time);

            lineRenderer.SetPosition(i, point);
        }
    }

    private void Update()
    {
        DrawProjection();
    }
    public void UseItem()
    {
        Debug.Log("Item used");
    }
}
