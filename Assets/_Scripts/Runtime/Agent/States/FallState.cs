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
            //Disable jump again
        }

        public override void StateUpdate()
        {
            movementData.currentVelocity = agent.rb2d.velocity;
            movementData.currentVelocity.y += agent.agentData.gravityModifier * Physics2D.gravity.y * Time.deltaTime;
            movementData.currentVelocity.y = Mathf.Clamp(movementData.currentVelocity.y, agent.agentData.maxFallSpeed, 0);

            agent.rb2d.velocity = movementData.currentVelocity;

            CalCulateVelocity();
            SetAgentVelocity();

            if (agent.groundDetector.isGrounded)
                agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Move));

            if (agent.roofDetector.hasRoof && agent.roofDetector.hasRoof && agent.climbDetector.CanClimb && Mathf.Abs(agent.agentInput.MovementVector.y) > 0)
                agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Climb));
        }
    }
}