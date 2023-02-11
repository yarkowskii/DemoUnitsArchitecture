using System.Threading.Tasks;
using ScriptableObjects.Unit;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace UnitsSystem.Base
{
    public class UnitMovingController : MonoBehaviour
    {
        public UnityEvent reachedDestination;

        private NavMeshAgent _agent;
        private bool NeedMove(Vector3 targetPos) => Vector3.Distance(transform.position, targetPos) > _agent.stoppingDistance;
    
        private bool _canMove;
    
        private void Awake() => _agent = GetComponent<NavMeshAgent>();

        public void ChangeMovingStatus(bool canMove) => _canMove = canMove;
        
        public void SetupAgentValues(UnitMovingPropsSO unitMovingPropsSo)
        {
            _agent.speed = unitMovingPropsSo.movingSpeed;
            _agent.stoppingDistance = .1f;
        }
        
        public async void ChaseTarget(Transform target)
        {
            _agent.enabled = true;
            var cachedTransform = target;
            
            while (_canMove && NeedMove(cachedTransform.position))
            {
                var properPos = cachedTransform.position;
            
                if(NavMesh.SamplePosition(cachedTransform.position, out var hit, 1f, NavMesh.AllAreas))
                    properPos = hit.position;
            
                _agent.SetDestination(properPos);
                await Task.Yield();
            }

            _agent.enabled = false;
            reachedDestination?.Invoke();
        }

        public async void MoveTo(Vector3 pos)
        {
            _agent.enabled = true;
            _agent.SetDestination(pos);
            
            while (_canMove && NeedMove(pos))
                await Task.Yield();
            
            _agent.enabled = false;
            reachedDestination?.Invoke();
        }
    }
}
