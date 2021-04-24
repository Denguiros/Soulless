using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{

    public class PlayerManager : MonoBehaviour
    {
        // Player Flags !
        public  bool isInAir { get; set; }
        public bool isGrounded { get; set; }
        public bool IsInteracting { get; set; }
        public bool IsSprinting { get; set; }


        private InputHandler inputHandler;
        private Animator animator;
        private PlayerLocomotion playerLocomotion;
        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            animator = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        // Update is called once per frame
        void Update()
        {
            float delta = Time.deltaTime;
            IsInteracting = animator.GetBool(PlayerAnimatorParameters.isInteracting.ToString());

            inputHandler.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollingAndSprinting(delta);
        }
        private void LateUpdate()
        {
            inputHandler.RollFlag = false;
            inputHandler.SprintFlag = false;
            IsSprinting = inputHandler.SprintAndRollButtonInput;
            if(isInAir)
            {
                playerLocomotion.InAirTimer += Time.deltaTime;
            }
        }
    }
}
