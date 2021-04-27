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
        private float groundDetectionRayStartPoint = 0.5f;
        [SerializeField]
        private float minimumDistanceNeededToBeginFall = 1f;
        [SerializeField]
        private float groundDirectionRayDistance = 0.2f;



        [Header("Handling Steps & Slopes")]
        [SerializeField]
        private Transform stepRayUpper;
        [SerializeField]
        private Transform stepRayLower;
        [SerializeField]
        private float stepHeight = 0.3f;
        [SerializeField]
        private float stepSmooth = 0.1f;
        [SerializeField]
        private float downStairsSmooth = 0.01f;
        [SerializeField]
        private float lowerStepRayRange = 0.1f;
        [SerializeField]
        private float upperStepRayRange = 0.15f;


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
        private int fall = 0;
        private Vector3 lastPosition;
        private Vector3 targetPosition;
        private Vector3 normalVector;
        private float currentSpeed;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerRigidbody = GetComponent<Rigidbody>();
            playerManager = GetComponent<PlayerManager>();
            cameraObject = Camera.main.transform;
            animatorManager = GetComponentInChildren<AnimatorManager>();
            stepRayUpper.position = new Vector3(stepRayUpper.position.x, stepHeight, stepRayUpper.position.z);
        }
        public void HandleAllMovement()
        {

            HandleFallingAndLanding();

            if (playerManager.isInteracting)
                return;
            if (!playerManager.isGrounded)
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

            currentSpeed = runningSpeed;
            if (playerManager.isSprinting && inputManager.moveAmount >= 0.5f)
            {
                currentSpeed = sprintingSpeed;
            }
            else
            {
                if (inputManager.moveAmount >= 0.5f)
                {
                    currentSpeed = runningSpeed;
                }
                else
                {
                    currentSpeed = walkingSpeed;
                }
            }
            moveDirection *= currentSpeed;


            Vector3 movementVelocity = moveDirection;
            playerRigidbody.velocity = movementVelocity;
        }

        private void HandleFallingAndLanding()
        {
            RaycastHit hit;
            Vector3 rayCastOrigin = transform.position;
            rayCastOrigin.y += groundDetectionRayStartPoint;
            playerManager.isGrounded = false;
            if (Physics.Raycast(rayCastOrigin, transform.forward, out hit, stepRayUpper.position.y))
            {
                moveDirection = Vector3.zero;
            }
            if (playerManager.isInAir && !playerManager.isJumping)
            {
                inAirTimer += Time.deltaTime;
                playerRigidbody.AddForce(transform.forward * leapingVelocity);
                playerRigidbody.AddForce(-Vector3.up * fallingSpeed * inAirTimer);
            }
            Vector3 dir = moveDirection;
            dir.Normalize();
            rayCastOrigin += dir*groundDirectionRayDistance;
            targetPosition = transform.position;
            Debug.DrawRay(rayCastOrigin, -Vector3.up * minimumDistanceNeededToBeginFall, Color.red, 0.1f, false);
            if (Physics.Raycast(rayCastOrigin, -Vector3.up, out hit, minimumDistanceNeededToBeginFall, groundLayer))
            {
                normalVector = hit.normal;
                Vector3 tp = hit.point;
                targetPosition.y = tp.y;
                playerManager.isGrounded = true;
                if (playerManager.isInAir)
                {
                    if (inAirTimer > 0.5f)
                    {
                        animatorManager.PlayTargetAnimation(PlayerActionAnimations.Land.ToString(), true, true);
                    }
                    else
                    {
                        animatorManager.PlayTargetAnimation(PlayerActionAnimations.Empty.ToString(), false, false);
                    }
                    inAirTimer = 0;
                    playerManager.isInAir = false;
                }
            }
            else
            {
                if (playerManager.isGrounded)
                {
                    playerManager.isGrounded = false;
                }
                if (!playerManager.isInAir)
                {
                    if (!playerManager.isInteracting && inAirTimer > 0.2f)
                    {
                        animatorManager.PlayTargetAnimation(PlayerActionAnimations.Fall.ToString(), true, false);
                    }
                    playerManager.isInAir = true;
                }
            }
            if (playerManager.isGrounded && !playerManager.isJumping)
            {
                if (playerManager.isInteracting || inputManager.moveAmount > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime/0.1f);
                }
                else
                {
                    transform.position = targetPosition;
                }
            }

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