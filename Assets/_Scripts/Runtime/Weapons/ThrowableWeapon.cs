using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class ThrowableWeapon : MonoBehaviour
    {
 
        private RangeWeaponData data;
        private bool isInitialized = false;
        private Vector2 startPosition = Vector2.zero;
        private Vector2 movementDirection;

        [SerializeField]
        private Rigidbody2D rb2d;

        [SerializeField]
        private Transform spriteTransform;
        [SerializeField]
        private float rotationSpeed = 1;

        [SerializeField]
        private RotateObject rotateObject;

        [Header("Collision detection data")]
        [SerializeField]
        private Vector2 center = Vector2.zero;
        [SerializeField]
        [Range(0.1f, 1f)]
        private float radius = 1;
        [SerializeField]
        private Color gizmoColor = Color.red;
        private LayerMask layerMask;

        private void Awake()
        {
            rotateObject = GetComponent<RotateObject>();
            rb2d = GetComponent<Rigidbody2D>();
            if (spriteTransform == null)
                spriteTransform = transform.GetChild(0);
        }

        private void Start()
        {
            startPosition = transform.position;
        }

        public void Initialize(RangeWeaponData data, Vector2 direction, LayerMask mask)
        {
            this.data = data;
            movementDirection = direction;
            isInitialized = true;
            rb2d.velocity = movementDirection * data.weaponThrowSpeed;
            layerMask = mask;
        }

        private void Update()
        {
            if (isInitialized)
            {
                rotateObject.Rotate(spriteTransform, rotationSpeed, movementDirection);
                DetectCollision();
                if (((Vector2)transform.position - startPosition).magnitude >= data.attackRange)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void DetectCollision()
        {
            Collider2D collision = Physics2D.OverlapCircle((Vector2)transform.position + center, radius, layerMask);

            if (collision != null)
            {
                foreach (IHittable hittable in collision.GetComponents<IHittable>())
                {
                    hittable.GetHit(gameObject, data.weaponDamage);
                }
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position + (Vector3)center, radius);
        }
    }
}