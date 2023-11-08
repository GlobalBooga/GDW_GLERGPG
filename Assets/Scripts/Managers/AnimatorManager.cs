using System;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    private Animator animator;
    private int horizontalCache;
    private int verticalCache;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        horizontalCache = Animator.StringToHash("horizontal");
        verticalCache = Animator.StringToHash("vertical");
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        var snappedHorizontal = SnapAnimation(horizontalMovement);
        var snappedVertical = SnapAnimation(verticalMovement);
        
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