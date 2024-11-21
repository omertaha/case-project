using Characters.PlayerStates;

namespace Characters
{
    public class Player : Character
    {
        protected override void LoadStateMachine()
        {
            base.LoadStateMachine();

            IState run = new Run(this.transform, _animator);
            IState hit = new Hit(_capsuleCollider, _stateMachine, _animator);
            IState dragged = new Dragged(_animator);
            IState painting = new Painting(_navMeshAgent, _animator);

            RegisterState("Run", run);
            RegisterState("Hit", hit);
            RegisterState("Dragged", dragged);
            RegisterState("Painting", painting);

            _stateMachine.SetState(run);
        }
    }
}

