using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReturnBtn : EmissiveButtons
{
    [SerializeField] SettingsBackground settingsPanel;
    bool isReturning;

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (isReturning) return;
        base.OnPointerClick(eventData);
        settingsPanel.DisableSelf();
        isReturning = true;
    }

    private void OnEnable()
    {
        isReturning = false;
    }
}
