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

        public float Horizontal => horizontal;
        public float Vertical => vertical;
        public float MoveAmount => moveAmount;

        PlayerControls inputActions;

        Vector2 movementInput;

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
        }
        private void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        }
    }

}