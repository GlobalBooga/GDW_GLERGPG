using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReturnBtn : EmissiveButtons
{
    [SerializeField] SettingsBackground settingsPanel;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        // return

        settingsPanel.DisableSelf();
    }
}
