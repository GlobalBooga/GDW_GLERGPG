using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsBtn : EmissiveButtons
{
    [SerializeField] GameObject settingsPanel;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (settingsPanel) settingsPanel.SetActive(true);
    }
}
