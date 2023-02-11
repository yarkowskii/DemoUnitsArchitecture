using System;
using UnitsSystem.Base;
using UnitsSystem.States;
using UnityEngine;

namespace UnitsSystem.CustomUnits.Ally.States
{
    public class AttackTarget : IState
    {
        private readonly UnitCombatController _unitCombatController;
        private readonly CharacterAnimationController _characterAnimationController;


        public AttackTarget(UnitCombatController unitCombatController, CharacterAnimationController characterAnimationController)
        {
            _unitCombatController = unitCombatController;
            _characterAnimationController = characterAnimationController;
        }

        public void Tick() { }

        public void OnEnter()
        {
            _unitCombatController.attackTrigger.gameObject.SetActive(false);
            _characterAnimationController.ChangeAnimation(AnimationType.AttackIdle);
            
            _unitCombatController.beginSingleAttack.AddListener(() => _characterAnimationController.ChangeAnimation(AnimationType.Attack));
        }

        public void OnExit()
        {
            _unitCombatController.beginSingleAttack.RemoveListener(() => _characterAnimationController.ChangeAnimation(AnimationType.Attack));
            _unitCombatController.attackTrigger.gameObject.SetActive(true);

        }
    }
}