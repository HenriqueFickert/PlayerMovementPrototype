using System;
using UnityEngine;

namespace StatePattern
{
    public class HitState : State
    {
        protected override void EnterState()
        {
            agent.animationManager.PlayAnimation(EAgentState.Hit);
            agent.animationManager.OnAnimationEnd.AddListener(TransitionToIdle);
        }

        private void TransitionToIdle()
        {
            agent.animationManager.OnAnimationEnd.RemoveListener(TransitionToIdle);
            agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Idle));
        }

        protected override void HandleAttack()
        {
            //Disable attack
        }

        protected override void HandleJumpPressed()
        {
            //Disable jump
        }

        protected override void HandleJumpReleased()
        {
            //Disable jump
        }

        protected override void HandleMovement(Vector2 vector2)
        {
            //Disable movement
        }

        protected override void HandleDash()
        {
            //Disable dash
        }

        public override void GetHit()
        {
            //Disable getting hit 2 times
        }

        public override void StateUpdate()
        {
            //Disable update
        }

        public override void StateFixedUpdate()
        {
            //Disable fixedupdate
        }
    }
}
