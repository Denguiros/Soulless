using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Item
{

    public class WeaponHolderSlot : MonoBehaviour
    {

        [SerializeField]
        private Transform parentOverride;
        [field: SerializeField]
        public GameObject currentWeaponModel { get; set; }

        [field:SerializeField]
        public bool isLeftHandSlot { get; set; }
        [field: SerializeField]
        public bool isRightHandSlot { get; set; } = true;
        public void UnloadWeapon()
        {
            if (currentWeaponModel != null)
            {
                currentWeaponModel.SetActive(false);

            }
        }
        public void UnloadWeaponAndDestroy()
        {
            if (currentWeaponModel != null)
            {
                Destroy(currentWeaponModel);
            }
        }
        public void LoadWeaponModel(WeaponItem weaponItem)
        {
            UnloadWeaponAndDestroy();
            if (weaponItem == null)
            {
                UnloadWeapon();
                return;
            }
            GameObject model = Instantiate(weaponItem.modelPrefab) as GameObject;
            if (model != null)
            {
                if (parentOverride != null)
                {
                    model.transform.parent = parentOverride;
                }
                else
                {
                    model.transform.parent = transform;
                }
                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
            }
            currentWeaponModel = model;
        }
    }
}
