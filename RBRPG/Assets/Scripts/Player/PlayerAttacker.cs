using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;

namespace PlayerControl
{
    public class PlayerAttacker : MonoBehaviour
    {
        private AnimatorManager animatorManager;
        private InputManager inputManager;
        private WeaponSlotManager weaponSlotManager;
        private PlayerOneHandedAttackAnimations lastAttack { get; set; }
        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            animatorManager = GetComponentInChildren<AnimatorManager>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        }
        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (inputManager.comboFlag)
            {
                animatorManager.animator.SetBool(PlayerAnimatorParameters.CanDoCombo.ToString(), false);
                if (lastAttack == weapon.lightAttack1)
                {
                    animatorManager.PlayTargetAnimation(weapon.lightAttack2.ToString(), true, true);
                    lastAttack = weapon.lightAttack2;
                }
                else if (lastAttack == weapon.lightAttack2)
                { 
                    animatorManager.PlayTargetAnimation(weapon.lightAttack3.ToString(), true, true);
                    lastAttack = weapon.lightAttack3;
                }
                else if (lastAttack == weapon.lightAttack3)
                {
                    animatorManager.PlayTargetAnimation(weapon.lightAttack4.ToString(), true, true);
                    lastAttack = weapon.lightAttack4;
                }
                else if (lastAttack == weapon.lightAttack4)
                {
                    animatorManager.PlayTargetAnimation(weapon.lightAttack5.ToString(), true, true);
                    lastAttack = weapon.lightAttack5;
                }
                else if (lastAttack == weapon.lightAttack5)
                {
                    animatorManager.PlayTargetAnimation(weapon.lightAttack1.ToString(), true, true);
                    lastAttack = weapon.lightAttack1;
                }

            }
        }
        public void HandleLightAttack(WeaponItem weaponItem)
        {
            weaponSlotManager.weapon = weaponItem;
            animatorManager.PlayTargetAnimation(weaponItem.lightAttack1.ToString(), true, true);
            lastAttack = weaponItem.lightAttack1;
        }
        public void HandleHeavyAttack(WeaponItem weaponItem)
        {
            weaponSlotManager.weapon = weaponItem;
            animatorManager.PlayTargetAnimation(weaponItem.heavyAttack1.ToString(), true, true);
            lastAttack = weaponItem.heavyAttack1;
        }
    }
}
