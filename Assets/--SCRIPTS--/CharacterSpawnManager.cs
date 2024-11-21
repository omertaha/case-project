using System.Collections.Generic;
using UnityEngine;


namespace Managers
{
    public class CharacterSpawnManager : Manager<CharacterSpawnManager>
    {
        [SerializeField] private GameObject _aiPrefab;
        [SerializeField] private List<Transform> _aiSpawnPoints;
        [SerializeField] private int _numberOfAIsToSpawn;

        private void Start()
        {
            SpawnAICharacters();
        }

        private void SpawnAICharacters()
        {
            if (_aiPrefab == null || _aiSpawnPoints.Count == 0)
            {
                Debug.LogWarning("AI prefab or spawn points are not assigned.");
                return;
            }

            for (int i = 0; i < _numberOfAIsToSpawn; i++)
            {
                Transform spawnPoint = _aiSpawnPoints[i % _aiSpawnPoints.Count];//Using mod so if there aren't enough spawn points.
                Instantiate(_aiPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }
}

