using Cinemachine;
using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;
    [HideInInspector] public float moveAmount;
    [HideInInspector] public bool sprintInput;
    [HideInInspector] public bool jumpInput;
    [HideInInspector] public bool pause;
    
    private Vector2 movementInput;
    
    public PlayerControls playerControls;
    private PlayerLocomotion playerLocomotion;
    private AnimatorManager animatorManager;
    public CinemachineFreeLook freelook;

    private void Start()
    {
        playerLocomotion = GetComponent<PlayerLocomotion>();
        animatorManager = GetComponent<AnimatorManager>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.Default.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            
            playerControls.Default.Sprint.performed += i => sprintInput = true;
            playerControls.Default.Sprint.canceled += i => sprintInput = false;

            playerControls.Default.Jump.performed += i => jumpInput = true;

            playerControls.Menu.PauseUnpause.performed += i => pause = !pause;
        }
        
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpingInput();
        HandlePauseInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
    }

    private void HandleSprintingInput()
    {
        if (sprintInput && moveAmount > 0.5f) playerLocomotion.isSprinting = true;
        else playerLocomotion.isSprinting = false;
    }

    private void HandleJumpingInput()
    {
        if (jumpInput)
        {
            jumpInput = false;
            playerLocomotion.HandleJumping();
        }
    }

    private void HandlePauseInput()
    {
        if (!GameManager.Instance) return;
     
        if (pause)
        {
            GameManager.Instance.PauseGame();
            PausePlayer();
        }
        else
        {
            GameManager.Instance.UnPauseGame();
            UnPausePlayer();
        }
    }



    public void PausePlayer()
    {
        playerControls.Default.Disable();
        freelook.enabled = false;
    }

    public void UnPausePlayer()
    {
        freelook.enabled = true;
        playerControls.Default.Enable();
    }
}