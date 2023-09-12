using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class JumpState : MovementState
    {
        private bool jumpPressed = false;

        protected override void EnterState()
        {
            agent.animationManager.PlayAnimation(EAgentState.Jump);

            movementData.currentVelocity = agent.rb2d.velocity;
            movementData.currentVelocity.y = agent.agentData.jumpSpeed;
            agent.rb2d.velocity = movementData.currentVelocity;
            jumpPressed = true;
        }

        protected override void HandleJumpPressed()
        {
            jumpPressed = true;
        }

        protected override void HandleJumpReleased()
        {
            jumpPressed = false;
        }

        public override void StateUpdate()
        {
            ControlJumpHeight();
            CalCulateVelocity();
            SetPlayerVelocity();

            if (agent.rb2d.velocity.y <= 0)
                agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Fall));

            if (agent.climbDetector.CanClimb && Mathf.Abs(agent.agentInput.MovementVector.y) > 0)
                agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Climb));
        }

        private void ControlJumpHeight()
        {
            if (!jumpPressed)
            {
                movementData.currentVelocity = agent.rb2d.velocity;
                movementData.currentVelocity.y += agent.agentData.lowJumpMultiplier * Physics2D.gravity.y * Time.deltaTime;
                agent.rb2d.velocity = movementData.currentVelocity;
            }
        }
    }
}