using PlayerControl;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Item
{
    public class WeaponPickUp : Interactable
    {
        private PlayerInventory playerInventory;
        private PlayerLocomotion playerLocomotion;
        private AnimatorManager animatorManager;
        [field: SerializeField] public WeaponItem weapon { get; set; }
        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);
            PickUpItem(playerManager);
        }
        private void PickUpItem(PlayerManager playerManager)
        {
            playerInventory = playerManager.GetComponent<PlayerInventory>();
            playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
            animatorManager = playerManager.GetComponentInChildren<AnimatorManager>();
            playerLocomotion.playerRigidbody.velocity = Vector3.zero; // Stop moving whilst picking up an item
            animatorManager.PlayTargetAnimation(PlayerActionAnimations.PickUpItem.ToString(), true, true);
            playerInventory.weaponsInventory.Add(weapon);
            playerManager.itemInteractableGameObject.GetComponentInChildren<TextMeshProUGUI>().text = weapon.name;
            playerManager.itemInteractableGameObject.GetComponentInChildren<RawImage>().texture = weapon.icon.texture;
            playerManager.ResetInteractionWithObject(this);
            playerManager.itemInteractableGameObject.SetActive(true);
            playerManager.StartCoroutine(playerManager.ResetItemInteractionWithObject(this));
            Destroy(gameObject);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals(Tags.Player.ToString()))
            {
                other.GetComponent<PlayerManager>().InteractWithObject(this);
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.tag.Equals(Tags.Player.ToString()))
            {
                if (other.GetComponent<InputManager>().interactInput)
                {
                    Interact(other.GetComponent<PlayerManager>());
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals(Tags.Player.ToString()))
            {
                other.GetComponent<PlayerManager>().ResetInteractionWithObject(this);
            }
        }
    }
}