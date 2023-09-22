using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RespawnSystem
{
    public class RespawnHelper : MonoBehaviour
    {
        private RespawnPointManager respawnPointManager;

        private void Awake()
        {
            respawnPointManager = FindAnyObjectByType<RespawnPointManager>();
        }

        public void RespawnPlyer()
        {
            respawnPointManager.Respawn(gameObject);
        }

        public void ResetPlayer()
        {
            respawnPointManager.ResetAllSpawnPoints();
            respawnPointManager.Respawn(gameObject);
        }
    }
}