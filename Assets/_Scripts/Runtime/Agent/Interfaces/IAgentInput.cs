using System;
using UnityEngine;

namespace StatePattern
{
    public interface IAgentInput
    {
        public Vector2 MovementVector { get; }

        public event Action OnAttack;
        public event Action OnJumpPressed;
        public event Action OnJumpReleased;
        public event Action OnWeaponChange;
        public event Action OnDash;

        public event Action<Vector2> OnMovement;
    }
}