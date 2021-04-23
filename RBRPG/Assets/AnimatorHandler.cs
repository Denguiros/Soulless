using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{
    public enum PlayerAnimations
    {
        Vertical,
        Horizontal
    }
    public class AnimatorHandler : MonoBehaviour
    {
        private Animator animator;
        private int vertical;
        private int horizontal;
        private bool canRotate = true;
        public bool CanRotate => canRotate;
        public void Initialize()
        {
            animator = GetComponent<Animator>();
            vertical = Animator.StringToHash(PlayerAnimations.Vertical.ToString());
            horizontal = Animator.StringToHash(PlayerAnimations.Horizontal.ToString());

        }
        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement)
        {
            #region Vertical
            float v = 0;
            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }
            else if (verticalMovement > 0.55f)
            {
                v = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if (verticalMovement < -0.5f)
            {
                v = -1;
            }
            else
            {
                v = 0;
            }
            #endregion

            #region Horizontal
            float h = 0;
            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if (horizontalMovement > 0.55f)
            {
                h = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontalMovement < -0.5f)
            {
                h = -1;
            }
            else
            {
                h = 0;
            }
            #endregion

            animator.SetFloat(PlayerAnimations.Vertical.ToString(), v, 0.1f, Time.deltaTime);
            animator.SetFloat(PlayerAnimations.Horizontal.ToString(), h, 0.1f, Time.deltaTime);
        }
        public void Rotate()
        {
            canRotate = true;
        }
        public void StopRotate()
        {
            canRotate = false;
        }
    }
}
