using System;
using UnityEngine;

namespace StatePattern
{
    public class StateFactory : MonoBehaviour
    {
        [SerializeField]
        private State Idle, Move, Fall, Climbing, Hit, Jump, Attack, Die, Dash;

        public State GetAppropriateState(EAgentState stateType)
            => stateType switch
            {
                EAgentState.Idle => Idle,
                EAgentState.Move => Move,
                EAgentState.Fall => Fall,
                EAgentState.Climb => Climbing,
                EAgentState.Hit => Hit,
                EAgentState.Jump => Jump,
                EAgentState.Attack => Attack,
                EAgentState.Die => Die,
                EAgentState.Dash => Dash,
                _ => throw new Exception("State not defined for " + stateType.ToString() + " state type enum.")
            };

        public void InitializeStates(Agent agent)
        {
            foreach (State state in GetComponents<State>())
                state.InitializeState(agent);
        }
    }
}