using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{
    public class PlayerManager : MonoBehaviour
    {
        private AnimatorManager animatorManager;
        private CameraManager cameraManager;
        // Player Flags !
        [field: SerializeField]
        public bool isGrounded { get; set; }  
        [field: SerializeField]
        public bool isInAir { get; set; }
        [field: SerializeField]
        public bool isInteracting { get; set; }
        [field: SerializeField]
        public bool isSprinting { get; set; }
        [field: SerializeField]
        public bool isRolling { get; set; }
        [field: SerializeField]
        public bool isJumping { get; set; }
        [field: SerializeField]
        public bool isDead { get; set; }


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
            cameraManager = FindObjectOfType<CameraManager>();
            isDead = false;
        }
        void Update()
        {
            if (!isDead)
                inputManager.HandleAllInput();

        }
        private void FixedUpdate()
        {
            if (!isDead)
                playerLocomotion.HandleAllMovement();
        }

        void LateUpdate()
        {
            cameraManager.HandleAllCameraMovement();
            if (!isDead)
            {
                isInteracting = animator.GetBool(PlayerAnimatorParameters.IsInteracting.ToString());
                isJumping = animator.GetBool(PlayerAnimatorParameters.IsJumping.ToString());
                animator.SetBool(PlayerAnimatorParameters.IsGrounded.ToString(), isGrounded);
            }
        }
    }
}
