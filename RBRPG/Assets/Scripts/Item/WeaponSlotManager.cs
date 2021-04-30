using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControl;
using UI;

namespace Item
{
    public class WeaponSlotManager : MonoBehaviour
    {
        public WeaponItem weapon { get; set; }

        private WeaponHolderSlot leftHandSlot;
        private WeaponHolderSlot rightHandSlot;
        private DamageCollider leftHandDamageCollider;
        private DamageCollider rightHandDamageCollider;
        private Animator animator;
        private QuickSlotsUI quickSlotsUI;
        private PlayerStats playerStats;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
            playerStats = GetComponentInParent<PlayerStats>();
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponHolderSlot in weaponHolderSlots)
            {
                if (weaponHolderSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponHolderSlot;

                }
                else if (weaponHolderSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponHolderSlot;
                }
            }
            
        }
        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            
            if (isLeft)
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
                quickSlotsUI.UpdateWeaponQuickSlotsUI(true, weaponItem);
                #region Handle Left Weapon Idle Animations
                if (weaponItem !=null)
                {
                    animator.CrossFade(weaponItem.leftHandIdle.ToString(), 0.2f);
                }
                else
                {
                    animator.CrossFade(PlayerLocomotionAnimations.LeftArmEmpty.ToString(), 0.2f);
                }
                #endregion
            }
            else
            {
                #region Handle Right Weapon Idle Animations
                rightHandSlot.LoadWeaponModel(weaponItem);
                quickSlotsUI.UpdateWeaponQuickSlotsUI(false, weaponItem);
                LoadRightWeaponDamageCollider();
                if (weaponItem != null)
                {
                    animator.CrossFade(weaponItem.rightHandIdle.ToString(), 0.2f);
                }
                else
                {
                    animator.CrossFade(PlayerLocomotionAnimations.RightArmEmpty.ToString(), 0.2f);
                }
                #endregion 
            }
        }
       
        #region Handle Weapon's Damage Collider
        private void LoadLeftWeaponDamageCollider()
        {
            leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }
        private void LoadRightWeaponDamageCollider()
        {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }
        public void OpenRightDamageCollider()
        {
            rightHandDamageCollider.EnableDamageCollider();
        }
        public void CloseRightDamageCollider()
        {
            rightHandDamageCollider.DisableDamageCollider();
        }
        public void OpenLeftDamageCollider()
        {
            leftHandDamageCollider.EnableDamageCollider();
        }
        public void CloseLeftDamageCollider()
        {
            leftHandDamageCollider.DisableDamageCollider();
        }
        #endregion
        #region Handle Weapon's Stamina Damage
        public void DrainStaminaLightAttack()
        { 
                playerStats.TakeStaminaDamage(Mathf.RoundToInt(weapon.baseStamina * weapon.lightAttackMultiplier));
        } 
        public void DrainStaminaHeavyAttack()
        {
            playerStats.TakeStaminaDamage(Mathf.RoundToInt(weapon.baseStamina * weapon.heavyAttackMultiplier));

        }
        #endregion
    }
}