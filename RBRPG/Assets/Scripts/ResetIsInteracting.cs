using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{

    public class ResetIsInteracting : StateMachineBehaviour
    {

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(PlayerAnimatorParameters.isInteracting.ToString(), false);
        }

    }
}
