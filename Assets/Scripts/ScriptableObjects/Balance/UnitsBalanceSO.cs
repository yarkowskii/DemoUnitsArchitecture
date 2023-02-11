using UnityEngine;

namespace ScriptableObjects.Balance
{
    [CreateAssetMenu(fileName = "New Units Balance", menuName = "Units Balance", order = 0)]
    public class UnitsBalanceSO : ScriptableObject
    {
        public float lookAtTargetSmoothness;
    }
}