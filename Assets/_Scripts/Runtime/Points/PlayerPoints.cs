using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerData
{
    public class PlayerPoints : MonoBehaviour
    {
        public UnityEvent<int> OnPointsValueChange;
        public UnityEvent OnPickUpPoints;
        private int points = 0;

        public int Points { get => points; set => points = value; }

        private void Start()
        {
            OnPointsValueChange?.Invoke(Points);
        }

        public void Add(int amout)
        {
            Points += amout;
            OnPickUpPoints?.Invoke();
            OnPointsValueChange?.Invoke(Points);
        }
    }
}