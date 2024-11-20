using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic;

namespace Characters
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class Character : MonoBehaviour
    {
        protected StateMachine _stateMachine;
        protected Animator _animator;
        protected Rigidbody _rigidBody;
        protected NavMeshAgent _navMeshAgent;
        protected Dictionary<string, IState> _states;

        public Action<string> TriggerState;

        private void OnEnable()
        {
            LoadComponents();
            LoadStateMachine();
            TriggerState += TriggerMyState;
        }

        private void OnDisable()
        {
            TriggerState -= TriggerMyState;
        }

        protected virtual void LoadComponents()
        {
            _animator = this.GetComponent<Animator>();

            _rigidBody = this.GetComponent<Rigidbody>();
            _rigidBody.useGravity = false;
            _rigidBody.freezeRotation = true;

            _navMeshAgent = this.GetComponent<NavMeshAgent>();
        }

        protected virtual void LoadStateMachine()
        {

            _stateMachine = new StateMachine();
            _states = new Dictionary<string, IState>();


            //void Pass(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        }

        protected virtual void Update()
        {
            _stateMachine.Tick();
            _rigidBody.velocity = new Vector3(0, 0, 0);//For stability
        }



        #region State Management
        public virtual void RegisterState(string stateName, IState state)
        {
            _states[stateName] = state;
        }

        public virtual void TriggerMyState(string stateName)
        {
            if (_states.TryGetValue(stateName, out var newState))
            {
                _stateMachine.SetState(newState);
            }
        }
        #endregion

        #region Interaction
        public virtual void Push(Vector3 value)
        {
            _rigidBody.AddForce(value, ForceMode.VelocityChange);
        }
        #endregion
    }
}

