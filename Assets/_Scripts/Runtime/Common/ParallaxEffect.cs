using UnityEngine;

namespace Scenario
{
    public class ParallaxEffect : MonoBehaviour
    {
        public Camera mainCamera;

        [Range(0, 1)]
        public float movementSpeed = 0.3f;

        private void Awake()
        {
            mainCamera = mainCamera == null ? Camera.main : mainCamera;
        }

        private void Update()
        {
            transform.position = new Vector2(mainCamera.transform.position.x * movementSpeed, 0);
        }
    }
}