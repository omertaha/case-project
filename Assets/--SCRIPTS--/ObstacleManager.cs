using UnityEngine;

/// <summary>
/// First I started with a queue system for multiple platforms. However that didn't give me enough flexibility in terms of obstacles.
/// So I changed for a fixed platform and random obstacles.
/// </summary>
namespace Managers
{
    public class ObstacleManager : Manager<ObstacleManager>
    {
        //Private variables
        [SerializeField]
        private GameObject _platform;

        [SerializeField]
        private GameObject _obstacleContainerPrefab;//Obstacle container

        [SerializeField]
        private Transform player;

        //private Queue<GameObject> platformQueue = new Queue<GameObject>();
        private float _platformLength;
        private Vector3 nextSpawnPosition;

        void OnEnable()
        {
            _platformLength = _platform.GetComponent<Renderer>().bounds.size.z;
            nextSpawnPosition = new Vector3(0,0,2);

            SpawnObstacles();
        }


        void SpawnObstacles()
        {
            float lengthInBetween = UnityEngine.Random.Range(1f, 2f);
            int obstacleCount = Mathf.FloorToInt(_platformLength / lengthInBetween);
            for (int i = 0; i < obstacleCount - 2; i++)//Because we started with z=2 in OnEnable method, this needs to stop before 2 items.
            {
                GameObject newPlatform = Instantiate(_obstacleContainerPrefab, nextSpawnPosition, _obstacleContainerPrefab.transform.rotation);
                //platformQueue.Enqueue(newPlatform);
                nextSpawnPosition.z += lengthInBetween;
            }
            Ready?.Invoke();
        }
    }

}

