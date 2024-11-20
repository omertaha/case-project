using UnityEngine;
using Misc;
using Managers;

namespace Characters.PlayerStates
{
    internal class Hit : IState
    {
        private StateMachine _stateMachine;
        private Animator _animator;

        internal Hit(StateMachine stateMachine, Animator animator)
        {
            _stateMachine = stateMachine;
            _animator = animator;
        }


        public void OnEnter()
        {
            _stateMachine.StateMachineActive = false;
            _animator.SetTrigger("Hit");

            TimerFunction.Create(() => GameManager.RestartLevel?.Invoke(), 2.5f, "RestartPlayerRace");
        }

        public void OnExit()
        {

        }

        public void Tick()
        {

        }
    }
}