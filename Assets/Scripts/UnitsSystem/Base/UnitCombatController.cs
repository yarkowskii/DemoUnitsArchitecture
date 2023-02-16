using System;
using CustomHelpers;
using Managers;
using ScriptableObjects.Fighting;
using UnitsSystem.Base.Fighting;
using UnitsSystem.Base.Interfaces;
using UnityEngine;
using UnityEngine.Events;


namespace UnitsSystem.Base
{
    public abstract class UnitCombatController : MonoBehaviour
    {
        [HideInInspector] public UnityEvent stoppedAttacking; 
        [HideInInspector] public UnityEvent beginSingleAttack;

        public DamagaebleDetector attackTrigger;
        
        public bool HasTarget => currentIDamageable != null && currentIDamageable.CanBeDamaged();

        public IDamageable CurrentIDamagaeble => currentIDamageable;
        
        protected IDamageable currentIDamageable;
        
        protected Unit unit;
        
        private Func<bool> _canAttack;
        
        private Transform _currentTargetTransform;
        private Transform _transform;
    
        private float _nextAttackTime;

    
        protected abstract void MakeAttack(float speedModifier);
        
        
        private void Awake() => _transform = GetComponent<Transform>();

        private void Update()
        {
            if (!HasTarget || !_canAttack.Invoke()) return;
            Attacking();
            LookAtTarget();
        }

        public void SetupFightingController(Unit _unit, Func<bool> canAttackFunc, UnitFightingPropsSO unitFightingPropsSo)
        {
            unit = _unit;
            attackTrigger.SetupTriggerRadius(unitFightingPropsSo.attackRange);
            
            _canAttack = canAttackFunc;
        }
        
        public void SetupFightTarget(IDamageable target)
        {
            if (unit.unitType.IsFriendly(target.GetUnitType()))
                return;
            
            _nextAttackTime = Time.time;
            
            currentIDamageable = target;
            _currentTargetTransform = currentIDamageable.GetTransform();
        }
    
        private void Attacking()
        {
            if (!(Time.time > _nextAttackTime)) return;
            
            var attackSpd = unit.UnitProps.fightingPropsSo.attackSpeed;
            var singleAttackLength = 1f / attackSpd;
                
            var speedModifier = unit.UnitProps.fightingPropsSo.baseSingleAttackAnimLength / singleAttackLength;
            speedModifier = Mathf.Max(speedModifier, 1f);
                
            _nextAttackTime = Time.time + singleAttackLength;

            beginSingleAttack?.Invoke();
            
            MakeAttack(speedModifier);
        }
    
        private void LookAtTarget()
        {
            var targetPos = _currentTargetTransform.position;
            targetPos.y = transform.position.y;
            var targetRotation = Quaternion.LookRotation(targetPos - _transform.position);
       
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, GameManager.Instance.unitsBalance.lookAtTargetSmoothness * Time.deltaTime);
        }

        private void OnMadeSingleAttack()
        {
            if (unit.IsAlive && currentIDamageable.CanBeDamaged()) return;
            
            stoppedAttacking?.Invoke();
            currentIDamageable = null;
        }

        protected void DealDamage()
        {
            if (!HasTarget)
                return;
            
            currentIDamageable.TakeDamage(unit.UnitProps.fightingPropsSo.damage);
        
            OnMadeSingleAttack();
        }

    }
}
