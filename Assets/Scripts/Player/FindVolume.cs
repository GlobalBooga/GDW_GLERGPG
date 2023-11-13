using FMOD;
<<<<<<< Updated upstream
using FMOD.Studio;
using FMODUnity;
=======
>>>>>>> Stashed changes
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
=======

public class FindVolume : MonoBehaviour
{
    [HideInInspector] public float playerVolume;
    public Channel playerChannel;

    // Start is called before the first frame update
    void Start()
    {
        playerChannel = GetComponent<Channel>();
    }

    // Update is called once per frame
    void Update()
    {
        playerChannel.getAudibility(out playerVolume);
    }
}

>>>>>>> Stashed changes

public class FindVolume : MonoBehaviour
{
    private float volume;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //RESULT "EventInstance".getVolume(out float volume);
    }
}
