using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class DashState : State
    {
        [SerializeField]
        protected MovementData movementData;

        private float previousGravityScale = 0f;

        private Vector2 direction;

        private void Awake()
        {
            movementData = GetComponentInParent<MovementData>();
        }

        protected override void EnterState()
        {
            agent.animationManager.PlayAnimation(EAgentState.Dash);

            previousGravityScale = agent.rb2d.gravityScale;
            agent.rb2d.gravityScale = 0;

            direction = agent.transform.right * (agent.transform.localScale.x > 0 ? 1 : -1);

            movementData.currentVelocity = agent.rb2d.velocity;
            movementData.currentVelocity = new Vector2(agent.agentData.dashForce, 0) * direction;
            agent.rb2d.velocity = movementData.currentVelocity;
        }

        public override void StateUpdate()
        {
            StartCoroutine(Dash());
        }

        private IEnumerator Dash()
        {
            yield return new WaitForSeconds(agent.agentData.dashTime);
            agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Move));
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