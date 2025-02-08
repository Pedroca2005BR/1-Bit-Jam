using UnityEngine;

public class onFinishEny2 : StateMachineBehaviour
{
    [SerializeField] private string animation;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<FollowPlayerComponent>().ChangeAnimation(animation, 0.2f, stateInfo.length);
        animator.GetComponent<ShootProjectileComponent>().ChangeAnimation(animation, 0.2f, stateInfo.length);
    }

}

