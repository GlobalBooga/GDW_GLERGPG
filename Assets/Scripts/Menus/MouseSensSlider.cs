using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MouseSensSlider : MonoBehaviour
{
    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener((value) => { SaveValue(value); });
        slider.value = PlayerPrefs.GetFloat("Sensitivity", 1);
    }


    public void SaveValue(float value)
    {
        PlayerPrefs.SetFloat("Sensitivity", value);
    }
}
