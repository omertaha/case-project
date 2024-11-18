using UnityEngine;

namespace Characters
{
    internal class Death : IState
    {
        private StateMachine _stateMachine;
        private Animator _animator;

        internal Death(StateMachine stateMachine, Animator animator)
        {
            _stateMachine = stateMachine;
            _animator = animator;
        }


        public void OnEnter()
        {
            _stateMachine.StateMachineActive = false;
        }

        public void OnExit()
        {

        }

        public void Tick()
        {

        }
    }
}