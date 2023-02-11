using UnitsSystem.Base.Fighting;
using UnitsSystem.Base.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace UnitsSystem.Base
{
    [RequireComponent(typeof(SphereCollider))]
    public class DamagaebleDetector : MonoBehaviour
    {
        public UnityEvent<IDamageable> triggeredEnterWithIAttackable;
        public UnityEvent<IDamageable> triggeredExitWithIAttackable;

        private SphereCollider _sphereCollider;
        
        public void SetupTriggerRadius(float radius) => _sphereCollider.radius = radius;

        private void Awake() => _sphereCollider = GetComponent<SphereCollider>();

        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
                triggeredEnterWithIAttackable?.Invoke(damageable);
            
        }
        
        private void OnTriggerExit(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
                triggeredExitWithIAttackable?.Invoke(damageable);
            
        }
    }
}