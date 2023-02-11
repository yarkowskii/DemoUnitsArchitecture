using System;
using Managers;
using ScriptableObjects.Unit;
using UnitsSystem.Base;
using UnitsSystem.Base.Interfaces;
using UnitsSystem.CustomUnits.Ally.States;
using UnitsSystem.CustomUnits.Guardians.States;
using UnitsSystem.Misc;
using UnitsSystem.States;
using UnityEngine;


namespace UnitsSystem.CustomUnits.Guardians
{
    [RequireComponent(typeof(CharacterAnimationController))]
    public class GuardianController : Unit, IDamageable
    {

        public bool CanBeDamaged() => IsAlive;
        public UnitType GetUnitType() => unitType; 
        public Transform GetTransform() => _transform;
        public Vector3 GetDamagePoint() => _transform.position + Vector3.up * UnitProps.modelHeight;

        // There can be any specific check.
        // For example, if Unit is poisoned/frozen it can't attacking so return 'false' 
        private Func<bool> CanAttack() => () => true;
        

        private CharacterAnimationController _characterAnimController;
        private UnitCombatController _unitCombatController;

        private Transform _transform;
        
        private StatesMachine _statesMachine;
        
        
        public override void OnDeath() => Destroy(gameObject);
        
        private void Update() => _statesMachine.Tick();

        
        private void Awake()
        {
            _transform = transform;
            
            _unitCombatController = GetComponent<UnitCombatController>();
            _characterAnimController = GetComponent<CharacterAnimationController>();
            
            
            #region States machine initialization

            _statesMachine = new StatesMachine();

            var idle = new Idle(_characterAnimController);
            var attackTarget = new AttackTarget(_unitCombatController, _characterAnimController);
            var die = new Die(this, _characterAnimController, _transform);
            
            
            _statesMachine.AddTransition(idle, attackTarget, FoundTarget());
            _statesMachine.AddTransition(attackTarget, idle, KilledTarget());

            _statesMachine.AddAnyTransition(die, () => !IsAlive);
            
            Func<bool> FoundTarget() => () => _unitCombatController.HasTarget;
            Func<bool> KilledTarget() => () => !_unitCombatController.HasTarget;
            
            _statesMachine.SetInitState(idle);
            
            #endregion
        }

        

        protected override void SetupValues(UnitPropsSO unitPropsSo)
        {
            base.SetupValues(unitPropsSo);
            
            _unitCombatController.SetupFightingController(this, CanAttack(), unitPropsSo.fightingPropsSo);
            _unitCombatController.attackTrigger.triggeredEnterWithIAttackable.AddListener(_unitCombatController.SetupFightTarget);

            GameManager.instance.guardiansCollections.AddUnit(this);
            
            died.AddListener(OnDeath);
            
            _statesMachine.RunFromInitState();
        }


        public void TakeDamage(float amount)
        {
            currentHp -= amount;
            currentHp = Mathf.Max(currentHp, 0);
            
            damageTaken?.Invoke();
        }
        
    }
}