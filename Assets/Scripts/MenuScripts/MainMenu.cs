using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button playBtn;
    [SerializeField] Button optionsBtn;
    [SerializeField] Button quitBtn;

    private void Awake()
    {
        if (playBtn) playBtn.onClick.AddListener(Play);
        else Debug.LogWarning("MainMenu - playBtn not assigned!");
        if (optionsBtn) optionsBtn.onClick.AddListener(Options);
        else Debug.LogWarning("MainMenu - optionsBtn not assigned!");
        if (quitBtn) quitBtn.onClick.AddListener(Quit);
        else Debug.LogWarning("MainMenu - quitBtn not assigned!");
    }

    // play btn
    void Play()
    {

    }

    // options btn
    void Options()
    {

    }

    // quit btn
    void Quit()
    {
        Application.Quit();
    }
}
