using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener((value) => { SaveValue(value); });
        slider.value = PlayerPrefs.GetFloat("Volume", 1);
    }

    private void Start()
    {
        AudioManager.instance.masterVolume = slider.value;
    }

    public void SaveValue(float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
    }
}
