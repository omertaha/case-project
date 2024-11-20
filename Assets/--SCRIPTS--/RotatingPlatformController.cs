using System.Collections.Generic;
using UnityEngine;
using Characters;

namespace Obstacles
{
    public class RotatingPlatformController : MonoBehaviour
    {
        private Animator _animator;

        public enum Direction
        {
            CLOCKWISE,
            COUNTERCLOCKWISE
        }

        [SerializeField]
        private Direction _rotationDirection;
        private Vector3 _myPushDirection;

        private static readonly Dictionary<Direction, Vector3> _enumToVectorMap = new Dictionary<Direction, Vector3>
        {
            { Direction.CLOCKWISE, new Vector3(0.25f, 0, 0) },
            { Direction.COUNTERCLOCKWISE, new Vector3(-0.25f, 0, 0) }
        };


        public static Vector3 GetVector(Direction key)
        {
            if (_enumToVectorMap.TryGetValue(key, out Vector3 value))
            {
                return value;
            }
            else
            {
                return Vector3.zero; // Default value
            }
        }

        private void Start()
        {
            _myPushDirection = GetVector(_rotationDirection);
            _animator = this.GetComponent<Animator>();
            SetAnimator();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Character character))
            {
                character.Push(_myPushDirection);
            }
        }

        private void SetAnimator()
        {
            switch (_rotationDirection)
            {
                case Direction.CLOCKWISE:
                    _animator.SetTrigger("ClockWise");
                    break;
                case Direction.COUNTERCLOCKWISE:
                    _animator.SetTrigger("CounterClockWise");
                    break;
                default:
                    break;
            }
        }

    }
}

