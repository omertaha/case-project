using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic;
using Managers;

namespace Characters
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class Character : MonoBehaviour
    {
        protected StateMachine _stateMachine;
        protected Animator _animator;
        protected Rigidbody _rigidBody;
        protected NavMeshAgent _navMeshAgent;
        protected Dictionary<string, IState> _states;
        protected CapsuleCollider _capsuleCollider;

        public float Progress { get; private set; }

        public Action<string> TriggerState;


        private void OnEnable()
        {
            LoadComponents();
            LoadStateMachine();
            TriggerState += TriggerMyState;

            if(RankingManager.Instance != null)
                RankingManager.Instance.RegisterCharacter(this);//Ranking System hook
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

            _capsuleCollider = this.GetComponent<CapsuleCollider>();
            _capsuleCollider.enabled = true;
        }

        protected virtual void LoadStateMachine()
        {

            _stateMachine = new StateMachine();
            _states = new Dictionary<string, IState>();

            //I could use this to make transitions. Usually I use this for AI characters. However in this game it's not necessary.
            //void Pass(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        }

        protected virtual void Update()
        {
            _stateMachine.Tick();
            _rigidBody.velocity = new Vector3(0, 0, 0);//For stability

            TrackDistance();
        }

        private void TrackDistance()//To measure player's ranking.
        {
            Progress = Vector3.Distance(Vector3.zero, transform.position);
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

