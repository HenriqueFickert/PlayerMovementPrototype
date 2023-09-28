using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AIBehaviourMeleeAttack : AIBehaviour
    {
        public AIMeleeAttackDetector meleeAttackRangeDetector;

        [SerializeField]
        private bool isWaiting;

        [SerializeField]
        private float Delay = 1;

        private void Awake()
        {
            if (meleeAttackRangeDetector == null)
                meleeAttackRangeDetector = transform.parent.GetComponentInChildren<AIMeleeAttackDetector>();
        }

        public override void PerformAction(AIEnemy enemyAI)
        {
            if (isWaiting || !meleeAttackRangeDetector.PlayerDetected)
                return;

            enemyAI.CallAttack();
            isWaiting = true;
            StartCoroutine(AttackDelayCoroutine());
        }

        IEnumerator AttackDelayCoroutine()
        {
            yield return new WaitForSeconds(Delay);
            isWaiting = false;
        }
    }
}