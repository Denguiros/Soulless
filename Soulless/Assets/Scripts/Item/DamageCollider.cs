using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControl;
using Enemy;
namespace Item
{
    public class DamageCollider : MonoBehaviour
    {
        Collider damageCollider;

        [SerializeField]
        private int currentWeaponDamage=25;

        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;
        }
        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }
        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag==Tags.Player.ToString())
            {
                PlayerStats playerStats = other.GetComponent<PlayerStats>();
                playerStats?.TakeDamage(currentWeaponDamage);
            }
            if (other.tag == Tags.Enemy.ToString())
            {
                EnemyStats enemyStats = other.GetComponent<EnemyStats>();
                enemyStats?.TakeDamage(currentWeaponDamage);
            }
        }
    }
}