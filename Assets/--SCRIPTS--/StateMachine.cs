using System.Collections.Generic;
using System;
using UnityEngine;

namespace Characters
{
    public class StateMachine
    {
        public IState CurrentState;
        public bool StateMachineActive = true;

        private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> _currentTransitions = new List<Transition>();
        private List<Transition> _anyTransitions = new List<Transition>();


        private static List<Transition> EmptyTransitions = new List<Transition>(0);

        public void Tick()
        {
            var transition = GetTransition();
            if (transition != null)
            {
                SetState(transition.To);
            }

            CurrentState?.Tick();
        }

        public void SetState(IState state, int? value = null)
        {
            if (state == CurrentState || !StateMachineActive)
                return;

            CurrentState?.OnExit();

            // Check if transitioning to Attack and set attack number if provided
            if (value.HasValue)
            {
                state.InsertValue(value.Value);//Can be seen in IState. Can be overwritten.
            }

            CurrentState = state;

            _transitions.TryGetValue(CurrentState.GetType(), out _currentTransitions);
            if (_currentTransitions == null)
                _currentTransitions = EmptyTransitions;

            CurrentState.OnEnter();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            if (!_transitions.TryGetValue(from.GetType(), out var transitions))
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }

            transitions.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IState state, Func<bool> predicate)
        {
            _anyTransitions.Add(new Transition(state, predicate));
        }


#nullable enable
        public void TriggerActionTransition(IState to, int? value = null, IState? from = null)
        {
            if (from != null && CurrentState != from)
            {
                return;
            }
            SetState(to, value);
        }
#nullable disable


        private Transition GetTransition()
        {
            // Check any state transitions first, without calling triggers
            foreach (var transition in _anyTransitions)
                if (transition.Condition())
                    return transition;

            // Check current state transitions
            foreach (var transition in _currentTransitions)
                if (transition.Condition())
                    return transition;

            return null;
        }

        public class Transition
        {
            public Func<bool> Condition { get; }
            public IState To { get; }

            public Transition(IState to, Func<bool> condition)
            {
                To = to;
                Condition = condition;
            }
        }

    }
}