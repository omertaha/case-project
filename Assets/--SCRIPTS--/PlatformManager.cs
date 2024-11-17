using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Managers
{
    using System.Collections.Generic;
    using UnityEngine;

    public class PlatformManager : Manager<PlatformManager>
    {
        public float platformWidth { get; private set; }//To be used to clamp the player.


        //Private variables
        [SerializeField]
        private GameObject platformPrefab;

        [SerializeField]
        private Transform player;

        [SerializeField]
        private int startingPlatformCount;

        private Queue<GameObject> platformQueue = new Queue<GameObject>();
        private float platformLength;
        private Vector3 nextSpawnPosition;

        void Start()
        {
            platformLength = platformPrefab.GetComponent<Renderer>().bounds.size.z;
            platformWidth = platformPrefab.GetComponent<Renderer>().bounds.size.x;
            nextSpawnPosition = Vector3.zero;

            for (int i = 0; i < startingPlatformCount; i++)
            {
                SpawnPlatform();
            }

            Ready?.Invoke();
        }

        void Update()
        {
            if (player.position.z > platformQueue.Peek().transform.position.z + (platformLength / 2) + 0.25f )
            {
                DeQueuePlatform();
            }
        }

        void SpawnPlatform()
        {
            GameObject newPlatform = Instantiate(platformPrefab, nextSpawnPosition, platformPrefab.transform.rotation);
            platformQueue.Enqueue(newPlatform);
            nextSpawnPosition.z += platformLength - 0.01f;
        }

        void DeQueuePlatform()
        {
            GameObject oldPlatform = platformQueue.Dequeue();
            oldPlatform.transform.position = nextSpawnPosition;
            platformQueue.Enqueue(oldPlatform);
            nextSpawnPosition.z += platformLength - 0.01f;
        }
    }

}

