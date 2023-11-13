using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private Bus uiBus;
    private Bus ambianceBus;
    private Bus playerBus;
    private Bus masterBus;

    public float uiVolume = 0.5f;
    public float playerVolume = 1;
    public float ambianceVolume = 0.75f;
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
        uiBus = RuntimeManager.GetBus("bus:/Menu");
        ambianceBus = RuntimeManager.GetBus("bus:/Ambiance");
        playerBus = RuntimeManager.GetBus("bus:/Player");
    }

    private void Update()
    {
        uiBus.setVolume(uiVolume);
        masterBus.setVolume(masterVolume);
        ambianceBus.setVolume(ambianceVolume);
        playerBus.setVolume(playerVolume);
    }

    public void PlayOneShot(EventReference sound, Vector3 pos)
    {
        RuntimeManager.PlayOneShot(sound, pos);
    }
}
