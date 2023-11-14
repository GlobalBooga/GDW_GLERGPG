using FMOD;
using UnityEngine;
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