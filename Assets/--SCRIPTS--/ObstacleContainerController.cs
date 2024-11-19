using UnityEngine;


namespace Obstacles
{
    public class ObstacleContainerController : MonoBehaviour
    {
        [SerializeField]
        GameObject[] _obstacles;

        private void Start()
        {
            int randomObstacle = Random.Range(0, _obstacles.Length);
            _obstacles[randomObstacle].SetActive(true);
        }
    }
}

