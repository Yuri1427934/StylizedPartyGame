using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private Animator characterAnimator;
    private Vector3 targetVelocity = Vector3.zero;
    [SerializeField]
    private float MoveSpeed = 10f;
    [SerializeField]
    private float acceleration = 1f;
    private float speedFactor = 1f;
    [SerializeField]
    private AnimationCurve accelerationFactorFromDot;
    [SerializeField]
    private AnimationCurve maxAccelerationForceFactorFromDot;
    private float maxAccelForceFactor = 1f;
    [SerializeField] private float _maxAccelForce = 150f;
    [SerializeField] private float _leanFactor = 0.25f;
    [SerializeField] private Vector3 _moveForceScale = new Vector3(1f, 0f, 1f);

    [SerializeField]
    private float GravityScale = 2f;

    [Header("Floating effect")]
    private Vector3 rayDir = Vector3.down;

    [SerializeField]
    private float groundMaxDis = 1.5f;
    [SerializeField]
    private float rideHeight = 1.4f;
    public LayerMask GroundLayers;
    [SerializeField]
    private float springStrength = 400f;
    [SerializeField]
    private float springDamper = 20f;

    [Header("Upright maintain")]
    [SerializeField]
    private float uprightspringStrength = 30f;
    [SerializeField]
    private float uprightspringDamper = 3f;
    [SerializeField]
    private float JumpForce = 5f;
    private bool IsGround;

    private bool IsStun;

    private Quaternion _uprightTargetRot = Quaternion.identity; // Adjust y value to match the desired direction to face.

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (!IsStun)
        {
            (bool rayHitGround, RaycastHit rayHit) = RayGround();
            if (rayHitGround) SetFloating(rayHit);
            Vector3 velocity = rb.velocity;
            velocity.y = 0;
            KeepUpright(velocity, rayHit);
        }
        FallMul();
    }

    public void SetStun(bool i_isStun)
    {
        this.IsStun = i_isStun;
    }

    void SetFloating(RaycastHit hit)
    {
        Vector3 vel = rb.velocity;
        Vector3 otherVel = Vector3.zero;

        Rigidbody hitBody = hit.rigidbody;
        if (hitBody != null) otherVel = hitBody.velocity;

        float rayDirVel = Vector3.Dot(rayDir, vel);
        float otherDirVel = Vector3.Dot(rayDir, otherVel);

        float relVel = rayDirVel - otherDirVel;
        float currentHeight = hit.distance - rideHeight;
        float sprignForce = (currentHeight * springStrength) - (relVel * springDamper);
        this.rb.AddForce(rayDir * sprignForce);
    }

    void KeepUpright(Vector3 lookAt, RaycastHit hit = new RaycastHit())
    {
        if (lookAt != Vector3.zero)
            _uprightTargetRot = Quaternion.LookRotation(lookAt, Vector3.up);
        Quaternion _current = transform.rotation;
        Quaternion toGoal = ShortestRotation(_uprightTargetRot, _current);

        Vector3 rotAxis;
        float rotDegrees;
        toGoal.ToAngleAxis(out rotDegrees, out rotAxis);
        rotAxis.Normalize();

        float rotRad = rotDegrees * Mathf.Deg2Rad;

        rb.AddTorque((rotAxis * (rotRad * uprightspringStrength)) - (rb.angularVelocity * uprightspringDamper));
    }


    public Quaternion ShortestRotation(Quaternion a, Quaternion b)
    {
        if (Quaternion.Dot(a, b) < 0) return a * Quaternion.Inverse(Multiply(b, -1));
        else return a * Quaternion.Inverse(b);
    }
    public Quaternion Multiply(Quaternion input, float scalar)
    {
        return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
    }

    private (bool, RaycastHit) RayGround()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, rayDir);
        bool rayHitGround = Physics.Raycast(ray, out hit, groundMaxDis, GroundLayers);
        if (rayHitGround)
            IsGround = hit.distance <= rideHeight * 1.3f;//add some buffer
        else
            IsGround = false;
        if (characterAnimator) characterAnimator.SetBool("IsGround", IsGround);
        return (rayHitGround, hit);
    }

    void FallMul()
    {
        if (!IsGround) this.rb.AddForce(Vector3.up * GravityScale * Physics.gravity.y);
    }

    public void MoveCharacter(Vector3 i_input)
    {
        Vector3 unitVel = targetVelocity.normalized;
        float velDot = Vector3.Dot(i_input, unitVel);
        float accel = acceleration * accelerationFactorFromDot.Evaluate(velDot);
        Vector3 goalVel = i_input * MoveSpeed * speedFactor;
        Vector3 otherVel = Vector3.zero;
        targetVelocity = Vector3.MoveTowards(targetVelocity,
                                        goalVel,
                                        accel * Time.fixedDeltaTime);
        Vector3 neededAccel = (targetVelocity - rb.velocity) / Time.fixedDeltaTime;
        float maxAccel = _maxAccelForce * maxAccelerationForceFactorFromDot.Evaluate(velDot) * maxAccelForceFactor;
        neededAccel = Vector3.ClampMagnitude(neededAccel, maxAccel);
        if (characterAnimator) characterAnimator.SetFloat("MoveSpeed", Mathf.Clamp(rb.velocity.magnitude / MoveSpeed, 0, 1));
        rb.AddForceAtPosition(Vector3.Scale(neededAccel * rb.mass, _moveForceScale), transform.position + new Vector3(0f, transform.localScale.y * _leanFactor, 0f));
    }
    public void CharacterJump()
    {
        if (IsGround && !IsStun)
        {
            this.rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            if (characterAnimator) characterAnimator.SetTrigger("Jump");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundMaxDis);
    }
}
