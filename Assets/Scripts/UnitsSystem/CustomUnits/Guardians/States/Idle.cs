using UnitsSystem.Base;
using UnitsSystem.States;

namespace UnitsSystem.CustomUnits.Guardians.States
{
    public class Idle : IState
    {
        private readonly CharacterAnimationController _characterAnimationController;
        
        public Idle(CharacterAnimationController characterAnimationController)
        {
            _characterAnimationController = characterAnimationController;
        }

        public void Tick() { }

        public void OnEnter() => _characterAnimationController.ChangeAnimation(AnimationType.Idle);

        public void OnExit() { }
    }
}