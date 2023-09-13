using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class TimerChecker
    {
        private float elapsedTime = 0;

        public bool CheckTime(float interval)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime < interval)
                return false;

            elapsedTime = 0;
            return true;
        }
    }
}