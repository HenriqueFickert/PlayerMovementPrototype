using UnityEngine;

namespace StatePattern
{
    public class MovementState : State
    {
        [SerializeField]
        protected MovementData movementData;

        private void Awake()
        {
            movementData = GetComponentInParent<MovementData>();
        }

        protected override void EnterState()
        {
            agent.animationManager.PlayAnimation(EAgentState.Move);

            movementData.horizontalMovementDirection = 0;
            movementData.currentSpeed = 0;
            movementData.currentVelocity = Vector2.zero;
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            CalCulateVelocity();
            SetPlayerVelocity();

            if (Mathf.Abs(agent.rb2d.velocity.x) < 0.01f)
                agent.TransitionToState(agent.stateFactory.GetAppropriateState(EAgentState.Idle));
        }

        private void SetPlayerVelocity()
        {
            agent.rb2d.velocity = movementData.currentVelocity;
        }

        private void CalCulateVelocity()
        {
            CalculateSpeed(agent.agentInput.MovementVector, movementData);
            CalculateHorizontalDirection(movementData);
            movementData.currentVelocity = Vector3.right * movementData.horizontalMovementDirection * movementData.currentSpeed;
            movementData.currentVelocity.y = agent.rb2d.velocity.y;
        }

        private void CalculateHorizontalDirection(MovementData movementData)
        {
            if (agent.agentInput.MovementVector.x == 0)
                return;

            movementData.horizontalMovementDirection = agent.agentInput.MovementVector.x < 0 ? -1 : 1;        
        }

        private void CalculateSpeed(Vector2 movementVector, MovementData movementData)
        {
            if (Mathf.Abs(movementVector.x) > 0)
                movementData.currentSpeed += agent.agentData.acceleration * Time.deltaTime;
            else
                movementData.currentSpeed -= agent.agentData.deacceleration * Time.deltaTime;

            movementData.currentSpeed = Mathf.Clamp(movementData.currentSpeed, 0, agent.agentData.maxSpeed);
        }
    }
}