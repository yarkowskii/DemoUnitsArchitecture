using System.Collections;
using ScriptableObjects.Fighting;
using UnityEngine;

namespace UnitsSystem.Base.Fighting
{
    public class MeleeUnitCombat : UnitCombatController
    {
        private MeleeUnitFightingPropsSO MeleeUnitFightingPropsSo => (MeleeUnitFightingPropsSO) unit.UnitProps.fightingPropsSo;
        
        protected override void MakeAttack(float speedModifier)
        {
            // Play animation and deal damage with delay
            StartCoroutine(WaitImpactDelay(speedModifier));
        }

        private IEnumerator WaitImpactDelay(float speedModifier)
        {
            yield return new WaitForSeconds(MeleeUnitFightingPropsSo.impactPointInAnimation / speedModifier);
            DealDamage();
        }
    }
}