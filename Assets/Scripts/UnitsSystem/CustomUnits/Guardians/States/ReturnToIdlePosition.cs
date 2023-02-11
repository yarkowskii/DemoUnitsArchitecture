using System;
using UnitsSystem.Base;
using UnitsSystem.States;
using UnityEngine;

namespace UnitsSystem.CustomUnits.Guardians.States
{
    public class ReturnToIdlePosition : IState
    {
        
        private readonly UnitMovingController _unitMovingController;
        private readonly CharacterAnimationController _characterAnimationController;

        private readonly Func<Vector3> _getCurrIdlePos;

        public bool reachedDestination;
        
        public ReturnToIdlePosition(Func<Vector3> GetCurrIdlePos, UnitMovingController unitMovingController, 
            CharacterAnimationController characterAnimationController)
        {
            _getCurrIdlePos = GetCurrIdlePos;
            _unitMovingController = unitMovingController;
            _characterAnimationController = characterAnimationController;
        }
        
        public void Tick() { }

        public void OnEnter()
        {
            _unitMovingController.ChangeMovingStatus(true);
            _unitMovingController.MoveTo(_getCurrIdlePos.Invoke());
            _unitMovingController.reachedDestination.AddListener(() => reachedDestination = true);
            
            _characterAnimationController.ChangeAnimation(AnimationType.Run);
        }

        public void OnExit()
        {
            reachedDestination = false;
            _unitMovingController.ChangeMovingStatus(false);
        }
    }
}