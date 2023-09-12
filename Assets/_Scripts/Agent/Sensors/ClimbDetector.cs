using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StatePattern
{
    public class ClimbDetector : MonoBehaviour
    {
        [SerializeField]
        private LayerMask climbLayerMask;

        [SerializeField]
        private bool canClimb;

        public bool CanClimb
        {
            get { return canClimb; }
            private set { canClimb = value; }
        }

        [HideInInspector]
        public Vector2 collisionTransform; 

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (CheckCollision(collision))
            {
                collisionTransform = collision.GetComponent<TilemapCollider2D>().bounds.center;
                CanClimb = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (CheckCollision(collision))
            {
                collisionTransform = Vector2.zero;
                CanClimb = false;
            }
        }

        private bool CheckCollision(Collider2D collision)
        {
            LayerMask collisionLayerMask = 1 << collision.gameObject.layer;
            return (collisionLayerMask & climbLayerMask) != 0;
        }
    }
}