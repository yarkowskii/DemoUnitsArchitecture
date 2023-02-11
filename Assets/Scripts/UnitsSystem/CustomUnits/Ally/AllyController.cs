using System;
using System.Collections;
using DG.Tweening;
using Managers;
using ScriptableObjects.Unit;
using UnitsSystem.Base;
using UnitsSystem.Base.Fighting;
using UnitsSystem.Base.Interfaces;
using UnitsSystem.CustomUnits.Ally.States;
using UnitsSystem.CustomUnits.Guardians.States;
using UnitsSystem.Misc;
using UnitsSystem.States;
using UnityEngine;


namespace UnitsSystem.CustomUnits.Ally
{
    [RequireComponent(typeof(CharacterAnimationController))]
    [RequireComponent(typeof(UnitMovingController))]
    public class AllyController : Unit, IDamageable
    {
        public IDamageable chasingTarget;
        
        public bool CanBeDamaged() => IsAlive;
        public UnitType GetUnitType() => unitType; 

        // There can be any specific check.
        // For example, if Unit is poisoned/frozen it can't attacking so return 'false' 
        private Func<bool> CanAttack() => () => true;
        
        private CharacterAnimationController _characterAnimController;
        private UnitCombatController _unitCombatController;
        private UnitMovingController _unitMovingController;

        private Transform _transform;
        
        private StatesMachine _statesMachine;
        
        
        public Transform GetTransform() => _transform;

        public Vector3 GetDamagePoint() => _transform.position + Vector3.up * UnitProps.modelHeight;
        
        public override void OnDeath() => Destroy(gameObject);

        private void Update() => _statesMachine.Tick();

        
        private void Awake()
        {
            _transform = transform;
            
            _unitCombatController = GetComponent<UnitCombatController>();
            _unitMovingController = GetComponent<UnitMovingController>();
            _characterAnimController = GetComponent<CharacterAnimationController>();
            

            #region State machine initialization

            _statesMachine = new StatesMachine();

            var idle = new Idle(_characterAnimController);
            var searchForGuardians = new SearchForGuardians(this);
            var chaseTarget = new ChaseTarget(() => chasingTarget, _unitMovingController, _characterAnimController, _unitCombatController);
            var attackTarget = new AttackTarget(_unitCombatController, _characterAnimController);
            var die = new Die(this, _characterAnimController, _transform);
            
            _statesMachine.AddTransition(searchForGuardians, chaseTarget, FoundTarget());
            _statesMachine.AddTransition(chaseTarget, searchForGuardians, LostTarget());
            _statesMachine.AddTransition(chaseTarget, attackTarget, ReachedTarget());
            _statesMachine.AddTransition(attackTarget, searchForGuardians, KilledTarget());
            
            _statesMachine.AddAnyTransition(die, () => !IsAlive);
            _statesMachine.AddAnyTransition(idle, () => !GameManager.instance.IsGame);

            Func<bool> FoundTarget() => () => chasingTarget != null && chasingTarget.CanBeDamaged();
            Func<bool> LostTarget() => () => !chasingTarget.CanBeDamaged();
            Func<bool> ReachedTarget() => () => _unitCombatController.HasTarget;
            Func<bool> KilledTarget() => () => !_unitCombatController.HasTarget;
            
            _statesMachine.SetInitState(searchForGuardians);
            
            #endregion
        }


        protected override void SetupValues(UnitPropsSO unitPropsSo)
        {
            base.SetupValues(unitPropsSo);
            
            _unitMovingController.SetupAgentValues(UnitProps.movingPropsSo);
            
            _unitCombatController.SetupFightingController(this, CanAttack(), unitPropsSo.fightingPropsSo);
            _unitCombatController.attackTrigger.triggeredEnterWithIAttackable.AddListener(_unitCombatController.SetupFightTarget);

            GameManager.instance.alliesCollection.AddUnit(this);
            
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
