using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace PlayerControl
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField]
        private int healthLevel = 10;
        [SerializeField]
        private int maxHealth = 10;
        [SerializeField]
        private int currentHealth = 10;
        [SerializeField]
        private HealthBar healthBar;
        private AnimatorManager animatorManager;
        private PlayerManager playerManager;
        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            playerManager = GetComponent<PlayerManager>();
            healthBar.SetMaxHealth(maxHealth);
            animatorManager = GetComponentInChildren<AnimatorManager>();
        }
        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            healthBar.SetCurrentHealth(currentHealth);
            animatorManager.PlayTargetAnimation(PlayerActionAnimations.TakeDamage.ToString(), true, true);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animatorManager.PlayTargetAnimation(PlayerActionAnimations.Death.ToString(), true, true);
                playerManager.isDead = true;
            }
        }
    }
}