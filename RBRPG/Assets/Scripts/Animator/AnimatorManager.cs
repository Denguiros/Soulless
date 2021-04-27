using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{
    public enum PlayerAnimatorParameters
    {
        Vertical,
        Horizontal,
        IsInteracting,
        IsJumping,
        IsGrounded,
        CanDoCombo
    }
    public enum PlayerActionAnimations
    {
        RollForward,
        RollBackward,
        Land,
        Locomotion,
        Fall,
        Jump,
        TakeDamage,
        Death,
        Empty
    }
    public enum PlayerOneHandedAttackAnimations
    {
        OneHandedLightAttack1,
        OneHandedLightAttack2,
        OneHandedLightAttack3,
        OneHandedLightAttack4,
        OneHandedLightAttack5,
        OneHandedHeavyAttack1,
        OneHandedHeavyAttack2,
        OneHandedHeavyAttack3,
    }
    public enum EnemyActionAnimations
    {
        TakeDamage,
        Death
    }
    public class AnimatorManager : MonoBehaviour
    {
        private bool canRotate = true;

        PlayerLocomotion playerLocomotion;
        PlayerManager playerManager;
        public Animator animator { get; set; }
        public bool CanRotate => canRotate;
        public void Initialize()
        {
            animator = GetComponent<Animator>();
            playerLocomotion = GetComponentInParent<PlayerLocomotion>();
            playerManager = GetComponentInParent<PlayerManager>();
        }
        public void PlayTargetAnimation(string animationName, bool isInteracting,bool useRootMotion)
        {
            animator.applyRootMotion = useRootMotion;
            animator.SetBool(PlayerAnimatorParameters.IsInteracting.ToString(), isInteracting);
            animator.CrossFade(animationName, 0.2f);
        }

        public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting)
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
            if (isSprinting)
            {
                v = 2f;
                h = horizontalMovement;
            }
            animator.SetFloat(PlayerAnimatorParameters.Vertical.ToString(), v, 0.1f, Time.deltaTime);
            animator.SetFloat(PlayerAnimatorParameters.Horizontal.ToString(), h, 0.1f, Time.deltaTime);
        }
        public void Rotate()
        {
            canRotate = true;
        }
        public void StopRotate()
        {
            canRotate = false;
        }

        private void OnAnimatorMove()
        {
            if (!playerManager.isInteracting)
                return;
            if (!animator.applyRootMotion)
                return;
            float delta = Time.deltaTime;
            playerLocomotion.playerRigidbody.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            Vector3 velocity = deltaPosition / delta;
            playerLocomotion.playerRigidbody.velocity = velocity;
        }
        public void EnableCombo()
        {
            animator.SetBool(PlayerAnimatorParameters.CanDoCombo.ToString(), true);
        }
        public void DisableCombo()
        {
            animator.SetBool(PlayerAnimatorParameters.CanDoCombo.ToString(), false);

        }
    }
}
