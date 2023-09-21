using UnityEngine;

namespace StatePattern
{
    public class IdleState : State
    {
        protected override void EnterState()
        {
            agent.animationManager.PlayAnimation(EAgentState.Idle);

            agent.rb2d.isKinematic = true;
            if (agent.groundDetector.isGrounded)
                agent.rb2d.velocity = Vector2.zero;
        }

        protected override void HandleMovement(Vector2 input)
        {
            if (agent.roofDetector.hasRoof && agent.climbDetector.CanClimb && agent.roofDetector.hasRoof && Mathf.Abs(input.y) > 0)
            {
                agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Climb));
            }

            if (Mathf.Abs(input.x) > 0)
            {
                agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Move));
            }
        }

        protected override void ExitState()
        {
            agent.rb2d.isKinematic = false;
        }
    }
}