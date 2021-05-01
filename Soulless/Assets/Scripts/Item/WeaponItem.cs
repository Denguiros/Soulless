using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControl;
namespace Item
{
    [CreateAssetMenu(menuName ="Items/Weapon Item")]
    public class WeaponItem : Item
    {

        [field: SerializeField] public GameObject modelPrefab { get; set; }
        [field: SerializeField] public bool isUnarmed { get; set; }

        [field: SerializeField] public PlayerLocomotionAnimations rightHandIdle { get; set; }
        [field: SerializeField] public PlayerLocomotionAnimations leftHandIdle { get; set; }

        [field: SerializeField] public PlayerOneHandedAttackAnimations lightAttack1 { get; set; }
        [field: SerializeField] public PlayerOneHandedAttackAnimations lightAttack2 { get; set; }
        [field: SerializeField] public PlayerOneHandedAttackAnimations lightAttack3 { get; set; }
        [field: SerializeField] public PlayerOneHandedAttackAnimations lightAttack4 { get; set; }
        [field: SerializeField] public PlayerOneHandedAttackAnimations lightAttack5 { get; set; }
        [field: SerializeField] public PlayerOneHandedAttackAnimations heavyAttack1 { get; set; }
        [field: SerializeField] public PlayerOneHandedAttackAnimations heavyAttack2 { get; set; }
        [field: SerializeField] public PlayerOneHandedAttackAnimations heavyAttack3 { get; set; }
        [field: SerializeField] public int baseStamina { get; set; }
        [field: SerializeField] public float lightAttackMultiplier { get; set; }
        [field: SerializeField] public float heavyAttackMultiplier { get; set; }

    }
}
