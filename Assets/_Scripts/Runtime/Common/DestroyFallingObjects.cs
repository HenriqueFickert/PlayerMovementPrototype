using StatePattern;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Common
{
    public class DestroyFallingObjects : MonoBehaviour
    {
        public LayerMask objectToDestroyLayerMask;
        public Vector2 size;

        [Header("Gizmo Parameters")]
        public Color gizmoColor = Color.red;
        public bool showGizmo = true;

        private void FixedUpdate()
        {
            Collider2D collider = Physics2D.OverlapBox(transform.position, size, 0, objectToDestroyLayerMask);

            if (collider != null)
            {
                Agent agent = collider.GetComponent<Agent>();

                if (agent == null)
                    Destroy(collider.gameObject);

                //Damagable damagable = agent.GetComponent<Damagable>();
                //if (damagable != null)
                //    damagable.GetHit(1);

                agent.damagable.GetHit(1);
                agent.AgentDied();
            }
        }

        private void OnDrawGizmos()
        {
            if (showGizmo)
            {
                Gizmos.color = gizmoColor;
                Gizmos.DrawCube(transform.position, size);
            }
        }
    }
}