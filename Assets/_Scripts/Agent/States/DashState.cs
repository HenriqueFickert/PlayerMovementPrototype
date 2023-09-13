using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace StatePattern
{
    public class DashState : State
    {
        [SerializeField]
        protected MovementData movementData;

        private float previousGravityScale = 0f;
        private TimerChecker timerChecker = new();
        private Vector2 direction;

        private void Awake()
        {
            movementData = GetComponentInParent<MovementData>();
        }

        protected override void EnterState()
        {
            agent.agentCooldownManager.dashCooldown.IsAvailable = false;
            agent.animationManager.PlayAnimation(EAgentState.Dash);
            direction = agent.transform.right * (agent.transform.localScale.x > 0 ? 1 : -1);

            previousGravityScale = agent.rb2d.gravityScale;
            agent.rb2d.gravityScale = 0;

            movementData.currentVelocity = agent.rb2d.velocity;
            movementData.currentVelocity = new Vector2(agent.agentData.dashForce, 0) * direction;
            agent.rb2d.velocity = movementData.currentVelocity;
        }

        public override void StateUpdate()
        {
            Dash();
        }

        private void Dash() {

            if(timerChecker.CheckTimer(agent.agentData.dashTime))
                agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Idle));
        }

        protected override void HandleJumpPressed()
        {
            //Disable jump
        }

        protected override void HandleMovement(Vector2 vector2)
        {
            //disable walk
        }

        protected override void HandleDash()
        {
            //disable dash
        }

        protected override void ExitState()
        {
            agent.rb2d.gravityScale = previousGravityScale;
        }
    }
}