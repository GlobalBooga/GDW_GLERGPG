using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private Bus uiBus;
    private Bus ambienceBus;
    private Bus playerBus;
    private Bus masterBus;

    public float uiVolume = 0.5f;
    public float playerVolume = 1;
    public float ambienceVolume = 0.75f;
    public float masterVolume = 1;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }
        masterBus = RuntimeManager.GetBus("bus:/");
        uiBus = RuntimeManager.GetBus("bus:/Sound/Menu");
        ambienceBus = RuntimeManager.GetBus("bus:/Sound/Ambience");
        playerBus = RuntimeManager.GetBus("bus:/Sound/Player");

        masterVolume = PlayerPrefs.GetFloat("Volume", masterVolume);
    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        uiBus.setVolume(uiVolume);
        ambienceBus.setVolume(ambienceVolume);
        playerBus.setVolume(playerVolume);
    }

    public void PlayOneShot(EventReference sound, Vector3 pos)
    {
        RuntimeManager.PlayOneShot(sound, pos);
    }
}
