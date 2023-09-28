using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AI
{
    public class AIMeleeAttackDetector : MonoBehaviour
    {
        public bool PlayerDetected { get; private set; }

        [SerializeField]
        [Range(-2f, 2f)]
        private float radius;

        [SerializeField]
        private LayerMask targetLayer;

        public UnityEvent<GameObject> OnPlayerDetected;

        [Header("Gizmos parameters")]
        [SerializeField]
        private Color gizmosColor = Color.green;
        [SerializeField]
        public bool showGizmos = true;

        private void Update()
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, targetLayer);
            PlayerDetected = collider != null;

            if (PlayerDetected)
                OnPlayerDetected?.Invoke(collider.gameObject);
        }

        private void OnDrawGizmos()
        {
            if (showGizmos)
            {
                Gizmos.color = gizmosColor;
                Gizmos.DrawSphere(transform.position, radius);
            }
        }
    }
}