using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHackyhack : MonoBehaviour
{
    public float range = 4f;
    public Image hackPrompt;
    public GameObject hackGame;
    public LayerMask whatIsEnemy;

    float detectInterval = 0.1f;
    float time;

    Camera maincam;
    InputManager inputManager;

    private void Awake()
    {
        maincam = Camera.main;
        inputManager = GetComponent<InputManager>();
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
                hackGame.SetActive(true);
                GameManager.Instance.PauseGame();
                inputManager.PausePlayer();
            }
            
        }
    }

    Vector3 MainCamFwd()
    {
        return maincam.transform.forward;
    }
}
