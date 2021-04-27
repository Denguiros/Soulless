using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControl;
namespace Enemy
{
    public class DamagePlayer : MonoBehaviour
    {
        [SerializeField]
        private int damage=25;
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == Tags.Player.ToString())
            {
               PlayerStats playerStats = other.GetComponent<PlayerStats>();

                playerStats?.TakeDamage(damage);
            }
        }
    }
}