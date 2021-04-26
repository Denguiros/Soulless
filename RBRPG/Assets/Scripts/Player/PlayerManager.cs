using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{
    public class PlayerManager : MonoBehaviour
    {
        private AnimatorManager animatorManager;
        // Player Flags !
        [field: SerializeField]
        public bool isInAir { get; set; }
        [field: SerializeField]
        public bool isGrounded { get; set; }
        [field: SerializeField]
        public bool isInteracting { get; set; }
        [field: SerializeField]
        public bool isSprinting { get; set; }
        [field: SerializeField]
        public bool isRolling { get; set; }       
        [field: SerializeField]
        public bool isJumping { get; set; }



        private InputManager inputManager;
        private Animator animator;
        private PlayerLocomotion playerLocomotion;
        void Start()
        {
            animatorManager = GetComponentInChildren<AnimatorManager>();
            inputManager = GetComponent<InputManager>();
            animator = GetComponentInChildren<Animator>();
            animatorManager.Initialize();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }
        void Update()
        {
            inputManager.HandleAllInput();
           
           // animator.SetBool(PlayerAnimatorParameters.IsInAir.ToString(), isInAir);
        }
        private void FixedUpdate()
        {
            playerLocomotion.HandleAllMovement();
        }
        // Update is called once per frame


        void LateUpdate()
        {
            isInteracting = animator.GetBool(PlayerAnimatorParameters.IsInteracting.ToString());
            isJumping = animator.GetBool(PlayerAnimatorParameters.IsJumping.ToString());
            animator.SetBool(PlayerAnimatorParameters.IsGrounded.ToString(), isGrounded);
        }
    }
}
