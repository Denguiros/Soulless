using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class WeaponSlotManager : MonoBehaviour
    {
        private WeaponHolderSlot leftHandSlot;
        private WeaponHolderSlot rightHandSlot;
        private void Awake()
        {
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponHolderSlot in weaponHolderSlots)
            {
                if (weaponHolderSlot.IsLeftHandSlot)
                {
                    leftHandSlot = weaponHolderSlot;
                }
                else if (weaponHolderSlot.IsrightHandSlot)
                {
                    rightHandSlot = weaponHolderSlot;
                }
            }
        }
        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            WeaponHolderSlot hand;
            if (isLeft)
            {
                hand = leftHandSlot;
            }
            else
            {
                hand = rightHandSlot;
            }
            hand.LoadWeaponModel(weaponItem);
        }
    }
}