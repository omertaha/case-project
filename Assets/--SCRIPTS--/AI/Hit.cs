using UnityEngine;
using Misc;

namespace Characters.AIStates
{
    internal class Hit : IState
    {
        private Transform _transform;
        private Character _character;
        private StateMachine _stateMachine;
        private Animator _animator;

        internal Hit(Transform transform, Character character, StateMachine stateMachine, Animator animator)
        {
            _transform = transform;
            _character = character;
            _stateMachine = stateMachine;
            _animator = animator;
        }

        public void OnEnter()
        {
            _stateMachine.StateMachineActive = false;
            _animator.SetTrigger("Hit");

            TimerFunction.Create(RestartRace, 2.5f, "RestartPlayerRace");
        }

        public void OnExit()
        {

        }

        public void Tick()
        {

        }

        private void RestartRace()
        {
            _stateMachine.StateMachineActive = true;
            _transform.position = Vector3.zero;
            _character.TriggerState("Run");
        }
    }
}