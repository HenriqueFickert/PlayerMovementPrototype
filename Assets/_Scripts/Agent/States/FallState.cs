using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class FallState : MovementState
    {

        protected override void EnterState()
        {
            agent.animationManager.PlayAnimation(EAgentState.Fall);
        }

        protected override void HandleJumpPressed()
        {
            // Disable Jump Again
        }

        public override void StateUpdate()
        {
            movementData.currentVelocity = agent.rb2d.velocity;
            movementData.currentVelocity.y += agent.agentData.gravityModifier * Physics2D.gravity.y * Time.deltaTime;
            agent.rb2d.velocity = movementData.currentVelocity;

            CalCulateVelocity();
            SetPlayerVelocity();

            if (agent.groundDetector.isGrounded)
                agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Move));

            if (agent.climbDetector.CanClimb && Mathf.Abs(agent.agentInput.MovementVector.y) > 0)
                agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Climb));
        }
    }
}
