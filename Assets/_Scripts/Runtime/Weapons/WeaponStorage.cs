using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponStorage
    {
        private List<WeaponData> weaponDataList = new ();
        private int currentWeaponIndex = -1;
        public int WeaponCount { get => weaponDataList.Count; }

        internal bool AddWeaponData(WeaponData weaponData)
        {
            if (weaponDataList.Contains(weaponData)) return false;

            weaponDataList.Add(weaponData);
            currentWeaponIndex = weaponDataList.Count - 1;
            return true;
        }

        internal WeaponData GetCurrentWeapon()
        {
            if (currentWeaponIndex == -1)
                return null;

            return weaponDataList[currentWeaponIndex];
        }

        internal WeaponData SwapWeapon()
        {
            if (weaponDataList.Count == -1)
                return null;

            currentWeaponIndex++;

            if (currentWeaponIndex >= weaponDataList.Count)
                currentWeaponIndex = 0;

            return weaponDataList[currentWeaponIndex];
        }

        internal List<string> GetPlayerWeaponNames()
        {
            return weaponDataList.Select(x => x.name).ToList();
        }
    }
}