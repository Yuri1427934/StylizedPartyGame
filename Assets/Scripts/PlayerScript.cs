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
    public float JumpForce = 10;

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



    private void Update()
    {
        FallMul();
    }

    private void FixedUpdate()
    {
        MoveFunc();
        RotateFunc();
    }
    #region-Input Methods
    /// <summary>
    /// Set the input controller of the player
    /// </summary>
    /// <param name="i_controller"></param>
    public void SetController(PlayerControlManager i_controller)
    {
        controlManager = i_controller;
        i_controller.MovementEvent.AddListener(GetMovementInput);
        i_controller.JumpEvent.AddListener(GetJumpInput);
    }
    /// <summary>
    /// Get the input of movement
    /// </summary>
    /// <param name="i_movement"></param>
    private void GetMovementInput(Vector3 i_movement)
    {
        this.Movement = i_movement;
    }

    /// <summary>
    /// Get the input of jump
    /// </summary>
    /// <param name="i_jump"></param>
    private void GetJumpInput(bool i_jump)
    {
        if (i_jump && IsGround()) this.rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }
    #endregion
    #region-Movement Methods
    /// <summary>
    /// For player rotate to the direction
    /// </summary>
    void RotateFunc()
    {
        if (Movement.magnitude > 0)
        {
            float singleStep = 100 * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, GetRelativeMovement(), singleStep, 0.0f);
            this.rb.rotation = Quaternion.LookRotation(newDirection);
        }

    }

    /// <summary>
    /// If player is in air, multiply the gravity
    /// </summary>
    void FallMul()
    {
        if (!IsGround() && this.rb.velocity.y < 0) this.rb.AddForce(Vector3.up * GravityScale * Physics.gravity.y);
    }

    /// <summary>
    /// Set player movement
    /// </summary>
    void MoveFunc()
    {
        if (!rb) return;
        this.rb.AddForce(GetMovement());
    }
    /// <summary>
    /// Check player is on ground
    /// </summary>
    /// <returns></returns>
    public bool IsGround()
    {
        return Physics.OverlapBox(this.transform.position + CheckPosition, CheckSize, Quaternion.identity, GroundLayers).Count() > 0;
    }
    #endregion
    #region-Calculate Methods
    /// <summary>
    /// Get the force of the movement
    /// </summary>
    /// <returns></returns>
    public Vector3 GetMovement()
    {
       
        Vector3 TargetSpeed = GetRelativeMovement() * MoveSpeed;
        return new Vector3(GetDirForce(TargetSpeed.x, rb.velocity.x), 0, GetDirForce(TargetSpeed.z, rb.velocity.z));
    }

    public Vector3 GetRelativeMovement()
    {
        Vector3 Camf = Camera.main.transform.forward;
        Vector3 Camr = Camera.main.transform.right;
        Camf.y = 0;
        Camr.y = 0;
        Camf = Camf.normalized;
        Camr = Camr.normalized;
        return Movement.x * Camr + Movement.z * Camf;
    }

    /// <summary>
    /// Get the force calculate of the direction
    /// </summary>
    /// <param name="TargetSpeed"></param>
    /// <param name="rbVel"></param>
    /// <returns></returns>
    float GetDirForce(float TargetSpeed, float rbVel)
    {
        float SpeedDif = TargetSpeed - rbVel;
        float accelRate = (Mathf.Abs(TargetSpeed) > 0.01f) ? acceleration : decceleration;
        return Mathf.Pow(Mathf.Abs(SpeedDif) * accelRate, VelPower) * Mathf.Sign(SpeedDif);
    }
    #endregion
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireCube(this.transform.position + CheckPosition, CheckSize);
    }
}
