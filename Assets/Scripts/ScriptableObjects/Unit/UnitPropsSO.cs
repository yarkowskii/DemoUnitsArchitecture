using ScriptableObjects.Fighting;
using UnityEngine;

namespace ScriptableObjects.Unit
{
    [CreateAssetMenu(fileName = "New Unit Props", menuName = "Unit/UnitProps", order = 0)]
    public class UnitPropsSO : ScriptableObject
    {
        public UnitFightingPropsSO fightingPropsSo;
        public UnitMovingPropsSO movingPropsSo;
        
        public int hp;
        
        public float modelHeight;
    }
}