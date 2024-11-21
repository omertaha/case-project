using UnityEngine;

namespace Characters.AIStates
{
    internal class Run : IState
    {
        private Animator _animator;
        private Transform _transform;
        private float _detectionRadius = Random.Range(0.5f, 2f);
        private float _clampLeft;
        private float _clampRight;
        LayerMask _avoidanceLayer;
        private bool _obstacleDetectedLastFrame = false;//To random clamp values every time we detect an obstacle.

        internal Run(Animator animator, Transform transform)
        {
            _animator = animator;
            _transform = transform;
            _avoidanceLayer = (1 << 7);
        }

        public void OnEnter()
        {
            _animator.SetTrigger("Run");
        }

        public void OnExit()
        {
            _animator.ResetTrigger("Run");
        }

        public void Tick()
        {
            MoveForward();
            AvoidObstacles();
        }

        private void AvoidObstacles()
        {
            Collider[] obstacles = Physics.OverlapSphere(_transform.position, _detectionRadius, _avoidanceLayer);
            Vector3 avoidanceDirection = Vector3.zero;

            bool obstacleDetected = obstacles.Length > 0;

            // Randomize clamping values only on new obstacle encounter
            if (obstacleDetected && !_obstacleDetectedLastFrame)
            {
                _clampLeft = Random.Range(-0.1f, -0.3f);
                _clampRight = Random.Range(0.1f, 0.3f);
            }

            _obstacleDetectedLastFrame = obstacleDetected;

            foreach (Collider obstacle in obstacles)
            {
                Vector3 directionToObstacle = obstacle.transform.position - _transform.position;

                // Checking if the obstacle is on the right or on the left
                if (Vector3.Dot(directionToObstacle, _transform.right) > 0)
                {
                    avoidanceDirection += -_transform.right;
                }
                else
                {
                    avoidanceDirection += _transform.right;
                }
            }

            if (avoidanceDirection != Vector3.zero)
            {
                float moveSpeed = Random.Range(0.1f, 0.35f);

                Vector3 newPosition = _transform.position + (avoidanceDirection.normalized * moveSpeed * Time.deltaTime);

                //To add some randomness to where they stand...
                newPosition.x = Mathf.Clamp(newPosition.x, _clampLeft, _clampRight);
                _transform.position = newPosition;
            }
        }

        private void MoveForward()
        {
            Vector3 runForward = Vector3.forward * 1f * Time.deltaTime;
            _transform.Translate(runForward);
        }
    }
}
