using Codice.CM.Client.Differences;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class RotateObject : MonoBehaviour
    {
        public void Rotate(Transform objectTransform, float rotationSpeed, Vector2 movementDirection)
        {
            objectTransform.rotation *= Quaternion.Euler(0, 0, Time.deltaTime * rotationSpeed * -movementDirection.x);
        }
    }
}