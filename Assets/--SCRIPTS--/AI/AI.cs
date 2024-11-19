using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.AIStates;

namespace Characters
{
    public class AI : Character
    {
        protected override void LoadStateMachine()
        {
            IState _run;
            IState _death;

            base.LoadStateMachine();

            _run = new Run(_animator, this.transform);
            _death = new Death();

            _stateMachine.SetState(_run);
        }
    }
}

