using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{

    public class ResetAnimatorBool : StateMachineBehaviour
    {
        [SerializeField]
        private PlayerAnimatorParameters parameter;
        [SerializeField]
        private bool status;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(parameter.ToString(), status);
        }

    }
}
