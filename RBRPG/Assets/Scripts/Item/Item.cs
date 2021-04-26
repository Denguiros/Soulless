using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Item : ScriptableObject
    {
        [Header("Item Information")]
        [SerializeField]
        private Sprite itemIcon;
        [SerializeField]
        private string itemName;
    }
}