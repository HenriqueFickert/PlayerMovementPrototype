using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class AgentRenderer : MonoBehaviour
    {
        public void FaceDirection(Vector2 input)
        {
            if (input.x == 0)
                return;

            int direction = input.x < 0 ? -1 : 1;
            transform.parent.localScale = new Vector3(direction * Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y, transform.parent.localScale.z);
        }
    }
}