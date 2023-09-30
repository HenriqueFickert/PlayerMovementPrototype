using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class WeaponUI : MonoBehaviour
    {
        [SerializeField]
        private Image weaponSprite;
        [SerializeField]
        private GameObject weaponSwapTip;

        public UnityEvent weaponSwapEvent, toggleWeaponTipUI;

        private void Start()
        {
            weaponSprite.enabled = false;
            weaponSprite.sprite = null;
            weaponSwapTip.SetActive(false);
        }

        public void SetWeaponImage(Sprite weaponSprite)
        {
            if (this.weaponSprite.sprite == weaponSprite)
                return;

            this.weaponSprite.enabled = true;
            this.weaponSprite.sprite = weaponSprite;
            weaponSwapEvent?.Invoke();
        }

        public void ToggleWeaponTip(bool value)
        {
            weaponSwapTip.SetActive(value);
            if (value)
                toggleWeaponTipUI?.Invoke();
        }
    }
}