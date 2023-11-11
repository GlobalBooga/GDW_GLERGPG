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
    private float viewAlert = 0.0f;
    //scaler for detecting how long the player is in view
    
    float targetX = 10.0f; // Replace with your desired global X coordinate
    float targetZ = 5.0f; 
    public float visionRange = 10f;
    public LayerMask visionLayerMask;
    private void Start()
    {
        HunterAgent = GetComponent<NavMeshAgent>();
        hunterAnimator = GetComponent<Animator>();

    }

    private void Update()
    {
// Check if the player is within the AI's vision range
        if (IsPlayerInVision())
        {
           
        }

    }
    private bool IsPlayerInVision()
    {
        // Define the ray from the AI's position to the player's position
        Vector3 directionToPlayer = PlayerLocation.position - transform.position;

        // Use raycasting to check if there are obstacles between the AI and the player
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, visionRange, visionLayerMask))
        {
            // Check if the hit object is the player
            if (hit.collider.CompareTag("Player"))
            {
                return true; // Player is in vision
            }
        }

        return false; // Player is not in vision
    }
    public void HunterIdle()
    {
        // Define the radius around the hunter
        float radius = 5.0f; // Adjust this value as needed

        // Generate a random position within the defined radius
        Vector3 randomDestination;
        do
        {
            randomDestination = HunterAgent.transform.position +
                                new Vector3(
                                    Random.Range(-radius, radius),
                                    0.0f,  // Assuming your terrain is flat, so Y = 0
                                    Random.Range(-radius, radius)
                                );
        } while (!IsDestinationWithinRadius(randomDestination, HunterAgent.transform.position, radius));

        // Ensure the agent is not stopped and clear the current path
        HunterAgent.isStopped = false;
        HunterAgent.ResetPath();

        // Set the AI's destination to the random position
        HunterAgent.SetDestination(randomDestination);

        // Set the AI's speed to a lower value
        HunterAgent.speed = 1.0f; // You can adjust this value as needed
    }
    private bool IsDestinationWithinRadius(Vector3 destination, Vector3 center, float radius)
    {
        return Vector3.Distance(destination, center) <= radius;
    }
    public void HunterSearching()
    {
        //set position of noise, if noise is louder then set speed of navmesh higher, if its lower then yeah lol
        
        //
    }

    public void HunterAggression()
    {
        HunterAgent.SetDestination(PlayerLocation.position);
        // Check if the agent has reached its destination or is very close
        if (!HunterAgent.pathPending && HunterAgent.remainingDistance <= HunterAgent.stoppingDistance)
        {
            // The agent has reached the destination or is very close
            // Perform any actions you want when the agent reaches the target
            Debug.Log("Hunter has reached the player!");
        }
    }

}