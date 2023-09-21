using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RespawnSystem
{
    public class RespawnPointManager : MonoBehaviour
    {
        List<RespawnPoint> respawnPoints = new();
        RespawnPoint currentRespawnPoint;

        private void Awake()
        {
            foreach(Transform item in transform)
            {
                respawnPoints.Add(item.GetComponent<RespawnPoint>());
            }

            currentRespawnPoint = respawnPoints.First();
        }

        public void UpdateRespawnPoint(RespawnPoint newRespawnPoint)
        {
            currentRespawnPoint.DisableRespawnPoint();
            currentRespawnPoint = newRespawnPoint;
        }

        public void Respawn(GameObject objectToRespawn)
        {
            currentRespawnPoint.RespawnPlayer();
            objectToRespawn.SetActive(true);
        }

        public void RespawnAt(RespawnPoint spawnPoint, GameObject playerGO)
        {
            spawnPoint.SetPlayerGO(playerGO);
            Respawn(playerGO);
        }

        public void ResetAllSpawnPoints()
        {
            foreach (RespawnPoint item in respawnPoints)
            {
                item.ResetRespawnPoint();
            }

            currentRespawnPoint = respawnPoints.First();
        }
    }

}
