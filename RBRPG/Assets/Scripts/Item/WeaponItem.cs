using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControl;
namespace Item
{
    [CreateAssetMenu(menuName ="Items/Weapon Item")]
    public class WeaponItem : Item
    {
        [SerializeField]
        private GameObject modelPrefab;
        [SerializeField]
        private bool isUnarmed;

        [Tooltip("For each weapon, choose which animation you want to play.")]
        [Header("One Handed Attack Animations")]
        [SerializeField]
        private PlayerOneHandedAttackAnimations lightAttack1;
        [SerializeField]
        private PlayerOneHandedAttackAnimations heavyAttack1;

        public GameObject ModelPrefab => modelPrefab;

        public PlayerOneHandedAttackAnimations LightAttack1 => lightAttack1;
        public PlayerOneHandedAttackAnimations HeavyAttack1 => heavyAttack1;
    }
}
