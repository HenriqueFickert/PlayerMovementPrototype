using System;
using UnityEngine;
using UnityEngine.Events;

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
        public AgentCooldownManager agentCooldownManager;

        [Header("State Debugging:")]
        public string stateName = "";

        [field: SerializeField]
        private UnityEvent OnRespawnRequired { get; set; }

        private void Awake()
        {
            agentInput = GetComponentInParent<PlayerInput>();
            rb2d = GetComponent<Rigidbody2D>();
            agentCooldownManager = GetComponent<AgentCooldownManager>();
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
            currentState.StateUpdate();
        }

        private void FixedUpdate()
        {
            roofDetector.CheckRoof();
            groundDetector.CheckIsGrounded();
            currentState.StateFixedUpdate();
        }

        public void AgentDied()
        {
            OnRespawnRequired?.Invoke();
        }
    }
}