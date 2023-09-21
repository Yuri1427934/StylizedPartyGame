using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserScript : TriggerObj
{
    public string TargetTag;

    public bool EnableAtStart;

    [SerializeField]
    private LineRenderer Laser;
    [SerializeField]
    private Transform MuzzlePoint;
    [SerializeField]
    private float MaxLength = 10f;

    private void FixedUpdate()
    {
        ProjectLaser();
    }

    void ProjectLaser()
    {
        if (!Laser) return;
        Transform StartPoint = MuzzlePoint ? MuzzlePoint : this.transform;
        Ray ray = new Ray(StartPoint.position, StartPoint.forward);
        bool IsHit = Physics.Raycast(ray, out RaycastHit hit, MaxLength);
        Vector3 HitPos = IsHit ? hit.point : StartPoint.position + StartPoint.forward * MaxLength;
        if (IsHit) LaserHitCheck(hit);
        SetLaserPositions(new Vector3[] { StartPoint.position, HitPos });
    }

    void LaserHitCheck(RaycastHit hit)
    {
        if (hit.transform.tag.Equals(TargetTag))
        {
            Debug.Log("Target hit");
        }
    }

    protected override void StartAction()
    {
        SetLaserEnabel(EnableAtStart);
    }

    void SetLaserEnabel(bool i_isOn)
    {
        if (Laser) Laser.enabled = i_isOn;
    }

    void SetLaserPositions(Vector3[] i_positions)
    {
        if (!Laser) return;
        if (Laser.positionCount != i_positions.Length)
            Laser.positionCount = i_positions.Length;


        Laser.SetPositions(i_positions);
    }

    void LaserFlipFlop()
    {
        if (Laser) SetLaserEnabel(!Laser.enabled);
    }

    public override void TriggerAction()
    {
        LaserFlipFlop();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0);
        Transform StartPoint = MuzzlePoint ? MuzzlePoint : this.transform;
        Gizmos.DrawLine(StartPoint.position, StartPoint.position+StartPoint.forward * MaxLength);
    }
}
