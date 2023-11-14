using Cinemachine;
using FMODUnity;
using System.Collections;
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
    bool isDead;

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
        if (!isDead) inputManager.HandleAllInputs();
        playerLocomotion.HandleFootsteps();

        if (GameManager.GamePaused || GameManager.GameOver) return;



        if (time > regenHitTime && regen) 
        { 
            hits = Mathf.Clamp(hits - 1, 0, maxHits);
            deathVignette.color = new Color(1, 1, 1, vignetteStages[hits]);
            deathVignetteDirt.color = new Color(1, 1, 1, dirtStages[hits]);

            if (hits == 0) regen = false;
        }
        time += Time.deltaTime;
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
        if (!isDead) regen = true;
        time = 0;
        if (!hurtSound.IsNull) AudioManager.instance.PlayOneShot(hurtSound, transform.position);
        hits = Mathf.Clamp(hits + 1, 0, maxHits);
        deathVignette.color = new Color(1, 1, 1, vignetteStages[hits]);
        deathVignetteDirt.color = new Color(1, 1, 1, dirtStages[hits]);

        if (hits >= maxHits && !isDead)
        {
            isDead = true;
            regen = false;
            StartCoroutine(DeadRoutine());
            return;
        }
    }

    IEnumerator DeadRoutine()
    {
        GameManager.Instance.PlayerDied();

        yield return new WaitForSecondsRealtime(0.6f);

        deadmsg.color = Color.white;

        yield return new WaitForSecondsRealtime(2f);

        while (deadmsg.color.a > 0)
        {
            deadmsg.color -= new Color(0,0,0,Time.unscaledDeltaTime);
            yield return null;
        }

        GameManager.Instance.ShowRestartMenu();
    }
}