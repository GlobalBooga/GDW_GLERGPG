using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResumeBtn : EmissiveButtons
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.UnPauseGame();
        GameManager.Instance.Player.GetInputManager().UnPausePlayer();
        base.OnPointerClick(eventData);
    }
}
