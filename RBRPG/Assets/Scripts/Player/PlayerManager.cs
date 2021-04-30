using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Item;

delegate void CheckForInteractableObject();
namespace PlayerControl
{
    public enum Tags
    {
        Hittable,
        Player,
        Enemy,
        Interactable
    }
    public class PlayerManager : MonoBehaviour
    {
        private AnimatorManager animatorManager;
        private CameraManager cameraManager;
        private InputManager inputManager;
        private Animator animator;
        private PlayerLocomotion playerLocomotion;
        private InteractableUI interactableUI;

        // Player Flags !
        [field: SerializeField] public bool isGrounded { get; set; }
        [field: SerializeField] public bool isInAir { get; set; }
        [field: SerializeField] public bool isInteracting { get; set; }
        [field: SerializeField] public bool isSprinting { get; set; }
        [field: SerializeField] public bool isRolling { get; set; }
        [field: SerializeField] public bool isJumping { get; set; }
        [field: SerializeField] public bool isDead { get; set; }
        [field: SerializeField] public bool canDoCombo { get; set; }
        [field: SerializeField] public LayerMask interactableMask { get; set; }
        [field: SerializeField] public GameObject interactableUIGameObject { get; set; }
        [field: SerializeField] public GameObject itemInteractableGameObject { get; set; }


        void Start()
        {
            animatorManager = GetComponentInChildren<AnimatorManager>();
            inputManager = GetComponent<InputManager>();
            animator = GetComponentInChildren<Animator>();
            animatorManager.Initialize();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            cameraManager = FindObjectOfType<CameraManager>();
            isDead = false;
            interactableUI = FindObjectOfType<InteractableUI>();
        }
        void Update()
        {
            inputManager.HandleAllInput();

        }
        private void FixedUpdate()
        {
            playerLocomotion.HandleAllMovement();
        }

        void LateUpdate()
        {
            cameraManager.HandleAllCameraMovement();
            isInteracting = animator.GetBool(PlayerAnimatorParameters.IsInteracting.ToString());
            canDoCombo = animator.GetBool(PlayerAnimatorParameters.CanDoCombo.ToString());
            isJumping = animator.GetBool(PlayerAnimatorParameters.IsJumping.ToString());
            animator.SetBool(PlayerAnimatorParameters.IsGrounded.ToString(), isGrounded);
        }
        public void InteractWithObject(Interactable interactable)
        {
            interactableUI.interactableText.text = interactable.interactableText;
            interactableUIGameObject.SetActive(true);
        }
        public void ResetInteractionWithObject(Interactable interactable)
        {
            interactableUIGameObject.SetActive(false);
        }
        public IEnumerator ResetItemInteractionWithObject(Interactable interactable)
        {
            yield return new WaitForSeconds(2);
            itemInteractableGameObject.SetActive(false);
        }
    }
}
