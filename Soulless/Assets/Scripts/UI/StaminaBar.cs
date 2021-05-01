using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StaminaBar : MonoBehaviour
    {
        [field:SerializeField]
        public Slider slider { get; set; }
        private void Awake()
        {
            slider = GetComponent<Slider>();
        }
        public void SetMaxStamina(int maxStamina)
        {
            slider.maxValue = maxStamina;
            slider.value = maxStamina;
        }
        public void SetCurrentStamina(float stamina)
        {
            slider.value = stamina;
        }


    }
}