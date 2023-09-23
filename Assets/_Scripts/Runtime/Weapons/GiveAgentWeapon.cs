using StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class GiveAgentWeapon : MonoBehaviour
    {
        public List<WeaponData> weaponData = new List<WeaponData>();

        void Start()
        {
            Agent agent = GetComponentInChildren<Agent>();

            if (agent == null)
                return;

            foreach (WeaponData item in weaponData)
            {
                agent.agentWeaponManager.AddWeapondata(item);
            }
        }
    }
}