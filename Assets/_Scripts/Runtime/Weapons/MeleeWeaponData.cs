using StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "New melee weapon data", menuName = "Weapons/MeleeWeaponData")]
    public class MeleeWeaponData : WeaponData
    {
        public Vector2 attackRange = new (2, 2);
        public Vector2 offset = Vector2.zero;
        private Vector2 currentDirectionOffset; 

        public override bool CanBeUsed(bool isGrounded)
        {
            return isGrounded == true;
        }

        public override void PerformAttack(Agent agent, LayerMask hitabbleMask, Vector3 direction)
        { 
            currentDirectionOffset = new Vector2(offset.x * direction.x, offset.y);

            RaycastHit2D hit = Physics2D.BoxCast(agent.agentWeaponManager.transform.position + direction * (attackRange.x/2) + new Vector3(currentDirectionOffset.x, currentDirectionOffset.y, 0), 
                                      attackRange, 0, direction, 0, hitabbleMask);

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
            Vector3 center = origin + direction * (attackRange.x / 2) + new Vector3(currentDirectionOffset.x, currentDirectionOffset.y, 0);
            Gizmos.DrawWireCube(center, new Vector3(attackRange.x, attackRange.y, 0));
        }
    }
}
