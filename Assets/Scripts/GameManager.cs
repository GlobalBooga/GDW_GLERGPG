using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Sun Settings")]
    public Light sun;
    public int secondsTillNoon = 900;
    public float sunStartRot = -30f;
    public float sunEndRot = 90f;
    public AnimationCurve sunRiseCurve = AnimationCurve.EaseInOut(0,0,1,1);

    [Header("Canvas Settings")]
    public GameObject pauseMenu;




    private float time;
    private Quaternion start;
    private Quaternion end;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        start = Quaternion.Euler(sunStartRot, transform.rotation.eulerAngles.y, 0);
        end = Quaternion.Euler(sunEndRot, transform.rotation.eulerAngles.y, 0);
    }


    void Update()
    {
        if (!sun) return;

        sun.transform.rotation = Quaternion.Lerp(start, end, sunRiseCurve.Evaluate(time / secondsTillNoon));

        time += Time.deltaTime;
    }

    public void PauseGame()
    {
        if (!pauseMenu) return;
        pauseMenu.SetActive(true);
    }

    public void UnPauseGame()
    {
        if (!pauseMenu) return;
        pauseMenu.SetActive(false);
    }
}
