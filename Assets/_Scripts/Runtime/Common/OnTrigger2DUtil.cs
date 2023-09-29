using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    public class OnTrigger2DUtil : MonoBehaviour
    {
        [SerializeField]
        private LayerMask collisionMask;

        [SerializeField]
        private UnityEvent OnTriggerEnterEvent, OnTriggerExitEvent;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((1 << collision.gameObject.layer & collisionMask) != 0)
            {
                OnTriggerEnterEvent?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if ((1 << collision.gameObject.layer & collisionMask) != 0)
            {
                OnTriggerExitEvent?.Invoke();
            }
        }
    }
}