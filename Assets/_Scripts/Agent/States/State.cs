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

            OnExit?.Invoke();
            ExitState();
        }

        protected virtual void ExitState()
        { }

        public virtual void StateUpdate()
        {
            TesteFallTrasition();
        }

        public virtual void StateFixedUpdate()
        { }

        protected virtual void HandleMovement(Vector2 vector2)
        { }

        protected virtual void HandleJumpPressed()
        {
            TestJumpTransition();
        }

        protected virtual void HandleJumpReleased()
        { }

        protected virtual void HandleAttack()
        { }

        protected bool TesteFallTrasition()
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