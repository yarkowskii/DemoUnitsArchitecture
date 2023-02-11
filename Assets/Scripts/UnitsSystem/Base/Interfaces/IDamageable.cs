using UnitsSystem.Misc;
using UnityEngine;

namespace UnitsSystem.Base.Interfaces
{
    public interface IDamageable
    {
        public bool CanBeDamaged();
        
        public void TakeDamage(float amount);

        public UnitType GetUnitType();
        
        public Transform GetTransform();

        public Vector3 GetDamagePoint();
    }
}