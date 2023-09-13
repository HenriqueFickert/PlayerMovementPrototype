using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace StatePattern
{
    public class AgentCooldownManager : MonoBehaviour
    {
        private Agent agent;
        private TimerChecker dashTimerChecker = new();

        private void Awake()
        {
            agent = GetComponent<Agent>();
        }

        public bool DashCooldownCheck() => dashTimerChecker.CheckTime(agent.agentData.dashCooldown) && agent.groundDetector.isGrounded;
    }
}