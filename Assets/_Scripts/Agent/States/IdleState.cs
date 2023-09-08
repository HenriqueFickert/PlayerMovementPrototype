using UnityEngine;

namespace StatePattern
{
    public class IdleState : State
    {
        protected override void EnterState()
        {
            agent.animationManager.PlayAnimation(EAgentState.Idle);
        }

        protected override void HandleMovement(Vector2 input)
        {
            if (Mathf.Abs(input.x) > 0)
            {
                agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Move));
            }
        }
    }
}