using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class MazeManager : MonoBehaviour
{
    public Transform startObject;
    public Transform endObject;
    public GameObject pressStartImages;
    public Image[] timerImages;

    private int currentImageIndex = 0;
    private bool gameStarted = false;
    private bool gameEnded = false;
    private float timer = 10f;
    private System.Action<bool> onGameEnd;


    void OnEnable()
    {
        GameManager.Instance.Player.GetInputManager().playerControls.Menu.StartMinigame.performed += ctx => OnEnterKeyPressed();
        GameManager.Instance.Player.GetInputManager().PausePlayer();
        GameManager.Instance.PauseGame();
    }

    public void OnEnterKeyPressed()
    {
        if (!gameStarted)
        {
            StartMaze();
        }
    }
    
    IEnumerator TimerUpdates()
    {
        for(int i = 0; i < 10; i++) 
        {
            UpdateImage();
            yield return new WaitForSecondsRealtime(1);
        }

    }
    void UpdateImage()
    {
        currentImageIndex = (currentImageIndex + 1) % timerImages.Length;
    }
    void Update()
    {          

        if (gameStarted && !gameEnded )
        {

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                EndGame(false);
            }
        }
    }

    void StartMaze()
    {
        StartCoroutine(TimerUpdates());        
        gameStarted = true;
        gameEnded = false;
        pressStartImages.gameObject.SetActive(false);
        timer = 10f;
        WarpCursorToStartObject();
       
    }
    public void Restart()
    {
        timer = 10f;
        gameEnded = false;
        gameStarted = false;
        pressStartImages.gameObject.SetActive(true);

    }
    public void OnEndObjectEnter()
    {
        if (gameEnded == false)
        {
            EndGame(true);
        }
    }
    public void OnWallEnter()
    {
        if (gameStarted == true)
        {            
            WarpCursorToStartObject();
        }
    }

    void EndGame(bool playerWins)
    {
        gameEnded = true;
        StopCoroutine(TimerUpdates());
        if (playerWins)
        {
            Debug.Log("You won!");
            onGameEnd?.Invoke(true);        
        }
        else
        {
            Debug.Log("Game over");
            onGameEnd?.Invoke(false);      
        }
    }
    void WarpCursorToStartObject()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(startObject.position);

        if (screenPos.x >= 0 && screenPos.x <= Screen.width && screenPos.y >= 0 && screenPos.y <= Screen.height)
        {
            Mouse.current.WarpCursorPosition(new Vector2(screenPos.x, screenPos.y));
        }
    }
    public void SetOnGameEndCallback(System.Action<bool> callback)
    {
        onGameEnd = callback;
    }
}
