using UnityEngine;

public class onFinish : StateMachineBehaviour
{
    [SerializeField] private string animation;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<PlayerMovement>().ChangeAnimation(animation, 0.2f, stateInfo.length);
    }

    
}
