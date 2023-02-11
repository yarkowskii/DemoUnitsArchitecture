using System;
using UnitsSystem.Base;
using UnitsSystem.Base.Fighting;
using UnitsSystem.Base.Interfaces;
using UnitsSystem.States;

namespace UnitsSystem.CustomUnits.Ally.States
{
    public class ChaseTarget : IState
    {
        
        private readonly UnitMovingController _unitMovingController;
        private readonly CharacterAnimationController _characterAnimationController;
        private readonly UnitCombatController _unitCombatController;

        private readonly Func<IDamageable> _getDamageableTargetFunc;
        
        public ChaseTarget(Func<IDamageable> getDamageableTargetFunc, UnitMovingController unitMovingController,
            CharacterAnimationController characterAnimationController, UnitCombatController unitCombatController)
        {
            _getDamageableTargetFunc = getDamageableTargetFunc;
            _unitMovingController = unitMovingController;
            _characterAnimationController = characterAnimationController;
            _unitCombatController = unitCombatController;
        }
        
        public void Tick() { }
        
        public void OnEnter()
        {
            _unitMovingController.ChangeMovingStatus(true);
            _unitMovingController.ChaseTarget(_getDamageableTargetFunc.Invoke().GetTransform());
            
            _characterAnimationController.ChangeAnimation(AnimationType.Run);
            
            _unitCombatController.attackTrigger.gameObject.SetActive(true);
        }

       

        public void OnExit()
        {
            _unitCombatController.attackTrigger.gameObject.SetActive(false);
            
            _unitMovingController.ChangeMovingStatus(false);
        }
    }
}