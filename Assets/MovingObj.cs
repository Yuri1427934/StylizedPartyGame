using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using static UnityEngine.GraphicsBuffer;
using System.Linq;

[System.Serializable]
public class LocationSetting
{
    public LocationSetting(Vector3 i_pos, Quaternion i_rot)
    {
        this.Position = i_pos;
        this.Rotation = i_rot;
    }
    public Vector3 Position;
    public Quaternion Rotation;
}
public class MovingObj : TriggerObj
{
    public float cycleLength = 2f;
    public float WaitTime = 0;
    public List<LocationSetting> MoveLocations = new List<LocationSetting>();
    private int MoveIndex = 0;
    protected override void StartAction()
    {
        if (MoveLocations.Count > 0)
        {
            SetObjectLocation(MoveLocations[0]);
            Next();
        }
    }

    void SetObjectLocation(LocationSetting i_target)
    {
        this.transform.position = i_target.Position;
        this.transform.rotation = i_target.Rotation;
    }

    public void Next()
    {
        if (MoveLocations.Count <= 0) return;
        MoveIndex = (MoveIndex + 1) % MoveLocations.Count;
        MoveObject();
    }

    public void AddPosition()
    {
        if (MoveLocations == null) MoveLocations = new List<LocationSetting>();
        MoveLocations.Add(new LocationSetting(this.transform.position, this.transform.rotation));
    }

    IEnumerator WaitForNextMove()
    {
        yield return new WaitForSeconds(WaitTime);
        Next();
    }

    LocationSetting CurrentTarget()
    {
        return MoveLocations[MoveIndex];
    }

    async void MoveObject()
    {
        LocationSetting _target = CurrentTarget();
        var tasks=new List<Task>();

        tasks.Add(this.transform.DOMove(_target.Position, cycleLength).AsyncWaitForCompletion());
        tasks.Add(this.transform.DORotate(_target.Rotation.eulerAngles, cycleLength).AsyncWaitForCompletion());
        await Task.WhenAll(tasks);
        StartCoroutine(WaitForNextMove());
    }
}
