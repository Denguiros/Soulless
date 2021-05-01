using System;
using System.Collections;
using UnityEngine;
using PlayerControl;
using UI;
namespace Enemy
{
    public class EnemyStats : MonoBehaviour
    {
        [SerializeField]
        private int healthLevel = 10;
        [SerializeField]
        private int maxHealth = 10;
        [SerializeField]
        private int currentHealth = 10;
        [SerializeField]
        private HealthBar healthBar;
        private Animator animator;
        private bool isDead = false;

        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            animator = GetComponentInChildren<Animator>();
            healthBar.SetMaxHealth(maxHealth);
        }
        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }
        public void TakeDamage(int damage)
        {
            if (isDead)
                return;
            currentHealth -= damage;
            healthBar.SetCurrentHealth(currentHealth);
            animator.Play(EnemyActionAnimations.TakeDamage.ToString());
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play(EnemyActionAnimations.Death.ToString());
                isDead = true;
            }
        }
    }
}