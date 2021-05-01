using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    public class WeaponInventorySlot : MonoBehaviour
    {
        [field:SerializeField] public Image icon { get; set; }
        [field: SerializeField] public WeaponItem item { get; set; }
        public void AddItem(WeaponItem newItem)
        {
            item = newItem;
            icon.sprite = item.icon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }
        public void ClearInventorySlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }
    }
}