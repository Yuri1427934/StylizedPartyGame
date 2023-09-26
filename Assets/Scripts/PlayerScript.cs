using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Projectile))]
public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private PlayerControlManager controlManager;

    private Projectile ShootManager;
    [SerializeField]
    private Rigidbody rb;

    private PlayerMovement momvementController;
    public SkinnedMeshRenderer skinMeshrenderer;
    [SerializeField]
    private string respawnPointId;
    [Header("Info")]
    private bool IsStun;

    [SerializeField]
    protected Vector3 Movement;
    private void Awake()
    {
        if (rb == null) rb.GetComponent<Rigidbody>();
        if (controlManager) SetController(controlManager);
        momvementController = GetComponent<PlayerMovement>();
        ShootManager = GetComponent<Projectile>();
    }



    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if (IsStun) return;
        MoveFunc();
    }

    public void SetRespawnPointId(string i_NewId)
    {
        if (!string.IsNullOrEmpty(i_NewId) && !i_NewId.Equals(respawnPointId))
            respawnPointId = i_NewId;
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
        i_controller.InteractEvent.AddListener(GetInteractInput);
    }

    public void SetCharacter(Material i_mat)
    {
        if (skinMeshrenderer)
        {
            skinMeshrenderer.material = i_mat;
        }
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
        if (i_jump) momvementController.CharacterJump();
    }

    private void GetInteractInput()
    {
        if (ShootManager)
            ShootManager.Shoot();
    }
    #endregion
    #region-Movement Methods
    /// <summary>
    /// Set player movement
    /// </summary>
    void MoveFunc()
    {
        //if (!rb) return;
        //this.rb.AddForce(GetMovement());
        momvementController.MoveCharacter(GetRelativeMovement());
    }
    #endregion
    #region-Calculate Methods


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
    #endregion

    public void StunFunc()
    {
        if (!IsStun)
            StartCoroutine(StunTimer());
    }

    IEnumerator StunTimer()
    {
        IsStun = true;
        momvementController.SetStun(IsStun);
        yield return new WaitForSecondsRealtime(1.2f);
        if (GameEventManager.instance) GameEventManager.instance.PlayerRespawn.Invoke(this.respawnPointId, this.gameObject);
        yield return new WaitForSecondsRealtime(0.3f);
        IsStun = false;
        momvementController.SetStun(IsStun);
    }
}
