using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AIEndPlatformDetector : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider2D detectorCollider;
        [SerializeField]
        private LayerMask groundLayerMask;
        [SerializeField]
        private float groundRaycastLenght = 2;

        [SerializeField]
        [Range(0, 1)]
        private float groundRaycastDelay = 0.1f;

        public bool PathBlocked { get; private set; }

        public event Action OnPathBlocked;

        [Header("Gizmo Parameters")]
        [SerializeField]
        private Color colliderColor = Color.magenta;
        [SerializeField]
        private Color groudRaycastColor = Color.blue;
        [SerializeField]
        private bool showGizmos = true;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsInLayerMask(collision.gameObject.layer, groundLayerMask))
                OnPathBlocked?.Invoke();
        }

        bool IsInLayerMask(int layer, LayerMask layerMask) => layerMask == (layerMask | (1 << layer));

        private void Start()
        {
            StartCoroutine(CheckGroundCoroutine());
        }

        private IEnumerator CheckGroundCoroutine()
        {
            yield return new WaitForSeconds(groundRaycastDelay);

            RaycastHit2D hit = Physics2D.Raycast(detectorCollider.bounds.center, Vector2.down, groundRaycastLenght, groundLayerMask);

            if (hit.collider == null)
                OnPathBlocked?.Invoke();

            PathBlocked = hit.collider == null;
            StartCoroutine(CheckGroundCoroutine());
        }

        private void OnDrawGizmos()
        {
            if (showGizmos && detectorCollider != null)
            {
                Gizmos.color = groudRaycastColor;
                Gizmos.DrawRay(detectorCollider.bounds.center, Vector2.down * groundRaycastLenght);
                Gizmos.color = colliderColor;
                Gizmos.DrawCube(detectorCollider.bounds.center, detectorCollider.bounds.size);
            }
        }
    }
}