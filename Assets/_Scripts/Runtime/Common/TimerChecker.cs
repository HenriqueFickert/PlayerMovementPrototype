using UnityEngine;

namespace Common
{
    public class TimerChecker
    {
        private float elapsedTime = 0;

        public bool CheckTimer(float interval)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime < interval)
                return false;

            elapsedTime = 0;
            return true;
        }
    }
}