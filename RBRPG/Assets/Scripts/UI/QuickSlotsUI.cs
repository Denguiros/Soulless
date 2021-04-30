using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class QuickSlotsUI : MonoBehaviour
    {
        [field:SerializeField]
        public Image leftWeaponIcon { get; set; }
        [field:SerializeField]
        public Image rightWeaponIcon { get; set; }
        public void UpdateWeaponQuickSlotsUI(bool isLeft, WeaponItem weapon)
        {
            if (!isLeft)
            {
                if (weapon.icon != null)
                {
                    rightWeaponIcon.sprite = weapon.icon;
                    rightWeaponIcon.enabled = true;

                }
                else
                {
                    rightWeaponIcon.sprite = null;
                    rightWeaponIcon.enabled = false;
                }
            }
            else
            {
                if (weapon.icon != null)
                {
                    leftWeaponIcon.sprite = weapon.icon;
                    leftWeaponIcon.enabled = true;
                }
                else
                {
                    leftWeaponIcon.sprite = null;
                    leftWeaponIcon.enabled = false;
                }
            }
        }
    }
}