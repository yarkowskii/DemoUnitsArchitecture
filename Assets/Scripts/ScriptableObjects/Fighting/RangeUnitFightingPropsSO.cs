using CustomHelpers;
using UnityEngine;

namespace ScriptableObjects.Fighting
{
    [CreateAssetMenu(fileName = "New Range Fighting", menuName = "Fighting/Range Fighting", order = 0)]
    public class RangeUnitFightingPropsSO : UnitFightingPropsSO
    {
        public float projectileSpawnDelay;
        
        public ObjectType projectileType;
    }
}