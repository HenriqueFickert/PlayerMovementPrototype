using UnityEngine;

namespace StatePattern
{
    public class RoofDetector : MonoBehaviour
    {
        public Collider2D agentCollider;
        public LayerMask layerMask;

        public bool hasRoof { get; private set; } = false;

        [Header("Gizmo Parameters:")]
        [Range(-2, 2)]
        public float boxCastYOffeset = -0.1f;

        [Range(-2, 2)]
        public float boxCastXOffeset = -0.1f;

        [Range(0, 2)]
        public float boxCastWidth = 1, boxCastHeight = 1;

        public Color gizmoColorNotRoof = Color.red;
        public Color gizmoColorRoof = Color.green;

        private void Awake()
        {
            if (agentCollider == null)
                agentCollider = GetComponent<Collider2D>();
        }

        public void CheckRoof()
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(agentCollider.bounds.center + new Vector3(boxCastXOffeset, boxCastYOffeset, 0),
                                            new Vector3(boxCastWidth, boxCastHeight), 0, Vector2.down, 0, layerMask);

            if (raycastHit.collider != null)
                hasRoof = true;
            else
                hasRoof = false;
        }

        private void OnDrawGizmos()
        {
            if (agentCollider == null)
                return;

            Gizmos.color = hasRoof ? gizmoColorRoof : gizmoColorNotRoof;
            Gizmos.DrawWireCube(agentCollider.bounds.center + new Vector3(boxCastXOffeset, boxCastYOffeset, 0), new Vector3(boxCastWidth, boxCastHeight));
        }
    }
}