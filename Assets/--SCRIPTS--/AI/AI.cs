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
            base.LoadStateMachine();

            IState run = new Run(_animator, this.transform);
            IState hit = new Hit(this.transform, this, _stateMachine, _animator);

            RegisterState("Run", run);
            RegisterState("Hit", hit);

            _stateMachine.SetState(run);
        }
    }
}

