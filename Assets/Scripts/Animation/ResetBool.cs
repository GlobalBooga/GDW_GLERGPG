using UnityEngine;

public class ResetBool : StateMachineBehaviour
{
    [SerializeField] private string isInteractingBool = "isInteracting";
    [SerializeField] private bool isInteractingStatus;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(isInteractingBool, isInteractingStatus);
    }
}