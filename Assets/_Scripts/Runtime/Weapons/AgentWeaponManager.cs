using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WeaponSystem
{
    public class AgentWeaponManager : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        private WeaponStorage weaponStorage;

        public UnityEvent<Sprite> OnWeaponSwap;
        public UnityEvent OnMultipleWeapons;
        public UnityEvent OnWeaponPickUp;

        private void Awake()
        {
            weaponStorage = new WeaponStorage();
            spriteRenderer = GetComponent<SpriteRenderer>();
            ToggleWeaponVisibility(false);
        }

        private void ToggleWeaponVisibility(bool value)
        {
            if (value)
                SwapWeaponSprite(GetCurrentWeapon().weaponSprite);

            spriteRenderer.enabled = value;
        }

        public WeaponData GetCurrentWeapon()
        {
            return weaponStorage.GetCurrentWeapon();
        }

        private void SwapWeaponSprite(Sprite weaponSprite)
        {
            spriteRenderer.sprite = weaponSprite;
            OnWeaponSwap?.Invoke(weaponSprite);
        }

        public void SwapWeapon()
        {
            if (weaponStorage.WeaponCount <= 0)
                return;

            SwapWeaponSprite(weaponStorage.SwapWeapon().weaponSprite);
        }

        public void AddWeapondata(WeaponData weaponData)
        {
            if (!weaponStorage.AddWeaponData(weaponData))
                return;

            if (weaponStorage.WeaponCount > 1)
                OnMultipleWeapons?.Invoke();

            SwapWeaponSprite(weaponData.weaponSprite);
        }

        public void PickUpWeapon(WeaponData weaponData)
        {
            AddWeapondata(weaponData);
            OnWeaponPickUp?.Invoke();
        }

        public bool CanIUseWeapon(bool isGrounded)
        {
            if (weaponStorage.WeaponCount <= 0)
                return false;

            return weaponStorage.GetCurrentWeapon().CanBeUsed(isGrounded);
        }

        public List<string> GetPlayerWeaponNames()
        {
            return weaponStorage.GetPlayerWeaponNames();
        }
    }
}