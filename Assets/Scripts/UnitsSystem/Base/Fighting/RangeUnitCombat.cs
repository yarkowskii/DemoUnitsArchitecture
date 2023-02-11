using System.Collections;
using CustomHelpers;
using ScriptableObjects.Fighting;
using UnitsSystem.Base.Weapons;
using UnityEngine;

namespace UnitsSystem.Base.Fighting
{
    public class RangeUnitCombat : UnitCombatController
    {
        [SerializeField] private Weapon weapon;

        private RangeUnitFightingPropsSO RangeUnitFightingPropsSo => (RangeUnitFightingPropsSO) unit.UnitProps.fightingPropsSo;
        
        protected override void MakeAttack(float speedModifier)
        {
            StartCoroutine(ShootingAnimation(speedModifier));
        }

        private IEnumerator ShootingAnimation(float speedModifier)
        {
            yield return new WaitForSeconds(RangeUnitFightingPropsSo.projectileSpawnDelay / speedModifier);

            var projectile = ObjectPooler.instance.GetObjectFromPool(RangeUnitFightingPropsSo.projectileType, weapon.shootingPoint.position,
                    weapon.shootingPoint.rotation).GetComponent<ProjectileScript>();
            projectile.SetupProjectile(currentIDamageable);
            projectile.reachedTarget.AddListener(() => ObjectPooler.instance.ReturnToPool(RangeUnitFightingPropsSo.projectileType, projectile.gameObject));
            
            DealDamage();
        }
    }
}