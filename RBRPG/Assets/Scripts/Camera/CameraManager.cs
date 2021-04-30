using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControl;
using System;

public class CameraManager : MonoBehaviour
{
    private InputManager inputManager;

    [Header("Camera Optios")]
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float defaultPosition;
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private Vector3 cameraFollowVelocity = Vector3.zero;
    [SerializeField] private Vector3 cameraVectorPosition;

    [Header("Camera Collision")]
    [SerializeField] private float cameraCollisionRadius = 0.2f;
    [SerializeField] private float minimumCollisionOffset = 0.2f;
    [SerializeField] private float cameraCollisionOffset = 0.2f;

    [Header("Camera Zoom")]
    [SerializeField] private float zoomSpeed = 2;
    [SerializeField] private float maxDistance = -2;
    [SerializeField] private float minDistance = -10;

    [Header("Camera Speeds")]
    [SerializeField] private float cameraPivotSpeed = 2;
    [SerializeField] private float cameraLookSpeed = 2;
    [SerializeField] private float cameraFollowSpeed = .2f;

    [SerializeField] private float lookAngle;
    [SerializeField] private float pivotAngle;
    [SerializeField] private float minimumPivotAngle = -35;
    [SerializeField] private float maximumPivotAngle = 35;

    private void Awake()
    {
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        inputManager = FindObjectOfType<InputManager>();
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;

    }
    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisionsAndZooming();
    }



    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;
    }
    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;

        lookAngle += inputManager.cameraInputX * cameraLookSpeed;
        pivotAngle -= inputManager.cameraInputY * cameraPivotSpeed;
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);

        cameraPivot.localRotation = targetRotation;
    }
    private void HandleCameraCollisionsAndZooming()
    {
        defaultPosition += inputManager.cameraZoomInput.y * zoomSpeed*Time.deltaTime;
        defaultPosition = Mathf.Clamp(defaultPosition, minDistance, maxDistance);
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();
        if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition = -distance - cameraCollisionOffset;
        }
        if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
        {
            targetPosition -= minimumCollisionOffset;
        }
        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }

}
