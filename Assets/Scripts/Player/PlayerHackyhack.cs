using UnityEngine;

public class PlayerHackyhack : MonoBehaviour
{
    public float range = 4f;
    public GameObject hackPrompt;
    public GameObject hackGame;
    public LayerMask whatIsEnemy;

    float detectInterval = 0.1f;
    float time;

    Camera maincam;
    InputManager inputManager;

    private void Start()
    {
        maincam = Camera.main;
        inputManager = GetComponent<InputManager>();
        inputManager.playerControls.Default.Hack.performed += ctx => Hack();
        inputManager.playerControls.Menu.PauseUnpause.performed += ctx =>
        {
            if (hackGame.activeInHierarchy)
                hackGame.SetActive(false);
        };
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > detectInterval)
        {
            time = 0;
            Vector3 camfwd = MainCamFwd();
            Vector3 forwards = new Vector3(camfwd.x, 0, camfwd.z);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, forwards, out hit, range, whatIsEnemy))
            {
                hackPrompt.SetActive(true);
            }
            else hackPrompt.SetActive(false);
            
        }
    }

    Vector3 MainCamFwd()
    {
        return maincam.transform.forward;
    }

    void Hack()
    {
        if (hackPrompt.activeInHierarchy)
            hackGame.SetActive(true);
    }
}
