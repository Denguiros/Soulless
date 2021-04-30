using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [field:SerializeField] public PlayerInventory playerInventory { get; set; }
        [field:SerializeField] public GameObject selectWindow { get; set; }
        [field:SerializeField] public GameObject hudWindow { get; set; }
        [field:SerializeField] public GameObject weaponInventoryWindow { get; set; }


        [field:SerializeField] public Transform weaponInventorySlotsParent { get; set; }
        [field:SerializeField] public GameObject weaponInventorySlotPrefab { get; set; }
        [field:SerializeField] public WeaponInventorySlot[] weaponInventorySlots { get; set; }
        private void Start()
        {
            weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
        }
        public void UpdateUI()
        {
            #region Weapon Inventory Slots
            for(int i=0;i<weaponInventorySlots.Length;i++)
            {
                if(i < playerInventory.weaponsInventory.Count)
                {
                    if(weaponInventorySlots.Length<playerInventory.weaponsInventory.Count)
                    {
                        Instantiate(weaponInventorySlotPrefab, weaponInventorySlotsParent);
                        weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
                    }
                    weaponInventorySlots[i].AddItem(playerInventory.weaponsInventory[i]);
                }
                else
                {
                    weaponInventorySlots[i].ClearInventorySlot();
                }
            }
            #endregion
        }
        public void OpenSelectWindow()
        {
            selectWindow.SetActive(true);
        }
        public void CloseSelectWindow()
        {
            selectWindow.SetActive(false);
        }
        public void CloseAllInventoryWindows()
        {
            weaponInventoryWindow.SetActive(false);
        }
    }
}