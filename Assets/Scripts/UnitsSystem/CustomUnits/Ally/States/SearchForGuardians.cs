using Managers;
using UnitsSystem.Base;
using UnitsSystem.Base.Fighting;
using UnitsSystem.Base.Interfaces;
using UnitsSystem.CustomUnits.Guardians;
using Unity.VisualScripting;
using UnityEngine;
using IState = UnitsSystem.States.IState;

namespace UnitsSystem.CustomUnits.Ally.States
{
    public class SearchForGuardians : IState
    {
        private readonly AllyController _allyController;

        public SearchForGuardians(AllyController allyController)
        {
            _allyController = allyController;
        }
        
        public void Tick() { }

        public void OnEnter()
        {
            var guardianController = FindNearestGuardian();

            if (guardianController == null)
                return;
            
            _allyController.chasingTarget = guardianController.GetComponent<IDamageable>();
        }

        public void OnExit() { }

        
        private GuardianController FindNearestGuardian()
        {
            var nearestGuardian = GameManager.instance.guardiansCollections.FindNearestGuardianFromPosition(_allyController.transform.position);
            return nearestGuardian;
        }
    }
}