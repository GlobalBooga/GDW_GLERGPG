using FMOD.Studio;
using FMODUnity;
using System;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    [HideInInspector] public bool isSprinting;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool isGrounded = true;
    
    [Header("Ground values")]
    [SerializeField] private float walkingSpeed = 3f;
    [SerializeField] private float runningSpeed = 7f;
    [SerializeField] private float sprintingSpeed = 12f;
    [SerializeField] private float rotationSpeed = 15f;

    [Header("Aerial values")]
    [SerializeField] private float leapingVelocity;
    [SerializeField] private float fallingVelocity;
    [SerializeField] private float jumpHeight = 5f;

    [Header("Others")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField, Range(0f, 1.5f)] private float raycastHeightOffset = 0.5f;
    [SerializeField] private float gravityIntensity = -9.8f;
    
    private Vector3 moveDirection;
    private float inAirTimer;
    
    private InputManager inputManager;
    private PlayerManager playerManager;
    private AnimatorManager animatorManager;
    private Rigidbody rb;
    private Transform cam;

    public EventReference footstepSound;
    EventInstance footstepEvt;

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
        
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;

        footstepEvt = AudioManager.instance.CreateInstance(footstepSound);
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();
        
        if (playerManager.isInteracting || isJumping) return;
        
        HandleMovement();
        HandleRotation();
        HandleFootsteps();
    }

    private void HandleMovement()
    {
        moveDirection = cam.forward * inputManager.verticalInput + cam.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (isSprinting) moveDirection *= sprintingSpeed;
        else
        {
            if (inputManager.moveAmount >= 0.5f) moveDirection *= runningSpeed;
            else moveDirection *= walkingSpeed;
        }

        Vector3 movementVelocity = moveDirection;
        rb.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = cam.forward * inputManager.verticalInput + cam.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero) targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        Vector3 targetPos = transform.position;
        raycastOrigin.y += raycastHeightOffset;
        
        if (!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
               // animatorManager.PlayTargetAnimation("Fall", true);

            inAirTimer += Time.deltaTime;
            rb.AddForce(transform.forward * leapingVelocity);
            rb.AddForce(-Vector3.up * (fallingVelocity * inAirTimer));
        }

        if (Physics.SphereCast(raycastOrigin, 0.2f, -Vector3.up, out hit, 1f, groundLayer))
        {
            if (!isGrounded && playerManager.isInteracting) animatorManager.PlayTargetAnimation("Land", true);

            Vector3 raycastHitPoint = hit.point;
            targetPos.y = raycastHitPoint.y;
            inAirTimer = 0f;
            isGrounded = true;
            playerManager.isInteracting = false;
        }
        else
            isGrounded = false;

        if (isGrounded && !isJumping)
        {
            if (playerManager.isInteracting || inputManager.moveAmount > 0)
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime / 0.1f);
            else
                transform.position = targetPos;
        }
    }

    public void HandleJumping()
    {
        if (isGrounded)
        {
          //  animatorManager.animator.SetBool("isJumping", true);
          animatorManager.animator.SetTrigger("jump");
          //  animatorManager.PlayTargetAnimation("Jump", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;

            rb.velocity = playerVelocity;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }

    void HandleFootsteps()
    {
        if (isSprinting) footstepEvt.setPitch(0.8f);
        else footstepEvt.setPitch(2);

        if (moveDirection != Vector3.zero && isGrounded)
        {
            PLAYBACK_STATE ps;
            footstepEvt.getPlaybackState(out ps);
            if (ps.Equals(PLAYBACK_STATE.STOPPED))
            {
                footstepEvt.start();
            }
        }
        else
        {
            footstepEvt.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void OnDestroy()
    {
        footstepEvt.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        footstepEvt.release();
    }
}