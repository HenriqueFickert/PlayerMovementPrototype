using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class DieState : State
    {
        public float timeToWaitBeforeDieAction = 2;

        protected override void EnterState()
        {
            agent.animationManager.PlayAnimation(EAgentState.Die);
            agent.animationManager.OnAnimationEnd.AddListener(WaitBeforeDieAction);
        }

        private void WaitBeforeDieAction()
        {
            agent.animationManager.OnAnimationEnd.RemoveListener(WaitBeforeDieAction);
            StartCoroutine(WaitToDieAction());
        }

        IEnumerator WaitToDieAction()
        {
            yield return new WaitForSeconds(timeToWaitBeforeDieAction);
            agent.OnAgentDied?.Invoke();
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

        public override void Die()
        {
            //Disable die
        }

        public override void StateUpdate()
        {
            agent.rb2d.velocity = new Vector2(0, agent.rb2d.velocity.y);
        }

        public override void StateFixedUpdate()
        {
            //Disable fixedupdate
        }

        protected override void ExitState()
        {
            StopAllCoroutines();
            agent.animationManager.ResetEvents();
        }
    }
}