using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class MazeManager : MonoBehaviour
{
    public Transform startObject;
    public Transform endObject;
   // public Text timerText;
  //  public Text startMessageText;

    private bool gameStarted = false;
    private bool gameEnded = false;
    private float timer = 10f;
    private System.Action<bool> onGameEnd;


    void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //  startMessageText.text = "Press Enter to Start";
        // startMessageText.gameObject.SetActive(true);
        //  timerText.gameObject.SetActive(false);
    }

    public void OnEnterKeyPressed()
    {
        if (!gameStarted)
        {
            StartMaze();
        }
    }

    void Update()
    {          

        if (gameStarted && !gameEnded )
        {

            timer -= Time.deltaTime;
          //  timerText.text = "Time: " + timer.ToString("F1");

            if (timer <= 0)
            {
                EndGame(false);
            }
        }
    }

    void StartMaze()
    {
        gameStarted = true;
        gameEnded = false;
     //   startMessageText.gameObject.SetActive(false);
     //   timerText.gameObject.SetActive(true);
        timer = 10f;
        WarpCursorToStartObject();
       
    }
    public void Restart()
    {
        timer = 10f;
        gameEnded = false;
        gameStarted = false;
     //   startMessageText.gameObject.SetActive(true);
    //    timerText.gameObject.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
        Vector2 startObjectScreenPos = new Vector2(startObject.position.x, startObject.position.y);


        if (startObjectScreenPos.x >= 0 && startObjectScreenPos.x <= Screen.width
            && startObjectScreenPos.y >= 0 && startObjectScreenPos.y <= Screen.height)
        {
            Mouse.current.WarpCursorPosition(startObjectScreenPos);
        }
    }
    public void SetOnGameEndCallback(System.Action<bool> callback)
    {
        onGameEnd = callback;

    }
}
