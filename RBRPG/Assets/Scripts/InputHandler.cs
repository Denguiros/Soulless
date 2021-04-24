using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{

    public class InputHandler : MonoBehaviour
    {
        private float horizontal;
        private float vertical;
        private float moveAmount;
        private float rollInputTimer;



        private PlayerControls inputActions;
        private Vector2 movementInput;



        public bool SprintFlag{ get; set; }
        public bool RollFlag { get; set; }
        public float Horizontal => horizontal;
        public float Vertical => vertical;
        public float MoveAmount => moveAmount;

        public bool SprintAndRollButtonInput { get; set; }


        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
            }
            inputActions.Enable();
        }
        private void OnDisable()
        {
            inputActions.Disable();
        }
        public void TickInput(float delta)
        {
            MoveInput(delta);
            HandleRollInput(delta);
        }
        private void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        }
        private void HandleRollInput(float delta)
        {
            SprintAndRollButtonInput = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            if (SprintAndRollButtonInput)
            {
                rollInputTimer += delta;
                SprintFlag = true;

            }
            else
            {
                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    SprintFlag = false;
                    RollFlag = true;
                }
                rollInputTimer = 0;
            }
        }
    }

}