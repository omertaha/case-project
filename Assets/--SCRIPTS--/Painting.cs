using UnityEngine;
using UnityEngine.AI;
using Managers;

namespace Characters
{
    internal class Painting : IState
    {
        NavMeshAgent _navMeshAgent;
        private Animator _animator;

        internal Painting(NavMeshAgent navMeshAgent, Animator animator)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
        }

        public void OnEnter()
        {
            _navMeshAgent.SetDestination(WallPainterManager.Instance.PaintingPlace.position);
        }

        public void OnExit()
        {

        }

        public void Tick()
        {
            HandleInput();
            CheckIfDestinationReached();
        }

        private void CheckIfDestinationReached()
        {
            if (!_navMeshAgent.hasPath)
            {
                _animator.SetTrigger("Idle");
            }
        }

        public void HandleInput()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject == WallPainterManager.Instance.WallRenderer.gameObject)
                    {
                        WallPainterManager.Instance.PaintOnWall(hit.textureCoord);
                    }
                }
            }
        }
    }
}