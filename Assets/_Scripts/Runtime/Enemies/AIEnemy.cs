using StatePattern;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public abstract class AIEnemy : MonoBehaviour, IAgentInput
    {
        public Vector2 MovementVector { get; set; }

        public event Action OnAttack;
        public event Action OnJumpPressed;
        public event Action OnJumpReleased;
        public event Action OnWeaponChange;
        public event Action OnDash;
        public event Action<Vector2> OnMovement;

        public void CallAttack()
        {
            OnAttack?.Invoke();
        }

        public void CallJumpPressed()
        {
            OnJumpPressed?.Invoke();
        }

        public void CallJumpReleased()
        {
            OnJumpReleased?.Invoke();
        }

        public void CallWeaponChange()
        {
            OnWeaponChange?.Invoke();
        }

        public void CallDash()
        {
            OnDash?.Invoke();
        }

        public void CallMovement(Vector2 input)
        {
            OnMovement?.Invoke(input);
        }
    }
}