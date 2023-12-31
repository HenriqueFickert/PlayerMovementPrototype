using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class StopRb2dUtil : MonoBehaviour
    {
        private Rigidbody2D rb2d;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        public void StopMovement()
        {
            rb2d.velocity = Vector2.zero;
        }
    }
}