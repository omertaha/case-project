using UnityEngine;
using Characters;
using Managers;
using Misc;


namespace Obstacles
{
    public class Obstacle : MonoBehaviour
    {

        [SerializeField]
        private ParticleSystem _particle;

        private enum OnCollision
        {
            HIT,
            CHANGEPARTICLEMATERIAL,
            DRAG,
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
                    case OnCollision.HIT:
                        character.TriggerState?.Invoke("Hit");
                        VFXManager.PlayVFX("Hit", collision.contacts[0].point, Quaternion.identity);
                        break;
                    case OnCollision.CHANGEPARTICLEMATERIAL:
                        if(_particle != null)
                        {
                            var mainModule = _particle.main;
                            mainModule.startColor = Color.red;
                        }
                        break;
                    case OnCollision.DRAG:
                        character.TriggerState?.Invoke("Drag");
                        TimerFunction.Create(() => character.TriggerState?.Invoke("Run"), 1f, "Dragging" + character.GetInstanceID());
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

