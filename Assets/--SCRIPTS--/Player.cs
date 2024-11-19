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
            IState _run;
            IState _death;

            base.LoadStateMachine();

            _run = new Run(this.transform, _animator);
            _death = new Death(_stateMachine, _animator);

            _stateMachine.SetState(_run);
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
        }
    }
}

