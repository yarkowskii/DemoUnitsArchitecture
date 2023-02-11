using System.Collections;
using ScriptableObjects.Misc;
using UnitsSystem.Base.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace UnitsSystem.Base.Fighting
{
    public class ProjectileScript : MonoBehaviour
    {
        [SerializeField] private ProjectilePropsSO projectileProps;


        public UnityEvent reachedTarget;
        
        private IDamageable _damageableTarget;
        private Transform _transform;

        private bool CanFly => _damageableTarget != null &&
                               Vector3.Distance(_transform.position, _damageableTarget.GetDamagePoint()) >
                               projectileProps.reachedTargetDistanceThreshold;

        private void Awake() => _transform = GetComponent<Transform>();

        public void SetupProjectile(IDamageable target)
        {
            _damageableTarget = target;
            StartCoroutine(FlyToTarget());
        }

        private IEnumerator FlyToTarget()
        {
            while (CanFly)
            {
                var currPos = _transform.position;
                currPos += projectileProps.speed * Time.deltaTime *
                           (_damageableTarget.GetDamagePoint() - currPos).normalized;
                _transform.position = currPos;
                yield return null;
            }
            
            reachedTarget?.Invoke();
            reachedTarget?.RemoveAllListeners();
        }

    }
}