using UnityEngine;
using FMODUnity;
using UnityEngine.AI;

public class HunterAiBrain : MonoBehaviour
{
    private NavMeshAgent HunterAgent;
    private Animator hunterAnimator;
    //other components 
    public Transform PlayerLocation;
    //float value for navmeshagent speed
    private float viewAlert = 0.0f;
    //scaler for detecting how long the player is in view
    private float daylightLevel = 0.03f;
    //Amount of daylight, tied to time of day
    private Rigidbody cc;
    float targetX = 10.0f; // Replace with your desired global X coordinate
    float targetZ = 5.0f; 
    public float visionRange = 10f;
    public LayerMask visionLayerMask;
    private void Start()
    {
        cc = GetComponent<Rigidbody>();
        HunterAgent = GetComponent<NavMeshAgent>();
        hunterAnimator = GetComponent<Animator>();
     //   viewAlert = Mathf.Clamp(0.1f, 0f, 5f);
    }

    private void Update()
    {
     
     
   
        if (IsPlayerInVision())
        {
            
        }
        Debug.Log(viewAlert);
    }
    private bool IsPlayerInVision()
    {
      // The maximum distance of the cone
      float coneAngle = 45f; // The angle of the cone in degrees
      float coneDistance = 10f; // The maximum distance of the cone
      viewAlert = Mathf.Clamp(viewAlert, 0f, 5f);
// Loop through angles to create rays in a cone
        for (float angle = -coneAngle / 2f; angle <= coneAngle / 2f; angle += 5f)
        {
            // Convert the angle to radians
            float radianAngle = Mathf.Deg2Rad * angle;

            // Calculate the direction vector based on the angle
            Vector3 coneDirection = new Vector3(Mathf.Sin(radianAngle), 0f, Mathf.Cos(radianAngle));

            // Cast a ray in the calculated direction
            Ray ray = new Ray(transform.position, transform.TransformDirection(coneDirection));
            RaycastHit hit;
            // Visualize the ray in the editor
            Debug.DrawRay(ray.origin, ray.direction * coneDistance, Color.green);
            // Check if the ray hits something
            if (Physics.Raycast(ray, out hit, coneDistance))
            {
                // Handle the hit, for example, check if the hit object is the player
                if (hit.collider.CompareTag("Player"))
                {
                    // Increase the visionAlert based on the hit distance or any other criteria
                    viewAlert += daylightLevel;
                    hunterAnimator.SetFloat("viewAlert", viewAlert);
                   
                    
                }
                else
                {
                     
                    viewAlert -= 0.01f;
                    hunterAnimator.SetFloat("viewAlert", viewAlert);
                }
            }
           
        }
           
        
         return false;
    }
        
        
    public void HunterIdle()
    {
       
        // Define the radius around the hunter
        float radius = 2.0f; // Adjust this value as needed

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
        
        float zSpeed = HunterAgent.speed;
        hunterAnimator.SetFloat("ZSpeed", zSpeed);
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
        
        //set stopping distance to 3
        
        HunterAgent.SetDestination(PlayerLocation.position);
        HunterAgent.speed = 1.5f;
        // Check if the agent has reached its destination or is very close
        if (!HunterAgent.pathPending && HunterAgent.remainingDistance <= HunterAgent.stoppingDistance)
        {
            // The agent has reached the destination or is very close
            // Perform any actions you want when the agent reaches the target
            Debug.Log("Hunter has reached the player!");
            
            //shoot player
        }
        float zSpeed = HunterAgent.speed;
        hunterAnimator.SetFloat("ZSpeed", zSpeed);
    }

}