using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerControl
{

    public class PlayerLocomotion : MonoBehaviour
    {
        private InputManager inputManager;
        private Transform cameraObject;
        private Vector3 moveDirection;
        private AnimatorManager animatorManager;
        private PlayerManager playerManager;

        [HideInInspector]
        public Rigidbody playerRigidbody;

        [Header("Ground & Air Detection Stats")]
        [SerializeField]
        public float rayCastHeightOffset = 0.5f;
        [SerializeField]
        public float sphereRadius = 0.5f;
        [SerializeField]
        private float minimumDistanceNeededToBeginFall = 1f;
        [SerializeField]
        private float groundDirectionRayDistance = 0.2f;


        [Header("Falling")]
        [SerializeField]
        private float fallingSpeed = 45f;
        [field: SerializeField]
        public float inAirTimer { get; set; }
        [SerializeField]
        private float leapingVelocity = 2f;
        [SerializeField]
        private LayerMask groundLayer;

        [Header("Movement Stats")]
        [SerializeField]
        private float runningSpeed = 5f;
        [SerializeField]
        private float walkingSpeed = 1f;
        [SerializeField]
        private float sprintingSpeed = 7f;
        [SerializeField]
        private float rotationSpeed = 10f;


        [Header("Jumping")]
        [SerializeField]
        private float jumpHeight = 10f;
        [SerializeField]
        private float gravityIntensity = -15f;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerRigidbody = GetComponent<Rigidbody>();
            playerManager = GetComponent<PlayerManager>();
            cameraObject = Camera.main.transform;
            animatorManager = GetComponentInChildren<AnimatorManager>();
        }
        public void HandleAllMovement()
        {
            HandleFallingAndLanding();
            if (playerManager.isInteracting)
                return;
            if (playerManager.isInAir)
                return;
            if (playerManager.isJumping)
                return;
            HandleMovement();
            HandleRotation();
            HandleRolling();
            HandleJumping();
        }

        private void HandleRotation()
        {
            Vector3 targetDirection = Vector3.zero;
            targetDirection = cameraObject.forward * inputManager.verticalInput + cameraObject.right * inputManager.horizontalInput;
            targetDirection.Normalize();
            targetDirection.y = 0;
            if (targetDirection == Vector3.zero)
                targetDirection = transform.forward;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = playerRotation;
        }
        private void HandleMovement()
        {
            moveDirection = cameraObject.forward * inputManager.verticalInput + cameraObject.right * inputManager.horizontalInput;
            moveDirection.Normalize();
            moveDirection.y = 0;

            float speed = runningSpeed;
            if (playerManager.isSprinting && inputManager.moveAmount >= 0.5f)
            {
                speed = sprintingSpeed;
            }
            else
            {
                if (inputManager.moveAmount >= 0.5f)
                {
                    speed = runningSpeed;
                }
                else
                {
                    speed = walkingSpeed;
                }
            }
            moveDirection *= speed;


            Vector3 movementVelocity = moveDirection;
            playerRigidbody.velocity = movementVelocity;
        }

        private void HandleFallingAndLanding()
        {
            Vector3 rayCastOrigin = transform.position;
            rayCastOrigin.y += rayCastHeightOffset;
            if (!playerManager.isGrounded && !playerManager.isJumping)
            {
                if (!playerManager.isInteracting)
                {
                    animatorManager.PlayTargetAnimation(PlayerActionAnimations.Fall.ToString(), true, false);
                }
                inAirTimer += Time.deltaTime;
                playerRigidbody.AddForce(transform.forward * leapingVelocity);
                playerRigidbody.AddForce(-Vector3.up * fallingSpeed * inAirTimer);
            }

            if (Physics.CheckSphere(rayCastOrigin, sphereRadius, groundLayer))
            {
                if (!playerManager.isGrounded)
                {
                    animatorManager.PlayTargetAnimation(PlayerActionAnimations.Land.ToString(), true, true);
                }
                inAirTimer = 0;
                playerManager.isGrounded = true;
                playerManager.isInAir = false;
            }
            else
            {
                playerManager.isGrounded = false;
                playerManager.isInAir = true;
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position + new Vector3(0, rayCastHeightOffset, 0), sphereRadius);
        }
        public void HandleJumping()
        {

            if (playerManager.isInteracting)
                return;
            if (inputManager.jumpInput && playerManager.isGrounded)
            {
                animatorManager.animator.SetBool(PlayerAnimatorParameters.IsJumping.ToString(), true);
                animatorManager.PlayTargetAnimation(PlayerActionAnimations.Jump.ToString(), true, false);
                Vector3 playerVelocity = moveDirection;
                playerVelocity.y = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
                playerRigidbody.velocity = playerVelocity;
                
            }
        }
        public void HandleRolling()
        {

            if (playerManager.isInteracting)
                return;
            if (playerManager.isRolling)
            {

                if (inputManager.moveAmount > 0)
                {
                    animatorManager.PlayTargetAnimation(PlayerActionAnimations.RollForward.ToString(), true, true);
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    transform.rotation = rollRotation;
                }
                else
                {
                    animatorManager.PlayTargetAnimation(PlayerActionAnimations.RollBackward.ToString(), true, true);
                }
                playerManager.isRolling = false;
            }
        }

    }

}