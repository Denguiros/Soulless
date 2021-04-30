using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class InteractableUI : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI interactableText { get; set; }
        [field: SerializeField] public TextMeshProUGUI itemText { get; set; }
        [field: SerializeField] public RawImage itemIcon { get; set; }
    }
}