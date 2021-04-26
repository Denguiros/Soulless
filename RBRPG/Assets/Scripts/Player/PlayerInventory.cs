using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Item
{

    public class PlayerInventory : MonoBehaviour
    {
        WeaponSlotManager weaponSlotManager;
        [SerializeField]
        private WeaponItem rightWeapon;
        [SerializeField]
        private WeaponItem leftWeapon;

        public WeaponItem RightWeapon { get => rightWeapon; set => rightWeapon = value; }
        public WeaponItem LeftWeapon { get => leftWeapon; set => leftWeapon = value; }

        private void Awake()
        {
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>(); 
        }
        private void Start()
        {
            weaponSlotManager.LoadWeaponOnSlot(RightWeapon, false);
            weaponSlotManager.LoadWeaponOnSlot(LeftWeapon, true);
        }

    }

}