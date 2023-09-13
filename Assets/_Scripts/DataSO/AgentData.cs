using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    [CreateAssetMenu(fileName = "AgentData", menuName = "Agent/AgentData")]
    public class AgentData : ScriptableObject
    {
        [Header("Basic data")]
        public int health = 2;

        [Header("Movement data")]
        [Space]
        public float maxSpeed = 5;

        public float acceleration = 50;
        public float deacceleration = 50;

        [Header("Jump data")]
        [Space]
        public float jumpSpeed = 10;

        public float gravityModifier = 3.5f;
        public float lowJumpMultiplier = 5f;

        [Header("Climb data")]
        [Space]
        public float climbVerticalSpeed = 5;

        public float climbHorizontalSpeed = 2;

        [Header("Dash data")]
        [Space]
        public float dashForce = 10;
        public float dashTime = 1;
        public float dashCooldown = 1;
    }
}