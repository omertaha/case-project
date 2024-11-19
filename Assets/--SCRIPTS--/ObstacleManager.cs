using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Managers
{
    using System.Collections.Generic;
    using UnityEngine;

    public class ObstacleManager : Manager<ObstacleManager>
    {
        //Private variables
        [SerializeField]
        private GameObject _platform;

        [SerializeField]
        private GameObject _obstacleContainerPrefab;//Obstacle container

        [SerializeField]
        private Transform player;

        private Queue<GameObject> platformQueue = new Queue<GameObject>();
        private float _platformLength;
        private Vector3 nextSpawnPosition;

        void OnEnable()
        {
            _platformLength = _platform.GetComponent<Renderer>().bounds.size.z;
            nextSpawnPosition = Vector3.zero;

            SpawnObstacles();
        }


        void SpawnObstacles()
        {
            float lengthInBetween = Random.Range(0.5f, 2.5f);
            int obstacleCount = Mathf.FloorToInt(_platformLength / lengthInBetween);
            for (int i = 0; i < obstacleCount; i++)
            {
                GameObject newPlatform = Instantiate(_obstacleContainerPrefab, nextSpawnPosition, _obstacleContainerPrefab.transform.rotation);
                platformQueue.Enqueue(newPlatform);
                nextSpawnPosition.z += lengthInBetween;
            }
            Ready?.Invoke();
        }
    }

}

