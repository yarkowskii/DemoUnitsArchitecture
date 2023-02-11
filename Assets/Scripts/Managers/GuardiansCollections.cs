using System.Collections.Generic;
using System.Linq;
using UnitsSystem.Base;
using UnitsSystem.CustomUnits.Guardians;
using UnityEngine;

namespace Managers
{
    public class GuardiansCollections : UnitsCollection
    {
        public GuardianController FindNearestGuardianFromPosition(Vector3 pos)
        {
            var unit = FindNearestAliveUnitFromPosition(pos);
            return unit as GuardianController;
        }
        public override void RemoveUnit(Unit unit)
        {
            base.RemoveUnit(unit);

            if (objects.Count == 0)
            {
                // Some logic
                // For example, level win because all guardians was defeated
                noObjectsLeft?.Invoke();
            }
        }
    }
}