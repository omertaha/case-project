using UnityEngine;
using UnityEngine.Events;
using Managers;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        private enum OnCollision
        {
            DEATH,
            CHANGEPARTICLEMATERIAL,
            NOTHING
        }

        [SerializeField]
        private OnCollision _myCollisionType;


        private void OnCollisionEnter(Collision collision)
        {
            switch (_myCollisionType)
            {
                case OnCollision.DEATH:
                    GameManager.RestartLevel?.Invoke();
                    break;
                case OnCollision.CHANGEPARTICLEMATERIAL:
                    break;
                case OnCollision.NOTHING:
                    break;
                default:
                    break;
            }
        }
    }
}

