using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{

    public class InputManager : MonoBehaviour
    {
        private float rollInputTimer;

        private PlayerManager playerManager;
        private AnimatorManager animatorManager;
        private PlayerControls inputActions;
        private PlayerAttacker playerAttacker;
        private PlayerInventory playerInventory;
        private Vector2 movementInput;

        private Vector2 cameraInput;

        public Vector2 cameraZoomInput { get; set; }
        public float cameraInputX { get; set; }
        public float cameraInputY { get; set; }

        [field: SerializeField]
        public bool lightAttackInput { get; set; }
        [field: SerializeField]
        public bool heavyAttackInput { get; set; }
        public float horizontalInput { get; set; }
        [field: SerializeField]
        public float verticalInput { get; set; }
        [field: SerializeField]
        public float moveAmount { get; set; }
        [field: SerializeField]
        public bool sprintAndRollInput { get; set; }
        [field: SerializeField]
        public bool rollFlag { get; set; }
        [field: SerializeField]
        public bool jumpInput { get; set; }
        [field: SerializeField]
        public bool comboFlag { get; set; }

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            animatorManager = GetComponentInChildren<AnimatorManager>();
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
        }
        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
                inputActions.PlayerMovement.CameraZoom.performed += i => cameraZoomInput = i.ReadValue<Vector2>();
                inputActions.PlayerActions.LightAttack.performed += _ => lightAttackInput = true;
                inputActions.PlayerActions.LightAttack.canceled += _ => lightAttackInput = false;
                inputActions.PlayerActions.HeavyAttack.performed += _ => heavyAttackInput = true;
                inputActions.PlayerActions.HeavyAttack.canceled += _ => heavyAttackInput = false;
                inputActions.PlayerActions.SprintAndRoll.performed += i => sprintAndRollInput = true;
                inputActions.PlayerActions.SprintAndRoll.canceled += i => sprintAndRollInput = false;
                inputActions.PlayerActions.Jump.performed += _ => jumpInput = true;
                inputActions.PlayerActions.Jump.canceled += _ => jumpInput = false;
            }
            inputActions.Enable();
        }
        private void OnDisable()
        {
            inputActions.Disable();
        }
        public void HandleAllInput()
        {
            MoveInput();
            HandleSprintAndRollInput();
            HandleAttackInput();
        }
        private void MoveInput()
        {
            horizontalInput = movementInput.x;
            verticalInput = movementInput.y;

            cameraInputX = cameraInput.x;
            cameraInputY = cameraInput.y;

            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
            animatorManager.UpdateAnimatorValues(0, moveAmount, playerManager.isSprinting);
        }
        private void HandleSprintAndRollInput()
        {
            if (sprintAndRollInput)
            {
                rollInputTimer += Time.deltaTime;
                if (moveAmount > 0.5f)
                    playerManager.isSprinting = true;
                else
                {
                    playerManager.isSprinting = false;
                }
            }
            else
            {
                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    playerManager.isSprinting = false;
                    playerManager.isRolling = true;
                }

                rollInputTimer = 0;
            }
            if (rollInputTimer == 0 && playerManager.isSprinting)
            {
                playerManager.isSprinting = false;
            }
        }

        private void HandleAttackInput()
        {
            if (lightAttackInput)
            {
                if (playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttacker.HandleWeaponCombo(playerInventory.RightWeapon);
                    comboFlag = false;
                }
                else
                {
                    if (playerManager.isInteracting)
                        return;
                    if (playerManager.canDoCombo)
                        return;
                    playerAttacker.HandleLightAttack(playerInventory.RightWeapon);
                }
            }
            if (heavyAttackInput)
            {
                if (playerManager.canDoCombo)
                {
                    playerAttacker.HandleWeaponCombo(playerInventory.RightWeapon);
                }
                playerAttacker.HandleHeavyAttack(playerInventory.RightWeapon);
            }
        }
    }

}