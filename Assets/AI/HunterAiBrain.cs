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
    private float zSpeed;


    public EventReference shootSound;
    public EventReference walkSound;
    public float shootDelay = 2f;
    float timeSinceLastShot = 2;


    private void Start()
    {
        cc = GetComponent<Rigidbody>();
        HunterAgent = GetComponent<NavMeshAgent>();
        hunterAnimator = GetComponent<Animator>();
     //   viewAlert = Mathf.Clamp(0.1f, 0f, 5f);
    }

    private void Update()
    {
        if (GameManager.GameOver || GameManager.GamePaused) return;

        float xSpeed = transform.InverseTransformVector(cc.velocity).x;
        float zSpeed = transform.InverseTransformVector(cc.velocity).z;
        hunterAnimator.SetFloat("ZSpeed", zSpeed);


        if (IsPlayerInVision())
        {

        }

        //Debug.Log(zSpeed);

        timeSinceLastShot += Time.deltaTime;
    }
    private bool IsPlayerInVision()
    {
    
      float coneAngle = 45f; 
      float coneDistance = 10f; 
      viewAlert = Mathf.Clamp(viewAlert, 0f, 5f);

        for (float angle = -coneAngle / 2f; angle <= coneAngle / 2f; angle += 5f)
        {
           
            float radianAngle = Mathf.Deg2Rad * angle;

          
            Vector3 coneDirection = new Vector3(Mathf.Sin(radianAngle), 0f, Mathf.Cos(radianAngle));

            Ray ray = new Ray(transform.position, transform.TransformDirection(coneDirection));
            RaycastHit hit;
         
            Debug.DrawRay(ray.origin, ray.direction * coneDistance, Color.green);
           
            if (Physics.Raycast(ray, out hit, coneDistance))
            {
     
                if (hit.collider.CompareTag("Player"))
                {
                   
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
        HunterAgent.stoppingDistance = 0;
       
        float radius = 2.0f; 
       
        
        hunterAnimator.SetBool("isShooting", false);

        Vector3 randomDestination;
        do
        {
            randomDestination = HunterAgent.transform.position +
                                new Vector3(
                                    Random.Range(-radius, radius),
                                    0.0f,  
                                    Random.Range(-radius, radius)
                                );
        } while (!IsDestinationWithinRadius(randomDestination, HunterAgent.transform.position, radius));

        HunterAgent.isStopped = false;
        HunterAgent.ResetPath();
        
        HunterAgent.SetDestination(randomDestination);

        
        HunterAgent.speed = 1.0f; 
      
    
    }
    private bool IsDestinationWithinRadius(Vector3 destination, Vector3 center, float radius)
    {
        return Vector3.Distance(destination, center) <= radius;
    }
    public void HunterSearching()
    {
    
    }

    public void HunterAggression()
    {
   
        HunterAgent.stoppingDistance = 3;
        HunterAgent.SetDestination(PlayerLocation.position);
        HunterAgent.speed = 1.5f;
        // Check if the agent has reached its destination or is very close
        if (!HunterAgent.pathPending && HunterAgent.remainingDistance <= HunterAgent.stoppingDistance)
        {
            if (timeSinceLastShot > shootDelay)
            {
                timeSinceLastShot = 0;

                // The agent has reached the destination or is very close
                // Perform any actions you want when the agent reaches the target
                //Debug.Log("Hunter has reached the player!");
                hunterAnimator.SetBool("isShooting", true);
                //shoot player

                if (!shootSound.IsNull)
                {
                    AudioManager.instance.PlayOneShot(shootSound, transform.position);
                }

                // damage the player
                GameManager.Instance.Player.TakeDamage();
            }
        }
        else
        {
            
        }
  
    }

}