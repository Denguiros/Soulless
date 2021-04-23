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
        private Rigidbody rigidBody;
        private GameObject normalCamera;
        [Header("Stats")]
        [SerializeField]
        private float movementSpeed = 5f;
        [SerializeField]
        private float rotationSpeed = 10f;
        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            cameraObject = Camera.main.transform;
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            myTransform = transform;
            animatorHandler.Initialize();
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

        private void HandleMovement(float delta)
        {
            inputHandler.TickInput(delta);
            moveDirection = cameraObject.forward * inputHandler.Vertical;
            moveDirection += cameraObject.right * inputHandler.Horizontal;
            moveDirection.y = 0;
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection.normalized, transform.up);
            rigidBody.velocity = projectedVelocity * movementSpeed;
        }
        #endregion
        private void Update()
        {
            float delta = Time.deltaTime;
            HandleMovement(delta);
            animatorHandler.UpdateAnimatorValues(inputHandler.MoveAmount, 0);
            if (animatorHandler.CanRotate)
            {
                HandleRotation(delta);
            }
        }

    }

}