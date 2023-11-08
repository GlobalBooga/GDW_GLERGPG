using System;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    private int horizontalCache;
    private int verticalCache;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        horizontalCache = Animator.StringToHash("horizontal");
        verticalCache = Animator.StringToHash("vertical");
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnim, 0.2f);
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting)
    {
        var snappedHorizontal = SnapAnimation(horizontalMovement);
        var snappedVertical = SnapAnimation(verticalMovement);

        if (isSprinting)
        {
            snappedHorizontal = horizontalMovement;
            snappedVertical = 2f;
        }
        
        animator.SetFloat(horizontalCache, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(verticalCache, snappedVertical, 0.1f, Time.deltaTime);
    }

    private float SnapAnimation(float movement)
    {
        if (movement is > 0 and < 0.55f) movement = 0.5f;
        else if (movement > 0.55f) movement = 1f;
        else if (movement is < 0 and > -0.55f) movement = -0.5f;
        else if (movement < -0.55f) movement = -1f;
        else movement = 0;

        return movement;
    }
}