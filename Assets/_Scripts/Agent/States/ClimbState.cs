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
            agent.transform.position = new(agent.climbDetector.collisionTransform.x, agent.transform.position.y);

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
            if ((!agent.roofDetector.hasRoof && agent.agentInput.MovementVector.y > 0 ) || (agent.agentInput.MovementVector.y < 0 && !agent.groundDetector.isGrounded))
            {
                SwitchAnimationIfCurrent(EAgentState.ClimbIdle, EAgentState.Climb);
                agent.rb2d.velocity = new Vector2(0, agent.agentInput.MovementVector.y * agent.agentData.climbVerticalSpeed);
            }
            else
            {
                SwitchAnimationIfCurrent(EAgentState.Climb, EAgentState.ClimbIdle);
                agent.rb2d.velocity = Vector2.zero;
            }

            if (!agent.climbDetector.CanClimb || (agent.groundDetector.isGrounded && Mathf.Abs(agent.agentInput.MovementVector.x) > 0))
                agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Move));
        }

        private void SwitchAnimationIfCurrent(EAgentState currentAnimationState, EAgentState desireAnimationState)
        {
            if (agent.animationManager.IsInAnimation(currentAnimationState))
                agent.animationManager.PlayAnimation(desireAnimationState);
        }

        protected override void HandleDash()
        {
            //Disable Dash
        }

        public override void StateFixedUpdate()
        {
            agent.roofDetector.CheckRoof();
        }
        
        protected override void ExitState()
        {
            agent.rb2d.gravityScale = previousGravityScale;
        }
    }
}