using System;
using System.Data;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem;

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
        public AgentWeaponManager agentWeaponManager;

        [HideInInspector]
        public State currentState = null, previousState = null;

        public StateFactory stateFactory;
        public Damagable damagable;
        public AgentCooldownManager agentCooldownManager;

        [Header("State Debugging:")]
        public string stateName = "";

        [field: SerializeField]
        private UnityEvent OnRespawnRequired { get; set; }

        [field: SerializeField]
        public UnityEvent OnAgentDied { get; set; }

        private void Awake()
        {
            agentInput = GetComponentInParent<PlayerInput>();
            rb2d = GetComponent<Rigidbody2D>();
            agentWeaponManager = GetComponentInChildren<AgentWeaponManager>();
            agentCooldownManager = GetComponent<AgentCooldownManager>();
            animationManager = GetComponentInChildren<AgentAnimation>();
            agentRenderer = GetComponentInChildren<AgentRenderer>();
            groundDetector = GetComponentInChildren<GroundDetector>();
            climbDetector = GetComponentInChildren<ClimbDetector>();
            roofDetector = GetComponentInChildren<RoofDetector>();
            damagable = GetComponent<Damagable>();
        }

        private void Start()
        {
            stateFactory.InitializeStates(this);
            InitializeAgent();
        }

        private void InitializeAgent()
        {
            TransitionToState(IdleSate);
            damagable.Initialize(agentData.health);
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
            if (damagable.CurrentHealth > 0)
                OnRespawnRequired?.Invoke();
            else
                currentState.Die(); 
        }

        public void GetHit()
        {
            currentState.GetHit();
        }
    }
}