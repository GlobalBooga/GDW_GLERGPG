using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayBtn : EmissiveButtons
{
    public int levelIndex;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        // goto level
        SceneManager.LoadScene(levelIndex);
    }
}
