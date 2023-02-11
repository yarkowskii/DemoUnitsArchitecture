using ScriptableObjects.Unit;
using UnitsSystem.Misc;
using UnityEngine;
using UnityEngine.Events;

namespace UnitsSystem.Base
{
    public abstract class Unit : MonoBehaviour
    {
        [HideInInspector] public UnityEvent damageTaken;
        [HideInInspector] public UnityEvent died;

        public UnitType unitType;
        
        public bool IsAlive => currentHp > 0;
        
        [SerializeField] private UnitPropsSO unitProps;
        public UnitPropsSO UnitProps => unitProps;
        
        protected float currentHp;
        
        private void Start() => SetupValues(unitProps);

        protected virtual void SetupValues(UnitPropsSO unitPropsSo)
        {
            unitProps = unitPropsSo;
            currentHp = UnitProps.hp;
        }
        
        public abstract void OnDeath();
    }
}
