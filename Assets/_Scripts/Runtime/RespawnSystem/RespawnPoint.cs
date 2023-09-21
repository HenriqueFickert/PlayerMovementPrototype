using UnityEngine;
using UnityEngine.Events;

namespace RespawnSystem
{
    public class RespawnPoint : MonoBehaviour
    {
        private GameObject respawnTarget;

        [SerializeField]
        private Transform spawnPoint;

        [field: SerializeField]
        private UnityEvent OnSpawnPointActivated { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                respawnTarget = collision.gameObject;
                OnSpawnPointActivated?.Invoke();
                
            }
        }

        public void RespawnPlayer()
        {
            respawnTarget.transform.position = spawnPoint.position;
        }

        public void SetPlayerGO(GameObject player)
        {
            respawnTarget = player;
            GetComponent<Collider2D>().enabled = false;
        }

        public void DisableRespawnPoint()
        {
            GetComponent<Collider2D>().enabled = false;
        }

        public void ResetRespawnPoint()
        {
            respawnTarget = null;
            GetComponent<Collider2D>().enabled = true;
        }
    }
}