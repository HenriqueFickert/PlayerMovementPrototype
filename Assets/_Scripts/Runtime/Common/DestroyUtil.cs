using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class DestroyUtil : MonoBehaviour
    {
        public void DestoySelf()
        {
            DestroyObject(gameObject);
        }

        public void DestroyObject(GameObject gameObject)
        {
            Destroy(gameObject);
        }
    }
}
