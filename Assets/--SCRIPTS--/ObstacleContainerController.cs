using UnityEngine;

/// <summary>
/// To random obstacles in the game in each beginning.
/// </summary>
namespace Obstacles
{
    public class ObstacleContainerController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _obstacles;

        [SerializeField]
        private GameObject[] _coinGroups;

        private void Start()
        {
            RandomizeObstacles();
            RandomizeCoins();
        }

        private void RandomizeObstacles()
        {
            int randomObstacle = Random.Range(0, _obstacles.Length);
            _obstacles[randomObstacle].SetActive(true);
        }

        private void RandomizeCoins()
        {
            if (Random.Range(0, 100) < 50)
                return;

            int randomCoinGroup = Random.Range(0, _coinGroups.Length);
            _coinGroups[randomCoinGroup].SetActive(true);
        }
    }
}

