using UnityEngine;
using Characters;


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
            if (collision.gameObject.TryGetComponent(out Character character))
            {
                switch (_myCollisionType)
                {
                    case OnCollision.DEATH:
                        character.TriggerState?.Invoke("Hit");
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
}

