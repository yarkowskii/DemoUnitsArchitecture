using UnityEngine;

namespace ScriptableObjects.Unit
{
    [CreateAssetMenu(fileName = "New Moving Props", menuName = "Unit/MovingProps", order = 0)]
    public class UnitMovingPropsSO : ScriptableObject
    {
        public float movingSpeed;
    }
}