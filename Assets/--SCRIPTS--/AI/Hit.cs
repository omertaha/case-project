using UnityEngine;
using Misc;

namespace Characters.AIStates
{
    internal class Hit : IState
    {
        private CapsuleCollider _capsuleCollider;
        private Transform _transform;
        private Character _character;
        private StateMachine _stateMachine;
        private Animator _animator;

        internal Hit(CapsuleCollider capsuleCollider, Transform transform, Character character, StateMachine stateMachine, Animator animator)
        {
            _capsuleCollider = capsuleCollider;
            _transform = transform;
            _character = character;
            _stateMachine = stateMachine;
            _animator = animator;
        }

        public void OnEnter()
        {
            _capsuleCollider.enabled = false;
            _stateMachine.StateMachineActive = false;
            _animator.SetTrigger("Hit");

            TimerFunction.Create(RestartRace, 2.5f, "RestartPlayerRace");
        }

        public void OnExit()
        {
            _capsuleCollider.enabled = true;
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