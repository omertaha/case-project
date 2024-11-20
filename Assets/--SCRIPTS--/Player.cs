using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.PlayerStates;


namespace Characters
{
    public class Player : Character
    {
        protected override void LoadStateMachine()
        {
            base.LoadStateMachine();

            IState run = new Run(this.transform, _animator);
            IState hit = new Hit(_stateMachine, _animator);

            RegisterState("Run", run);
            RegisterState("Hit", hit);

            _stateMachine.SetState(run);
        }
    }
}

