using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public bool isInteracting;

    private Animator animator;
    private InputManager inputManager;
    private PlayerLocomotion playerLocomotion;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        isInteracting = animator.GetBool("isInteracting");
        playerLocomotion.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", playerLocomotion.isGrounded);
    }
}