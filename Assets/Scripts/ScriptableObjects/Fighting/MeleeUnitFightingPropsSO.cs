using UnityEngine;

namespace ScriptableObjects.Fighting
{
    [CreateAssetMenu(fileName = "New Melee Fighting", menuName = "Fighting/Melee Fighting", order = 0)]
    public class MeleeUnitFightingPropsSO : UnitFightingPropsSO
    {
        public float impactPointInAnimation; // Approximately delay before weapon\fist hits target 
    }
}