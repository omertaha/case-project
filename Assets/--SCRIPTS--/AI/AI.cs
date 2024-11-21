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
            IState hit = new Hit(_capsuleCollider, this.transform, this, _stateMachine, _animator);
            IState dragged = new Dragged(_animator);
            IState cheer = new Cheer(_animator);

            RegisterState("Run", run);
            RegisterState("Hit", hit);
            RegisterState("Dragged", dragged);
            RegisterState("Cheer", cheer);

            _stateMachine.SetState(run);
        }
    }
}

