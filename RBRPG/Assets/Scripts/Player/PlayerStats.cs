using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using System;

namespace PlayerControl
{
    public class PlayerStats : MonoBehaviour
    {

        [field: SerializeField] public int healthLevel { get; set; } = 10;

        [field: SerializeField] public int maxHealth { get; set; } = 10;

        [field: SerializeField] public int currentHealth { get; set; } = 10;      
        [field: SerializeField] public int staminaLevel { get; set; } = 10;

        [field: SerializeField] public int maxStamina {get; set; } = 10;

        [field:SerializeField] public int currentStamina { get; set; } = 10;

        [field: SerializeField] private HealthBar healthBar { get; set; }
        [field: SerializeField] private StaminaBar staminaBar { get; set; }
        private AnimatorManager animatorManager;
        private PlayerManager playerManager;
        private void Start()
        {
            animatorManager = GetComponentInChildren<AnimatorManager>();
            playerManager = GetComponent<PlayerManager>();

            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);

            maxStamina = SetMaxStaminaFromStaminaLevel();
            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);
        }
        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }    
        private int SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
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
        public void TakeStaminaDamage(int damage)
        {
            currentStamina -= damage;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            staminaBar.SetCurrentStamina(currentStamina);
        }
    }
}