using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Item : ScriptableObject
    {
        [field: SerializeField]
        public Sprite icon { get; set; }
        [field: SerializeField]
        public new string name { get; set; }
    }
}