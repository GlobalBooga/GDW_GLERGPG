using UnityEngine;
using FMODUnity;
using UnityEngine.AI;

public class HunterAiBrain : MonoBehaviour
{
    private NavMeshAgent HunterAgent;
    private Animator hunterAnimator;
    //other components yeah boi
    public Transform PlayerLocation;
    //float value for navmeshagent speed
    private void Start()
    {
        HunterAgent = GetComponent<NavMeshAgent>();
        hunterAnimator = GetComponent<Animator>();

    }

    private void Update()
    {


    }

    public void HunterIdle()
    {
        // Define a wandering area, e.g., minX, maxX, minZ, maxZ
        float minX = -10.0f;
        float maxX = 10.0f;
        float minZ = -10.0f;
        float maxZ = 10.0f;

        // Generate a random position within the defined area
        Vector3 randomDestination = new Vector3(
            Random.Range(minX, maxX),
            0.0f,  // Assuming your terrain is flat, so Y = 0
            Random.Range(minZ, maxZ)
        );

        // Set the AI's destination to the random position
        HunterAgent.SetDestination(randomDestination);

        // Set the AI's speed to a lower value
        HunterAgent.speed = 0.5f; // You can adjust this value as needed
   
    }

    public void HunterSearching()
    {
        //set position of noise, if noise is louder then set speed of navmesh higher, if its lower then yeah lol
        
        //
    }

    public void HunterAggression()
    {
        HunterAgent.SetDestination(PlayerLocation.position);
    }

}