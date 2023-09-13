using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class Agent : MonoBehaviour
    {
        [Header("Agent Data:")]
        public AgentData agentData;

        [Header("Inputs:")]
        public PlayerInput agentInput;

        public GroundDetector groundDetector;
        public ClimbDetector climbDetector;
        public RoofDetector roofDetector;

        public bool canDash = true;
        public float dashTime;

        [Header("Components:")]
        [HideInInspector]
        public Rigidbody2D rb2d;

        [HideInInspector]
        public AgentRenderer agentRenderer;

        [HideInInspector]
        public AgentAnimation animationManager;

        [Header("States:")]
        [SerializeField]
        private State IdleSate;

        [HideInInspector]
        public State currentState = null, previousState = null;

        public StateFactory stateFactory;

        [Header("State Debugging:")]
        public string stateName = "";

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            agentInput = GetComponentInParent<PlayerInput>();
            animationManager = GetComponentInChildren<AgentAnimation>();
            agentRenderer = GetComponentInChildren<AgentRenderer>();
            groundDetector = GetComponentInChildren<GroundDetector>();
            climbDetector = GetComponentInChildren<ClimbDetector>();
            roofDetector = GetComponentInChildren<RoofDetector>();
        }

        private void Start()
        {
            foreach (State state in GetComponentsInChildren<State>())
                state.InitializeState(this);

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
                stateName = currentState.GetType().ToString();
        }

        private void Update()
        {
            DashInputCheck();
            currentState.StateUpdate();
        }

        private void FixedUpdate()
        {
            groundDetector.CheckIsGrounded();
            currentState.StateFixedUpdate();
        }

        private void DashInputCheck()
        {
            if (!canDash)
            {
                dashTime += Time.deltaTime;
                if (dashTime > agentData.dashCooldown && groundDetector.isGrounded)
                {
                    dashTime = 0;
                    canDash = true;
                }
            }
        }
    }
}