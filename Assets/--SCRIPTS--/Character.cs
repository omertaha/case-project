using UnityEngine;
using System;


namespace Characters
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    public class Character : MonoBehaviour
    {
        protected StateMachine _stateMachine;
        protected Animator _animator;
        protected Rigidbody _rigidBody;


        private void OnEnable()
        {
            LoadComponents();
            LoadStateMachine();
        }

        private void OnDisable()
        {

        }

        protected virtual void LoadComponents()
        {
            _animator = this.GetComponent<Animator>();

            _rigidBody = this.GetComponent<Rigidbody>();
            _rigidBody.useGravity = false;
            _rigidBody.freezeRotation = true;
        }

        protected virtual void LoadStateMachine()
        {

            _stateMachine = new StateMachine();

            void Pass(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        }

        protected virtual void Update()
        {
            _stateMachine.Tick();
            _rigidBody.velocity = new Vector3(0, 0, 0);
        }
    }
}

