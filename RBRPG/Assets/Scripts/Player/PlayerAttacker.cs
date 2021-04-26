using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;

namespace PlayerControl
{
    public class PlayerAttacker : MonoBehaviour
    {
        private AnimatorManager animator;
        private PlayerManager playerManager;
        private void Awake()
        {
            animator = GetComponentInChildren<AnimatorManager>();
            playerManager = GetComponent<PlayerManager>();
        }
        public void HandleLightAttack(WeaponItem weaponItem)
        {
            if (playerManager.isInteracting)
                return;
            animator.PlayTargetAnimation(weaponItem.LightAttack1.ToString(), true,true);
        }       
        public void HandleHeavyAttack(WeaponItem weaponItem)
        {
            if (playerManager.isInteracting)
                return;
            animator.PlayTargetAnimation(weaponItem.HeavyAttack1.ToString(), true, true);
        }
    }
}
