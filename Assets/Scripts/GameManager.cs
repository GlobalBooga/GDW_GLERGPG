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

    public PlayerManager Player;

    private float time;
    private Quaternion start;
    private Quaternion end;

    public static bool GameOver { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }

        start = Quaternion.Euler(sunStartRot, -30, 0);
        end = Quaternion.Euler(sunEndRot, -30, 0);
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
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void UnPauseGame()
    {
        if (!pauseMenu) return;

        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PlayerDied()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameOver = true;
    }

    public void ShowRestartMenu()
    {

    }
}
