using UnityEngine;

namespace Characters.AIStates
{
    internal class Run : IState
    {

        private Animator _animator;
        private Transform _transform;
        private float _moveSpeed = Random.Range(1,2);
        private float _raycastDistance = Random.Range(1, 3);

        internal Run(Animator animator, Transform transform)
        {
            _animator = animator;
            _transform = transform;
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
            AvoidObstacles();

#if UNITY_EDITOR
            DrawDebugRays();
#endif

        }

        private void AvoidObstacles()
        {
            RaycastHit hit;
            bool obstacleAhead = Physics.Raycast(_transform.position, _transform.forward, out hit, _raycastDistance);
            bool obstacleLeft = Physics.Raycast(_transform.position, -_transform.right, out hit, _raycastDistance);
            bool obstacleRight = Physics.Raycast(_transform.position, _transform.right, out hit, _raycastDistance);

            if (obstacleAhead)
            {
                if (!obstacleRight)
                {
                    _transform.Translate(Vector3.right * _moveSpeed * Time.deltaTime);
                }
                else if (!obstacleLeft)
                {
                    _transform.Translate(Vector3.left * _moveSpeed * Time.deltaTime);
                }
            }
            else
            {
                MoveForward();
            }
        }

        private void MoveForward()
        {
            Vector3 runForward = Vector3.forward * 1f * Time.deltaTime;
            _transform.Translate(runForward);
        }

        private void DrawDebugRays()
        {
            // Draw forward, left, and right rays to visualize obstacle detection
            Debug.DrawRay(_transform.position, _transform.forward * _raycastDistance, Color.red);    // Forward ray
            Debug.DrawRay(_transform.position, -_transform.right * _raycastDistance, Color.red);   // Left ray
            Debug.DrawRay(_transform.position, _transform.right * _raycastDistance, Color.red);   // Right ray
        }
    }
}