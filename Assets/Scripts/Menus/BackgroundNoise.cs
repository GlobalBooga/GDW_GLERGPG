using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundNoise : MonoBehaviour
{
    public EventReference sound;
    private EventInstance evtInst;

    // Start is called before the first frame update
    void Start()
    {
        evtInst = AudioManager.instance.CreateInstance(sound);
        evtInst.start();
    }

    private void OnDestroy()
    {
        evtInst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        evtInst.release();
    }

}
