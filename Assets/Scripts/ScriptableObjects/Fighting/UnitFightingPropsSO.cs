using UnityEngine;

namespace ScriptableObjects.Fighting
{
    public abstract class UnitFightingPropsSO : ScriptableObject
    {
        public float damage;
        public float attackRange;
        public float attackSpeed;
        
        public float baseSingleAttackAnimLength;
    }
}