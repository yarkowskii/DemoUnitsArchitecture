using System.Collections.Generic;
using System.Linq;
using System;
using CustomHelpers;
using UnitsSystem.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public abstract class UnitsCollection : ObjectsCollection<Unit>
    {
        protected Unit FindNearestAliveUnitFromPosition(Vector3 pos)
        {
            return objects
                .OrderBy(unit => Vector3.Distance(unit.transform.position, pos))
                .FirstOrDefault(g => g.IsAlive);
        }

        public override void AddUnit(Unit unit)
        {
            base.AddUnit(unit);            
            unit.died.AddListener(() => RemoveUnit(unit));
        }
    }
}