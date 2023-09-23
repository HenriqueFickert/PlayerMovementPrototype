using StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "New melee weapon data", menuName = "Weapons/MeleeWeaponData")]
    public class MeleeWeaponData : WeaponData
    {
        public Vector2 attackRange = new(2, 2);

        public override bool CanBeUsed(bool isGrounded)
        {
            return isGrounded == true;
        }

        public override void PerformAttack(Agent agent, LayerMask hitabbleMastk, Vector3 direction)
        {
            Debug.Log("attacked");
            RaycastHit2D hit = Physics2D.BoxCast(agent.agentWeaponManager.transform.position,
                                          attackRange, 0, direction, 0, hitabbleMastk);

            if (hit.collider != null)
            {
                foreach (IHittable hittable in hit.collider.GetComponents<IHittable>())
                {
                    hittable.GetHit(agent.gameObject, weaponDamage);
                }
            }
        }

        public override void DrawWeaponGizmo(Vector3 origin, Vector3 direction)
        {
            Gizmos.DrawWireCube(origin, attackRange * direction);
        }
    }
}