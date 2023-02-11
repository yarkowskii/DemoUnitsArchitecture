using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using Managers;
using UnitsSystem.Base;
using UnitsSystem.States;
using UnityEngine;
using UnityEngine.Events;

namespace UnitsSystem.CustomUnits.Ally.States
{
    public class Die : IState
    {
        private readonly Unit _unit;
        private readonly CharacterAnimationController _characterAnimationController;
        private readonly Transform _transform;
        
        public Die(Unit unit, CharacterAnimationController characterAnimationController, Transform transform)
        {
            _unit = unit;
            _characterAnimationController = characterAnimationController;
            _transform = transform;
        }
        
        public void Tick() { }

        public void OnEnter()
        {
            _characterAnimationController.ChangeAnimation(AnimationType.Die);
            DieAnimation();
        }

        public void OnExit() { }
        
        
        private async void DieAnimation()
        {
            await Task.Delay((int) (1000f * _characterAnimationController.GetAnimationLength(AnimationType.Die)));
            
            var currY = _transform.transform.position.y;

            var animLength = 1f;
            
            // Move model under ground
            _transform.DOMoveY(currY - 3f, animLength).WaitForCompletion();
            
            await Task.Delay((int) (1000f * animLength));

            _unit.died?.Invoke();
        }
    }
}