using Item;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
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
        private UIManager uiManager;
        private Vector2 movementInput;

        private Vector2 cameraInput;


        public Vector2 cameraZoomInput { get; set; }
        public float cameraInputX { get; set; }
        public float cameraInputY { get; set; }


        [field: SerializeField] public bool comboFlag { get; set; }
        [field: SerializeField] public bool rollFlag { get; set; }
        [field: SerializeField] public bool inventoryFlag { get; set; }

        [field: SerializeField] public bool lightAttackInput { get; set; }
        [field: SerializeField] public bool heavyAttackInput { get; set; }
        [field: SerializeField] public float horizontalInput { get; set; }
        [field: SerializeField] public float verticalInput { get; set; }
        [field: SerializeField] public float moveAmount { get; set; }
        [field: SerializeField] public bool sprintAndRollInput { get; set; }
        [field: SerializeField] public bool jumpInput { get; set; }
        [field: SerializeField] public bool arrowUp { get; set; }
        [field: SerializeField] public bool arrowDown { get; set; }
        [field: SerializeField] public bool arrowLeft { get; set; }
        [field: SerializeField] public bool arrowRight { get; set; }
        [field: SerializeField] public bool interactInput { get; set; }



        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            animatorManager = GetComponentInChildren<AnimatorManager>();
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            uiManager = FindObjectOfType<UIManager>();
        }
        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += _ => cameraInput = _.ReadValue<Vector2>();
                inputActions.PlayerMovement.CameraZoom.performed += _ => cameraZoomInput = _.ReadValue<Vector2>();
                inputActions.PlayerActions.LightAttack.performed += _ => lightAttackInput = true;
                inputActions.PlayerActions.LightAttack.canceled += _ => lightAttackInput = false;
                inputActions.PlayerActions.HeavyAttack.performed += _ => heavyAttackInput = true;
                inputActions.PlayerActions.HeavyAttack.canceled += _ => heavyAttackInput = false;
                inputActions.PlayerActions.SprintAndRoll.performed += _ => sprintAndRollInput = true;
                inputActions.PlayerActions.SprintAndRoll.canceled += _ => sprintAndRollInput = false;
                inputActions.PlayerActions.Jump.started += _ => jumpInput = true;
                inputActions.PlayerActions.Jump.canceled += _ => jumpInput = false;
                inputActions.PlayerActions.ArrowLeft.started += _ => playerInventory.ChangeLeftWeapon();
                inputActions.PlayerActions.ArrowRight.started += _ => playerInventory.ChangeRightWeapon();
                inputActions.PlayerActions.Interact.started += _ => interactInput = true;
                inputActions.PlayerActions.Interact.canceled += _ => interactInput = false;
                inputActions.PlayerActions.Inventory.started += _ => HandleInventoryInput();

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
        private void HandleInventoryInput()
        {
            inventoryFlag = !inventoryFlag;
            if (inventoryFlag)
            {
                uiManager.OpenSelectWindow();
                uiManager.UpdateUI();
                uiManager.hudWindow.SetActive(false);
            }
            else
            {
                uiManager.CloseSelectWindow();
                uiManager.CloseAllInventoryWindows();
                uiManager.hudWindow.SetActive(true);
            }
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
            if (!playerManager.isConsultingUi)
            {

                if (heavyAttackInput)
                {

                    if (playerManager.canDoCombo)
                    {
                        comboFlag = true;
                        playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                        comboFlag = false;
                    }
                    else
                    {
                        if (playerManager.isInteracting)
                            return;
                        if (playerManager.canDoCombo)
                            return;
                        playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
                    }
                }
                if (lightAttackInput)
                {

                    if (playerManager.canDoCombo)
                    {
                        comboFlag = true;
                        playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                        comboFlag = false;
                    }
                    else
                    {
                        if (playerManager.isInteracting)
                            return;
                        if (playerManager.canDoCombo)
                            return;
                        playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
                    }
                }
            }
        }

    }

}