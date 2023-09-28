using StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AIPatrollingBrain : AIEnemy
    {
        public GroundDetector agentGroundDetector;

        public AIBehaviour attackBehaviour, patrolBehaviour;

        private void Awake()
        {
            if (agentGroundDetector == null)
                agentGroundDetector = GetComponentInChildren<GroundDetector>();
        }

        private void Update()
        {
            if (agentGroundDetector.isGrounded)
            {
                attackBehaviour.PerformAction(this);
                patrolBehaviour.PerformAction(this);
            }
        }
    }
}