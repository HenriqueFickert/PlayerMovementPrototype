using StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    public class IsGroundedCheck : MonoBehaviour
    {
        [SerializeField]
        private GroundDetector groundDetector;

        public UnityEvent OnConditionValidAction;

        public void TryPerformingAction()
        {
            if (groundDetector.isGrounded)
            {
                OnConditionValidAction?.Invoke();
            }
        }
    }
}