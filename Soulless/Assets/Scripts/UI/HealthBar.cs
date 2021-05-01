using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [field:SerializeField]
        public Slider slider { get; set; }
        private void Awake()
        {
            slider = GetComponent<Slider>();
        }
        public void SetMaxHealth(int maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }
        public void SetCurrentHealth(float health)
        {
            slider.value = health;
        }


    }
}