using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StatePattern
{
    public class AttackState : State
    {
        [SerializeField]
        public LayerMask hittableLayerMask;

        protected Vector2 direction;

        public UnityEvent<AudioClip> OnWeaponSound;

        [SerializeField]
        private bool showGizmo = true;

        protected override void EnterState()
        {
            agent.animationManager.ResetEvents();
            agent.animationManager.PlayAnimation(EAgentState.Attack);
            agent.animationManager.OnAnimationEnd.AddListener(TransitionToStateOnExit);
            agent.animationManager.OnAnimationAction.AddListener(PerformAttack);

            agent.agentWeaponManager.ToggleWeaponVisibility(true);
            direction = agent.transform.right * (agent.transform.localScale.x > 0 ? 1 : -1);

            if (agent.groundDetector.isGrounded)
                agent.rb2d.velocity = Vector2.zero;
        }

        private void PerformAttack()
        {
            OnWeaponSound?.Invoke(agent.agentWeaponManager.GetCurrentWeapon().weaponSwingSound);
            agent.animationManager.OnAnimationAction.RemoveListener(PerformAttack);
            agent.agentWeaponManager.GetCurrentWeapon().PerformAttack(agent, hittableLayerMask, direction);
        }

        private void TransitionToStateOnExit()
        {
            agent.animationManager.OnAnimationEnd.RemoveListener(TransitionToStateOnExit);

            if (agent.groundDetector.isGrounded)
                agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Idle));
            else
                agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Fall));
        }

        protected override void ExitState()
        {
            agent.agentWeaponManager.ToggleWeaponVisibility(false);
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying || !showGizmo) return;

            Gizmos.color = Color.red;

            Vector3 position = agent.agentWeaponManager.transform.position;

            agent.agentWeaponManager.GetCurrentWeapon().DrawWeaponGizmo(position, direction);
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