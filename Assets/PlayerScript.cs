using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private PlayerControlManager controlManager;
    [SerializeField]
    private Rigidbody rb;

    [Header("Movement")]
    public float MoveSpeed = 10f;
    public float acceleration = 1f;
    public float decceleration = -1f;
    public float VelPower = 1f;
    public float JumpForce = 100f;

    [Header("GroundChecking")]
    public Vector3 CheckPosition;
    public Vector3 CheckSize;
    public LayerMask GroundLayers;
    [Header("Info")]

    public float GravityScale = 2f;

    [SerializeField]
    protected Vector3 Movement;
    private void Awake()
    {
        if (rb == null) rb.GetComponent<Rigidbody>();
        if (controlManager) SetController(controlManager);
    }
    public void SetController(PlayerControlManager i_controller)
    {
        i_controller.MovementEvent.AddListener(GetMovementInput);
        i_controller.JumpEvent.AddListener(GetJumpInput);
    }

    private void GetMovementInput(Vector3 i_movement)
    {
        this.Movement = i_movement;
    }

    private void GetJumpInput(bool i_jump)
    {
        Debug.Log(i_jump && IsGround());
        if (i_jump && IsGround()) this.rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }
    private void Update()
    {
        FallMul();
    }

    private void FixedUpdate()
    {
        MoveFunc();
        RotateFunc();
    }

    void RotateFunc()
    {
        if (Movement.magnitude > 0)
        {
            float singleStep = 100 * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, Movement, singleStep, 0.0f);
            this.rb.rotation = Quaternion.LookRotation(newDirection);
        }

    }

    void FallMul()
    {
        if (!IsGround() && this.rb.velocity.y < 0) this.rb.AddForce(Vector3.up * GravityScale * Physics.gravity.y);
    }

    void MoveFunc()
    {
        if (!rb) return;
        this.rb.AddForce(GetMovement());
    }

    public Vector3 GetMovement()
    {
        Vector3 TargetSpeed = Movement * MoveSpeed;
        return new Vector3(GetDirForce(TargetSpeed.x, rb.velocity.x), 0, GetDirForce(TargetSpeed.z, rb.velocity.z));
    }

    public bool IsGround()
    {
        return Physics.OverlapBox(this.transform.position + CheckPosition, CheckSize, Quaternion.identity, GroundLayers).Count() > 0;
    }

    float GetDirForce(float TargetSpeed, float rbVel)
    {
        float SpeedDif = TargetSpeed - rbVel;
        float accelRate = (Mathf.Abs(TargetSpeed) > 0.01f) ? acceleration : decceleration;
        return Mathf.Pow(Mathf.Abs(SpeedDif) * accelRate, VelPower) * Mathf.Sign(SpeedDif);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireCube(this.transform.position + CheckPosition, CheckSize);
    }
}
