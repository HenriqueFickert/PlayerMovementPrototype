using AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public abstract class AIBehaviour : MonoBehaviour
    {
        public abstract void PerformAction(AIEnemy enemyAI);
    }
}