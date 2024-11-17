using UnityEngine;
using System;


namespace Characters
{
    [RequireComponent(typeof(Animator))]
    public class Character : MonoBehaviour
    {
        protected StateMachine _stateMachine;
        protected Animator _animator;


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
        }

        protected virtual void LoadStateMachine()
        {

            _stateMachine = new StateMachine();

            void Pass(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        }

        protected virtual void Update()
        {
            _stateMachine.Tick();
        }
    }
}

