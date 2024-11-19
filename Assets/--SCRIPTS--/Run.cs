using UnityEngine;

namespace Characters.PlayerStates
{
    internal class Run : IState
    {
        private Transform _transform;
        private Animator _animator;
        private PlayerControllerInputLayer _playerControllerInputLayer;

        internal Run(Transform transform, Animator animator)
        {
            _transform = transform;
            _animator = animator;
            _playerControllerInputLayer = new PlayerControllerInputLayer(_transform);
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
            _playerControllerInputLayer.Tick();
            MoveForward();
        }

        private void MoveForward()
        {
            Vector3 runForward = Vector3.forward * 1f * Time.deltaTime;
            _transform.Translate(runForward);
        }
    }
}