using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace StatePattern
{
    public abstract class State : MonoBehaviour
    {
        protected Agent agent;

        public UnityEvent OnEnter, OnExit;

        public void InitializeState(Agent agent)
        {
            this.agent = agent;
        }

        public void Enter()
        {
            agent.agentInput.OnAttack += HandleAttack;
            agent.agentInput.OnJumpPressed += HandleJumpPressed;
            agent.agentInput.OnJumpReleased += HandleJumpReleased;
            agent.agentInput.OnMovement += HandleMovement;
            agent.agentInput.OnDash += HandleDash;

            OnEnter?.Invoke();
            EnterState();
        }

        protected virtual void EnterState()
        { }

        public void Exit()
        {
            agent.agentInput.OnAttack -= HandleAttack;
            agent.agentInput.OnJumpPressed -= HandleJumpPressed;
            agent.agentInput.OnJumpReleased -= HandleJumpReleased;
            agent.agentInput.OnMovement -= HandleMovement;
            agent.agentInput.OnDash -= HandleDash;

            OnExit?.Invoke();
            ExitState();
        }

        protected virtual void ExitState()
        { }

        public virtual void StateUpdate()
        {
            TestFallTrasition();
        }

        public virtual void StateFixedUpdate()
        { }

        protected virtual void HandleMovement(Vector2 vector2)
        {
            agent.agentRenderer.FaceDirection(vector2);
        }

        protected virtual void HandleJumpPressed()
        {
            TestJumpTransition();
        }

        protected virtual void HandleJumpReleased()
        { }

        protected virtual void HandleDash()
        {
            TestDashTransition();
        }

        protected virtual void HandleAttack()
        { }

        protected void TestDashTransition()
        {
            if (agent.canDash)
                agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Dash));
        }

        protected bool TestFallTrasition()
        {
            if (!agent.groundDetector.isGrounded)
                agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Fall));

            return !agent.groundDetector.isGrounded;
        }

        protected virtual void TestJumpTransition()
        {
            if (agent.groundDetector.isGrounded)
            {
                agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Jump));
            }
        }
    }
}