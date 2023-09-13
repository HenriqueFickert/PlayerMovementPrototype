using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerChecker : MonoBehaviour
{
    public float timer;

    public bool CheckTimer(float timer, float time)
    {
        timer += Time.deltaTime;
        if (timer < time)
            return false;

        timer = 0;
        return true;
    } 
}
