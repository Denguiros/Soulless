using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Control
{

    public class PlayerLocomotion : MonoBehaviour
    {
        private InputHandler inputHandler;
        private Transform cameraObject;
        private Vector3 moveDirection;
        private Transform myTransform;
        private AnimatorHandler animatorHandler;
        private PlayerManager playerManager;

        [HideInInspector]
        public Rigidbody rigidBody;

        [Header("Ground & Air Detection Stats")]
        [SerializeField]
        private float groundDetectionRayStartPoint = 0.5f;
        [SerializeField]
        private float minimumDistanceNeededToBeginFall = 1f;
        [SerializeField]
        private float groundDirectionRayDistance = 0.2f;

        [Header("Movement Stats")]
        [SerializeField]
        private float movementSpeed = 5f;
        [SerializeField]
        private float sprintSpeed = 7f;
        [SerializeField]
        private float rotationSpeed = 10f; 
        [SerializeField]
        private float fallingSpeed = 10f;

        private LayerMask ignoreForGroundCheck;

        public float InAirTimer{ get; set;}


        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            cameraObject = Camera.main.transform;
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            myTransform = transform;
            animatorHandler.Initialize();
            playerManager = GetComponent<PlayerManager>();
        }

        #region Movement Calculations
        Vector3 targetPosition;



        private void HandleRotation(float delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = inputHandler.MoveAmount;
            targetDir = cameraObject.forward * inputHandler.Vertical;
            targetDir += cameraObject.right * inputHandler.Horizontal;
            targetDir.Normalize();
            targetDir.y = 0;
            if (targetDir == Vector3.zero)
                targetDir = myTransform.forward;
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(targetDir), rotationSpeed * delta);
        }

        public void HandleMovement(float delta)
        {
            moveDirection = cameraObject.forward * inputHandler.Vertical;
            moveDirection += cameraObject.right * inputHandler.Horizontal;
            moveDirection.y = 0;
            float speed = movementSpeed;
            if (inputHandler.SprintFlag)
            {
                speed = sprintSpeed;
                playerManager.IsSprinting = true;
            }
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection.normalized, transform.up);
            rigidBody.velocity = projectedVelocity * speed;
            animatorHandler.UpdateAnimatorValues(inputHandler.MoveAmount, 0, playerManager.IsSprinting);
            if (animatorHandler.CanRotate)
            {
                HandleRotation(delta);
            }
        }
        #endregion

        public void HandleRollingAndSprinting(float delta)
        {
            if (animatorHandler.Animator.GetBool(PlayerAnimatorParameters.isInteracting.ToString()))
                return;
            if (inputHandler.RollFlag)
            {
                moveDirection = cameraObject.forward * inputHandler.Vertical;
                moveDirection += cameraObject.right * inputHandler.Horizontal;
                if (inputHandler.MoveAmount > 0)
                {
                    animatorHandler.PlayTargetAnimation(PlayerAnimations.UnarmedRollForward.ToString(), true);
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = rollRotation;
                }
                else
                {
                    animatorHandler.PlayTargetAnimation(PlayerAnimations.UnarmedRollBackward.ToString(), true);
                }
            }
        }

    }

}