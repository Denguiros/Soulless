using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Item
{

    public class PlayerInventory : MonoBehaviour
    {
        [field: SerializeField] WeaponSlotManager weaponSlotManager;
        [field: SerializeField] public WeaponItem leftWeapon { get; set; }
        [field: SerializeField] public WeaponItem rightWeapon { get; set; }
        [field: SerializeField] public WeaponItem[] weaponsInRightHandSlots { get; set; }
        [field: SerializeField] public WeaponItem[] weaponsInLeftHandSlots { get; set; }
        [field: SerializeField] public int currentRightWeaponIndex { get; set; } = 0;
        [field: SerializeField] public int currentLeftWeaponIndex { get; set; } = 0;

        [field: SerializeField] public List<WeaponItem> weaponsInventory { get; set; }
        private void Awake()
        {
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();

        }
        private void Start()
        {
            rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
            leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];
            weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
            weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
        }
        public void ChangeRightWeapon()
        {
            currentRightWeaponIndex++;
            currentRightWeaponIndex %= weaponsInRightHandSlots.Length;
            if (weaponsInRightHandSlots[currentRightWeaponIndex] != null)
            {
                rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
            }
            else
            {
                ChangeRightWeapon();
            }
            if (rightWeapon != null)
                weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        }
        public void ChangeLeftWeapon()
        {
            currentLeftWeaponIndex++;
            currentLeftWeaponIndex %= weaponsInLeftHandSlots.Length;
            if (weaponsInLeftHandSlots[currentLeftWeaponIndex] != null)
            {
                leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];
            }
            else
            {
                ChangeLeftWeapon();
            }
            if (leftWeapon != null)
                weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
        }

    }

}