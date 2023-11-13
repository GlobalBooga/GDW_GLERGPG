using FMODUnity;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public bool isInteracting;

    private Animator animator;
    private InputManager inputManager;
    private PlayerLocomotion playerLocomotion;
    public EventReference hurtSound;
    public Image deathVignette;
    public Image deathVignetteDirt;
    public Image deadmsg;
    float restartScreenDelay = 5;
    float deadmsgtime;
    bool isDead;
    //float fadeTime;

    float[] vignetteStages = new float[] { 0, 0.75f, 1f, 1f };
    float[] dirtStages = new float[] { 0, 0.2f, 0.5f, 1f };


    int hits;
    int maxHits = 3;
    public float regenHitTime = 5f;
    float time;
    bool regen;


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


        if (time > regenHitTime && regen) 
        { 
            hits = Mathf.Clamp(hits - 1, 0, maxHits);
            deathVignette.color = new Color(1, 1, 1, vignetteStages[hits]);
            deathVignetteDirt.color = new Color(1, 1, 1, dirtStages[hits]);
            Debug.Log("regen hit");

            if (hits == 0) regen = false;
        }
        time += Time.deltaTime;

        if (isDead)
        {
            deadmsgtime += Time.deltaTime;

            if(deadmsgtime > restartScreenDelay)
            {
                deadmsg.color = new Color(1, 1, 1, Mathf.Lerp(deadmsg.color.a,0, Time.unscaledDeltaTime * 0.5f));
            }
        }


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

    public InputManager GetInputManager() { return inputManager; }

    public void TakeDamage()
    {
        regen = true;
        time = 0;
        if (!hurtSound.IsNull) AudioManager.instance.PlayOneShot(hurtSound, transform.position);
        hits++;
        deathVignette.color = new Color(1, 1, 1, vignetteStages[hits]);
        deathVignetteDirt.color = new Color(1, 1, 1, dirtStages[hits]);

        if (hits >= maxHits)
        {
            // dead
            deadmsg.color = Color.white;
            GameManager.Instance.PlayerDied();
            return;
        }

        // otherwise just get hurt

    }
}