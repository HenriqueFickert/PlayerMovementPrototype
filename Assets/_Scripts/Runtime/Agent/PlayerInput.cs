using System;
using UnityEngine;
using UnityEngine.Events;

namespace StatePattern
{
    public class PlayerInput : MonoBehaviour, IAgentInput
    {
        [field: SerializeField]
        public Vector2 MovementVector { get; private set; }

        public event Action OnAttack, OnJumpPressed, OnJumpReleased, OnWeaponChange, OnDash;

        public event Action<Vector2> OnMovement;

        public KeyCode jumpKey, attackKey, weaponSwapKey, dashKey, menuKey;

        public UnityEvent OnMenuKeyPressed;

        private void Update()
        {
            if (Time.timeScale > 0)
            {
                GetMovementInput();
                GetJumpInput();
                GetAttackInput();
                GetWeaponSwapInput();
                GetDashInput();
            }

            GetMenuInput();
        }

        private void GetMovementInput()
        {
            MovementVector = GetMovementVector();
            OnMovement?.Invoke(MovementVector);
        }

        private void GetJumpInput()
        {
            if (Input.GetKeyDown(jumpKey))
            {
                OnJumpPressed?.Invoke();
            }

            if (Input.GetKeyUp(jumpKey))
            {
                OnJumpReleased?.Invoke();
            }
        }

        private void GetDashInput()
        {
            if (Input.GetKeyDown(dashKey))
            {
                OnDash?.Invoke();
            }
        }

        private void GetAttackInput()
        {
            if (Input.GetKeyDown(attackKey))
            {
                OnAttack?.Invoke();
            }
        }

        private void GetWeaponSwapInput()
        {
            if (Input.GetKeyDown(weaponSwapKey))
            {
                OnWeaponChange?.Invoke();
            }
        }

        private void GetMenuInput()
        {
            if (Input.GetKeyDown(menuKey))
            {
                OnMenuKeyPressed?.Invoke();
            }
        }

        protected Vector2 GetMovementVector()
        {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }
}