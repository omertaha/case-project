using UnityEngine;
using Misc;
using Managers;

namespace Characters.PlayerStates
{
    internal class Hit : IState
    {
        private CapsuleCollider _capsuleCollider;
        private StateMachine _stateMachine;
        private Animator _animator;

        internal Hit(CapsuleCollider capsuleColldier, StateMachine stateMachine, Animator animator)
        {
            _capsuleCollider = capsuleColldier;
            _stateMachine = stateMachine;
            _animator = animator;
        }


        public void OnEnter()
        {
            _capsuleCollider.enabled = false;
            _stateMachine.StateMachineActive = false;
            _animator.SetTrigger("Hit");

            //If player hits something we restart the level. However on AI's we just change their position.
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