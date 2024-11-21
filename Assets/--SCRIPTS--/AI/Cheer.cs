using UnityEngine;

namespace Characters.AIStates
{
    internal class Cheer : IState
    {
        private Animator _animator;

        internal Cheer(Animator animator)
        {
            _animator = animator;
        }

        public void OnEnter()
        {
            _animator.SetTrigger("Cheer");
        }

        public void OnExit()
        {

        }

        public void Tick()
        {

        }
    }
}