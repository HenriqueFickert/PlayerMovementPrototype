using UnityEngine;

namespace StatePattern
{
    public class AgentCooldownManager : MonoBehaviour
    {
        private Agent agent;
        public DashCooldown dashCooldown;

        private void Awake()
        {
            agent = GetComponent<Agent>();
            dashCooldown = new DashCooldown(agent);
        }

        public void Update()
        {
            dashCooldown.CheckCooldown();
        }
    }
}