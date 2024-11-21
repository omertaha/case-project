using UnityEngine;

namespace Characters
{
    internal class Dragged : IState
    {
        private Animator _animator;

        internal Dragged(Animator animator)
        {
            _animator = animator;
        }

        public void OnEnter()
        {
            _animator.SetTrigger("Dragged");
        }

        public void OnExit()
        {
            _animator.ResetTrigger("Dragged");
        }

        public void Tick()
        {

        }
    }
}