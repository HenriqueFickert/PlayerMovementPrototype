using UnityEngine;

namespace StatePattern
{
    public class DashCooldown : StateCooldown
    {
        private bool touchedGround = true;

        public DashCooldown(Agent agent) : base(agent)
        {
        }

        public bool IsAvailable
        {
            get { return isAvailable; }

            private set
            {
                isAvailable = value;
                if (!isAvailable)
                {
                    touchedGround = false;
                    isReady = false;
                }
            }
        }

        public override bool CheckCooldown()
        {
            if (isAvailable)
                return true;

            if (agent.groundDetector.isGrounded)
                touchedGround = true;

            CheckTimer(agent.agentData.dashCooldown);
            return isAvailable = isReady && touchedGround;
        }

        public void DashUsed()
        {
            IsAvailable = false;
        }
    }
}