using Utils;

namespace StatePattern
{
    public abstract class StateCooldown
    {
        protected Agent agent;
        protected TimerChecker timerChecker = new();

        protected bool isAvailable;
        protected bool isReady;

        public StateCooldown(Agent agent)
        {
            this.agent = agent;
        }

        public abstract bool CheckCooldown();

        protected virtual void CheckTimer(float cooldownTime)
        {
            isReady = isReady ? isReady : timerChecker.CheckTimer(cooldownTime);
        }
    }
}