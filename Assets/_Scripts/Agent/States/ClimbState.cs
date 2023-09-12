using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class ClimbState : State
    {
        private float previousGravityScale = 0f;

        protected override void EnterState()
        {
            agent.animationManager.PlayAnimation(EAgentState.ClimbIdle);
            agent.transform.position = new (agent.climbDetector.collisionTransform.x, agent.transform.position.y);

            previousGravityScale = agent.rb2d.gravityScale;
            agent.rb2d.gravityScale = 0;
            agent.rb2d.velocity = Vector2.zero;
        }

        protected override void HandleJumpPressed()
        {
            agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Jump));
        }

        public override void StateUpdate()
        {
            if (agent.agentInput.MovementVector.magnitude > 0 && !agent.roofDetector.hasRoof)
            {
                if (agent.animationManager.IsInAnimation(EAgentState.ClimbIdle))
                    agent.animationManager.PlayAnimation(EAgentState.Climb);

                agent.rb2d.velocity = new Vector2(0, agent.agentInput.MovementVector.y * agent.agentData.climbVerticalSpeed);
            }
            else
            {
                if (agent.animationManager.IsInAnimation(EAgentState.Climb))
                    agent.animationManager.PlayAnimation(EAgentState.ClimbIdle);
               
                agent.rb2d.velocity = Vector2.zero;
            }

            if (!agent.climbDetector.CanClimb)
                agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Move));
        }

        protected override void ExitState()
        {
            agent.rb2d.gravityScale = previousGravityScale;
        }
    }
}