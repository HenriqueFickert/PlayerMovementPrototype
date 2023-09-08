using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class Agent : MonoBehaviour
    {
        public AgentData agentData;

        public Rigidbody2D rb2d;
        public PlayerInput agentInput;
        public AgentAnimation animationManager;
        public AgentRenderer agentRenderer;

        public State currentState = null, previousState = null;
        public State IdleSate;

        public StateFactory stateFactory;

        [Header("State Debugging:")]
        public string stateName = "";

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            agentInput = GetComponentInParent<PlayerInput>();
            animationManager = GetComponentInChildren<AgentAnimation>();
            agentRenderer = GetComponentInChildren<AgentRenderer>();
        }

        private void Start()
        {
            foreach (State state in GetComponentsInChildren<State>())
            {
                state.InitializeState(this);
            }

            agentInput.OnMovement += agentRenderer.FaceDirection;
            TransitionToState(IdleSate);
        }

        public void TransitionToState(State desiredState)
        {
            if (desiredState == null)
                return;

            if (currentState != null)
                currentState.Exit();

            previousState = currentState;
            currentState = desiredState;
            currentState.Enter();

            DisplayState();
        }

        private void DisplayState()
        {
            if (previousState == null || previousState.GetType() != currentState.GetType())
            {
                stateName = currentState.GetType().ToString();
            }
        }

        private void Update()
        {
            currentState.StateUpdate();
        }

        private void FixedUpdate()
        {
            currentState.StateFixedUpdate();
        }
    }
}